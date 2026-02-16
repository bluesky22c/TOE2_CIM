using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM
{
    public partial class ClientCIM
    {
        //상태 변경 이벤트
        public event EventHandler<GLSMVArgs> GlassMoveEvent = delegate { };

        //상태 바꿀때 UI 변경되는 이벤트
        public event EventHandler<CIMArgs> OnRCSModeChanged = delegate { };
        public event EventHandler<CIMArgs> OnOPModeChanged = delegate { };
        public event EventHandler<CIMArgs> OnGLSMVChanged = delegate { };
        public event EventHandler<CIMArgs> OnEQStatusChanged = delegate { };
        public event EventHandler UpdateUI = delegate { };

        //EQ data 요청 시 사용하는 이벤트
        public event EventHandler<CimCommData> ReqEQData = delegate { };

        //Alarm
        /// <summary>
        /// 알람 대화 상자를 띄웁니다.
        /// </summary>
        public event EventHandler<AlarmArgs> CimAlarmSet = delegate { };

        public event EventHandler<RecipeArgs> RecipeChangePopUp = delegate { };
        public event EventHandler<CIMArgs> HostMessagePopup = delegate { };
        public event EventHandler<CassetteArgs> ShowCassetteMAP = delegate { };

        public event EventHandler<CIMArgs> OnChangeRecipe = delegate { };
        
        public void RefreshUI()
        {
            UpdateUI?.Invoke(this, EventArgs.Empty);
        }

    }

    public class CIMArgs :EventArgs
    {
        public string Message { get; set; }
        public string RecipeName { get; set; }
    }

    public class EQPArgs : EventArgs
    {
        public EQPSTATUS.EQPSTATUSEnum CurStatus { get; set; } = EQPSTATUS.EQPSTATUSEnum.IDLE;
        public EQPSTATUS.EQPSTATUSEnum ChangeStatus { get; set; } = EQPSTATUS.EQPSTATUSEnum.IDLE;
        public string Message { get; set; }
    }

    public class GLSMVArgs : EventArgs
    {
        public GLSMVSTATUS.GLSMVSTATUSEnum CurStatus { get; set; } = GLSMVSTATUS.GLSMVSTATUSEnum.LR;
        public GLSMVSTATUS.GLSMVSTATUSEnum ChangeStatus { get; set; } = GLSMVSTATUS.GLSMVSTATUSEnum.LR;

        public string Message { get; set; }
    }

    //public class GlassArg : EventArgs
    //{
    //    public GlassOrder GlassOrder { get; set; }
    //}

    public class AlarmArgs : EventArgs
    {
        public AlarmInfo Alarm { get; set; }
    }

    public class CassetteArgs : EventArgs
    {
        public List<CassetteMAP> CassetteMap { get; set; } = new List<CassetteMAP>();
    }

    public class RecipeArgs : EventArgs
    {
        public bool ACK { get; set; } = true;
        public RECIPEIDCMD.RECIPEIDCMDEnum RecipeCMD { get; set; } = RECIPEIDCMD.RECIPEIDCMDEnum.Built;
        public ByWho ByWho { get; set; } = ByWho.None;
        public RecipeSet RecipeSet { get; set; } = new RecipeSet();
    }
}
