using DBMulti;
// using System.Windows.Forms.DataVisualization.Charting;
using Engion;
using Engion.Net;
using LGHSMSBase;
using LGHSMSBase.Forms;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOECIM.Forms;
using TOECIMLib;
//using TOELib;

namespace TOECIM
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class MainForm : Form
    {

        
        ClientCIM cim => Vars.cim;
        CIMStatus cimStatus => cim.cimStatus;
        LogEx log => ClientCIM.log;

        JsonServer<CimCommData> server;

        //Alarm
        AlarmForm alarmForm;
        AlarmInfo selectAlarm;


        Task StatusTimer = null;
        //eq event
        //public event EventHandler<CIMtoEQData> OnCIMEvent = delegate { };  //CIM to EQ
        //public event EventHandler<EQtoCIMData> OnEQEvent = delegate { };  //EQ to CIM

        private object l = new object();

        public MainForm()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm: FormMain_Load Starting...");
            
            Program.SystemSetup = SystemSetup.Load(Program.systemSetupDir, log);

            // 3501 포트 연동 확인을 위해 시뮬레이터를 먼저 가동
            // 알람 페이지를 없앤다.
            tabControl1.TabPages.Remove(tabPage2);
            logViewer1.Log = log;
            log.OnLogEvent += (s, ee) => { logViewer1.ManageLog(ee); };


            // 콘솔 연결 확인 (비동기 재시도 로직이 StatusTimerFunc에서 수행되므로 초기 체크는 선택적)
            //ReqEqData(false); 

            // TOECIM이 서버 역할을 하던 로직 제거 (시뮬레이터가 서버 역할을 하므로)
            ListenFromConsole();

            InitialUpdate();

            cim.InitGem();

            cim.gem.OnGemEvent += (_, args) =>
            {
                var s = args.gemMessage.Stream;
                var f = args.gemMessage.Function;
                if (s > 0)
                    log.Information($"S{s}F{f}, {args.gemMessage.Title} Received");
            };
            cim.gem.OnSentEvent += (_, args) =>
            {
                var s = args.gemMessage.Stream;
                var f = args.gemMessage.Function;
                if (s > 0)
                    log.Information($"S{s}F{f} {args.gemMessage.Title} Sent");
            };

            cim.CimAlarmSet += (_, __) => ShowAlarm();
            //DB 초기화 (Alarm)
            cim.AlarmDBManager.InitDB();
            UpdateAlarmView();
            cim.CimAlarmSet += Cim_CimAlarmSet;

            //UI 업데이트
            cim.UpdateUI += Cim_UpdateUI;
            cim.OnRCSModeChanged += Cim_OnRCSModeChanged;
            cim.OnOPModeChanged += Cim_OnOPModeChanged;
            cim.OnGLSMVChanged += Cim_OnGLSMVChanged;

            //EQP Status 변경 이벤트

            cim.ReqEQData += Cim_ReqEQData;

            //Glass Move Status 변경 이벤트
            cim.GlassMoveEvent += GlassMoveEvent;

            //Pop up
            cim.HostMessagePopup += Cim_HostMessagePopup;
            cim.RecipeChangePopUp += Cim_RecipeChangePopUp;

            //Timeout Event
            cim.timeout.TimeoutEvent += Timeout_TimeoutEvent;


            StatusTimer = Task.Run(() => StatusTimerFunc());
        }

        private void Cim_ReqEQData(object sender, CimCommData e)
        {
            ReqEqData();
        }

        private void Timeout_TimeoutEvent(object sender, TimeoutParam e)
        {
            cim.SendS9F13(e.Name);
        }

        private void ListenFromConsole()
        {
            server = new JsonServer<CimCommData>(this, OnReceiveConsoleData, null);
            server.Listen(Program.SystemSetup.CIMPortNo);
            log.Information($"Cim port {Program.SystemSetup.CIMPortNo} Opened");
        }

        private CimCommData OnReceiveConsoleData(object owner, CimCommData spec)
        {
            // 연결 성공 처리
            cimStatus.ConsoleData.Error = false;
            UpdateStatusUI();

            var res = OnReceiveData(this, spec);
            return res;
        }

        private CimCommData OnReceiveData(object sender, CimCommData spec)
        {
            var data = spec.ToCIM;

            // 빈번한 로그를 줄이기 위해 명령이 None이 아닌 경우에만 출력 (또는 주석 처리)
            // 빈번한 로그를 줄이기 위해 명령이 None이 아닌 경우에만 출력 (또는 주석 처리)
            if (data.Command != EQPtoCIM.None)
            {
                string logMsg = $"[OnReceiveEQData] CMD : {data.Command}, EQPStatus : {data.eqpStatus}, AutoRun : {data.AutoRun}, GlassIn : {data.GlassIn}, ReadyStatus : {data.EqpReadyStatus}";
                log.AddLogMessage(LogType.Information, 0, logMsg);
            }
            
            var oldStatus = cimStatus.EQPStatus;
            cimStatus.CimCommData.ToCIM = data;
            var status = Vars.CIMEQPStatsu(data.eqpStatus);
            
            // SendData 커맨드일 때 강제 상태 업데이트 (디버깅용)
            /*
            if (data.Command == EQPtoCIM.SendData)
            {
                 // ChangeEQPStatusEvent(status); // 로직상 아래에서 처리됨
            }
            */

            if (cim.cimStatus.RCS == RCS.RCSEnum.OffLine)
            {
                UpdateStatusUI();
                return spec;
            }


            switch (data.Command)
            {
            case EQPtoCIM.None:
                break;

            //Console이 필요할 경우 CIM의 요청 없이 eq data를 보냄
            //ex) AutoRun이 false가 될 경우
            case EQPtoCIM.SendData:
                break;

            case EQPtoCIM.LoadReadyDone:
                {
                    ChangeEQPStatusEvent(status);
                    cim.SendS6F61();
                    //spec.ToConsole.Command = CIMtoEQP.LoadReadyDoneACK;
                    //spec.ToConsole.Online = cimStatus.RCS == RCS.RCSEnum.OffLine ? false : true;
                }
                break;

            case EQPtoCIM.LoadDone:
                {
                    ChangeEQPStatusEvent(status);
                    log.AddLogMessage(LogType.Information, 0, $"[OnReceiveEQData] Glass Load Done. EQPStatus : {data.eqpStatus}");
                    cim.MoveLoadtoUnload();
                    cim.SendS1F67();
                }
                break;

            case EQPtoCIM.InspectDone:
                {
                    // 상태를 RUN으로 강제하여 S6F91 보고 유도
                    ChangeEQPStatusEvent(EQPSTATUS.EQPSTATUSEnum.RUN);
                }
                break;

            case EQPtoCIM.ExchangeReadyDone:
                {
                    ChangeEQPStatusEvent(status);
                    cimStatus.UnloadGlass.AGlass.PNLIF = data.GlassPanelJudgeA;
                    cimStatus.UnloadGlass.BGlass.PNLIF = data.GlassPanelJudgeB;
                    cim.SendS6F63();
                    cim.SendS6F61();
                }
                break;

            case EQPtoCIM.ExchangeDone:
                {
                    ChangeEQPStatusEvent(status);
                    log.AddLogMessage(LogType.Information, 0, $"[OnReceiveEQData] Glass Exchange Done. EQPStatus : {data.eqpStatus}");

                    cim.MoveLoadtoUnload();
                    //Run 보고
                    cim.SendS1F67();
                }
                break;

            case EQPtoCIM.UnloadReadyDone:
                {
                    ChangeEQPStatusEvent(status);
                    cimStatus.UnloadGlass.AGlass.PNLIF = data.GlassPanelJudgeA;
                    cimStatus.UnloadGlass.BGlass.PNLIF = data.GlassPanelJudgeB;
                    cim.SendS6F63();
                    cim.SendS6F61();
                }
                break;

            case EQPtoCIM.UnloadDone:
                {
                    ChangeEQPStatusEvent(status);
                    cim.MoveLoadtoUnload();
                    //idle 보고
                    cim.SendS1F67();
                    log.AddLogMessage(LogType.Information, 0, $"Last Glass Unload Complete.");
                }
                break;

            case EQPtoCIM.Alarm:
                {
                    //알람 set 보고
                    var alarmList = data.AlarmList;

                    cim.AlarmOccur(alarmList);
                    ShowAlarm();
                    UpdateAlarmView();
                }
                break;

            case EQPtoCIM.AlarmClear:
                {
                    var reportedAlarms = cimStatus.AlarmList;
                    foreach (var a in reportedAlarms)
                    {
                        a.ALCD = ALCD.ReleaseCode(a.ALCD);
                        cim.SendS5F1(a, false);
                        cim.AlarmDBManager.ReleaseAlarm(a);
                    }
                    reportedAlarms.Clear();
                    cimStatus.Save();

                    status = Vars.CIMEQPStatsu((TOECIMLib.EQPStatus)data.eqpStatus);

                    //cim.SendS1F61();
                    //cim.SendS1F67();
                    ShowAlarm();
                    UpdateAlarmView();

                }
                break;

            case EQPtoCIM.Pause:
                {
                    cimStatus.Pause = true;
                    status = Vars.CIMEQPStatsu((TOECIMLib.EQPStatus)data.eqpStatus);

                    //Glass 없으면 바로 Process Down 상태 변경
                    if (cimStatus.EQGlassIn == false)
                    {
                        ChangeEQPStatusEvent(status);
                    }
                }
                break;

            case EQPtoCIM.Error:
                break;

            case EQPtoCIM.RecipeCreate:
                //cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Built);
                break;

            case EQPtoCIM.RecipeChange:
                //cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Change);
                break;

            case EQPtoCIM.RecipeDelete:
                //cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Delete);
                break;

            case EQPtoCIM.RecipeRename:
                //cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Rename);
                break;

            case EQPtoCIM.ShutDown:
                data.eqpStatus = TOECIMLib.EQPStatus.DOWN;
                break;

            case EQPtoCIM.UpdateStatus:
                break;

            case EQPtoCIM.CancelReq:
                // 알람 보고하거나 장비 DOWN 보고할 때 필요에 따라 자동으로 CR 보고함.
                //cim.SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.CR);
                break;

            case EQPtoCIM.GLSMV_LR:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.LR);
                break;
            case EQPtoCIM.GLSMV_UR:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.UR);
                break;
            case EQPtoCIM.GLSMV_ER:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.ER);
                break;
            case EQPtoCIM.GLSMV_UW:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.UW);
                break;
            case EQPtoCIM.GLSMV_LW:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.LW);
                break;
            case EQPtoCIM.GLSMV_CR:
                cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.CR);
                break;
            }

            // 상태가 실제로 바뀐 경우에만 이벤트 발생 (무한 루프 방지)
            if (oldStatus != status)
            {
                ChangeEQPStatusEvent(status);
            }

            lock (l)
            {
                cimStatus.CimCommData.ToCIM = data;
                cim.cimStatus.Save();
            }

            UpdateStatusUI();

            return spec;
        }

        //마지막 값 로드
        private void InitialUpdate()
        {

            //UI 업데이트
            UpdateJobDataUI();
            UpdateStatusUI();
            //Cim_OnEQStatusChanged(this, null);
            //Cim_OnGLSMVChanged(this, null); ;
            //Cim_OnOPModeChanged(this, null);
            //Cim_OnRCSModeChanged(this, null);
            //Cim_OnEQStatusChanged(this, null);
        }

        private void loopBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cim.SendS2F25();
        }

        private void timeCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cim.SendS2F17();
        }

        private void Cim_UpdateUI(object sender, EventArgs e)
        {
            UpdateJobDataUI();
        }

        private void Cim_OnGLSMVChanged(object sender, CIMArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Cim_OnGLSMVChanged(sender, e); });
            else
            {
                log.AddLogMessage(LogType.Information, 0, $"GLSMV Changed.");
                // glsmvLabel.Text = cimStatus.GLSMVSTATUS.ToString();
                glsmvLabel.Text = cimStatus.CimCommData?.ToCIM?.EqpReadyStatus.ToString();
            }
        }

        private void Cim_OnOPModeChanged(object sender, CIMArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Cim_OnOPModeChanged(sender, e); });
            else
            {
                log.AddLogMessage(LogType.Information, 0, $"OPMode Changed. {cimStatus.OPMode}");
                opModeButton.Text = cimStatus.OPMode.ToString();
            }
        }

        private void Cim_OnRCSModeChanged(object sender, CIMArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Cim_OnRCSModeChanged(sender, e); });
            else
            {
                log.AddLogMessage(LogType.Information, 0, $"RCSMode Changed. {cimStatus.RCS}");
                rcsButton.Text = cimStatus.RCS.ToString();
            }
        }

        private void UpdateStatusUI()
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { UpdateStatusUI(); });

            else
            {
                autoRunLabel.BackColor = cimStatus.IsAutoRun ? Color.Lime : Color.Red;
                glassInlabel.BackColor = cimStatus.EQGlassIn == true ? Color.Lime : Color.Red;
                glsmvLabel.Text = cimStatus.CimCommData.ToCIM.EqpReadyStatus.ToString();

                switch (cimStatus.EQPStatus)
                {
                    case EQPSTATUS.EQPSTATUSEnum.RUN:
                        EQStatusLabel.BackColor = Color.Lime;
                        break;

                    case EQPSTATUS.EQPSTATUSEnum.IDLE:
                        EQStatusLabel.BackColor = Color.Yellow;
                        break;

                    case EQPSTATUS.EQPSTATUSEnum.DOWN:
                        EQStatusLabel.BackColor = Color.Red;
                        break;

                    default:
                        EQStatusLabel.BackColor = Color.Gold;
                        break;
                }
                EQStatusLabel.Text = cimStatus.EQPStatus.ToString();

                // System.IO.File.AppendAllText("ui_debug.txt", $"{DateTime.Now}: UpdateStatusUI - ConsoleData.Error: {cimStatus.ConsoleData.Error}, Hash: {cimStatus.ConsoleData.GetHashCode()}\n");
                if (cimStatus.ConsoleData.Error == true)
                {
                    consoleStatusLabel.BackColor = Color.Red;
                }
                else
                {
                    consoleStatusLabel.BackColor = Color.Lime;
                }

                if (cimStatus.CurAlarm != null)
                {
                    alarmStatusLabel.BackColor = Color.Red;
                }
                else
                {
                    alarmStatusLabel.BackColor = Color.Lime;
                }
            }
        }

        private void UpdateJobDataUI()
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { UpdateJobDataUI(); });

            else
            {
                unitIDTextBox.Text = cimStatus.UnitID.Value.ToString();
                stageIDTextBox.Text = cimStatus.StageID.Value.ToString();

                //if (cimStatus.LoadGlass != null)
                {
                    portidTextBox.Text = cimStatus.LoadGlass?.AGlass?.PORTID.ToString() ?? "";
                    lotidTextBox.Text = cimStatus.LoadGlass?.AGlass?.LOTID ?? "";
                    cstidTextBox.Text = cimStatus.LoadGlass?.AGlass?.CSTID ?? "";

                    aGlassIDTextBox.Text = cimStatus.LoadGlass?.AGlass?.GLSID ?? "";
                    aGlassCodeTextBox.Text = cimStatus.LoadGlass?.AGlass?.GLSCODE.ToString() ?? "";
                    aGlassOdrTextBox.Text = cimStatus.LoadGlass?.AGlass?.GLSODR.ToString() ?? "";
                    aLoadPropertyGrid.SelectedObject = cimStatus.LoadGlass?.AGlass ?? null;
                    aLoadPropertyGrid.Refresh();

                    bGlassIDTextBox.Text = cimStatus.LoadGlass?.BGlass?.GLSID ?? "";
                    bGlassCodeTextBox.Text = cimStatus.LoadGlass?.BGlass?.GLSCODE.ToString() ?? "";
                    bGlassOdrTextBox.Text = cimStatus.LoadGlass?.BGlass?.GLSODR.ToString() ?? "";
                    bLoadPropertyGrid.SelectedObject = cimStatus.LoadGlass?.BGlass ?? null;
                    bLoadPropertyGrid.Refresh();
                }

                //if (cimStatus.UnloadGlass != null)
                {
                    aUnLoadPropertyGrid.SelectedObject = cimStatus.UnloadGlass?.AGlass ?? null;
                    aUnLoadPropertyGrid.Refresh();
                    bUnLoadPropertyGrid.SelectedObject = cimStatus.UnloadGlass?.BGlass ?? null;
                    bUnLoadPropertyGrid.Refresh();
                }

            }
        }

        private void rcsButton_Click(object sender, EventArgs e)
        {
            RCSForm form = new RCSForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                cimStatus.TryRCS = form.RCS;
                cimStatus.ChangeRCS = true;
                switch (form.RCS)
                {
                    case LGHSMSBase.RCS.RCSEnum.ONLineRemote:
                        cim.OnlineRemote();
                        break;

                    case LGHSMSBase.RCS.RCSEnum.OnLineLocal:
                        cim.OnlineLocal();
                        break;

                    case LGHSMSBase.RCS.RCSEnum.OffLine:
                        cim.Offline();
                        break;
                }
            }
        }

        private void opModeButton_Click(object sender, EventArgs e)
        {
            OPModeForm form = new OPModeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                cimStatus.TryOPMode = form.OPMODE;
                switch (form.OPMODE)
                {
                    case LGHSMSBase.OPMODE.OPMODEEnum.Normal:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.Normal);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.SequenceTransfer:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.SequenceTransfer);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.CleanOut:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.CleanOut);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.Test:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.Test);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.Transfer:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.Transfer);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.ParticleCheck:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.ParticleCheck);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.ReverseMode:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.ReverseMode);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.RepairMode:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.RepairMode);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.Manual:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.Manual);
                        break;
                    case LGHSMSBase.OPMODE.OPMODEEnum.Restoration:
                        cim.ReportOPMode(LGHSMSBase.OPMODE.OPMODEEnum.Restoration);
                        break;
                }
            }
        }

        private void eQPStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EQPStatusForm form = new EQPStatusForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ChangeEQPStatusEvent(form.EQPStatus);
            }
        }

        //상태 변경 시 실시간 보고
        private void ChangeEQPStatusEvent(EQPSTATUS.EQPSTATUSEnum status)
        {
            cim.ReportEQPStatus(status);
            //switch (e.ChangeStatus)
            //{
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.RUN:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.RUN);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Experiment:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Experiment);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Development:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Development);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.ModelChange:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.ModelChange);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Monitoring:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.Monitoring);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.IDLE:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.IDLE);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.DOWN:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.DOWN);
            //        break;
            //    case LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.ProcessDown:
            //        cim.ReportEQPStatus(LGHSMSBase.EQPSTATUS.EQPSTATUSEnum.ProcessDown);
            //        break;
            //}
        }

        GLSMVSTATUSForm glsMvForm = new GLSMVSTATUSForm();
        private void gLSMVStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (glsMvForm.ShowDialog() == DialogResult.OK)
            {
                cim.ReportGLSMVStatus(glsMvForm.GLSMVSTATUS);
                Vars.curGLSM = glsMvForm.GLSMVSTATUS;

                //cimStatus.TryGLSMVSTATUS = glsMvForm.GLSMVSTATUS;
                //    GlassMoveEvent(this, new GLSMVArgs() { CurStatus = cimStatus.GLSMVSTATUS, ChangeStatus = cimStatus.TryGLSMVSTATUS });
            }
        }

        private void GlassMoveEvent(object sender, GLSMVArgs e)
        {
            //if (cimStatus.LoadGlassDataReady == false && !glsMvForm.forcedTransferCheckBox.Checked)
            //if (!glsMvForm.forcedTransferCheckBox.Checked)
            //{
            //    log.Warning("GlassMoveEvent : Glass 정보 없음. GlassMoveStatus 변경 불가");
            //    return;
            //}
            switch (e.ChangeStatus)
            {
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LR:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LR);
                    break;
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LW:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LW);
                    break;
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UR:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UR);
                    break;
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UW:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UW);
                    break;
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.ER:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.ER);
                    break;
                case LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.CR:
                    cim.ReportGLSMVStatus(LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.CR);
                    break;
            }
        }

        private void Cim_HostMessagePopup(object sender, CIMArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { Cim_HostMessagePopup(this, e); });

            else
            {
                MessageForm form = new MessageForm();
                form.messageTextBox.Text = e.Message;
                form.Show();
            }
        }

        private void systemSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SystemSetupForm();
            form.systemSetup = Program.SystemSetup;
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.systemSetup.Save(Program.systemSetupDir, log);
            }
        }

        /// <summary>
        /// 현재 글라스 강제 UW, UR 보고
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unloadButton_Click(object sender, EventArgs e)
        {
            //var OrderList = Enum.GetValues(typeof(GlassOrder));
            //cim.SendS6F63(glsOrderComboBox.SelectedIndex.ToString());

            cim.SendS6F63();
            //cimStatus.LoadGlass = null;

            //flag 강제 변경
            cimStatus.CstGlassExist = false;
            cim.NextGlassProcess();

            //glass position 보고
            Thread.Sleep(1000);
            cim.SendS1F67();
        }


        private void LoadButton_Click(object sender, EventArgs e)
        {
            var res = Common.ToConsole(new CIMtoEQPData
            {
                Command = CIMtoEQP.EQStatusDataReq,
            });
            cimStatus.ConsoleData = res;
            UpdateStatusUI();

            cim.SendS6F61();
        }

        private void lotEndButton_Click(object sender, EventArgs e)
        {
            cim.SendS2F65();
        }

        private void eQPSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void Cim_CimAlarmSet(object sender, AlarmArgs e)
        {
            cim.AlarmDBManager.SetAlarm(e.Alarm);
            cim.SendS5F1(cimStatus.CurAlarm, true);
            ShowAlarm();
            UpdateAlarmView();
        }

        private void alarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAlarm();

            AlarmSimulForm form = new AlarmSimulForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                var alarm = form.AlarmInfo;
                cimStatus.AlarmList.Add(alarm);
                cim.AlarmDBManager.SetAlarm(alarm);
                cim.SendS5F1(alarm, true);
                ShowAlarm();
                UpdateAlarmView();
            }
        }

        private void curAlarmButton_Click(object sender, EventArgs e)
        {
            ShowAlarm();
            UpdateAlarmView();
        }

        private void alarmClearButton_Click(object sender, EventArgs e)
        {
            //if(selectAlarm == null)
            //{
            //    string info = "해제할 알람을 선택해주세요.";
            //    MessageBox.Show(info);
            //    log.AddLogMessage(LogType.Information, 0, info);
            //    return;
            //}

            //var alarm = cim.AlarmDBManager.SearchAlarm($"SELECT * FROM AlarmInfo WHERE ID = {selectAlarm.ID}");
            //foreach (var a in alarm)
            //{
            //    a.ALCD = ALCD.ReleaseCode(a.ALCD);
            //    cim.AlarmDBManager.ReleaseAlarm(a);   //DB 업데이트
            //    cim.SendS5F1(a);
            //}

            //UpdateAlarmView();
        }

        private void allAlarmClearButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Control Program에서 Alarm 해제 버튼을 누르세요");
            return;

            /*
            string sql = "SELECT * FROM AlarmInfo " +
                         "WHERE (date(DateTime) = date(now())) AND (IsRelease = false)";
            var alarmItems = cim.AlarmDBManager.SearchAlarm(sql);
            alarmItems = cimStatus.AlarmList;

            //일괄 해제
            foreach (var a in alarmItems)
            {
                a.ALCD = ALCD.ReleaseCode(a.ALCD);
                cim.AlarmDBManager.ReleaseAlarm(a);
                cim.SendS5F1(a, false);
            }
            alarmItems.Clear();
            cimStatus.Save();
            

            UpdateAlarmView();
            */
        }

        private void alarmHistoryButton_Click(object sender, EventArgs e)
        {
            AlarmSearchForm form = new AlarmSearchForm();
            form.cim = cim;
            form.Owner = this;
            form.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            UpdateAlarmView();
        }

        void ShowAlarm()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)ShowAlarm);
                return;
            }

            if (alarmForm == null || alarmForm.IsDisposed == true)
            {
                if (cimStatus.AlarmList == null)
                    return;
                alarmForm = new AlarmForm();
                alarmForm.cim = cim;
                alarmForm.cimStatus = cimStatus;
                alarmForm.Alarms = cimStatus.AlarmList;
                //if (cimStatus.CurAlarm != null)
                //    alarmForm.Alarms.Add(cimStatus.CurAlarm);

                alarmForm.Show();
            }
            else
            {
                alarmForm.cim = cim;
                alarmForm.cimStatus = cimStatus;
                alarmForm.Alarms = cimStatus.AlarmList;
                //if (cimStatus.CurAlarm != null)
                //    alarmForm.Alarms.Add(cimStatus.CurAlarm);
                if (cimStatus.AlarmList == null)
                {
                    alarmForm.Hide();
                }
                else
                {
                    alarmForm.Activate();
                }
            }
            alarmForm.UpdateCurAlarmView();
        }

        //오늘 날짜 알람만 Main UI에 표시
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private void UpdateAlarmView()
        {
            if (InvokeRequired)
                BeginInvoke((Action)delegate { UpdateAlarmView(); });

            else
            {
                alarmDataGridView.Rows.Clear();
                //cimStatus.AlarmItems.Clear();

                string sql = "SELECT * FROM AlarmInfo " +
                             "WHERE date(DateTime) = date(now()) " +
                             "ORDER BY DateTime DESC";

                var alarmItems = cim.AlarmDBManager.SearchAlarm(sql);
                //cimStatus.AlarmItems.AddRange(alarmItems);

                foreach (var alarm in alarmItems)
                {
                    object[] row = { alarm.ID, alarm.ALID, alarm.ALCD, alarm.AlarmType, alarm.ALTX, alarm.DateTime, alarm.IsRelease };
                    alarmDataGridView.Rows.Add(row);
                }
            }
        }

        private void alarmDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                selectAlarm = new AlarmInfo();
                var ID = alarmDataGridView.Rows[e.RowIndex].Cells[0].Value;

                if (ID != null)
                {
                    selectAlarm.ID = (int)ID;
                }
            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, ee.ToString());
            }
        }

        private void scrapReportButton_Click(object sender, EventArgs e)
        {
            //panel, glass scrap
            //if (glassScrap.Checked)
            //{
            //    cimStatus.SCRTYPE = "G";
            //}
            //else
            //{
            //    cimStatus.SCRTYPE = "P";
            //}
            //A,B glass

            //if (Aglass.Checked)
            //{
            //    cimStatus.SCRGLASS = "A";
            //}
            //else
            //{
            //    cimStatus.SCRGLASS = "B";
            //}

            cim.SendS6F93(glassCodeTextBox.Text);

            if (Aglass.Checked)
            {
                //cimStatus.SCRGLASS = "A";
                cimStatus.UnloadGlass.AGlass.ClearInfo();
                var bglass = cimStatus.UnloadGlass.BGlass;
                bglass.SetGSD("PAIRINFO", "N");
                bglass.SetGSD("PAIR_GLASS_CODE", "");

            }
            else
            {
                //cimStatus.SCRGLASS = "B";

                cimStatus.UnloadGlass.BGlass.ClearInfo();
                var aglass = cimStatus.UnloadGlass.AGlass;
                aglass.SetGSD("PAIRINFO", "N");
                aglass.SetGSD("PAIR_GLASS_CODE", "");
            }

            //if (MessageBox.Show($"{cimStatus.SCRTYPE}, {cimStatus.SCRGLASS} Glass Scrap Report?",
            //    "Are You Sure About Scrap Report?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //    cim.SendS6F93();
            //else
            //    return;
            // scrap 보고 후 장비는 멈춤? idle?
        }

        private void messageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageForm form = new MessageForm();
            form.Owner = this;
            form.Show();
        }

        private void cassetteMAPRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CassetteMAPForm form = new CassetteMAPForm();
            form.cim = cim;
            form.Show();
        }

        private void recipeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //마지막 상태 저장
            cimStatus.Save();
        }

        private bool ReqEqData(bool WriteLog = true)
        {
            try
            {
                var eqData = cimStatus.GetStatusFromConsole(WriteLog);
                //cimStatus.ConsoleData.eqpStatus = EQPStatus.DOWN;
                if (eqData.Error == true)
                {
                    // 연결 안됨 - UI 업데이트 후 재시도
                    UpdateStatusUI();
                    return false;
                }


                UpdateStatusUI();

                if (cim.gem.ConnectMode != MMISoft.HSMS.HSMSSocket.Mode.SELECTED)
                    return true;

                var status = Vars.CIMEQPStatsu(eqData.eqpStatus);
                if (cimStatus.IsAutoRun == false)
                {
                    status = EQPSTATUS.EQPSTATUSEnum.DOWN;

                    //CR 보내기?
                    if(Vars.curGLSM != GLSMVSTATUS.GLSMVSTATUSEnum.CR)
                    {
                        cim.SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.CR);
                        Vars.curGLSM = GLSMVSTATUS.GLSMVSTATUSEnum.CR;
                    }
                }

                //장비 상태 보고
                ChangeEQPStatusEvent(status);

                //lastvalue에 상태 저장
                return true;
            }
            catch (Exception exp)
            {
                //console 통신 오류 시 autorun 상태가 아님
                UpdateStatusUI();
                log.Error("[ReqEqData]" + exp.ToString());
                return false;
            }
            finally
            {
                cimStatus.Save();
            }
        }

        private void ionizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReqEqData();
            cim.SendS1F75();
        }



        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            var eqData = cimStatus.GetStatusFromConsole();

            cim.SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.CR);
            Vars.curGLSM = GLSMVSTATUS.GLSMVSTATUSEnum.CR;
            //if (eqData.GlassIn)
            //{
            //    cim.SendS6F63();
            //}
            //cim.SendS6F61();
        }

        private void Cim_RecipeChangePopUp(object sender, RecipeArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { Cim_RecipeChangePopUp(sender, e); });
                return;
            }

            RecipeChangePopUp form = new RecipeChangePopUp();
            if (e.ACK == false)
            {
                form.UpdateUI(e.RecipeSet, $"Recipe {e.RecipeCMD} was denied From {e.ByWho}.");
            }
            else
            {
                form.UpdateUI(e.RecipeSet, $"Recipe {e.RecipeCMD} (From {e.ByWho})");
            }

            form.Show();
        }

        //private void reworkButton_Click(object sender, EventArgs e)
        //{
        //    int slotCount = int.Parse(slotCountTextBox.Text);
        //    cim.SendS6F89(slotCount);
        //}

        //private void cycleStopSetButton_Click(object sender, EventArgs e)
        //{
        //    cimStatus.CycleStop = true;
        //    cim.SendS2F67(CSSSTATUS.CSSSTATUSEnum.Set);
        //    cycleStopSetButton.BackColor = Color.Lime;
        //}

        //private void cycleStopReleaseButton_Click(object sender, EventArgs e)
        //{
        //    cimStatus.CycleStop = false;
        //    cim.SendS2F67(CSSSTATUS.CSSSTATUSEnum.Release);
        //    cim.SendS6F61();
        //    cycleStopSetButton.BackColor = Color.Transparent;

        //}

        private void glassData초기화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cimStatus.LoadGlass = null;
            cimStatus.UnloadGlass = null;
            UpdateJobDataUI();
            log.Information("Glass Data 초기화 완료.");
        }

        private void glassCallReqButton_Click(object sender, EventArgs e)
        {
            cim.SendS2F77();
        }

        private void dummyLotReqButton_Click(object sender, EventArgs e)
        {
            cim.SendS6F85();
        }

        private void glassDataReqButton_Click(object sender, EventArgs e)
        {
            REQOPTION.REQOPTIONEnum rEQOPTION = REQOPTION.REQOPTIONEnum.ALL;
            bool glsIDReq = GlassIDCheckBox.Checked;
            bool glsCDReq = GlassCodeCheckBox.Checked;

            if (!glsIDReq && !glsCDReq)
            {
                MessageBox.Show("검색 옵션이 설정되지 않았습니다. glassId 또는 glassCode 체크 설정 필요.");
                return;
            }

            if (glsIDReq && !glsCDReq)
            {
                rEQOPTION = REQOPTION.REQOPTIONEnum.GLSID;
            }
            else if (!glsIDReq && glsCDReq)
            {
                rEQOPTION = REQOPTION.REQOPTIONEnum.GLSCODE;
            }
            else if (glsIDReq && glsCDReq)
            {
                rEQOPTION = REQOPTION.REQOPTIONEnum.ALL;
            }

            string GlassID = glassIDTextBox.Text;
            ushort GlassCode = ushort.Parse(glassCodeTextBox.Text);

            cim.SendS2F79(rEQOPTION, GlassID, GlassCode);
        }

        private void lastTestButton_Click(object sender, EventArgs e)
        {
            var list = new List<AlarmInfo>();
            var alarm = list.LastOrDefault(l => l.AlarmType == AlarmType.HeavyAlarm);
        }

        private void refreshCosoleStatusButton_Click(object sender, EventArgs e)
        {
            ReqEqData();
            //cim.SendS6F73();
            cim.SendS1F67();
        }

        private void Aglass_CheckedChanged(object sender, EventArgs e)
        {
            cimStatus.IsAGlass = true;
        }

        private void Bglass_CheckedChanged(object sender, EventArgs e)
        {
            cimStatus.IsAGlass = false;

        }

        private void alarmStatusLabel_Click(object sender, EventArgs e)
        {
            ShowAlarm();
        }

        private void StatusTimerFunc(int delay = 2000)
        {
            /*
            startTimer = true;
            bool wasConnected = false;
            while (startTimer)
            {
                Thread.Sleep(delay);
                try {
                    bool connected = ReqEqData(false);
                    if (connected && !wasConnected)
                    {
                        log.Information("[Console] 연결 성공");
                    }
                    else if (!connected && wasConnected)
                    {
                        log.Warning("[Console] 연결 끊어짐");
                    }
                    else if (!connected && !wasConnected)
                    {
                        log.Information("[재시도] Console 연결 시도 중...");
                    }
                    wasConnected = connected;
                } catch {
                    if (wasConnected)
                    {
                        log.Warning("[Console] 연결 끊어짐 (예외)");
                    }
                    wasConnected = false;
                }
            }
            */
        }

        private void glassdataresetButton_Click(object sender, EventArgs e)
        {
            cimStatus.LoadGlass = null;
            cimStatus.UnloadGlass = null;
            UpdateJobDataUI();
            log.Information("Glass Data 초기화 완료.");
        }

        private void sW변경이력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VersionUpdateForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Vars.VerManager.Save();
            }
        }

    }//
}
