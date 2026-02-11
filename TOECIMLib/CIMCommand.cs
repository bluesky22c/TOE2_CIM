using Engion.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using TOELib;

namespace TOECIMLib
{
    [Serializable]
    public enum ConsoletoControl //Flow test
    {
        None,
        LoadReadyReq,
        ExchangeReadyReq,
        //LoadEnableOn, // Glass Data도 같이 보냄
        UnloadReadyReq,
        StartHandshake,
        Stop,
        MoveTo,   //ControlCommTest
        EqDataReq,
        AxisRes,
        GlassSettle,  // 글래스 안착작업
        UnloadHandhake, // exchnage ready 상태에서 unlaod 가능하도록,
        AlarmClear,
        BuzzerOff,
        ReadyCheck, // 글래스를 주고 받을 수 있는지 실시간 체ㅡ
        NomalStatusChange,
        GlassSettleFormconsole,
        ContactZMove2ContactPos,
        ContactZMove2SafePos,
        ContactZMove2CheckPos,
        UnitModelInfo,

        BlockFlatnessCheck,
        MC_Ready_Pos_Move,  // 0112 MC 준비 위치 이동 명령 추가

        OrderAutoRun,
    }

    /// <summary>
    /// 장비가 글래스를 받을 수 있는 상태여부
    /// LoadyReady와 UnloadReady 상태는 장비가 Autorun 상태임을 보장한다.
    /// </summary>
    [Serializable]
    public enum EQPReadyStatus
    {
        None,
        LoadyReady,
        UnloadReady,
        ExchangeReady,
    }
    public static class EQPReadyStatusExt
    {
        public static bool IsReady(this EQPReadyStatus val)
        {
            return (val == EQPReadyStatus.LoadyReady ||
                val == EQPReadyStatus.UnloadReady ||
                val == EQPReadyStatus.ExchangeReady);
        }
    }


    [Serializable]
    public enum ConsoletoCIM //Flow test
    {
        None,
        SendData,
        LoadReadyDone,  //s6f61
        LoadDone, // EQP Status : Run
        InspectDone,    //s6f63 (Unload Glass Data 보고)
        ExchangeReadyDone,
        ExchangeDone,   //s1F67 (현재 Glass 상태 보고)
        UnloadReadyDone,
        UnloadDone, //s1F67, LOT에 다음 글라스가 없음 
        Alarm,
        AlarmClear,
        Pause,  //abnormal
        Error,  //abnormal

        //레시피 보고 관련
        RecipeCreate,
        RecipeChange,
        RecipeDelete,
        RecipeRename,

        ProgramStarted,
        ShutDown,
        UpdateStatus,

        CancelReq,
        //Loading,
        //Exchange,
        //Unload,
        //UnloadReadyStart,
        //LoadReady,
        //GlassInOut,

    }
    [Serializable]
    public enum ConsoletoIP //Flow test
    {
        None,
        ImageGrabReady,
    }

    [Serializable]
    public enum IPtoConsole //Flow test
    {
        None,
        ReadyDone,
        GrabDone,
        InspectEnd, // log : Grabdone & insepect end
    }

    [Serializable]
    public enum ControltoConsole //Flow test
    {
        None,
        LoadHandshakeStart,
        LoadHandshakeEnd,

        //LoadDone, // align 까지 끝난 상태
        ExchangeHandshakeStart,
        ExchangeHandshakeEnd,
        ///*ExchangeDone*/, // align 까지 끝난 상태
        UnloadHandshakeStart,
        UnloadHandshakeEnd,
        //UnloadDone, // 마지막장 나간상태
        HandshakeAborted,
        AlarmOccur,
        AlarmClear,

        Clamp,
        LiftUp,
        LiftDown,

        UpdateStatus,
        Message,
        GetUnitPos,
        //LoadReadyDoneACK, //Loader의 Load Ready 신호가 Enable 되어 있어야 함
        //HandShakingDone, //Glass 투입 완료
        //MoveAndWait_Align,
        //MoveAndWait_Contact,
        //MoveAndWait_UnContact,
        //MoveAndWait_ScanReady,
        //ScanDefectPosMoveDone,
        //DefectScanDone,
        //DefectReviewMvDone,
        //ReviewGrabDone,
        //UnloadReadyDone,
        //UnloadDone,
        //MoveDone,   //ControlCommTest
        //EqDataRes,
        //AxisReq,
        
        ReqUnitData,
        ReqUnderCamData,
        ReqBlockPosData,

        initATF,    // 전축 초기화시 ATF Z축 초기화 명령
    }

    [Serializable]
    public enum HandshakeFlowError //Flow test
    {
        None,
        EQP,
        LOADER,
        TIMEOUT,
    }

    [Serializable]
    public enum HandshakeFlowReturn
    {
        Success,
        Fail,
        Timeout
    }

    [Serializable]
    public enum HandshakeFlowResult
    {
        Success,
        Fail,
        Cancel
    }

    [Serializable]
    public enum CIMtoConsole //Flow test
    {
        None,
        EQStatusDataReq,
        GlassDataSend,
        UnloadReq,  //Lot에 다음 로드할 글라스가 없고 장비에 글라스가 남아 있을 경우
        Pause,
        Error,
        LoadRecipe,
        AlarmClear,
        NomalStatusChange,
        APDReq,
        GlassDataReq,
    }


    [Serializable]
    public enum ProcessCMD //Flow test
    {
        None,
        LOAD,
        ALIGN1,
        ALIGN2,
        CONTACT1,
        CONTACT2,
        SCAN1,
        SCAN2,
        REVIEW1,
        REVIEW2
    }


    //[Serializable]
    //public class TestData : TENetData
    //{
    //    public TestData()
    //    {

    //    }

    //    public GlassInfo GlassInfo { get; set; } = new GlassInfo();
    //    public CIMtoConsole CIMtoConsole { get; set; } = CIMtoConsole.None;
    //    public ConsoletoCIM ConsoletoCIM { get; set; }
    //    public ConsoletoControl ConsoletoControl { get; set; }
    //    public ControltoConsole ControltoConsole { get; set; }
    //}


    [Serializable]
    public class ControlCommData : TENetData
    {
        public ConsoletoControlData ToControl { get; set; } = new ConsoletoControlData();
        public ControltoConsoleData ToConsole { get; set; } = new ControltoConsoleData();
    }
    [Serializable]
    public class CimCommData : TENetData
    {
        public ConsoletoCIMData ToCIM { get; set; } = new ConsoletoCIMData();
        public CIMtoConsoleData ToConsole { get; set; } = new CIMtoConsoleData();
    }


    [Serializable]
    public class ConsoletoCIMData //Console → CIM
    {
        public ConsoletoCIMData()
        {
            Command = ConsoletoCIM.None;
            eqpStatus = EQPStatus.DOWN;
            //scanDir = ScanDir.Forward;
            //scanTurn = ScanTurn.Normal;

            GlassJudgeCode = "G";
            Error = false;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public ConsoletoCIM Command { get; set; }

        public List<AlarmInfo> AlarmList { get; set; } = new List<AlarmInfo>();
        //public AlarmInfo AlarmInfo { get; set; }      //알람 항목

        //public IonizerInfo[] IonizerInfos => ControlData?.Ionizer; //Ionizer 값
        public bool AutoRun => eqpStatus == EQPStatus.RUN && ControlData?.EqpStatus == EQPStatus.RUN;           //EQ 오토런 상태
        public bool GlassIn => ControlData?.GlassIn ?? false;            //EQ에 Glass가 있는지 확인
        public EQPStatus eqpStatus { get; set; } = EQPStatus.DOWN;
        //public ScanDir scanDir { get; set; }
        //public ScanTurn scanTurn { get; set; }
        public string GlassPanelJudgeA { get; set; }
        public string GlassPanelJudgeB { get; set; }
        public string GlassJudgeCode { get; set; }
        public RecipeSet RecipeItem { get; set; }
        public ControltoConsoleData ControlData { get; set; }
        /// <summary>
        /// A cut apd data
        /// </summary>
        public ActualProcessData AcutAPD { get; set; }
        /// <summary>
        /// B cut apd data
        /// </summary>
        public ActualProcessData BcutAPD { get; set; }
        public bool Error { get; set; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    public class CIMtoConsoleData //CIM → Console
    {
        public CIMtoConsoleData()
        {
            Command = CIMtoConsole.None;
            GlassInfo = new CommonGlassInfo[2];
            Error = false;
        }
        public CIMtoConsole Command { get; set; }
        public ushort UNITID { get; set; }
        public byte STAGEID { get; set; }
        //public bool Online { get; set; }

        /// <summary>
        /// 0 : glass 있음, 1 : glass 없음
        /// </summary>
        public bool ContainGlass { get; set; }
        public CommonGlassInfo[] GlassInfo { get; set; }  //Glass Data
        public CIMRecipe RecipeItem { get; set; }
        public bool Error { get; set; } //Nomal Status 항목으로 사용.
        public bool NomalStatus { get; set; }
    }

    [Serializable]
    public class ConsoletoControlData   //Console -> Control
    {
        public ConsoletoControlData()
        {
            GlassInfo = new CommonGlassInfo[2];
        }

        public int No { get; set; }
        public ConsoletoControl Command { get; set; }
        public int Maualdex { get; set; }//매뉴얼로 데이터 전송할때 여기에 인덱스 분류 해서 보내야 함 
        public AxisPositions MotionPos { get; set; }
        public CommonGlassInfo[] GlassInfo { get; set; }  //Glass Data

        public object obj { get; set; }
        public bool AGlassIsNotExist { get; set; }
        public bool BGlassIsNotExist { get; set; }

        //public CommandResult Result { get; set; }
        public bool NomalStatus { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    public class ControltoConsoleData   //Control -> Console
    {
        public ControltoConsoleData()
        {
            for(int i = 0; i < Ionizer.Length; i++)
            {
                Ionizer[i] = new IonizerInfo { ID = (byte)i, Status = "R" };
            }
        }
        [RefreshProperties(RefreshProperties.All)]
        public ControltoConsole Command { get; set; }
        public int No { get; set; }
        [RefreshProperties(RefreshProperties.All)]
        public bool GlassIn { get; set; } = false;  //장비 안에 Glass가 있는지
        public bool AGlassIn { get; set; } = false;  //장비 안에 Glass가 있는지
        public bool BGlassIn { get; set; } = false;  //장비 안에 Glass가 있는지
        public AxisPositions MotionPos { get; set; } = new AxisPositions();

        public IonizerInfo[] Ionizer { get; set; } = new IonizerInfo[3];
        public string BlockSensorValue { get; set; } = "";
        public EQPStatus EqpStatus { get; set; } = EQPStatus.DOWN;
        [RefreshProperties(RefreshProperties.All)]
        public EQPReadyStatus EqpReadyStatus { get; set; }
        
        public AlarmInfo AlarmInfo { get; set; } = new AlarmInfo();

        [RefreshProperties(RefreshProperties.All)]
        public CommandResult Result { get; set; }
        public string Message { get; set; }
        public HandshakeFlowError HandshakeFlowError { get; set; }
        public MCC controlMCCChart { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    //public enum AxisNames
    //{
    //    CameraY,
    //    LiftZ,
    //    ReviewX,
    //    ReviewY,
    //    StageX,
    //    UnitX,
    //    UnitY,
    //    UnitZ,
    //    UVWT,
    //    UVWX,
    //    UVWY,
    //}
    //public enum AxisComm
    //{
    //    None,
    //    MinPos,
    //    MaxPos,
    //    Load,
    //    Align,
    //    Contact,
    //    Scan,
    //    Review,
    //    Unload,
    //}

    public enum CommandResult
    {
        ACK,
        NAK
    }

    ///// <summary>
    ///// CIM 에서 장비로 주는 명령
    ///// </summary>
    //[Serializable]
    //public enum GlassMVStatus
    //{
    //    None,
    //    LoadRequest,
    //    LoadWait,
    //    ExchangeRequest,
    //    UnloadRequest,
    //    UnloadWait,
    //    CancelRequest,
    //}

    /// <summary>
    /// 장비 상태
    /// </summary>
    [Serializable]
    public enum EQPStatus
    {
        RUN,
        IDLE,
        DOWN,
        Setup,
        ALARM,
        PAUSE,
        ERROR,
        ProcessDown
    }

    public enum ScanTurn //EQ 스캔 회전
    {
        Normal, Left_90, Right_90
    }


    [Serializable]
    public enum CIMtoEQ_CMD //CIM → EQ PGM
    {
        None,
        LoadDone,   //Load Ready 완료
        UnloadWait,
        UnloadDone, //Unload Ready 완료
        GlassInfo, //검사 Glass 정보 전송
        DataReq,
        Pause,
        Error,
        Response,   //그냥 응답용
    }

    [Serializable]
    public enum EQtoCIM_CMD //EQ → CIM PGM
    {
        None,
        SendData,
        LoadReadyStart,
        LoadReady,//Glass Load 요청
        Loading,
        LoadDone,
        UnloadReadyStart,
        UnloadReady,//Glass Unload 요청
        Exchange,
        ExchangeDone,   //s1F67 (현재 Glass 상태 보고)
        Unload,
        UnloadDone,
        Alarm,
        AlarmClear,
        ScanStart,
        GlassInOut,
        Pause,
        Error,
    }

    [Serializable]
    public enum CONSOLEtoCIM_CMD //CONSOLE → CIM PGM
    {
        None,
        RecipeCreate,
        RecipeChange,
        RecipeDelete,
        RecipeRename,
    }

    [Serializable]
    public class EQCIMData : TENetData
    {
        public EQtoCIMData EQtoCIM { get; set; }
        public CIMtoEQData CIMtoEQ { get; set; }
    }

    [Serializable]
    public class EQtoCIMData //EQ → CIM PGM
    {
        public EQtoCIMData()
        {
            Alarm_Item = new AlarmInfo();
            CMD = new EQtoCIM_CMD();
            AutoRun = false;
            GlassIn = false;
            eqpStatus = EQPStatus.IDLE;
            scanDir = ScanDir.Forward;
            scanTurn = ScanTurn.Normal;

            IonizerInfos = new IonizerInfo[3];
            for (int i = 0; i < IonizerInfos.Length; i++)
                IonizerInfos[i] = new IonizerInfo();
        }

        public AlarmInfo Alarm_Item { get; set; }      //알람 항목
        public IonizerInfo[] IonizerInfos { get; set; } //Ionizer 값
        public EQtoCIM_CMD CMD { get; set; }
        public bool AutoRun { get; set; }            //EQ 오토런 상태
        public bool GlassIn { get; set; }            //EQ에 Glass가 있는지 확인
        public EQPStatus eqpStatus { get; set; }
        public ScanDir scanDir { get; set; }
        public ScanTurn scanTurn { get; set; }

        public EQtoCIM_CMD GlassTransferStatus { get; set; }
    }

    [Serializable]
    public class CIMtoEQData //CIM → EQ PGM
    {
        public CIMtoEQData()
        {
            CMD = new CIMtoEQ_CMD();
            GlassInfo = new ECSData();
        }
        public CIMtoEQ_CMD CMD { get; set; }
        public ushort UNITID { get; set; }
        public byte STAGEID { get; set; }

        /// <summary>
        /// 0 : Exist
        /// 1 : Not Exist
        /// </summary>
        public string GLSSTATUS { get; set; }   //Glass 데이터 받았는지 확인

        public ECSData GlassInfo { get; set; }
    }

    [Serializable]
    public class CONSOLEtoCIMData : TENetData //CONSOLE → CIM PGM
    {
        public CONSOLEtoCIMData()
        {
            Alarm_Item = new AlarmInfo();
            CMD = new CONSOLEtoCIM_CMD();
        }

        public CONSOLEtoCIM_CMD CMD { get; set; }
        public AlarmInfo Alarm_Item { get; set; }      //알람 항목
        public RecipeSet RecipeItem { get; set; }
    }
    //#endregion
}
