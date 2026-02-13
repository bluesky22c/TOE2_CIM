using Engion.Net;
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
    [Serializable]
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class EQPSimulatorData
    {
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
        }

        public void LoadDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.LoadDone;
            eqData.eqpStatus = EQPStatus.RUN;
            eqData.GlassIn = true;
            SendToCIM(eqData);
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
            eqData.eqpStatus = EQPStatus.IDLE;
            eqData.GlassIn = true;

            SendToCIM(eqData);
        }

        public void ExchangeReadyDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.ExchangeReady;
            eqData.Command = EQPtoCIM.ExchangeReadyDone;
            eqData.GlassIn = false;

            // 판정 데이터 보완 (ToECIM의 S6F63 전송 시 필수 데이터)
            eqData.GlassPanelJudgeA = "G";
            eqData.GlassPanelJudgeB = "G";

            SendToCIM(eqData);
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
        }

        public void ChangeStatus(EQPStatus status)
        {
            eqData.eqpStatus = status;
            eqData.Command = EQPtoCIM.SendData;
            SendToCIM(eqData);
        }

        public void SendLR() { eqData.Command = EQPtoCIM.GLSMV_LR; SendToCIM(eqData); }
        public void SendUR() { eqData.Command = EQPtoCIM.GLSMV_UR; SendToCIM(eqData); }
        public void SendER() { eqData.Command = EQPtoCIM.GLSMV_ER; SendToCIM(eqData); }
        public void SendUW() { eqData.Command = EQPtoCIM.GLSMV_UW; SendToCIM(eqData); }
        public void SendLW() { eqData.Command = EQPtoCIM.GLSMV_LW; SendToCIM(eqData); }
        public void SendCR() { eqData.Command = EQPtoCIM.GLSMV_CR; SendToCIM(eqData); }
    }
}
