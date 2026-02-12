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
using TOECIM.Forms;
using TOECIMLib;

namespace TOECIM
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class EQPSimulForm : Form
    {

        EQPSimulator simul = new EQPSimulator();
        //public event EventHandler<EQtoCIMData> OnEQEvent = delegate { };    //EQ to CIM 이벤트

        //eq에서 관리하는 glass data들
        public EQPtoCIMData eqData => simul.eqData;
        public TOECIMLib.ECSData LoadGlass => simul.LoadGlass;
        public TOECIMLib.ECSData UnloadGlass => simul.UnloadGlass;

        //AlarmInfo alarm;
        //EQPStatus curEQPStatus => simul.preEQPStatus;
        List<RadioButton> radioCmd = new List<RadioButton>();

        public EQPSimulForm()
        {
            simul = EQPSimulator.LoadFromFile(simul.LastValuePath);
            InitializeComponent();
        }

        private void EQPSimulator_Load(object sender, EventArgs e)
        {
            simul.StartServer();

            radioCmd.Clear();

            // SxFx 매핑 정의
            // SxFx 매핑 정의
            Dictionary<string, string> cmdToSxFx = new Dictionary<string, string>()
            {
                { EQPtoCIM.LoadReadyDone.ToString(), "(S6F11)" },
                { EQPtoCIM.LoadDone.ToString(), "(S1F67)" },
                { EQPtoCIM.UnloadReadyDone.ToString(), "(S6F11)" },
                { EQPtoCIM.UnloadDone.ToString(), "(S1F67)" },
                { EQPtoCIM.ExchangeDone.ToString(), "(S1F67)" },
                { EQPtoCIM.Alarm.ToString(), "(S5F1)" },
                { EQPtoCIM.AlarmClear.ToString(), "(S5F1)" },

                // S6F67 관련 매핑 추가
                { EQPtoCIM.GLSMV_LR.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_UR.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_ER.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_UW.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_LW.ToString(), "(S6F67)" },
                { EQPtoCIM.GLSMV_CR.ToString(), "(S6F67)" },
            };

            // 그룹별 컨테이너 생성
            GroupBox flowGroup = new GroupBox { Text = "S6F67 Request Flow", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(5) };
            FlowLayoutPanel flowPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            flowGroup.Controls.Add(flowPanel);

            GroupBox eventGroup = new GroupBox { Text = "Done/Event Report", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(5) };
            FlowLayoutPanel eventPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            eventGroup.Controls.Add(eventPanel);

            eqpControlPanel.Controls.Clear();
            eqpControlPanel.Controls.Add(flowGroup);
            eqpControlPanel.Controls.Add(eventGroup);

            string[] s6f67Flows = { "GLSMV_LR", "GLSMV_UR", "GLSMV_ER", "GLSMV_UW", "GLSMV_LW", "GLSMV_CR" };

            foreach (var v in Enum.GetNames(typeof(EQPtoCIM)))
            {
                // Alarm, AlarmClear 명령은 별도 버튼으로 처리하므로 라디오 버튼 목록에서 제외
                if (v == EQPtoCIM.Alarm.ToString() || v == EQPtoCIM.AlarmClear.ToString() ||
                    v == EQPtoCIM.None.ToString() || v == EQPtoCIM.SendData.ToString() ||
                    v == EQPtoCIM.Pause.ToString() || v == EQPtoCIM.Error.ToString() ||
                    v == EQPtoCIM.RecipeCreate.ToString() || v == EQPtoCIM.RecipeChange.ToString() ||
                    v == EQPtoCIM.RecipeDelete.ToString() || v == EQPtoCIM.RecipeRename.ToString() ||
                    v == EQPtoCIM.ProgramStarted.ToString() || v == EQPtoCIM.ShutDown.ToString() ||
                    v == EQPtoCIM.UpdateStatus.ToString() || v == EQPtoCIM.CancelReq.ToString() ||
                    v == EQPtoCIM.InspectDone.ToString())
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

                radio.CheckedChanged += Radio_CheckedChanged;

                // 그룹 분류하여 패널에 추가
                if (s6f67Flows.Contains(v))
                {
                    flowPanel.Controls.Add(radio);
                }
                else
                {
                    eventPanel.Controls.Add(radio);
                }

                radioCmd.Add(radio);
            }

            alarmComboBox.DataSource = Enum.GetValues(typeof(AlarmType));
            alarmComboBox.SelectedIndex = 0;

            // Manual Status Control UI
            statusComboBox.DataSource = Enum.GetValues(typeof(EQPStatus));
            readyStatusComboBox.DataSource = Enum.GetValues(typeof(EQPReadyStatus));

            UpdateInfo();
        }

        private void statusChangeButton_Click(object sender, EventArgs e)
        {
            if (statusComboBox.SelectedItem is EQPStatus status)
            {
                simul.ChangeStatus(status);
                UpdateInfo();
            }
        }

        private void readyStatusChangeButton_Click(object sender, EventArgs e)
        {
            if (readyStatusComboBox.SelectedItem is EQPReadyStatus rStatus)
            {
                eqData.EqpReadyStatus = rStatus;
                simul.ChangeStatus(eqData.eqpStatus); // Send current status + updated ready status
                UpdateInfo();
            }
        }


        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked == false)
                return;

            var cmd = (EQPtoCIM)Enum.Parse(typeof(EQPtoCIM), button.Tag.ToString());
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

                //Unload Wait 커맨드 받으면 UnloadReady 눌러주기
                //worker 끝내는 이벤트
                case EQPtoCIM.UnloadReadyDone:
                    simul.UnloadReady();
                    break;
                
                //Normal Glass Sequence
                case EQPtoCIM.ExchangeReadyDone:
                    simul.Exchange();
                    break;

                //Normal Glass Sequence
                case EQPtoCIM.ExchangeDone:
                    simul.ExchangeDone();
                    break;
                
                case EQPtoCIM.UnloadDone:
                    simul.UnloadDone();
                    break;

                case EQPtoCIM.Alarm:
                case EQPtoCIM.AlarmClear:
                    // 전용 버튼에서 직접 처리함
                    break;
            }
            simul.Save(simul.LastValuePath);

            //OnEQEvent(this, eqData);
            UpdateInfo();
        }





        private void UpdateInfo()
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { UpdateInfo(); });
            else
            {
                glassInCheckBox.Checked = eqData.GlassIn;
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
            // 탭에 임베딩된 경우 서버 정지하지 않음 (SimulatorForm이 관리)
            if (TopLevel)
            {
                simul.StopServer();
            }
            Hide();
        }

        /// <summary>
        /// 외부에서 서버 정지 호출용
        /// </summary>
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
        private void AddLog(string msg)
        {
            string log = $"{DateTime.Now:HH:mm:ss.fff} > {msg}";
            simulLogListBox.Items.Insert(0, log);

            if (simulLogListBox.Items.Count > 500)
            {
                simulLogListBox.Items.RemoveAt(simulLogListBox.Items.Count - 1);
            }
        }
    }
}
