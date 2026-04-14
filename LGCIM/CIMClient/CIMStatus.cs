using Elp;
using LGHSMSBase;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LGCIM.Lib;

namespace LGCIM
{
    [Serializable]
    public class CIMStatus
    {
        static CIMStatus()
        {
            Console.WriteLine("Static Constructor: CIMStatus is initializing...");
        }

        public CIMStatus()
        {
            Console.WriteLine("CIMStatus: Constructor Starting...");
            try {
                for(int i = 0; i < IonizerInfos.Length; i++)
                {
                    IonizerInfos[i] = new IonizerInfo();
                }
                Console.WriteLine("CIMStatus: Constructor Complete.");
            } catch (Exception ex) {
                Console.WriteLine($"CIMStatus: Error in Constructor: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public UNITID UnitID => 201;
        [XmlIgnore][JsonIgnore]
        public STAGEID StageID { get; set; } = 1;
        public string EQModelType { get; set; } = "PXCEPI";
     
        /// <summary>
        /// S2F87에서 받아서 Handshake가 끝났을 때 UnloadGlass로 패쓰
        /// </summary>
        public ECSData LoadGlass { get; set; }
        /// <summary>
        /// 이게 실제로 안착되어 있는 글래스 정보
        /// </summary>
        public ECSData UnloadGlass { get; set; }

        public LotInfo LotData { get; set; } = new LotInfo();

        //Scrap 보고 할 데이터
        public string SCRTYPE { get; set; }
        //public string SCRGLASS { get; set; }

        //이전에 로드된 Glass Order를 기억해 놓음
        //public GlassOrder PreGlassOrder { get; set; } = GlassOrder.None;
        //public GlassOrder GlassOrder { get; set; } = GlassOrder.None;

        public CimCommData CimCommData { get; set; } = new CimCommData();

        public EQPtoCIMData ConsoleData 
        { 
            get => CimCommData.ToCIM;
            set => CimCommData.ToCIM = value;
        }
        //public ControltoConsoleData ControlData => ConsoleData.ControlData;
        /// <summary>
        /// Load, Exchange, Unload Ready 상태
        /// </summary>
        public bool IsTransferReady => IsAutoRun && ConsoleData.EqpReadyStatus != EQPReadyStatus.None;
        public IonizerInfo[] IonizerInfos { get; set; } = new IonizerInfo[3];

        public EQPtoCIMData GetStatusFromConsole(bool writeLog = true)
        {
            var toCim =  Common.ToConsole(new CIMtoEQPData { Command = CIMtoEQP.EQStatusDataReq }, true, 1000, 1);
            CimCommData.ToCIM = toCim;

            if (toCim.Ionizer != null)
            {
                var ion = toCim.Ionizer;
                for (int i = 0; i < ion.Length; i++)
                {
                    if (ion[i] != null)
                    {
                        IonizerInfos[i].ID = ion[i].ID;
                        IonizerInfos[i].Status = ion[i].Status;

                        if (writeLog)
                            log.Information($"Ionizer_{i + 1} : {ion[i].ID}, {ion[i].Status}");
                    }
                }
            }

            var status = Vars.CIMEQPStatsu((LGCIM.Lib.EQPStatus)toCim.eqpStatus);

            //if (toCim.GlassIn == false)
            //    UnloadGlass = null;
            return toCim;
        }


        // 상태 확인 flag--------------------------------------------------------------

        ///// <summary>
        ///// 실제 EQ에서 글라스 로딩되었을때 True로 변경됨
        ///// </summary>
        //public bool LoadGlassEQ { get; set; } = false;

        /// <summary>
        /// AutoRun 상태인지 확인 (EQ, Console)
        /// </summary>
        public bool IsAutoRun => ConsoleData.AutoRun && CurAlarm == null;

        /// <summary>
        /// 글라스가 장비 안에 있는지 확인 (EQ)
        /// true일 경우 UW 후 ER 보고,
        /// false일 경우 바로 LR 또는 종료 (GLSSTATUS에 따름)
        /// </summary>
        public bool? EQGlassIn => ConsoleData.GlassIn;

        /// <summary>
        /// 레시피 MisMatch Error (CIM)
        /// </summary>
        public bool RecipeError { get; set; } = false;

        /// <summary>
        /// Lot에 로드할 Glass가 있는지 확인 (CIM)
        /// GLSSTATUS = 1이면 true -> Exchange Request
        /// GLSSTATUS = 0이면 false -> Unload Request
        /// </summary>
        public bool CstGlassExist { get; set; } = true;

        /// <summary>
        /// pause 상태인지
        /// </summary>
        public bool Pause { get; set; } = false;

        /// <summary>
        /// 현재 cycleStop 상태인지
        /// </summary>
        public bool CycleStop { get; set; } = false;
        //---------------------------------------------------------------------------



        /// <summary>
        /// 현재 RCS 모드
        /// </summary>
        public RCS.RCSEnum RCS { get; set; } = LGHSMSBase.RCS.RCSEnum.OffLine;
        /// <summary>
        /// 바꾸려고 하는 RCS
        /// </summary>
        public RCS.RCSEnum TryRCS { get; set; } = LGHSMSBase.RCS.RCSEnum.OffLine;
        public bool IsOnline => RCS == LGHSMSBase.RCS.RCSEnum.OnLineLocal || RCS == LGHSMSBase.RCS.RCSEnum.ONLineRemote;
        public bool ChangeRCS { get; set; } = false;

        public OPMODE.OPMODEEnum OPMode { get; set; } = OPMODE.OPMODEEnum.Normal;
        public OPMODE.OPMODEEnum TryOPMode { get; set; } = OPMODE.OPMODEEnum.Normal;


        //down 상태 이전의 EQP Status를 저장해 놓음
        //public EQPSTATUS.EQPSTATUSEnum PreStatus { get; set; } = EQPSTATUS.EQPSTATUSEnum.IDLE;
        [XmlIgnore][JsonIgnore]
        public EQPSTATUS.EQPSTATUSEnum EQPStatus { get; set; } = EQPSTATUS.EQPSTATUSEnum.DOWN;
        //public EQPSTATUS.EQPSTATUSEnum TryEQPStatus { get; set; } = EQPSTATUS.EQPSTATUSEnum.IDLE;

        public GLSMVSTATUS.GLSMVSTATUSEnum LastReportMV { get; set; }
        //public GLSMVSTATUS.GLSMVSTATUSEnum GLSMVSTATUS { get; set; } = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LR;
        //public GLSMVSTATUS.GLSMVSTATUSEnum TryGLSMVSTATUS { get; set; } = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LR;


        /// <summary>
        /// Lot Start 전에 레시피 데이터 확인 여부 선택
        /// Control 상태에 따름
        /// Wait로 설정 시 레시피 데이터 확인 후 Lot Start
        /// </summary>
        public SAMPLINGFLAG.SAMPLINGFLAGEnum SAMPLINGFLAG { get; set; } = LGHSMSBase.SAMPLINGFLAG.SAMPLINGFLAGEnum.START;

        public AlarmInfo CurAlarm => AlarmInfo.GetReportAlarm(AlarmList);

        public List<AlarmInfo> AlarmList { get; set; } = new List<AlarmInfo>();

        /// <summary>
        /// Console에서 받아와서 ECS에 DVItem으로 보고
        /// </summary>
        public List<APDInfo> APDIItems { get; set; }


        LogEx log => ClientCIM.log;

        [XmlIgnore]
        [JsonIgnore]
        public bool IsAGlass { get; internal set; } = true;
        public string Attribute { get; internal set; } = "";

        static public CIMStatus Load()
        {
            var log = ClientCIM.log;
            var setup = new CIMStatus();
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            fileName = Path.Combine(fileName, "lastValue.json");
            Console.WriteLine($"CIMStatus.Load: Attempting to load from {fileName}");
            try
            {
                if (File.Exists(fileName) == false)
                {
                    Console.WriteLine("CIMStatus.Load: File not exists.");
                    log.AddLogMessage(LogType.Error, 0, $"{fileName} File Not Exists. New File Create.");
                    setup.Save();
                    return setup;
                }
                var jstring = File.ReadAllText(fileName, Encoding.Default);
                Console.WriteLine("CIMStatus.Load: File read success. Deserializing...");
                setup = JsonConvert.DeserializeObject<CIMStatus>(jstring);
                Console.WriteLine("CIMStatus.Load: Deserialization success.");
            }
            catch (Exception ee)
            {
                Console.WriteLine($"[CIMSTATUS LOAD ERROR] {ee.GetType().Name}: {ee.Message}");
                Console.WriteLine(ee.StackTrace);
                log.AddLogMessage(LogType.Error, 0, "(LastValue Load) " + ee.Message);
            }
            return setup;
        }

        public bool Save()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            fileName = Path.Combine(fileName, "lastValue.json");
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                var jstring = JsonConvert.SerializeObject(this, Formatting.Indented, new StringEnumConverter());
                File.WriteAllText(fileName, jstring);
            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, "(LastValue Save) " + ee.Message);
                log.AddLogMessage(LogType.Error, 0, ee.ToString());

                return false;
            }
            return true;
        }
    }

    [Serializable]
    //1 Lot에 존재하는 Glass 순서
    public enum GlassOrder
    {
        FirstGlass = 0,
        NormalGlass = 1,    //All glasses except the first glass and the last glass
        LastGlass = 2,
        OnlyGlass = 3,  //the only glass, when there is just one in a LOT.
        None    //No Next Glass
    }

    public enum ByWho
    {
        None,
        Host,
        Equipment,
    }
}
