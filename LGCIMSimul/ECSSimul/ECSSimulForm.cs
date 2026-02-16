
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
    public partial class ECSSimulForm : Form
    {
        ServerCIM server = new ServerCIM();
        public ServerCIM Server => server; // Exposed for ScenarioRunner
        ServerStatus serverStatus => server.serverStatus;
        RecipeSearchForm recipeForm;

        // EQP Simulator 관련 필드 제거 (LGCIMSimulForm으로 이관)
        // private LGCIMServer.EQPSimul.EQPSimulForm eqpSimulForm;
        // private Panel ecsFuncPanel;
        // private Panel ecsLogPanel;
        // private Panel eqpFuncPanel;
        // private Panel eqpLogPanel;
        // private TableLayoutPanel mainLayoutPanel;
        // private Button btnToggleSimul;

        List<HSMSData> messageList = new List<HSMSData>();
        TreeNode selectNode;
        HSMSData selectData;

        // 시나리오 비주얼 플로우 관련
        private class ScenarioStep
        {
            public int StepNumber { get; set; }
            public string MessageName { get; set; }
            public string Direction { get; set; } // "H->C", "C->H", "C->S", "S->C"
            public bool IsCompleted { get; set; }
            public Control StepControl { get; set; }
            public Label Bubble { get; set; }
        }

        private List<ScenarioStep> _currentScenarioSteps = new List<ScenarioStep>();

        public ECSSimulForm()
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

            // Scenario Tab Change Event
            tabControlScenarios.SelectedIndexChanged += tabControlScenarios_SelectedIndexChanged;

            // Initial Scenario Flow
            InitScenarioFlow("A");

            LogSecsMessage("INFO", "Log Started...");
        }

        // private void Setup4ZoneLayout() ... 제거됨
        // private void ShowEQPSimulator() ... 제거됨

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

                            // UI 스레드에서 안전하게 시나리오 상태 확인 및 하이라이트 트리거
                            this.BeginInvoke((Action)delegate
                            {
                                if (lblScenarioStatus.Text.Contains("Scenario B"))
                                {
                                    var sheader = finalMsg.Header;
                                    if (sheader.Stream == 2 && sheader.Function == 87) HighlightStep(7); // 8번: S2F87
                                }
                                else if (lblScenarioStatus.Text.Contains("Scenario A"))
                                {
                                    var sheader = finalMsg.Header;
                                    if (sheader.Stream == 7 && sheader.Function == 61) HighlightStep(0); // 1번: S7F61
                                    if (sheader.Stream == 2 && sheader.Function == 87) HighlightStep(2); // 3번: S2F87
                                }
                            });
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

            // 시나리오 상태 업데이트 (수신 메시지 기반)
            string currentStatus = lblScenarioStatus.Text;
            if (currentStatus.Contains("Scenario A"))
            {
                if (header.Stream == 7 && header.Function == 62) HighlightStep(1); // 2번: S7F62 (수신 시)
                else if (header.Stream == 2 && header.Function == 88) HighlightStep(3); // 4번: S2F88 (수신 시)
                else if (header.Stream == 6 && header.Function == 67)
                {
                    AddScenarioLog("Scenario A: Move Request (LR) Received.");
                    UpdateScenarioStatus("Scenario A: EQP에서 [LoadDone]을 실행하세요", Color.Orange);
                    HighlightStep(4); // 5번: Glass Move Req (LR)
                    HighlightStep(5); // 6번: S6F67 (Move Request: LR)
                }
                else if (header.Stream == 6 && header.Function == 91)
                {
                    AddScenarioLog("Scenario A: Status Change (RUN) Received.");
                    UpdateScenarioStatus("Scenario A: 완료 대기 중...", Color.Orange);
                    HighlightStep(7); // [보완] 8번: CMD: LoadDone
                    HighlightStep(8); // 9번: S6F91
                }
                else if (header.Stream == 1 && header.Function == 67)
                {
                    AddScenarioLog("Scenario A: All Glass Position Report Received. Completed!");
                    UpdateScenarioStatus("Scenario A Completed", Color.Green);
                    HighlightStep(10); // 11번: S1F67
                }
            }
            else if (currentStatus.Contains("Scenario B"))
            {
                if ((header.Stream == 6 && header.Function == 11) || (header.Stream == 6 && header.Function == 91))
                {
                    AddScenarioLog($"Scenario B: Status Change Report (S6F{header.Function}) Received.");
                    HighlightStep(0); // 1번: CMD: InspectDone (내부 처리)
                    HighlightStep(1); // 2번: S6F11 / S6F91
                }
                else if (header.Stream == 6 && header.Function == 63)
                {
                    AddScenarioLog("Scenario B: Unload Glass Data Report Received.");
                    HighlightStep(2); // 3번: CMD: ExchangeReadyDone (내부 처리)
                    HighlightStep(3); // 4번: S6F63
                }
                else if (header.Stream == 6 && header.Function == 61)
                {
                    AddScenarioLog("Scenario B: Load Glass Data Request Received.");
                    HighlightStep(5); // 6번: S6F61
                }
                else if (header.Stream == 6 && header.Function == 67)
                {
                    AddScenarioLog("Scenario B: Move Request Received.");
                    HighlightStep(8); // 9번: Move Req (UW/ER) (내부 처리)
                    HighlightStep(9); // 10번: S6F67
                }
                else if (header.Stream == 1 && header.Function == 67)
                {
                    AddScenarioLog("Scenario B: All Glass Position Report Received. Completed!");
                    UpdateScenarioStatus("Scenario B Completed", Color.Green);
                    HighlightStep(11); // 12번: CMD: ExchangeDone (내부 처리)
                    HighlightStep(12); // 13번: S1F67
                }
            }
            else if (currentStatus.Contains("Scenario C"))
            {
                if (header.Stream == 6 && header.Function == 83)
                {
                    AddScenarioLog("Scenario C: Actual Processing Data Received.");
                    HighlightStep(2); // 3번: S6F83
                }
            }

            // 2. 매칭되는 버퍼된 송신 응답(Reply)이 있으면 바로 이어서 로깅
            if (_pendingSends.TryRemove(header.SystemBytes, out var sentMsg))
            {
                LogSecsMessage("Send", sentMsg);
                
                // 시나리오 시퀀스 상태 업데이트 (송신 응답 기반)
                if (lblScenarioStatus.Text.Contains("Scenario A"))
                {
                    var sheader = sentMsg.Header;
                    if (sheader.Stream == 7 && sheader.Function == 61) HighlightStep(0); // 1번: S7F61
                    if (sheader.Stream == 7 && sheader.Function == 62) HighlightStep(1); // 2번: S7F62
                    if (sheader.Stream == 2 && sheader.Function == 87) HighlightStep(2); // 3번: S2F87
                    if (sheader.Stream == 2 && sheader.Function == 88) HighlightStep(3); // 4번: S2F88
                    if (sheader.Stream == 6 && sheader.Function == 68) HighlightStep(6); // 7번: S6F68
                    if (sheader.Stream == 6 && sheader.Function == 92) HighlightStep(9); // 10번: S6F92
                    if (sheader.Stream == 1 && sheader.Function == 68) HighlightStep(11); // 12번: S1F68
                }
                else if (lblScenarioStatus.Text.Contains("Scenario B"))
                {
                    var sheader = sentMsg.Header;
                    if (sheader.Stream == 6 && sheader.Function == 64) HighlightStep(4); // 5번: S6F64
                    if (sheader.Stream == 6 && sheader.Function == 62) HighlightStep(6); // 7번: S6F62
                    if (sheader.Stream == 2 && sheader.Function == 87) HighlightStep(7); // 8번: S2F87
                    if (sheader.Stream == 6 && sheader.Function == 68) HighlightStep(10); // 11번: S6F68
                    if (sheader.Stream == 1 && sheader.Function == 68) HighlightStep(13); // 14번: S1F68
                }
                else if (lblScenarioStatus.Text.Contains("Scenario C"))
                {
                    var sheader = sentMsg.Header;
                    if (sheader.Stream == 6 && sheader.Function == 65) HighlightStep(0); // 1번: S6F65
                    if (sheader.Stream == 6 && sheader.Function == 66) HighlightStep(1); // 2번: S6F66
                    if (sheader.Stream == 6 && sheader.Function == 84) 
                    {
                        HighlightStep(3); // 4번: S6F84
                        UpdateScenarioStatus("Scenario C Completed", Color.Green);
                    }
                }
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

                // 시나리오 로그 연동 (시나리오가 진행 중인 경우에만 기록 - Status: Ready가 아닌 경우)
                if (!lblScenarioStatus.Text.EndsWith("Ready"))
                {
                    AddScenarioLog($"[{direction.ToUpper()}] S{header.Stream}F{header.Function} {msg.Title ?? ""}");
                    
                    // CMD: LoadDone 등 특정 키워드에 대한 하이라이트 처리
                    if ((msg.Title ?? "").Contains("LoadDone")) HighlightStep(7);
                }

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
                        rtbLog.SelectionColor = Color.Green;
                    }
                    else if (direction == "Send")
                    {
                        rtbLog.SelectionColor = Color.Blue;
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

        #region Scenario Simulation

        private void AddScenarioLog(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AddScenarioLog(message)));
                return;
            }
            string timeStr = DateTime.Now.ToString("HH:mm:ss.fff");
            lbScenarioLog.Items.Add($"[{timeStr}] {message}");
            lbScenarioLog.SelectedIndex = lbScenarioLog.Items.Count - 1;
            if (lbScenarioLog.Items.Count > 100) lbScenarioLog.Items.RemoveAt(0);
        }

        private void InitScenarioFlow(string scenarioName)
        {
            pnlVisualFlow.Controls.Clear();
            _currentScenarioSteps.Clear();

            // 타이틀 레이블 추가 (ECS Simulator, CIM, EQP) - 좌측으로 최대한 이동
            Label lblHost = new Label { Text = "ECS Simulator", Font = new Font("Malgun Gothic", 8, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(5, 5), AutoSize = true };
            Label lblCim = new Label { Text = "CIM (Main)", Font = new Font("Malgun Gothic", 8, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(135, 5), AutoSize = true };
            Label lblEqp = new Label { Text = "EQP Simulator", Font = new Font("Malgun Gothic", 8, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(295, 5), AutoSize = true };
            pnlVisualFlow.Controls.Add(lblHost);
            pnlVisualFlow.Controls.Add(lblCim);
            pnlVisualFlow.Controls.Add(lblEqp);

            // ... flow 정의 생략 (이후 라인에서 캡처됨) ...
            List<(string msg, string dir)> flow = new List<(string, string)>();
            if (scenarioName == "A")
            {
                flow.Add(("S7F61 (Recipe ID Confirm Req)", "H->C"));
                flow.Add(("S7F62 (Ack)", "C->H"));
                flow.Add(("S2F87 (Load Glass Data)", "H->C"));
                flow.Add(("S2F88 (Ack)", "C->H"));
                flow.Add(("Glass Move Req (LR)", "C->E")); // [수정] C->S -> C->E
                flow.Add(("S6F67 (Move Request: LR)", "C->H"));
                flow.Add(("S6F68 (Ack)", "H->C"));
                flow.Add(("CMD: LoadDone", "E->C")); // [수정] S->C -> E->C
                flow.Add(("S6F91 (Status Change: RUN)", "C->H"));
                flow.Add(("S6F92 (Ack)", "H->C"));
                flow.Add(("S1F67 (All Glass Position Report)", "C->H"));
                flow.Add(("S1F68 (Ack)", "H->C"));
            }
            else if (scenarioName == "B")
            {
                flow.Add(("CMD: InspectDone", "E->C")); // [수정] S->C -> E->C
                flow.Add(("S6F11/S6F91 (Status Change Report)", "C->H"));
                flow.Add(("CMD: ExchangeReadyDone", "E->C")); // [수정] S->C -> E->C
                flow.Add(("S6F63 (Unload Glass Data Report)", "C->H"));
                flow.Add(("S6F64 (Ack)", "H->C"));
                flow.Add(("S6F61 (Load Glass Data Req)", "C->H"));
                flow.Add(("S6F62 (Ack)", "H->C"));
                flow.Add(("S2F87 (New Glass Data)", "H->C"));
                flow.Add(("Move Req (UW/ER)", "E->C")); // [수정] C->S -> E->C (EQP에서 CIM으로 요청하는 성격)
                flow.Add(("S6F67 (Move Request)", "C->H"));
                flow.Add(("S6F68 (Ack)", "H->C"));
                flow.Add(("CMD: ExchangeDone", "E->C")); // [수정] S->C -> E->C
                flow.Add(("S1F67 (Glass Position Report)", "C->H"));
                flow.Add(("S1F68 (Ack)", "H->C"));
            }
            else if (scenarioName == "C")
            {
                flow.Add(("S6F65 (LOT End Data Send)", "H->C"));
                flow.Add(("S6F66 (Ack)", "C->H"));
                flow.Add(("S6F83 (LOT Actual Processing Data Report)", "C->H"));
                flow.Add(("S6F84 (Ack)", "H->C"));
            }

            int y = 30; // 타이틀 아래부터 시작
            for (int i = 0; i < flow.Count; i++)
            {
                var step = new ScenarioStep
                {
                    StepNumber = i + 1,
                    MessageName = flow[i].msg,
                    Direction = flow[i].dir
                };

                Panel stepRow = CreateStepRow(step);
                stepRow.Location = new Point(5, y);
                pnlVisualFlow.Controls.Add(stepRow);
                step.StepControl = stepRow;
                _currentScenarioSteps.Add(step);
                y += 50; // 행 간격 확대 (40 -> 50)
            }
        }

        private Panel CreateStepRow(ScenarioStep step)
        {
            Panel p = new Panel { Size = new Size(520, 45), BackColor = Color.Transparent }; // [수정] 높이 35 -> 45 (2줄 대응)
            
            // 번호 버블
            Label lblNum = new Label
            {
                Text = step.StepNumber.ToString(),
                Size = new Size(20, 20),
                Location = new Point(155, 15), // 중앙 정렬 조정 (12 -> 15)
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(220, 220, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
            };
            step.Bubble = lblNum; // Bubble 레퍼런스 저장
            System.Drawing.Drawing2D.GraphicsPath bpath = new System.Drawing.Drawing2D.GraphicsPath();
            bpath.AddEllipse(0, 0, lblNum.Width, lblNum.Height);
            lblNum.Region = new Region(bpath);
            lblNum.Name = "StepNum";

            Label lblMsg = new Label
            {
                Text = step.MessageName,
                AutoSize = true,
                MaximumSize = new Size(115, 0), // [수정] 너비 제한하여 2줄 출력 유도
                Font = new Font("Malgun Gothic", 8),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            Label lblArrow = new Label
            {
                AutoSize = true,
                Font = new Font("Consolas", 8),
                ForeColor = Color.DarkGray
            };

            // 방향에 따른 배치 (여백 및 수직 정렬 정밀 조정)
            if (step.Direction == "H->C")
            {
                lblArrow.Text = "──────────▶";
                lblArrow.Location = new Point(35, 25); // 화살표 하향 (22 -> 25)
                lblMsg.Location = new Point(2, 5);    // 메시지 상단 여백 확보 (2 -> 5)
                lblMsg.MaximumSize = new Size(150, 0);
            }
            else if (step.Direction == "C->H")
            {
                lblArrow.Text = "◀──────────";
                lblArrow.Location = new Point(35, 25);
                lblMsg.Location = new Point(2, 5);
                lblMsg.MaximumSize = new Size(150, 0);
            }
            else if (step.Direction == "C->E")
            {
                lblArrow.Text = "──────────▶";
                lblArrow.Location = new Point(180, 25);
                lblMsg.Location = new Point(295, 5); // 메시지 상단 여백 확보 (2 -> 5)
                lblMsg.MaximumSize = new Size(200, 0); 
            }
            else if (step.Direction == "E->C")
            {
                lblArrow.Text = "◀──────────";
                lblArrow.Location = new Point(180, 25);
                lblMsg.Location = new Point(295, 5);
                lblMsg.MaximumSize = new Size(200, 0);
            }

            p.Controls.Add(lblNum);
            p.Controls.Add(lblMsg);
            p.Controls.Add(lblArrow);
            return p;
        }

        private void HighlightStep(int index)
        {
            if (index < 0 || index >= _currentScenarioSteps.Count) return;

            if (pnlVisualFlow.InvokeRequired)
            {
                pnlVisualFlow.Invoke(new Action(() => HighlightStep(index)));
                return;
            }

            // [보완] 현재 단계 이전의 모든 단계를 자동으로 Green(완료) 처리
            for (int i = 0; i < index; i++)
            {
                var prevStep = _currentScenarioSteps[i];
                if (prevStep.Bubble != null)
                {
                    prevStep.Bubble.BackColor = Color.LimeGreen;
                    prevStep.Bubble.ForeColor = Color.White;
                }
            }

            // 기존에 하이라이트된(Orange) 항목이 있다면 Green(완료)으로 변경
            foreach (var step in _currentScenarioSteps)
            {
                if (step.Bubble != null && step.Bubble.BackColor == Color.Orange)
                {
                    step.Bubble.BackColor = Color.LimeGreen;
                    step.Bubble.ForeColor = Color.White;
                }
            }

            // 현재 단계를 LimeGreen으로 강조 (사용자 요청 반영: 녹색으로 통일)
            var currentStep = _currentScenarioSteps[index];
            if (currentStep.Bubble != null)
            {
                currentStep.Bubble.BackColor = Color.LimeGreen;
                currentStep.Bubble.ForeColor = Color.White;
            }
        }

        private void UpdateScenarioStatus(string status, Color color)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateScenarioStatus(status, color)));
                return;
            }
            lblScenarioStatus.Text = $"Status: {status}";
            lblScenarioStatus.ForeColor = color;
        }

        private async void btnRunScenarioA_Click(object sender, EventArgs e)
        {
            try
            {
                btnRunScenarioA.Enabled = false;
                lbScenarioLog.Items.Clear();
                UpdateScenarioStatus("Running Scenario A (Load)", Color.Blue);
                AddScenarioLog("Starting Scenario A: Glass Loading Sequence");

                AddScenarioLog("Scenario A initiated. Waiting for CIM reports...");
                UpdateScenarioStatus("Scenario A: Waiting for Reports", Color.Orange);

                // 1. S7F61 전송 (Recipe Confirm)
                AddScenarioLog(">> Send S7F61 (Recipe ID Confirm)");
                InitScenarioFlow("A"); // 시나리오 플로우 초기화 및 생성
                server.SendS7F61(int.Parse(portCountTextBox.Text));
                HighlightStep(0); // 1번: S7F61
                await Task.Delay(500); 

                // 2. S2F87 전송 (Load Glass Data)
                AddScenarioLog($">> Send S2F87 (Load Glass Data) - GlassID: {txtScenarioGlassId.Text}");
                serverStatus.GLSSTATUS = "1";
                server.SendS2F87(serverStatus.GLSSTATUS, 0);
                HighlightStep(2); // 3번: S2F87
                // await Task.Delay(1000); // 전송 후 지연 제거 (이벤트 수신 대기를 위해)

            }
            catch (Exception ex)
            {
                AddScenarioLog($"Error: {ex.Message}");
                UpdateScenarioStatus("Scenario A Failed", Color.Red);
            }
            finally
            {
                btnRunScenarioA.Enabled = true;
            }
        }

        private async void btnRunScenarioB_Click(object sender, EventArgs e)
        {
            try
            {
                btnRunScenarioB.Enabled = false;
                lbScenarioLog.Items.Clear();
                UpdateScenarioStatus("Running Scenario B (Exchange)", Color.Blue);
                AddScenarioLog("Starting Scenario B: Glass Exchange Sequence");
                
                InitScenarioFlow("B");
                AddScenarioLog("Scenario B initiated. Waiting for InspectDone...");
                UpdateScenarioStatus("Scenario B: Waiting for InspectDone (S6F91)", Color.Orange);

                await Task.Delay(500);

            }
            catch (Exception ex)
            {
                AddScenarioLog($"Error: {ex.Message}");
                UpdateScenarioStatus("Scenario B Failed", Color.Red);
            }
            finally
            {
                btnRunScenarioB.Enabled = true;
            }
        }

        private async void btnRunScenarioC_Click(object sender, EventArgs e)
        {
            try
            {
                btnRunScenarioC.Enabled = false;
                lbScenarioLog.Items.Clear();
                UpdateScenarioStatus("Running Scenario C (Lot End)", Color.Blue);
                AddScenarioLog($"Starting Scenario C: LOT End Sequence (LOT: {txtScenarioLotId.Text})");

                InitScenarioFlow("C");
                AddScenarioLog("Scenario C initiated. Waiting for UnloadDone...");
                UpdateScenarioStatus("Scenario C: Waiting for UnloadDone (S6F91)", Color.Orange);

                // 1. S6F65 전송 (LOT End Data)
                AddScenarioLog(">> Send S6F65 (LOT End Data)");
                server.SendS6F65();
                await Task.Delay(500);

            }
            catch (Exception ex)
            {
                AddScenarioLog($"Error: {ex.Message}");
                UpdateScenarioStatus("Scenario C Failed", Color.Red);
            }
            finally
            {
                btnRunScenarioC.Enabled = true;
            }
        }

        private void tabControlScenarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string scenario = "A";
            switch (tabControlScenarios.SelectedIndex)
            {
                case 0: scenario = "A"; break;
                case 1: scenario = "B"; break;
                case 2: scenario = "C"; break;
            }
            InitScenarioFlow(scenario);
            AddScenarioLog($"Switched to Scenario {scenario} View");
        }

        #endregion
    }
}
