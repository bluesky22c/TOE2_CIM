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
                
                simulator.CheckIonizerData();
            }
            catch (Exception ee)
            {
                Console.WriteLine($"Failed to load EQPSimulator data: {ee.Message}");
                // In worst case, return default simulator rather than crashing
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
            // TOECIM(Main)에서 보낸 요청을 처리하고 시뮬레이터 상태를 응답에 매핑
            spec.ToCIM.eqpStatus = eqData.eqpStatus;
            spec.ToCIM.GlassIn = eqData.GlassIn;
            spec.ToCIM.EqpReadyStatus = eqData.EqpReadyStatus;
            
            // Fix: Populate AutoRun, Ionizer, AlarmList
            spec.ToCIM.AutoRun = eqData.AutoRun && eqData.eqpStatus == EQPStatus.RUN;
            
            if (eqData.Ionizer != null)
            {
                spec.ToCIM.Ionizer = eqData.Ionizer;
            }

            if (eqData.AlarmList.Count > 0)
            {
                spec.ToCIM.AlarmList.AddRange(eqData.AlarmList);
            }

            spec.ToCIM.Error = false; // 연결 성공 응답

            // Command 처리 (필요한 경우)
            // 현재는 주로 폴링 요청이 들어옴

            Save(LastValuePath);

            OnLog?.Invoke($"[RECV] REQ (Poll/Cmd)");

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
                
                // AutoRun logic: Both must be RUN to be true
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
                OnLog?.Invoke($"[SEND] CMD:{toCim.Command}, STATUS:{toCim.eqpStatus}, READY:{toCim.EqpReadyStatus}");
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message.ToString());
                Console.WriteLine($"SendToCIM Failed: {exp.Message}");
            }
        }



        public void SendData()
        {
            // eqData.Alarm_Item = alarm;
            //eqData.AutoRun = autoRunCheckBox.Checked;
            //eqData.GlassIn = glassInCheckBox.Checked;
            //eqData.eqpStatus = curEQPStatus;
            eqData.Command = EQPtoCIM.SendData;

            //ionizer
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var item = new IonizerInfo();
                //item.ID = (byte)rnd.Next(0, 255);
                item.ID = (byte)(i + 1);
                item.Status = eqData.Ionizer[i].Status;
                eqData.Ionizer[i] = item;
            }

            SendToCIM(eqData);
        }

        public void SetAlarm(AlarmType type, int id)
        {
            var alarm = eqData.AlarmList.FirstOrDefault();

            alarm = new AlarmInfo();
            alarm.ALID = id.ToString();
            alarm.ALCD = ALCD.SetCode(ALCD.ALCDEnum.EQPStatusWarning);
            alarm.ALTX = "EQPStatusWarning";
            alarm.AlarmType = type;
            alarm.DateTime = DateTime.Now;

            //EQtoCIMData eqData = new EQtoCIMData();
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
            // eqData.Alarm_Item = alarm; // AlarmList already has it, just updating logic if needed
            // But since we are clearing or updating status, we should ensure the list reflects the cleared state or just send the command.
            // The original code modified alarm and set it back. Since it's a ref, it might be fine, or we need to clear logic.
            // Original: eqData.Alarm_Item = alarm;
            // Let's assume we keep the alarm in the list but change its code, or clear the list?
            // "AlarmClear" usually sends the alarm that is being cleared.
            
            // Logic: Just update the alarm object in the list.
            
            // But if we want to add to list for sending:
            // It's already in the list.
            
            // Wait, previous SetAlarm logic CLEARED the list and added one.
            // So AlarmList[0] is our alarm.

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

        internal void LoadReady()
        {
            eqData.GlassIn = false;
            eqData.EqpReadyStatus = EQPReadyStatus.LoadyReady;
            eqData.Command = EQPtoCIM.LoadReadyDone;
            SendToCIM(eqData);
        }

        internal void LoadDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.LoadDone;
            eqData.eqpStatus = EQPStatus.RUN;
            eqData.GlassIn = true;
            SendToCIM(eqData);
        }

        internal void UnloadReady()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.UnloadReady;
            eqData.Command = EQPtoCIM.UnloadReadyDone;
            eqData.GlassIn = true;

            foreach (var i in eqData.Ionizer)
            {
                i.Status = "I";
            }

            SendToCIM(eqData);
        }

        internal void Exchange()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.ExchangeReady;
            eqData.Command = EQPtoCIM.ExchangeReadyDone;
            eqData.GlassIn = false;

            SendToCIM(eqData);
        }

        internal void ExchangeDone()
        {
            eqData.EqpReadyStatus = EQPReadyStatus.None;
            eqData.Command = EQPtoCIM.ExchangeDone;
            eqData.GlassIn = true;

            SendToCIM(eqData);
        }

        internal void UnloadDone()
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
            
            // 상태 변경 시 CIM에 보고
            SendToCIM(eqData);
        }
    }
}
