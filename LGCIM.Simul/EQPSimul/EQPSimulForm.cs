using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LGCIM.Lib;

namespace LGCIMServer.EQPSimul
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class EQPSimulForm : Form
    {
        EQPSimulator simul = new EQPSimulator();
        public EQPSimulator Simulator => simul;

        public EQPtoCIMData eqData => simul.eqData;
        public LGCIM.Lib.ECSData LoadGlass => simul.LoadGlass;
        public LGCIM.Lib.ECSData UnloadGlass => simul.UnloadGlass;

        List<RadioButton> radioCmd = new List<RadioButton>();

        public EQPSimulForm()
        {
            simul = EQPSimulator.LoadFromFile(simul.LastValuePath);
            InitializeComponent();
        }

        private void EQPSimulator_Load(object sender, EventArgs e)
        {
            simul.OnLog += AddLog;
            simul.OnDataUpdate += UpdateInfo;

            // 실행 시 Listen은 기본적으로 true 상태로 시작 (사용자 요청)
            simul.Data.IsListen = true;
            listenCheckBox.Checked = true;
            
            AddLog("Server Standby (Listen Check Required)");

            radioCmd.Clear();

            Dictionary<string, string> cmdToSxFx = new Dictionary<string, string>()
            {
                { EQPtoCIM.LoadReadyDone.ToString(), "(S6F11)" },
                { EQPtoCIM.LoadDone.ToString(), "(S6F91/S1F67)" },
                { EQPtoCIM.UnloadReadyDone.ToString(), "(S6F11)" },
                { EQPtoCIM.UnloadDone.ToString(), "(S6F91)" },
                { EQPtoCIM.InspectDone.ToString(), "(S6F91)" },
                { EQPtoCIM.ExchangeReadyDone.ToString(), "(S6F63)" },
                { EQPtoCIM.ExchangeDone.ToString(), "(S1F67)" },
                { EQPtoCIM.Alarm.ToString(), "(S5F1)" },
                { EQPtoCIM.AlarmClear.ToString(), "(S5F1)" },

                { EQPtoCIM.GLSMV_LR.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_UR.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_ER.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_UW.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_LW.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_CR.ToString(), "(S6F67)" },
            };

            GroupBox flowGroup = new GroupBox { Text = "S6F67 Request Flow", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(5) };
            FlowLayoutPanel flowPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            flowGroup.Controls.Add(flowPanel);

            GroupBox scenarioBGroup = new GroupBox { Text = "Scenario B (Exchange)", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(5) };
            FlowLayoutPanel scenarioBPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            scenarioBGroup.Controls.Add(scenarioBPanel);

            GroupBox eventGroup = new GroupBox { Text = "Done/Event Report", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(5) };
            FlowLayoutPanel eventPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            eventGroup.Controls.Add(eventPanel);

            // [추가] 시나리오 A/C 표준 매크로 버튼
            Button btnAStart = new Button { Text = "Macro: Standard Loading (RUN+LR)", AutoSize = true, Margin = new Padding(0, 5, 0, 5), BackColor = Color.LightYellow };
            btnAStart.Click += (s, e) => {
                simul.MacroStandardLoad();
                UpdateInfo();
            };
            flowPanel.Controls.Add(btnAStart);

            Button btnCStart = new Button { Text = "Macro: Atomic Unload Report", AutoSize = true, Margin = new Padding(0, 5, 0, 5), BackColor = Color.LightCoral };
            btnCStart.Click += (s, e) => {
                simul.MacroUnloadReport();
                UpdateInfo();
            };
            eventPanel.Controls.Add(btnCStart);

            Button btnBStart = new Button { Text = "Macro: Atomic Exchange Report", AutoSize = true, Margin = new Padding(0, 5, 0, 5), BackColor = Color.LightBlue };
            btnBStart.Click += (s, e) => {
                simul.MacroExchangeReport();
                UpdateInfo();
            };
            scenarioBPanel.Controls.Add(btnBStart);

            eqpControlPanel.Controls.Clear();
            eqpControlPanel.Controls.Add(flowGroup);
            eqpControlPanel.Controls.Add(scenarioBGroup);
            eqpControlPanel.Controls.Add(eventGroup);

            string[] s6f67Flows = { "GLSMV_LR", "GLSMV_UR", "GLSMV_ER", "GLSMV_UW", "GLSMV_LW", "GLSMV_CR" };
            string[] scenarioBItems = { "InspectDone", "ExchangeReadyDone" };

            foreach (var v in Enum.GetNames(typeof(EQPtoCIM)))
            {
                if (v == EQPtoCIM.Alarm.ToString() || v == EQPtoCIM.AlarmClear.ToString() ||
                    v == EQPtoCIM.None.ToString() || v == EQPtoCIM.SendData.ToString() ||
                    v == EQPtoCIM.Pause.ToString() || v == EQPtoCIM.Error.ToString() ||
                    v == EQPtoCIM.RecipeCreate.ToString() || v == EQPtoCIM.RecipeChange.ToString() ||
                    v == EQPtoCIM.RecipeDelete.ToString() || v == EQPtoCIM.RecipeRename.ToString() ||
                    v == EQPtoCIM.ProgramStarted.ToString() || v == EQPtoCIM.ShutDown.ToString() ||
                    v == EQPtoCIM.UpdateStatus.ToString() || v == EQPtoCIM.CancelReq.ToString())
                    continue;

                string text = $"{v}";
                if (cmdToSxFx.ContainsKey(v))
                {
                    text += $" {cmdToSxFx[v]}";
                }

                var radio = new RadioButton
                {
                    Name = $"{v}radio",
                    Text = text,
                    Tag = $"{v}",
                    AutoSize = true,
                };

                radio.Click += Radio_Click;

                if (s6f67Flows.Contains(v))
                {
                    flowPanel.Controls.Add(radio);
                }
                else if (scenarioBItems.Contains(v))
                {
                    scenarioBPanel.Controls.Add(radio);
                }
                else
                {
                    eventPanel.Controls.Add(radio);
                }

                radioCmd.Add(radio);
            }

            alarmComboBox.DataSource = Enum.GetValues(typeof(AlarmType));
            alarmComboBox.SelectedIndex = 0;

            statusComboBox.DataSource = Enum.GetValues(typeof(EQPStatus));
            readyStatusComboBox.DataSource = Enum.GetValues(typeof(EQPReadyStatus));

            UpdateInfo();
        }

        private void statusChangeButton_Click(object sender, EventArgs e)
        {
            // [통합] Status와 ReadyStatus를 한 번의 클릭으로 모두 적용
            bool isStatusChanged = false;
            
            if (statusComboBox.SelectedItem is EQPStatus status)
            {
                simul.ChangeStatus(status);
                isStatusChanged = true;
            }

            if (readyStatusComboBox.SelectedItem is EQPReadyStatus rStatus)
            {
                eqData.EqpReadyStatus = rStatus;
                if (!isStatusChanged) // status가 이미 바뀌지 않았다면 강제로 보고 로직 호출
                    simul.ChangeStatus(eqData.eqpStatus);
                isStatusChanged = true;
            }

            if (isStatusChanged)
            {
                UpdateInfo();
                AddLog($"Status applied: {eqData.eqpStatus} / {eqData.EqpReadyStatus}");
            }
        }

        private void Radio_Click(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button == null)
                return;

            button.Checked = true;

            var cmd = (EQPtoCIM)Enum.Parse(typeof(EQPtoCIM), button.Tag.ToString());

            // 인터락 체크
            if (!simul.CheckInterlock(cmd, out string interlockReason))
            {
                button.Checked = false; // 선택 취소
                AddLog($"[INTERLOCK] {cmd} 실행 실패: {interlockReason}");
                return;
            }

            eqData.Command = cmd;
            eqData.AutoRun = autoRunCheckBox.Checked;
            eqData.GlassIn = glassInCheckBox.Checked;

            switch (cmd)
            {
                case EQPtoCIM.LoadReadyDone:
                    simul.LoadReady();
                    break;
                
                case EQPtoCIM.LoadDone:
                    simul.LoadDone();
                    break;

                case EQPtoCIM.UnloadReadyDone:
                    simul.UnloadReady();
                    break;
                
                case EQPtoCIM.ExchangeReadyDone:
                    simul.ExchangeReadyDone();
                    break;

                case EQPtoCIM.InspectDone:
                    simul.InspectDone();
                    break;

                case EQPtoCIM.ExchangeDone:
                    simul.ExchangeDone();
                    break;
                
                case EQPtoCIM.UnloadDone:
                    simul.UnloadDone();
                    break;

                case EQPtoCIM.Alarm:
                case EQPtoCIM.AlarmClear:
                    break;

                case EQPtoCIM.GLSMV_LR:
                    simul.SendLR();
                    break;
                case EQPtoCIM.GLSMV_UR:
                    simul.SendUR();
                    break;
                case EQPtoCIM.GLSMV_ER:
                    simul.SendER();
                    break;
                case EQPtoCIM.GLSMV_UW:
                    simul.SendUW();
                    break;
                case EQPtoCIM.GLSMV_LW:
                    simul.SendLW();
                    break;
                case EQPtoCIM.GLSMV_CR:
                    simul.SendCR();
                    break;
            }
            simul.Save(simul.LastValuePath);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { UpdateInfo(); });
            else
            {
                glassInCheckBox.Checked = eqData.GlassIn;
                autoRunCheckBox.Checked = eqData.AutoRun;
                listenCheckBox.Checked = simul.Data.IsListen;
                eqpStatusLabel.Text = $"EQPStatus : {eqData.eqpStatus}  /  Ready : {eqData.EqpReadyStatus}";

                if (LoadGlass != null)
                {
                    loadGlassIDtextBox.Text = $"AGlass : {LoadGlass.AGlass.GLSID}, BGlass: {LoadGlass.BGlass.GLSID}";
                    loadGlassCodetextBox.Text = $"AGlass : {LoadGlass.AGlass.GLSCODE}, BGlass: {LoadGlass.BGlass.GLSCODE}";
                    loadGlassOrdertextBox.Text = $"AGlass : {LoadGlass.AGlass.GLSODR}, BGlass: {LoadGlass.BGlass.GLSODR}";
                }
                if (UnloadGlass != null)
                {
                    unloadGlassIDtextBox.Text = $"AGlass : {UnloadGlass.AGlass.GLSID}, BGlass: {UnloadGlass.BGlass.GLSID}";
                    unloadGlassCodetextBox.Text = $"AGlass : {UnloadGlass.AGlass.GLSCODE}, BGlass: {UnloadGlass.BGlass.GLSCODE}";
                    unloadGlassOrdertextBox.Text = $"AGlass : {UnloadGlass.AGlass.GLSODR}, BGlass: {UnloadGlass.BGlass.GLSODR}";
                }

                // 시퀀스 가이드 하이라이트
                HighlightNextStep();
            }
        }

        private void HighlightNextStep()
        {
            foreach (var radio in radioCmd)
            {
                radio.BackColor = Color.Transparent;
                radio.ForeColor = Color.Black;
            }

            string nextTarget = "";
            switch (simul.Data.CurrentStep)
            {
                case ScenarioStep.Idle:
                case ScenarioStep.UnloadReported:
                    nextTarget = "GLSMV_LR"; // 다음 권장: Load Request
                    break;
                case ScenarioStep.LoadReadyRequested:
                    nextTarget = "LoadDone"; // 다음 권장: Loading 완료
                    break;
                case ScenarioStep.LoadDataReceived:
                case ScenarioStep.ProcessStarted:
                    nextTarget = "InspectDone"; // 다음 권장: 검사 완료
                    break;
            }

            if (!string.IsNullOrEmpty(nextTarget))
            {
                var target = radioCmd.FirstOrDefault(r => r.Tag.ToString() == nextTarget);
                if (target != null)
                {
                    target.BackColor = Color.Yellow;
                    target.ForeColor = Color.Red;
                }
            }
        }

        private void alarmSetButton_Click(object sender, EventArgs e)
        {
            simul.SetAlarm((AlarmType)alarmComboBox.SelectedIndex, 100);
            UpdateInfo();
        }

        private void alarmClearButton_Click(object sender, EventArgs e)
        {
            simul.ClearAlarm();
            UpdateInfo();
        }

        private void EQPSimulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TopLevel)
            {
                simul.StopServer();
            }
            Hide();
        }

        public void StopSimulServer()
        {
            simul.StopServer();
        }

        private void glassInCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            eqData.GlassIn = glassInCheckBox.Checked;
        }

        private void autoRunCheckBox_CheckedChanged(object sender, EventArgs e) 
        {
            eqData.AutoRun = autoRunCheckBox.Checked;
        }

        private void listenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            simul.Data.IsListen = listenCheckBox.Checked;
            if (simul.Data.IsListen)
            {
                simul.StartServer();
                AddLog("Server Started (Port: 3501)");
            }
            else
            {
                simul.StopServer();
                AddLog("Server Stopped");
            }
            simul.Save(simul.LastValuePath);
        }

        private void AddLog(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { AddLog(msg); });
                return;
            }

            Color logColor = Color.Black;
            if (msg.Contains("[SEND]")) logColor = Color.Blue;
            else if (msg.Contains("[RECV]")) logColor = Color.Green;

            string nowStr = DateTime.Now.ToString("HH:mm:ss.fff");
            string log = $"{nowStr} > {msg}\r\n";

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = logColor;
            rtbLog.AppendText(log);
            rtbLog.SelectionColor = rtbLog.ForeColor; // Reset to default

            rtbLog.ScrollToCaret();

            if (rtbLog.Lines.Length > 500)
            {
                rtbLog.ReadOnly = false;
                rtbLog.Text = string.Join("\r\n", rtbLog.Lines.Skip(rtbLog.Lines.Length - 500));
                rtbLog.ReadOnly = true;
            }

            // [추가] 로그 파일 저장
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string fileName = Path.Combine(logPath, $"EQPSimul_{DateTime.Now:yyyyMMdd}.log");
                
                // 간단한 파일 쓰기 (개행 포함)
                File.AppendAllText(fileName, $"{nowStr}\t{msg}\r\n");
            }
            catch (Exception ex)
            {
                // 로깅 실패 시 무시 (UI 로깅은 유지)
                Console.WriteLine($"Log Save Failed: {ex.Message}");
            }
        }
    }
}
