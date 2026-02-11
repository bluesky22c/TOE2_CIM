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
using System.Windows.Forms;
//using TOELib;

namespace TOECIM
{
    [Serializable]
    public class EQPSimulatorData
    {
        public int CIMServerPort { get; set; } = 3500;
        public int EQServerPort { get; set; } = 3501;
        public TOECIMLib.ECSData LoadGlass { get; set; }
        public TOECIMLib.ECSData UnloadGlass { get; set; }
        public EQtoCIMData eqData { get; set; } = new EQtoCIMData();
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

        public EQtoCIMData eqData => Data.eqData;

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
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(EQPSimulatorData));
                    xs.Serialize(sw, this.Data);
                }
            }
            catch (Exception exp)
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
                        using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(EQPSimulatorData));
                            simulator.Data = (EQPSimulatorData)xs.Deserialize(sr);
                        }
                    }
                    catch (Exception)
                    {
                        // Deserialization failed (e.g. invalid format or empty). Recreate default.
                        recreate = true;
                    }
                }

                if (recreate)
                {
                    simulator.Save(fileName);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine($"Failed to load EQPSimulator data: {ee.Message}");
                // In worst case, return default simulator rather than crashing
            }
            return simulator;
        }
        #endregion

        public void StartServer()
        {
            try {
                // File.AppendAllText("simul_debug.txt", $"{DateTime.Now}: StartServer called. Port: {EQServerPort}\n");
                if (server != null)
                {
                    server.Stop();
                }
                server = new JsonServer<CimCommData>(this, OnReceiveData, null);
                server.Listen(EQServerPort);
                // File.AppendAllText("simul_debug.txt", $"{DateTime.Now}: Server.Listen success.\n");
            } catch (Exception ex) {
                // File.AppendAllText("simul_debug.txt", $"{DateTime.Now}: StartServer FAILED: {ex.Message}\n");
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
            // File.AppendAllText("simul_debug.txt", $"{DateTime.Now}: OnReceiveData received request.\n");
            // System.Diagnostics.Debug.WriteLine("[EQPSimulator] Data Received from Main");
            // TOECIM(Main)에서 보낸 요청을 처리하고 시뮬레이터 상태를 응답에 매핑
            spec.ToCIM.eqpStatus = eqData.eqpStatus;
            
            if (spec.ToCIM.ControlData == null)
                spec.ToCIM.ControlData = new ControltoConsoleData();
            
            spec.ToCIM.ControlData.EqpStatus = eqData.eqpStatus;
            spec.ToCIM.ControlData.GlassIn = eqData.GlassIn;
            spec.ToCIM.ControlData.EqpReadyStatus = MapReadyStatus(eqData.GlassTransferStatus);
            spec.ToCIM.Error = false; // 연결 성공 응답

            // Command 처리 (필요한 경우)
            // 현재는 주로 폴링 요청이 들어옴

            Save(LastValuePath);

            OnLog?.Invoke($"[RECV] REQ (Poll/Cmd)");

            return spec;
        }

        internal void SendToCIM(EQtoCIMData eqData)
        {
            try
            {
                var client = new JsonClient<CimCommData>(this, "127.0.0.1", CIMServerPort, null);

                ConsoletoCIMData toCim = new ConsoletoCIMData();
                toCim.Command = MapCommand(eqData.CMD);
                toCim.eqpStatus = eqData.eqpStatus;
                
                toCim.ControlData = new ControltoConsoleData();
                toCim.ControlData.GlassIn = eqData.GlassIn;
                // AutoRun logic: ConsoletoCIMData.AutoRun is true if eqpStatus == RUN && ControlData?.EqpStatus == RUN
                if (eqData.AutoRun)
                {
                    toCim.ControlData.EqpStatus = EQPStatus.RUN; // Ensure ControlData matches for AutoRun=true
                }
                else
                {
                    toCim.ControlData.EqpStatus = eqData.eqpStatus; // Or match current status
                }
                toCim.ControlData.EqpReadyStatus = MapReadyStatus(eqData.GlassTransferStatus);

                if (eqData.CMD == EQtoCIM_CMD.Alarm && eqData.Alarm_Item != null)
                {
                    toCim.AlarmList.Add(eqData.Alarm_Item);
                }

                CimCommData commData = new CimCommData();
                commData.ToCIM = toCim;
                commData.WaitResponse = false;

                client.Send(commData);
                OnLog?.Invoke($"[SEND] CMD:{toCim.Command}, STATUS:{toCim.eqpStatus}, READY:{toCim.ControlData?.EqpReadyStatus}");
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message.ToString());
                Console.WriteLine($"SendToCIM Failed: {exp.Message}");
            }
        }

        private ConsoletoCIM MapCommand(EQtoCIM_CMD cmd)
        {
            switch (cmd)
            {
                case EQtoCIM_CMD.LoadReady: return ConsoletoCIM.LoadReadyDone;
                case EQtoCIM_CMD.LoadDone: return ConsoletoCIM.LoadDone;
                case EQtoCIM_CMD.UnloadReady: return ConsoletoCIM.UnloadReadyDone;
                case EQtoCIM_CMD.UnloadDone: return ConsoletoCIM.UnloadDone;
                case EQtoCIM_CMD.Exchange: return ConsoletoCIM.ExchangeReadyDone;
                case EQtoCIM_CMD.ExchangeDone: return ConsoletoCIM.ExchangeDone;
                case EQtoCIM_CMD.Alarm: return ConsoletoCIM.Alarm;
                case EQtoCIM_CMD.AlarmClear: return ConsoletoCIM.AlarmClear;
                case EQtoCIM_CMD.SendData: return ConsoletoCIM.SendData;
                case EQtoCIM_CMD.Pause: return ConsoletoCIM.Pause;
                case EQtoCIM_CMD.Error: return ConsoletoCIM.Error;
                // Add other mappings as needed
                default: return ConsoletoCIM.None;
            }
        }

        private EQPReadyStatus MapReadyStatus(EQtoCIM_CMD cmd)
        {
            switch (cmd)
            {
                case EQtoCIM_CMD.LoadReady: return EQPReadyStatus.LoadyReady;
                case EQtoCIM_CMD.UnloadReady: return EQPReadyStatus.UnloadReady;
                case EQtoCIM_CMD.Exchange: return EQPReadyStatus.ExchangeReady;
                default: return EQPReadyStatus.None;
            }
        }

        public void SendData()
        {
            // eqData.Alarm_Item = alarm;
            //eqData.AutoRun = autoRunCheckBox.Checked;
            //eqData.GlassIn = glassInCheckBox.Checked;
            //eqData.eqpStatus = curEQPStatus;
            eqData.CMD = EQtoCIM_CMD.SendData;

            //ionizer
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var item = new IonizerInfo();
                //item.ID = (byte)rnd.Next(0, 255);
                item.ID = (byte)(i + 1);
                item.Status = eqData.IonizerInfos[i].Status;
                eqData.IonizerInfos[i] = item;
            }

            SendToCIM(eqData);
        }

        public void SetAlarm(AlarmType type, int id)
        {
            var alarm = eqData.Alarm_Item;

            alarm = new AlarmInfo();
            alarm.ALID = id.ToString();
            alarm.ALCD = ALCD.SetCode(ALCD.ALCDEnum.EQPStatusWarning);
            alarm.ALTX = "EQPStatusWarning";
            alarm.AlarmType = type;
            alarm.DateTime = DateTime.Now;

            //EQtoCIMData eqData = new EQtoCIMData();
            eqData.CMD = EQtoCIM_CMD.Alarm;
            eqData.Alarm_Item = alarm;
            if (alarm.AlarmType == AlarmType.HeavyAlarm)
            {
                preEQPStatus = eqData.eqpStatus;
                eqData.eqpStatus = EQPStatus.DOWN;
            }

            SendToCIM(eqData);
        }

        public void ClearAlarm()
        {
            var alarm = eqData.Alarm_Item;
            if (alarm == null)
                return;

            eqData.CMD = EQtoCIM_CMD.AlarmClear;
            alarm.ALCD = ALCD.ReleaseCode((byte)ALCD.ALCDEnum.EQPStatusWarning);
            eqData.Alarm_Item = alarm;

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

        public void SetIsGlass(bool on)
        {
            eqData.CMD = EQtoCIM_CMD.GlassInOut;
            eqData.GlassIn = on;
            SendToCIM(eqData);
        }

        internal void LoadReady()
        {
            eqData.GlassIn = false;
            eqData.GlassTransferStatus = EQtoCIM_CMD.LoadReady;
            eqData.CMD = EQtoCIM_CMD.LoadReady;
            SendToCIM(eqData);
        }

        internal void LoadDone()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.LoadDone;
            eqData.CMD = EQtoCIM_CMD.LoadDone;
            eqData.eqpStatus = EQPStatus.RUN;
            eqData.GlassIn = true;
            SendToCIM(eqData);
        }

        internal void Loading()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.Loading;
            eqData.CMD = EQtoCIM_CMD.Loading;
            foreach (var i in eqData.IonizerInfos)
            {
                i.Status = "R";
            }
            SendToCIM(eqData);
        }

        internal void UnloadReadyStart()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.UnloadReadyStart;
            eqData.CMD = EQtoCIM_CMD.UnloadReadyStart;
            eqData.GlassIn = true;

            UnloadGlass = new ECSData();
            if (LoadGlass != null)
            {
                UnloadGlass.AGlass = Common.Clone(LoadGlass.AGlass);
                UnloadGlass.BGlass = Common.Clone(LoadGlass.BGlass);
            }

            SendToCIM(eqData);
        }

        internal void UnloadReady()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.UnloadReady;
            eqData.CMD = EQtoCIM_CMD.UnloadReady;
            eqData.GlassIn = true;

            foreach (var i in eqData.IonizerInfos)
            {
                i.Status = "I";
            }

            SendToCIM(eqData);
        }

        internal void Exchange()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.Exchange;
            eqData.CMD = EQtoCIM_CMD.Exchange;
            eqData.GlassIn = false;

            SendToCIM(eqData);
        }

        internal void ExchangeDone()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.ExchangeDone;
            eqData.CMD = EQtoCIM_CMD.ExchangeDone;
            eqData.GlassIn = true;

            SendToCIM(eqData);
        }

        internal void Unload()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.Unload;
            eqData.CMD = EQtoCIM_CMD.Unload;
            eqData.GlassIn = true;

            SendToCIM(eqData);
        }

        internal void UnloadDone()
        {
            eqData.GlassTransferStatus = EQtoCIM_CMD.UnloadDone;
            eqData.CMD = EQtoCIM_CMD.UnloadDone;
            eqData.GlassIn = false;
            eqData.eqpStatus = EQPStatus.IDLE;

            SendToCIM(eqData);
        }

        internal void ProcessDown()
        {
            eqData.CMD = EQtoCIM_CMD.Pause;
            eqData.eqpStatus = EQPStatus.ProcessDown;

            SendToCIM(eqData);
        }

        public void ChangeStatus(EQPStatus status)
        {
            eqData.eqpStatus = status;
            eqData.CMD = EQtoCIM_CMD.SendData;
            
            // 상태 변경 시 CIM에 보고
            SendToCIM(eqData);
        }
    }
}
