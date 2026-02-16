using Elp.Net;
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
    public enum FlowResult
    {
        Success,
        Fail,
        Timeout,
        Cancel
    }

    public enum CommandResult
    {
        ACK,
        NAK
    }


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

    [Serializable]
    public enum EQPReadyStatus
    {
        None,
        LoadyReady,
        UnloadReady,
        ExchangeReady,
    }

    [Serializable]
    public enum EQPtoCIM //Flow test
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

        // S6F67 Glass Moving Status 관련
        GLSMV_LR,   // Load Request
        GLSMV_UR,   // Unload Request
        GLSMV_ER,   // Exchange Request
        GLSMV_UW,   // Unload Wait
        GLSMV_LW,   // Load Wait
        GLSMV_CR,   // Cancel Request
    }

    [Serializable]
    public enum CIMtoEQP //Flow test
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
    public class CimCommData : TENetData
    {
        public EQPtoCIMData ToCIM { get; set; } = new EQPtoCIMData();
        public CIMtoEQPData ToConsole { get; set; } = new CIMtoEQPData();
    }

    [Serializable]
    public class EQPtoCIMData //EQP → CIM
    {
        public EQPtoCIMData()
        {
            Command = EQPtoCIM.None;
            eqpStatus = EQPStatus.DOWN;
            //scanDir = ScanDir.Forward;
            //scanTurn = ScanTurn.Normal;

            for (int i = 0; i < Ionizer.Length; i++)
            {
                Ionizer[i] = new IonizerInfo();
            }

            GlassJudgeCode = "G";
            Error = false;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public EQPtoCIM Command { get; set; }

        public List<AlarmInfo> AlarmList { get; set; } = new List<AlarmInfo>();
        //public AlarmInfo AlarmInfo { get; set; }      //알람 항목

        //public IonizerInfo[] IonizerInfos => ControlData?.Ionizer; //Ionizer 값
        public bool AutoRun { get; set; }           //EQ 오토런 상태
        public bool GlassIn { get; set; }            //EQ에 Glass가 있는지 확인
        public EQPStatus eqpStatus { get; set; } = EQPStatus.DOWN;
        public EQPReadyStatus EqpReadyStatus { get; set; }
        public IonizerInfo[] Ionizer { get; set; } = new IonizerInfo[3];
        //public ScanDir scanDir { get; set; }
        //public ScanTurn scanTurn { get; set; }
        public string GlassPanelJudgeA { get; set; } = null!;
        public string GlassPanelJudgeB { get; set; } = null!;
        public string GlassJudgeCode { get; set; } = null!;
        public RecipeSet RecipeItem { get; set; } = null!;
        /// <summary>
        /// A cut apd data
        /// </summary>
        public ActualProcessData AcutAPD { get; set; } = null!;
        /// <summary>
        /// B cut apd data
        /// </summary>
        public ActualProcessData BcutAPD { get; set; } = null!;
        public bool Error { get; set; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    public class CIMtoEQPData //CIM → EQP
    {
        public CIMtoEQPData()
        {
            Command = CIMtoEQP.None;
            GlassInfo = new CommonGlassInfo[2];
            Error = false;
        }
        public CIMtoEQP Command { get; set; }
        public ushort UNITID { get; set; }
        public byte STAGEID { get; set; }
        //public bool Online { get; set; }

        /// <summary>
        /// 0 : glass 있음, 1 : glass 없음
        /// </summary>
        public bool ContainGlass { get; set; }
        public CommonGlassInfo[] GlassInfo { get; set; }  //Glass Data
        public CIMRecipe RecipeItem { get; set; } = null!;
        public bool Error { get; set; } //Nomal Status 항목으로 사용.
        public bool NomalStatus { get; set; }
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

}
