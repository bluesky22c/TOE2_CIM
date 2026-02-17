using Elp.Net;
using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TOECIMLib;
using ServiceStack;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace LGCIMServer.EQPSimul
{
    public enum ScenarioStep
    {
        Idle,
        UnloadStarted,      // S6F63 대기
        UnloadReported,     // S6F63 완료
        LoadReadyRequested, // S6F61 로딩 요청 중
        LoadDataReceived,   // S2F87 수신 완료
        ProcessStarted      // S6F65 등 공정 시작
    }

    [Serializable]
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class EQPSimulatorData
    {
        public ScenarioStep CurrentStep { get; set; } = ScenarioStep.Idle;
        public int CIMServerPort { get; set; } = 3500;
        public int EQServerPort { get; set; } = 3501;
        public bool IsListen { get; set; } = false;
        public TOECIMLib.ECSData LoadGlass { get; set; }
        public TOECIMLib.ECSData UnloadGlass { get; set; }
        public EQPtoCIMData eqData { get; set; } = new EQPtoCIMData();
        public TOECIMLib.EQPStatus preEQPStatus { get; set; } = TOECIMLib.EQPStatus.IDLE;
    }

    public partial class EQPSimulator : Form
    {
        public event Action<string> OnLog;
        public event Action OnDataUpdate;
        public string LastValuePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "eqSimul.xml");
        
        public EQPSimulatorData Data { get; set; } = new EQPSimulatorData();

        public int CIMServerPort => Data.CIMServerPort;

        public int EQServerPort => Data.EQServerPort;
        public TOECIMLib.ECSData LoadGlass { get => Data.LoadGlass; set => Data.LoadGlass = value; }
        public TOECIMLib.ECSData UnloadGlass { get => Data.UnloadGlass; set => Data.UnloadGlass = value; }

        public EQPtoCIMData eqData => Data.eqData;

        JsonServer<CimCommData> server;

        public TOECIMLib.EQPStatus preEQPStatus { get => Data.preEQPStatus; set => Data.preEQPStatus = value; }

        public EQPSimulator()
        {
            
        }

        private void alarmSetButton_Click(object sender, EventArgs e) { }
        private void alarmClearButton_Click(object sender, EventArgs e) { }
        private void EQPSimulator_FormClosing(object sender, FormClosingEventArgs e) { }
        private void EQPSimulator_Load(object sender, EventArgs e) { }

        #region Save Last Value (simul)
        public bool Save(string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(EQPSimulatorData));
                    xs.Serialize(sw, this.Data);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        static public EQPSimulator LoadFromFile(string fileName)
        {
            EQPSimulator simulator = new EQPSimulator();

            try
            {
                bool recreate = false;
                if (!File.Exists(fileName) || new FileInfo(fileName).Length == 0)
                {
                    recreate = true;
                }
                else
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(EQPSimulatorData));
                            simulator.Data = (EQPSimulatorData)xs.Deserialize(sr);
                        }
                    }
                    catch (Exception)
                    {
                        recreate = true;
                    }
                }

                if (recreate)
                {
                    simulator.Save(fileName);
                }
                
                simulator.CheckIonizerData();
            }
            catch (Exception ee)
            {
                Console.WriteLine($"Failed to load EQPSimulator data: {ee.Message}");
            }
            return simulator;
        }

        private void CheckIonizerData()
        {
            if (Data.eqData.Ionizer == null)
            {
                Data.eqData.Ionizer = new IonizerInfo[3];
            }
            
            for (int i = 0; i < Data.eqData.Ionizer.Length; i++)
            {
                if (Data.eqData.Ionizer[i] == null)
                {
                    Data.eqData.Ionizer[i] = new IonizerInfo();
                }
            }
        }
        #endregion

        public void StartServer()
        {
            try {
                if (server != null)
                {
                    server.Stop();
                }
                server = new JsonServer<CimCommData>(this, OnReceiveData, null);
                server.Listen(EQServerPort);
            } catch (Exception ex) {
                MessageBox.Show($"Failed to start simulator server: {ex.Message}");
            }
        }

        public void StopServer()
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        private CimCommData OnReceiveData(object owner, CimCommData spec)
        {
            if (spec.ToConsole.Command == CIMtoEQP.GlassDataSend || spec.ToConsole.Command == CIMtoEQP.GlassDataReq)
            {
                bool isUnload = spec.ToConsole.Command == CIMtoEQP.GlassDataReq;
                if (isUnload)
                {
                    if (Data.UnloadGlass == null) Data.UnloadGlass = new ECSData();
                }
                else
                {
                    if (Data.LoadGlass == null) Data.LoadGlass = new ECSData();
                }

                ECSData target = isUnload ? Data.UnloadGlass : Data.LoadGlass;

                // AGlass Mapping
                if (spec.ToConsole.GlassInfo[0] != null)
                {
                    var g = target.AGlass;
                    var src = spec.ToConsole.GlassInfo[0];
                    g.LOTID = src.LotID;
                    g.CSTID = src.CassetteID;
                    g.SLOTID = src.SlotNo;
                    g.RECIPEID = src.PPID;
                    g.PRCID = src.ProcessID;
                    g.GLSODR = src.GlassODR;
                    g.GLSID = src.GlassID;
                    g.GLSJUDGE = src.JudgeCode;
                    g.PNLIF = src.PanelInfo;
                    g.PORTID = src.PortID;
                    g.GLSCODE = src.GlassCode;
                    g.PRODTYPE = src.ProdType;
                }
                // BGlass Mapping
                if (spec.ToConsole.GlassInfo[1] != null)
                {
                    var g = target.BGlass;
                    var src = spec.ToConsole.GlassInfo[1];
                    g.LOTID = src.LotID;
                    g.CSTID = src.CassetteID;
                    g.SLOTID = src.SlotNo;
                    g.RECIPEID = src.PPID;
                    g.PRCID = src.ProcessID;
                    g.GLSODR = src.GlassODR;
                    g.GLSID = src.GlassID;
                    g.GLSJUDGE = src.JudgeCode;
                    g.PNLIF = src.PanelInfo;
                    g.PORTID = src.PortID;
                    g.GLSCODE = src.GlassCode;
                    g.PRODTYPE = src.ProdType;
                }
                OnDataUpdate?.Invoke();
                
                // 수신 시 시퀀스 상태 업데이트 (S2F87 Load Data Send 대응)
                if (spec.ToConsole.Command == CIMtoEQP.GlassDataSend)
                {
                    UpdateScenarioStep(ScenarioStep.LoadDataReceived);
                }
            }

            spec.ToCIM.eqpStatus = eqData.eqpStatus;
            spec.ToCIM.GlassIn = eqData.GlassIn;
            spec.ToCIM.EqpReadyStatus = eqData.EqpReadyStatus;
            
            spec.ToCIM.AutoRun = eqData.AutoRun && eqData.eqpStatus == EQPStatus.RUN;
            
            if (eqData.Ionizer != null)
            {
                spec.ToCIM.Ionizer = eqData.Ionizer;
            }

            if (eqData.AlarmList.Count > 0)
            {
                spec.ToCIM.AlarmList.AddRange(eqData.AlarmList);
            }

            spec.ToCIM.Error = false; 

            Save(LastValuePath);

            OnLog?.Invoke($"[RECV] CMD:{spec.ToConsole.Command}, DATA:{JsonConvert.SerializeObject(spec.ToConsole)}");
            OnLog?.Invoke($"[SEND] RESP, DATA:{JsonConvert.SerializeObject(spec.ToCIM)}");

            return spec;
        }

        internal void SendToCIM(EQPtoCIMData eqData)
        {
            try
            {
                var client = new JsonClient<CimCommData>(this, "127.0.0.1", CIMServerPort, null);

                EQPtoCIMData toCim = new EQPtoCIMData();
                toCim.Command = eqData.Command;
                toCim.eqpStatus = eqData.eqpStatus;
                
                toCim.GlassIn = eqData.GlassIn;
                toCim.AutoRun = eqData.AutoRun && eqData.eqpStatus == EQPStatus.RUN;
                toCim.EqpReadyStatus = eqData.EqpReadyStatus;

                if (eqData.Ionizer != null)
                {
                    toCim.Ionizer = eqData.Ionizer;
                }

                if (eqData.Command == EQPtoCIM.Alarm && eqData.AlarmList.Count > 0)
                {
                    toCim.AlarmList.AddRange(eqData.AlarmList);
                }

                CimCommData commData = new CimCommData();
                commData.ToCIM = toCim;
                commData.WaitResponse = false;

                client.Send(commData);
                OnLog?.Invoke($"[SEND] CMD:{toCim.Command}, DATA:{JsonConvert.SerializeObject(toCim)}");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"SendToCIM Failed: {exp.Message}");
            }
        }

        public void SendData()
        {
            eqData.Command = EQPtoCIM.SendData;

            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var item = new IonizerInfo();
                item.ID = (byte)(i + 1);
                item.Status = eqData.Ionizer[i].Status;
                eqData.Ionizer[i] = item;
            }

            SendToCIM(eqData);
        }

        public void SetAlarm(AlarmType type, int id)
        {
            var alarm = new AlarmInfo();
            alarm.ALID = id.ToString();
            alarm.ALCD = ALCD.SetCode(ALCD.ALCDEnum.EQPStatusWarning);
            alarm.ALTX = "EQPStatusWarning";
            alarm.AlarmType = type;
            alarm.DateTime = DateTime.Now;

            eqData.Command = EQPtoCIM.Alarm;
            eqData.AlarmList.Clear();
            eqData.AlarmList.Add(alarm);
            if (alarm.AlarmType == AlarmType.HeavyAlarm)
            {
                preEQPStatus = eqData.eqpStatus;
                eqData.eqpStatus = EQPStatus.DOWN;
            }

            SendToCIM(eqData);
        }

        public void ClearAlarm()
        {
            var alarm = eqData.AlarmList.FirstOrDefault();
            if (alarm == null)
                return;

            eqData.Command = EQPtoCIM.AlarmClear;
            alarm.ALCD = ALCD.ReleaseCode((byte)ALCD.ALCDEnum.EQPStatusWarning);

            if (alarm.AlarmType == AlarmType.HeavyAlarm)
            {
                eqData.eqpStatus = preEQPStatus;
            }

            SendToCIM(eqData);
        }

        public bool GetIsGlass()
        {
            return eqData.GlassIn;
        }

        public void LoadReady()
        {
            eqData.GlassIn = false;
            eqData.EqpReadyStatus = EQPReadyStatus.LoadyReady;
            eqData.Command = EQPtoCIM.LoadReadyDone;
            SendToCIM(eqData);
            UpdateScenarioStep(ScenarioStep.LoadReadyRequested);
        }

        public void LoadDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.LoadDone;
            eqData.eqpStatus = EQPStatus.RUN;
            eqData.GlassIn = true;
            SendToCIM(eqData);
            UpdateScenarioStep(ScenarioStep.ProcessStarted);
        }

        public void InspectDone()
        {
            // 로그 분석 결과: InspectDone은 S6F91(Status Change: RUN)을 보고함
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.InspectDone;
            eqData.eqpStatus = EQPStatus.RUN;

            // 판정 데이터 보완 (ToECIM의 S6F63 전송 시 필수 데이터)
            eqData.GlassPanelJudgeA = "G"; // OK
            eqData.GlassPanelJudgeB = "G"; // OK

            SendToCIM(eqData);
        }

        public void UnloadReady()
        {
            // 로그 분석 결과: UnloadReadyDone 시 설비 상태가 Idle/Wait(S6F11)로 변경되는 경우가 많음
            eqData.EqpReadyStatus = EQPReadyStatus.UnloadReady;
            eqData.Command = EQPtoCIM.UnloadReadyDone;
            // Unload/Exchange 중에도 AutoRun 상태(RUN)를 유지해야 CIM이 S2F87을 수락함
            eqData.eqpStatus = EQPStatus.RUN; 
            eqData.GlassIn = true;

            SendToCIM(eqData);
            UpdateScenarioStep(ScenarioStep.UnloadReported);
        }

        public void ExchangeReadyDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.ExchangeReady;
            eqData.Command = EQPtoCIM.ExchangeReadyDone;
            // 명시적으로 RUN 상태 유지
            eqData.eqpStatus = EQPStatus.RUN;
            eqData.GlassIn = false;

            // 판정 데이터 보완 (ToECIM의 S6F63 전송 시 필수 데이터)
            eqData.GlassPanelJudgeA = "G";
            eqData.GlassPanelJudgeB = "G";

            SendToCIM(eqData);
            UpdateScenarioStep(ScenarioStep.UnloadReported);
        }

        public void ExchangeDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.ExchangeDone;
            eqData.GlassIn = true;

            SendToCIM(eqData);
        }

        public void UnloadDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.UnloadDone;
            eqData.GlassIn = false;
            eqData.eqpStatus = EQPStatus.IDLE;

            SendToCIM(eqData);
            UpdateScenarioStep(ScenarioStep.Idle); // 완료 후 초기화
        }

        public void ChangeStatus(EQPStatus status)
        {
            eqData.eqpStatus = status;
            eqData.Command = EQPtoCIM.SendData;
            SendToCIM(eqData);
            
            // RUN으로 변경될 때 Idle이면 시작으로 간주 가능 (필요시)
            if (status == EQPStatus.RUN && Data.CurrentStep == ScenarioStep.Idle)
            {
                 // Trace 용도
            }
        }

        private void UpdateScenarioStep(ScenarioStep step)
        {
            Data.CurrentStep = step;
            OnLog?.Invoke($"[SEQUENCE] Current Step Changed to: {step}");
            Save(LastValuePath);
        }

        // --- 매크로 기능 (메시지 누락 방지용) ---
        
        /// <summary>
        /// 원자적 Unload 보고: ReadyDone 전송 시 S6F63이 유발되도록 상태 강제
        /// </summary>
        public void MacroUnloadReport()
        {
            if (!CheckInterlock(EQPtoCIM.UnloadReadyDone, out string reason))
            {
                OnLog?.Invoke($"[MACRO INTERLOCK] {reason}");
                return;
            }

            UpdateScenarioStep(ScenarioStep.UnloadStarted);
            UnloadReady();
            UpdateScenarioStep(ScenarioStep.UnloadReported);
        }

        /// <summary>
        /// 원자적 Exchange 보고: ExchangeReady 전송 시 S6F63이 유발되도록 상태 강제
        /// </summary>
        public void MacroExchangeReport()
        {
            if (!CheckInterlock(EQPtoCIM.ExchangeReadyDone, out string reason))
            {
                OnLog?.Invoke($"[MACRO INTERLOCK] {reason}");
                return;
            }

            UpdateScenarioStep(ScenarioStep.UnloadStarted);
            ExchangeReadyDone();
            UpdateScenarioStep(ScenarioStep.UnloadReported);
        }

        /// <summary>
        /// 표준 로딩 시퀀스: RUN 변경 -> LR 전송
        /// </summary>
        public void MacroStandardLoad()
        {
            if (eqData.eqpStatus != EQPStatus.RUN)
            {
                ChangeStatus(EQPStatus.RUN);
                Thread.Sleep(500); // 딜레이
            }
            // 매크로 실행 시 AutoRun 강제 활성화 (CIM이 'SystemNotReady' 알람을 띄우지 않도록)
            eqData.AutoRun = true;
            
            // [중요] 기존 알람이 남아있으면 AutoRun(IsAutoRun)이 true가 되지 않으므로 강제 클리어
            eqData.AlarmList.Clear();

            // CIM이 S2F87(Load Data Send)을 줄 때, Ready 상태가 아니면 거부하므로 강제로 Ready 설정
            eqData.EqpReadyStatus = EQPReadyStatus.LoadyReady;
            
            if (!CheckInterlock(EQPtoCIM.GLSMV_LR, out string reason))
            {
                OnLog?.Invoke($"[MACRO INTERLOCK] {reason}");
                return;
            }

            UpdateScenarioStep(ScenarioStep.LoadReadyRequested);
            SendLR();
        }

        public void SendLR() { eqData.Command = EQPtoCIM.GLSMV_LR; SendToCIM(eqData); }
        public void SendUR() { eqData.Command = EQPtoCIM.GLSMV_UR; SendToCIM(eqData); }
        public void SendER() { eqData.Command = EQPtoCIM.GLSMV_ER; SendToCIM(eqData); }
        public void SendUW() { eqData.Command = EQPtoCIM.GLSMV_UW; SendToCIM(eqData); }
        public void SendLW() { eqData.Command = EQPtoCIM.GLSMV_LW; SendToCIM(eqData); }
        public void SendCR() { eqData.Command = EQPtoCIM.GLSMV_CR; SendToCIM(eqData); }

        public bool CheckInterlock(EQPtoCIM cmd, out string reason)
        {
            reason = string.Empty;

            // 공통: RUN 상태가 아니면 인터락 발생 (UpdateStatus는 제외)
            if (eqData.eqpStatus != EQPStatus.RUN && cmd != EQPtoCIM.UpdateStatus && cmd != EQPtoCIM.SendData)
            {
                // S6F61 (Load Request) 보낼 때만 예외적으로 IDLE 상태 허용 (계획준수)
                if (cmd == EQPtoCIM.GLSMV_LR && eqData.eqpStatus == EQPStatus.IDLE)
                {
                    // OK
                }
                else
                {
                    reason = $"설비 상태가 RUN이 아닙니다. (현재: {eqData.eqpStatus})";
                    return false;
                }
            }

            // 시퀀스 검증 (ScenarioStep 기반)
            switch (cmd)
            {
                case EQPtoCIM.LoadReadyDone:
                case EQPtoCIM.GLSMV_LR:
                    // Load 시작 전에는 반드시 이전 Unload가 완료(Reported)되었거나 Idle이어야 함
                    if (Data.CurrentStep != ScenarioStep.Idle && Data.CurrentStep != ScenarioStep.UnloadReported)
                    {
                        reason = "이전 Unload 보고(S6F63)가 완료되지 않았습니다. 현재 단계: " + Data.CurrentStep;
                        return false;
                    }
                    break;
                case EQPtoCIM.UnloadReadyDone:
                case EQPtoCIM.ExchangeReadyDone:
                    if (!eqData.GlassIn)
                    {
                        reason = "설비 내부에 유리가 없습니다. (GlassIn: False)";
                        return false;
                    }
                    break;
            }

            switch (cmd)
            {
                case EQPtoCIM.LoadReadyDone:
                    if (eqData.GlassIn) reason = "설비 내부에 유리가 이미 존재합니다. (GlassIn: True)";
                    break;
                case EQPtoCIM.LoadDone:
                    // LoadDone은 외부에서 유리가 들어온 뒤 호출되므로 GlassIn이 True인 경우가 많지만, 
                    // 시나리오 흐름상 보수적으로 체크 가능
                    break;
                case EQPtoCIM.UnloadReadyDone:
                    if (!eqData.GlassIn) reason = "설비 내부에 배출할 유리가 없습니다. (GlassIn: False)";
                    break;
                case EQPtoCIM.ExchangeReadyDone:
                    if (!eqData.GlassIn) reason = "교체할 유리가 설비 내부에 없습니다. (GlassIn: False)";
                    break;
                case EQPtoCIM.InspectDone:
                    if (!eqData.GlassIn) reason = "검사할 유리가 설비 내부에 없습니다. (GlassIn: False)";
                    break;
                case EQPtoCIM.GLSMV_LR:
                    if (eqData.GlassIn) reason = "로딩 요청(LR) 인터락: 유리가 이미 존재합니다.";
                    break;
                case EQPtoCIM.GLSMV_UR:
                    if (!eqData.GlassIn) reason = "언로딩 요청(UR) 인터락: 유리가 없습니다.";
                    break;
                case EQPtoCIM.GLSMV_ER:
                    if (!eqData.GlassIn) reason = "교체 요청(ER) 인터락: 교체할 유리가 없습니다.";
                    break;
            }

            return string.IsNullOrEmpty(reason);
        }

        // Recipe Methods
        public void RecipeCreate(string recipeId)
        {
            eqData.Command = EQPtoCIM.RecipeCreate;
            eqData.RecipeItem = new RecipeSet { RECIPEID = recipeId };
            SendToCIM(eqData);
        }

        public void RecipeChange(string recipeId)
        {
            eqData.Command = EQPtoCIM.RecipeChange;
            eqData.RecipeItem = new RecipeSet { RECIPEID = recipeId };
            SendToCIM(eqData);
        }

        public void RecipeDelete(string recipeId)
        {
            eqData.Command = EQPtoCIM.RecipeDelete;
            eqData.RecipeItem = new RecipeSet { RECIPEID = recipeId };
            SendToCIM(eqData);
        }
    }
}
