// using System.Windows.Forms.DataVisualization.Charting;
using Elp;
using Elp.Net;
using LGHSMSBase;
using LGHSMSBase.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class MainForm : MaterialForm
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

            // 런타임에만 커스텀 드로잉 적용 (디자이너 가시성 확보)
            // DrawMode는 DarkTabControl 내부에서 자동 설정됨

            // MaterialSkinManager 초기화
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // Dark Blue 톤의 헤더 배경색
            Color darkBlueHeader = Color.FromArgb(30, 44, 58);
            menuStrip1.BackColor = darkBlueHeader;
            menuStrip1.ForeColor = Color.Gainsboro;
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.ForeColor = Color.Gainsboro;
                if (item is ToolStripMenuItem menuItem)
                {
                    foreach (ToolStripItem subItem in menuItem.DropDownItems)
                    {
                        subItem.ForeColor = Color.Gainsboro;
                        subItem.BackColor = darkBlueHeader;
                    }
                }
            }

            // 상태 라벨들에 커스텀 테두리 Paint 이벤트 연결
            autoRunLabel.Paint += StatusLabel_Paint;
            glassInlabel.Paint += StatusLabel_Paint;
            glsmvLabel.Paint += StatusLabel_Paint;
            EQStatusLabel.Paint += StatusLabel_Paint;
            consoleStatusLabel.Paint += StatusLabel_Paint;

            // 버튼에만 마우스 호버 효과 추가 (라벨은 색상/커서 변경 제외)
            var buttonHoverTargets = new Control[] { rcsButton, LoadButton, glassDataReqButton, unloadButton, scrapReportButton, opModeButton, alarmStatusButton };
            foreach (var ctrl in buttonHoverTargets)
            {
                ctrl.MouseEnter += StatusControl_MouseEnter;
                ctrl.MouseLeave += StatusControl_MouseLeave;

                // 시나리오 관련 버튼인 경우 필수 조건 안내 이벤트 추가
                if (ctrl == LoadButton || ctrl == glassDataReqButton || ctrl == unloadButton)
                {
                    ctrl.MouseEnter += ScenarioButton_MouseEnter;
                    ctrl.MouseLeave += ScenarioButton_MouseLeave;
                }
            }

            // 탭 컨트롤 DrawItem 이벤트는 Designer.cs에서 등록됨 (중복 방지)

            // SplitContainer 구분선 색상을 어둡게 하기 위해 BackColor 조정
            // (내부 패널들은 MaterialCard가 덮고 있어 구분선만 어둡게 보임)
            Color dividerColor = Color.FromArgb(25, 38, 52);
            splitContainer1.BackColor = dividerColor;
            splitContainer2.BackColor = dividerColor;
            splitContainer3.BackColor = dividerColor;
            splitContainer4.BackColor = dividerColor;
            splitContainer5.BackColor = dividerColor;

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // tabPage1(Status) 일 경우에만 세부 탭 선택기(Load/Unload)를 보여줌
            //materialTabSelector2.Visible = (tabControl1.SelectedTab == tabPage1);
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

            // PropertyGrid 다크 스타일 적용
            ApplyPropertyGridDarkStyle(aLoadPropertyGrid);
            ApplyPropertyGridDarkStyle(bLoadPropertyGrid);
            ApplyPropertyGridDarkStyle(aUnLoadPropertyGrid);
            ApplyPropertyGridDarkStyle(bUnLoadPropertyGrid);

            // TabPage 및 TabControl 배경색 강제 적용 (흰색 여백 제거)
            Color themeBackColor = Color.FromArgb(25, 38, 52);
            tabPage1.BackColor = themeBackColor;
            tabPage2.BackColor = themeBackColor;
            tabPage3.BackColor = themeBackColor;
            tabPage4.BackColor = themeBackColor;
            tabPage5.BackColor = themeBackColor;
            tabControl1.BackColor = themeBackColor;
            tabControl2.BackColor = themeBackColor;

            // 폼 전체의 모든 TextBox를 다크 모드로 일괄 적용
            ApplyDarkModeToAllTextBoxes(this, themeBackColor);

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

            // cim.CimAlarmSet += (_, __) => ShowAlarm();
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
                        // v50 시퀀스: InspectDone → S6F73 (Glass APD Report) → S6F74
                        cim.SendS6F73();
                        // 검사 완료 후 상태를 RUN으로 보고
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
                        // ShowAlarm();
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
                        // ShowAlarm();
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
                    cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Built);
                    break;

                case EQPtoCIM.RecipeChange:
                    cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Change);
                    break;

                case EQPtoCIM.RecipeDelete:
                    cim.SendS15F79(data.RecipeItem, RECIPEIDCMD.RECIPEIDCMDEnum.Delete);
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
                    // Console 요청에 의한 Cancel Request → S6F67(CR) 보고
                    cim.ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum.CR);
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
            UpdateStatusUI();
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
                // AutoRun 상태 업데이트
                autoRunLabel.BackColor = Color.Transparent;
                autoRunLabel.ForeColor = cimStatus.IsAutoRun ? Color.Lime : Color.OrangeRed; // Tomato보다 조금 더 선명한 OrangeRed 사용
                autoRunLabel.Text = cimStatus.IsAutoRun ? "AutoRun ON" : "AutoRun OFF";

                // GlassIn 상태 업데이트 (데이터 연동 정합성 체크 추가)
                glassInlabel.BackColor = Color.Transparent;
                if (cimStatus.EQGlassIn == true)
                {
                    // 실제 유리 데이터(ID)가 있는지 체크
                    bool hasData = !string.IsNullOrEmpty(cimStatus.LoadGlass?.AGlass?.GLSID) || 
                                   !string.IsNullOrEmpty(cimStatus.UnloadGlass?.AGlass?.GLSID);

                    if (hasData)
                    {
                        glassInlabel.ForeColor = Color.Lime;
                        glassInlabel.Text = "Glass IN";
                    }
                    else
                    {
                        // 센서는 IN인데 데이터는 없는 경우 (Warning 상태)
                        glassInlabel.ForeColor = Color.Yellow;
                        glassInlabel.Text = "Glass IN (No Data)";
                    }
                }
                else
                {
                    glassInlabel.ForeColor = Color.OrangeRed;
                    glassInlabel.Text = "Glass OUT";
                }

                // GLSMV 상태 텍스트 업데이트
                glsmvLabel.BackColor = Color.Transparent;
                glsmvLabel.ForeColor = Color.Gainsboro;
                glsmvLabel.Text = cimStatus.CimCommData?.ToCIM?.EqpReadyStatus.ToString() ?? "None";

                // EQP 상태별 색상 및 텍스트 업데이트
                EQStatusLabel.BackColor = Color.Transparent;
                switch (cimStatus.EQPStatus)
                {
                    case EQPSTATUS.EQPSTATUSEnum.RUN:
                        EQStatusLabel.ForeColor = Color.Lime;
                        EQStatusLabel.Text = "RUN";
                        break;

                    case EQPSTATUS.EQPSTATUSEnum.IDLE:
                        EQStatusLabel.ForeColor = Color.Yellow;
                        EQStatusLabel.Text = "IDLE";
                        break;

                    case EQPSTATUS.EQPSTATUSEnum.DOWN:
                        EQStatusLabel.ForeColor = Color.Red;
                        EQStatusLabel.Text = "DOWN";
                        break;

                    default:
                        EQStatusLabel.ForeColor = Color.Gold;
                        EQStatusLabel.Text = cimStatus.EQPStatus.ToString();
                        break;
                }

                // Console 연결 상태 업데이트 (Label - 색상으로 상태 표시)
                if (cimStatus.ConsoleData.Error == true)
                {
                    consoleStatusLabel.Text = "CONSOLE";
                    consoleStatusLabel.ForeColor = Color.Red;
                }
                else
                {
                    consoleStatusLabel.Text = "CONSOLE";
                    consoleStatusLabel.ForeColor = Color.Lime; // Green에서 Lime으로 변경하여 가독성 향상
                }

                // Alarm 발생 여부 업데이트
                if (cimStatus.CurAlarm != null || (cimStatus.AlarmList != null && cimStatus.AlarmList.Count > 0))
                {
                    alarmStatusButton.Text = "ALARM!";
                    alarmStatusButton.ForeColor = Color.Red;
                }
                else
                {
                    alarmStatusButton.Text = "Alarm";
                    alarmStatusButton.ForeColor = Color.White;
                }

                // 시나리오 필수 조건 UI 업데이트 (현재 호버 중인 시나리오 기준 또는 기본)
                UpdateScenarioPrereqUI();
            }
        }

        private string currentHoverScenario = null;
        private void UpdateScenarioPrereqUI()
        {
            if (scenarioPrereqLabel == null) return;

            // 공통: RUN 상태가 아니면 무조건 경고
            if (cimStatus.EQPStatus != EQPSTATUS.EQPSTATUSEnum.RUN)
            {
                scenarioPrereqLabel.ForeColor = Color.IndianRed;
                scenarioPrereqLabel.Text = "⚠️ [필수] 설비 상태를 시뮬레이터에서 'RUN'으로 변경하세요.";
                return;
            }

            // 시나리오별 상세 체크
            if (currentHoverScenario == "A") // Loading
            {
                bool isReady = (cimStatus.CimCommData?.ToCIM?.EqpReadyStatus == EQPReadyStatus.LoadyReady);
                bool noGlass = (cimStatus.EQGlassIn == false);

                if (isReady && noGlass)
                {
                    scenarioPrereqLabel.ForeColor = Color.Lime;
                    scenarioPrereqLabel.Text = "✅ [Scenario A] Loading 시퀀스 시작 가능 (LoadReady + No Glass)";
                }
                else
                {
                    scenarioPrereqLabel.ForeColor = Color.Yellow;
                    scenarioPrereqLabel.Text = $"❌ [Scenario A] 필요: LoadReady({(isReady ? "OK" : "NO")}), No Glass({(noGlass ? "OK" : "NO")})";
                }
            }
            else if (currentHoverScenario == "B") // Exchange (Data Request 지점)
            {
                bool isReady = (cimStatus.CimCommData?.ToCIM?.EqpReadyStatus == EQPReadyStatus.ExchangeReady);
                bool hasGlass = (cimStatus.EQGlassIn == true);

                if (isReady && hasGlass)
                {
                    scenarioPrereqLabel.ForeColor = Color.Lime;
                    scenarioPrereqLabel.Text = "✅ [Scenario B] Exchange 시퀀스 시작 가능 (ExchangeReady + Glass In)";
                }
                else
                {
                    scenarioPrereqLabel.ForeColor = Color.Yellow;
                    scenarioPrereqLabel.Text = $"❌ [Scenario B] 필요: ExchangeReady({(isReady ? "OK" : "NO")}), Glass In({(hasGlass ? "OK" : "NO")})";
                }
            }
            else if (currentHoverScenario == "C") // Unloading
            {
                bool isReady = (cimStatus.CimCommData?.ToCIM?.EqpReadyStatus == EQPReadyStatus.UnloadReady);
                bool hasGlass = (cimStatus.EQGlassIn == true);

                if (isReady && hasGlass)
                {
                    scenarioPrereqLabel.ForeColor = Color.Lime;
                    scenarioPrereqLabel.Text = "✅ [Scenario C] Unloading 시퀀스 시작 가능 (UnloadReady + Glass In)";
                }
                else
                {
                    scenarioPrereqLabel.ForeColor = Color.Yellow;
                    scenarioPrereqLabel.Text = $"❌ [Scenario C] 필요: UnloadReady({(isReady ? "OK" : "NO")}), Glass In({(hasGlass ? "OK" : "NO")})";
                }
            }
            else
            {
                scenarioPrereqLabel.ForeColor = Color.Gainsboro;
                scenarioPrereqLabel.Text = "💡 버튼 근처로 마우스를 가져가면 시나리오별 필수 조건을 확인할 수 있습니다.";
            }
        }

        private void ScenarioButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender == LoadButton) currentHoverScenario = "A";
            else if (sender == glassDataReqButton) currentHoverScenario = "B";
            else if (sender == unloadButton) currentHoverScenario = "C";

            UpdateScenarioPrereqUI();
        }

        private void ScenarioButton_MouseLeave(object sender, EventArgs e)
        {
            currentHoverScenario = null;
            UpdateScenarioPrereqUI();
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
            // ShowAlarm();
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

        #region Status Labels Custom Drawing
        private void StatusLabel_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Control ctrl)
            {
                // 테마에 맞춘 어두운 하늘색 테두리
                using (Pen pen = new Pen(Color.FromArgb(80, 120, 140), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, ctrl.Width - 1, ctrl.Height - 1);
                }
            }
        }
        #endregion

        #region TabControl Custom Drawing (Material Style)
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (sender is not TabControl tabControl) return;

            var tabPage = tabControl.TabPages[e.Index];
            var tabRect = tabControl.GetTabRect(e.Index);

            // 배경색 결정
            Color backColor = MaterialSkinManager.Instance.BackgroundColor;
            Color textColor = Color.FromArgb(200, Color.White); // 기본 텍스트 색상 (약간 투명)
            Color highlightColor = MaterialSkinManager.Instance.ColorScheme.PrimaryColor;

            // 선택된 상태 확인
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            using (Brush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, tabRect);
            }

            if (isSelected)
            {
                textColor = Color.White; // 선택된 탭은 밝은 흰색
                // 선택된 탭 하단에 굵은 강조선 표시 (Material Style)
                using (Pen highlightPen = new Pen(highlightColor, 3))
                {
                    e.Graphics.DrawLine(highlightPen, tabRect.Left + 5, tabRect.Bottom - 2, tabRect.Right - 5, tabRect.Bottom - 2);
                }
            }

            // 텍스트 그리기
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabControl.Font, tabRect, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }
        #endregion

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
                    if (Vars.curGLSM != GLSMVSTATUS.GLSMVSTATUSEnum.CR)
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

        private void ApplyPropertyGridDarkStyle(PropertyGrid grid)
        {
            if (grid == null) return;
            grid.BackColor = Color.FromArgb(43, 43, 43);
            grid.ViewBackColor = Color.FromArgb(43, 43, 43);
            grid.ViewForeColor = Color.Gainsboro;
            grid.ViewBackColor = Color.FromArgb(25, 38, 52);
            grid.LineColor = Color.FromArgb(50, 50, 50);
            grid.CategoryForeColor = Color.LightSteelBlue;
            grid.HelpBackColor = Color.FromArgb(25, 38, 52);
            grid.HelpForeColor = Color.Gainsboro;
            grid.CommandsBackColor = Color.FromArgb(25, 38, 52);
            grid.CommandsForeColor = Color.Gainsboro;
        }

        /// <summary>
        /// 폼 내 모든 TextBox, Panel, SplitContainer 등에 다크 모드 스타일을 재귀 적용합니다.
        /// </summary>
        private void ApplyDarkModeToAllTextBoxes(Control parent, Color backColor)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(43, 43, 43);
                    textBox.ForeColor = Color.Gainsboro;
                    textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                else if (ctrl is Panel panel)
                {
                    panel.BackColor = backColor;
                }
                else if (ctrl is SplitContainer splitContainer)
                {
                    splitContainer.BackColor = backColor;
                    splitContainer.Panel1.BackColor = backColor;
                    splitContainer.Panel2.BackColor = backColor;
                }
                else if (ctrl is GroupBox groupBox)
                {
                    groupBox.BackColor = backColor;
                    groupBox.ForeColor = Color.Gainsboro;
                }

                // 자식 컨트롤에도 재귀 적용
                if (ctrl.HasChildren)
                {
                    ApplyDarkModeToAllTextBoxes(ctrl, backColor);
                }
            }
        }

        private void refreshCosoleStatusButton_Click_1(object sender, EventArgs e)
        {
            ReqEqData();
            //cim.SendS6F73();
            cim.SendS1F67();
        }

        /// <summary>
        /// 마우스 진입 시 버튼 색상을 액센트 색상으로 변경합니다.
        /// </summary>
        private void StatusControl_MouseEnter(object sender, EventArgs e)
        {
            if (sender is MaterialButton matBtn)
            {
                matBtn.UseAccentColor = true;
                matBtn.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 마우스 이탈 시 버튼 색상을 원래대로 복원합니다.
        /// </summary>
        private void StatusControl_MouseLeave(object sender, EventArgs e)
        {
            if (sender is MaterialButton matBtn)
            {
                matBtn.UseAccentColor = false;
                matBtn.Cursor = Cursors.Default;
            }
        }
    }//
}

