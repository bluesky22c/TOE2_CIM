using LGCIMServer.Define;
using LGCIMServer.Forms;
using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGCIMServer
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class FormMain : Form
    {
        ServerCIM server = new ServerCIM();
        ServerStatus serverStatus => server.serverStatus;
        RecipeSearchForm recipeForm;

        List<HSMSData> messageList = new List<HSMSData>();
        TreeNode selectNode;
        HSMSData selectData;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server.InitGem();
            InitMessage();
            InitTreeView();

            server.propertyRefresh += Gem_OnRefreshPropertyEvent;
            server.ShowRecipeForm += Server_ShowRecipeForm;

            SERVERpropertyGrid.SelectedObject = serverStatus;
            AGlasspropertyGrid.SelectedObject = serverStatus.AGlass;
            BGlasspropertyGrid.SelectedObject = serverStatus.BGlass;

            serverStatus.AGlass.InitGlassInfo();
            serverStatus.BGlass.InitGlassInfo();

            // Port Status Display
            lblPortStatus.Text = $"Port {server.gem.Config.Port} : Listening";
            lblPortStatus.ForeColor = Color.Blue;

            // Subscribe to Gem Events for Logging
            server.gem.OnGemEvent += Gem_OnGemEvent;
            server.gem.OnSentEvent += Gem_OnSentEvent;

            // Connection Status Events
            server.gem.OnConnectEvent += Gem_OnConnectEvent;
            server.gem.OnDisconnectEvent += Gem_OnDisconnectEvent;
            server.gem.OnSelectEvent += Gem_OnSelectEvent;

            LogSecsMessage("INFO", "Log Started...");
        }

        private void Gem_OnConnectEvent(object sender, EventArgs e)
        {
            if (InvokeRequired) { BeginInvoke((Action)delegate { Gem_OnConnectEvent(sender, e); }); return; }
            lblPortStatus.Text = $"Port {server.gem.Config.Port} : Connected";
            lblPortStatus.ForeColor = Color.Orange;
            LogSecsMessage("INFO", "TCP Connected");
        }

        private void Gem_OnDisconnectEvent(object sender, EventArgs e)
        {
            if (InvokeRequired) { BeginInvoke((Action)delegate { Gem_OnDisconnectEvent(sender, e); }); return; }
            lblPortStatus.Text = $"Port {server.gem.Config.Port} : Listening";
            lblPortStatus.ForeColor = Color.Blue;
            LogSecsMessage("INFO", "Disconnected");
        }

        private void Gem_OnSelectEvent(object sender, EventArgs e)
        {
            if (InvokeRequired) { BeginInvoke((Action)delegate { Gem_OnSelectEvent(sender, e); }); return; }
            lblPortStatus.Text = $"Port {server.gem.Config.Port} : Selected";
            lblPortStatus.ForeColor = Color.Green;
            LogSecsMessage("INFO", "HSMS Selected");
        }

        // 응답(Send)을 버퍼링하여 요청(Recv) 다음에 표시하기 위한 딕셔너리
        private readonly System.Collections.Concurrent.ConcurrentDictionary<int, HSMSMessage> _pendingSends = new();

        // 50ms 내에 동일한 SystemBytes의 Send 메시지가 발생하면 마지막 것만 로깅 (Title 업데이트 대기)
        private readonly System.Collections.Concurrent.ConcurrentDictionary<int, HSMSMessage> _debouncedSends = new();
        private readonly System.Collections.Concurrent.ConcurrentDictionary<int, bool> _activeDebounceTasks = new();

        private void Gem_OnSentEvent(object sender, GemEventArgs e)
        {
            var msg = e.gemMessage;
            var header = msg.Header;
            if (header.SType != MessageHeader.STypes.DataMessage)
                return;

            if (header.Function % 2 == 0)
            {
                // 응답 메시지(짝수 Function)는 버퍼에 저장 (중복 시 최신 버전으로 덮어씀)
                _pendingSends[header.SystemBytes] = msg;
            }
            else
            {
                // 요청 메시지(홀수 Function)는 디바운싱 처리 (50ms 대기)
                // 중복 호출 시 최신 메시지로 덮어씀 (예: No Title -> Title 있는 버전)
                _debouncedSends[header.SystemBytes] = msg;

                // 이미 해당 SystemBytes에 대한 로깅 태스크가 실행 중이면 추가 실행 안 함 (기존 태스크가 최신 msg를 가져감)
                if (_activeDebounceTasks.TryAdd(header.SystemBytes, true))
                {
                    Task.Delay(50).ContinueWith(t =>
                    {
                        if (_debouncedSends.TryRemove(header.SystemBytes, out var finalMsg))
                        {
                            _activeDebounceTasks.TryRemove(header.SystemBytes, out _);
                            LogSecsMessage("Send", finalMsg);
                        }
                    });
                }
            }
        }


        private void Gem_OnGemEvent(object sender, GemEventArgs e)
        {
            var msg = e.gemMessage;
            var header = msg.Header;
            if (header.SType != MessageHeader.STypes.DataMessage)
                return;

            // [수정] 수신 메시지가 도착했을 때, 대기 중인 모든 송신 요청 메시지(Odd Function)를 즉시 로깅
            // 이유: S2F87(Send) -> S2F0(Recv, Abort) 같은 경우 SystemBytes가 다를 수 있음(154 vs 151).
            // 하지만 시간 순서상 Send가 먼저 발생했으므로, Recv 처리 전에 모든 Pending Send를 Flush하여 로그 순서를 보장함.
            var pendingKeys = _debouncedSends.Keys.ToArray();
            foreach (var key in pendingKeys)
            {
                if (_debouncedSends.TryRemove(key, out var delayedSendMsg))
                {
                    _activeDebounceTasks.TryRemove(key, out _);
                    LogSecsMessage("Send", delayedSendMsg);
                }
            }

            // 1. 수신 메시지 로깅
            LogSecsMessage("Recv", msg);

            // 2. 매칭되는 버퍼된 송신 응답(Reply)이 있으면 바로 이어서 로깅
            if (_pendingSends.TryRemove(header.SystemBytes, out var sentMsg))
            {
                LogSecsMessage("Send", sentMsg);
            }
        }

        private void LogSecsMessage(string direction, HSMSMessage msg)
        {
            try
            {
                var header = msg.Header;
                string timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string wBit = header.Wait ? "W" : "";

                // 메시지 본문 구성
                string body = msg.Title ?? "";
                if (msg.Items != null && msg.Items.Count > 0)
                {
                    body += "\r\n";
                    foreach (var item in msg.Items)
                    {
                        body += item.FormatString(true, 4) + "\r\n";
                    }
                    body = body.TrimEnd();
                }

                string line = $"{timeStr}\t{direction}\t{header.DeivceID}\t{header.SystemBytes}\t" +
                              $"S{header.Stream}F{header.Function}\t{wBit}\t\"{body}\"";

                LogSecsMessage(direction, line);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LogSecsMessage Error: {ex.Message}");
            }
        }

        private void LogSecsMessage(string direction, string line)
        {
            try
            {
                Action logAction = () =>
                {
                    if (direction == "Recv")
                    {
                        rtbLog.SelectionColor = Color.Blue;
                    }
                    else if (direction == "Send")
                    {
                        rtbLog.SelectionColor = Color.Black;
                    }
                    else
                    {
                        rtbLog.SelectionColor = Color.Gray;
                    }

                    rtbLog.AppendText(line + "\r\n");
                    rtbLog.ScrollToCaret();
                };

                if (rtbLog.InvokeRequired)
                {
                    rtbLog.Invoke(logAction);
                }
                else
                {
                    logAction();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LogSecsMessage Error: {ex.Message}");
            }
        }

        private void Server_ShowRecipeForm(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { Server_ShowRecipeForm(sender, e); });
                return;
            }

            if (recipeForm == null || recipeForm.IsDisposed)
            {
                recipeForm = new RecipeSearchForm();
                recipeForm.server = server;
                recipeForm.Show();
            }
            else
            {
                recipeForm.server = server;
            }
        }

        private void Gem_OnRefreshPropertyEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Gem_OnRefreshPropertyEvent(sender, e); });
            else
            {
                SERVERpropertyGrid.Refresh();
                //AGlasspropertyGrid.Refresh();
                //BGlasspropertyGrid.Refresh();
            }
        }

        private void s1f63Button_Click(object sender, EventArgs e)
        {
            // All Eqp Status Request
            server.gem.HSMSSend(new CIMS1F63());
        }



        private void ECSStartButton_Click(object sender, EventArgs e)
        {
            int portCount = int.Parse(portCountTextBox.Text);
            server.SendS7F61(portCount);
        }

        private void LOTENDButton_Click(object sender, EventArgs e)
        {
            server.SendS6F65();
        }

        private void unloadDataRequsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.SendS2F69();
        }

        private void sendTextToCIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextForm form = new TextForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                string sendText = form.messageTextBox.Text;
                server.SendS10F3(sendText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverStatus.GLSSTATUS = "1";
            server.SendS2F87(serverStatus.GLSSTATUS, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serverStatus.GLSSTATUS = "1";
            server.SendS2F87(serverStatus.GLSSTATUS, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serverStatus.GLSSTATUS = "1";
            server.SendS2F87(serverStatus.GLSSTATUS, 2);
        }

        private void recipeButton_Click(object sender, EventArgs e)
        {
            server.SendS15F71();
        }

        private void pdDownButton_Click(object sender, EventArgs e)
        {
            var form = new PDDownForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                var rmcd = form.RCMD;
                server.SendS2F99(rmcd);
            }
        }

        private void noGlassButton_Click(object sender, EventArgs e)
        {
            serverStatus.GLSSTATUS = "0";
            server.SendS2F87(serverStatus.GLSSTATUS, 2);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectNode = e.Node;
            selectData = (HSMSData)selectNode.Tag;
            if (selectData == null)
                return;

            streamTextBox.Text = selectData.Message.Header.Stream.ToString();
            functionTextBox.Text = selectData.Message.Header.Function.ToString();
            comboBox1.SelectedIndex = selectData.Message.Wait ? 1 : 0;

            selectData.CimClass = server.gem.GetCimClass(selectData.Message);
            propertyGrid1.SelectedObject = selectData.CimClass;
            hsmsMessageTextBox.Text = selectData.Message.ToString();
        }

        #region InitMessage
        void InitMessage()
        {
            try
            {
                //S1
                HSMSData data = new HSMSData(new CIMS1F1());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F2());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F61());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F62());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F63());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F64());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F67());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F68());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F75());
                messageList.Add(data);
                data = new HSMSData(new CIMS1F76());
                messageList.Add(data);

                //S2
                data = new HSMSData(new CIMS2F17());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F18());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F25());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F26());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F31());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F32());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F61());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F62());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F63());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F64());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F65());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F66());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F67());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F68());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F69());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F70());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F77());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F78());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F79());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F80());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F87());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F88());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F99());
                messageList.Add(data);
                data = new HSMSData(new CIMS2F100());
                messageList.Add(data);

                //S5
                data = new HSMSData(new CIMS5F1());
                messageList.Add(data);
                data = new HSMSData(new CIMS5F2());
                messageList.Add(data);

                //S6
                data = new HSMSData(new CIMS6F61());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F62());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F63());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F64());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F65());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F66());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F67());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F68());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F69());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F70());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F71());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F72());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F73());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F74());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F83());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F84());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F85());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F86());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F89());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F90());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F91());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F92());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F93());
                messageList.Add(data);
                data = new HSMSData(new CIMS6F94());
                messageList.Add(data);

                //S7
                data = new HSMSData(new CIMS7F19());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F20());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F61());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F62());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F63());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F64());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F67());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F68());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F69());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F70());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F77());
                messageList.Add(data);
                data = new HSMSData(new CIMS7F78());
                messageList.Add(data);

                //S9
                data = new HSMSData(new CIMS9F13());
                messageList.Add(data);

                //S10
                data = new HSMSData(new CIMS10F3());
                messageList.Add(data);
                data = new HSMSData(new CIMS10F4());
                messageList.Add(data);


                //S15
                data = new HSMSData(new CIMS15F65());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F66());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F71());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F72());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F73());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F74());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F75());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F76());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F77());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F78());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F79());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F80());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F81());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F82());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F83());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F84());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F95());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F96());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F97());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F98());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F99());
                messageList.Add(data);
                data = new HSMSData(new CIMS15F100());
                messageList.Add(data);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        void AddTreeItem(HSMSData d)
        {
            TreeNode findNode = null;
            string streamString = string.Format("S{0}", d.Message.Header.Stream);
            TreeNode[] node = treeView1.Nodes[0].Nodes.Find(streamString, false);
            if (node.Length == 0)
            {
                findNode = treeView1.Nodes[0].Nodes.Add(streamString);
                findNode.Name = streamString;
            }
            else
            {
                findNode = node[0];
            }
            findNode.Nodes.Add(d.SFString).Tag = d;
        }

        void InitTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("HSMS");
            foreach (HSMSData d in messageList)
            {
                AddTreeItem(d);
            }
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (selectData.CimClass != null)
            {
                selectData.Message.SetCimClass(selectData.CimClass);
                hsmsMessageTextBox.Text = selectData.Message.ToString();
                streamTextBox.Text = selectData.Message.Header.Stream.ToString();
                functionTextBox.Text = selectData.Message.Header.Function.ToString();
                comboBox1.SelectedIndex = selectData.Message.Wait ? 1 : 0;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            int stream = int.Parse(streamTextBox.Text);
            int function = int.Parse(functionTextBox.Text);
            bool wait = comboBox1.SelectedIndex == 1 ? true : false;

            selectData.Message = new HSMSMessage();
            selectData.Message.SetHSMS(stream, function, -1, wait, hsmsMessageTextBox.Text);
            //            selectData.CimClass = gem.GetCimClass(selectData.Message);
            //            propertyGrid1.SelectedObject = selectData.CimClass;

            server.gem.HSMSSend(selectData.Message);
        }

        private void setAGlassInfoButton_Click(object sender, EventArgs e)
        {
            serverStatus.AGlass.InitGlassInfo();
            AGlasspropertyGrid.Refresh();
        }

        private void setBGlassInfoButton_Click(object sender, EventArgs e)
        {
            serverStatus.BGlass.InitGlassInfo();
            BGlasspropertyGrid.Refresh();
        }

        private void clearAGlassInfoButton_Click(object sender, EventArgs e)
        {
            serverStatus.AGlass.ClearInfo();
            AGlasspropertyGrid.Refresh();

        }

        private void clearBGlassInfoButton_Click(object sender, EventArgs e)
        {
            serverStatus.BGlass.ClearInfo();
            BGlasspropertyGrid.Refresh();
        }

        private void dateTimeSetButton_Click(object sender, EventArgs e)
        {
            server.SendS2F31();
        }
    }
}
