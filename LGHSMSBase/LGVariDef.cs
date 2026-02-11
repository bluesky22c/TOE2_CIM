using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    /// <summary>
    /// U1[] 과 같은 경우 아래와 같입 입력
    /// </summary>
    //[Serializable]
    //public class ABS : UArrayType
    //{
    //    public ABS()
    //        : base(ItemFormat.UInt8, 10)
    //    {
    //        Value = new int[10];
    //    }

    //    public ABS(int[] value)
    //        : base(ItemFormat.UInt8, 10)
    //    {
    //        Value = value;
    //    }
    //}

    public partial class TIME : StringType
    {
        public void SetTime(DateTime dateTime)
        {
            Value = dateTime.ToString("yyyyMMddHHmmss");
        }
        public static implicit operator TIME(DateTime s) => new TIME(s.ToString("yyyyMMddHHmmss"));

    }

    public partial class RCS : StringType
    {
        public enum RCSEnum { ONLineRemote, OnLineLocal, OffLine };
        static private Descs<RCSEnum> _codes = new Descs<RCSEnum>();

        static public Descs<RCSEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RCSEnum>(RCSEnum.ONLineRemote, "Online Remote", "0"));
                    _codes.Items.Add(new Desc<RCSEnum>(RCSEnum.OnLineLocal, "Online Local", "1"));
                    _codes.Items.Add(new Desc<RCSEnum>(RCSEnum.OffLine, "Offline", "2"));
                }
                return _codes;
            }
        }

        public static implicit operator RCS(RCSEnum s) => new RCS((string)Codes[s].Value);
        public static implicit operator RCSEnum(RCS s) => Codes[s.Value].Code;
    }

    public partial class OPMODE : StringType
    {

        public enum OPMODEEnum { 
            Normal,
            SequenceTransfer,
            CleanOut,
            Test,
            Transfer,
            ParticleCheck,
            ReverseMode,
            RepairMode,
            Manual,
            Restoration
        };
        static private Descs<OPMODEEnum> _codes = new Descs<OPMODEEnum>();

        static public Descs<OPMODEEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.Normal, "Normal", "1"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.SequenceTransfer, "Sequence Transfer", "2"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.CleanOut, "CleanOut", "3"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.Test, "Test", "4"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.Transfer, "Transfer", "5"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.ParticleCheck, "Particle Check", "6"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.ReverseMode, "Reverse Mode", "7"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.RepairMode, "RepairMode", "8"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.Manual, "Manual", "9"));
                    _codes.Items.Add(new Desc<OPMODEEnum>(OPMODEEnum.Restoration, "Restoration", "A"));
                }
                return _codes;
            }
        }

        public static implicit operator OPMODE(OPMODEEnum s) => new OPMODE((string)Codes[s].Value);
        public static implicit operator OPMODEEnum(OPMODE s) => Codes[s.Value].Code;

        public const string OPNormal = "1";
        public const string OPSequenceTransfer = "2";
        public const string OPCleanOut = "3";
        public const string OPTest = "4";
        public const string OPTransfer = "5";
        public const string OPParticleCheck = "6";
        public const string OPReverseMode = "7";
        public const string OPRepairMode = "8";
        public const string OPManual = "9";
        public const string OPRestoration = "A";

    }

    public partial class EQPSTATUS : StringType
    {
        public enum EQPSTATUSEnum
        {
            RUN,
            Experiment,
            Development,
            ModelChange,
            Monitoring,
            IDLE,
            DOWN,
            ProcessDown,
        };
        static private Descs<EQPSTATUSEnum> _codes = new Descs<EQPSTATUSEnum>();

        static public Descs<EQPSTATUSEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.RUN, "RUN", "R"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.Experiment, "Experiment", "E"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.Development, "Development", "V"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.ModelChange, "ModelChange", "C"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.Monitoring, "Monitoring", "M"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.IDLE, "IDLE", "I"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.DOWN, "DOWN", "D"));
                    _codes.Items.Add(new Desc<EQPSTATUSEnum>(EQPSTATUSEnum.ProcessDown, "ProcessDown", "P"));
                }
                return _codes;
            }
        }

        public static implicit operator EQPSTATUS(EQPSTATUSEnum s) => new EQPSTATUS((string)Codes[s].Value);
        public static implicit operator EQPSTATUSEnum(EQPSTATUS s) => Codes[s.Value].Code;

        public const string RUN = "R";
        public const string Experiment = "E";
        public const string Development = "V";
        public const string ModelChange = "C";
        public const string Monitoring = "M";
        public const string IDLE = "I";
        public const string DOWN = "D";
        public const string ProcessDown = "P";
    }

    public partial class GLSMVSTATUS : StringType
    {
        public enum GLSMVSTATUSEnum 
        {
            LW, //Glass Load Wait Request
            UW, //Glass Unload Wait Request
            LR, //Glass Load Request
            UR, //Glass Unload Request
            ER, //Glass Exchange Request
            CR, //Cancel Request
        };
        static private Descs<GLSMVSTATUSEnum> _codes = new Descs<GLSMVSTATUSEnum>();

        static public Descs<GLSMVSTATUSEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.LW, "Load Wait", "LW"));
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.UW, "Unload Wait", "UW"));
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.LR, "Load Request", "LR"));
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.UR, "Unload Request", "UR"));
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.ER, "Exchange Request", "ER"));
                    _codes.Items.Add(new Desc<GLSMVSTATUSEnum>(GLSMVSTATUSEnum.CR, "Cancel Request", "CR"));
                }
                return _codes;
            }
        }

        public static implicit operator GLSMVSTATUS(GLSMVSTATUSEnum s) => new GLSMVSTATUS((string)Codes[s].Value);
        public static implicit operator GLSMVSTATUSEnum(GLSMVSTATUS s) => Codes[s.Value].Code;
    }

    public partial class MODECODE : StringType
    {
        public enum MODECODEEnum { ByHost, ByOperator };
        static private Descs<MODECODEEnum> _codes = new Descs<MODECODEEnum>();

        static public Descs<MODECODEEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<MODECODEEnum>(MODECODEEnum.ByHost, "ByHost", "N"));
                    _codes.Items.Add(new Desc<MODECODEEnum>(MODECODEEnum.ByOperator, "ByOperator", "F"));
                }
                return _codes;
            }
        }

        public static implicit operator MODECODE(MODECODEEnum s) => new MODECODE((string)Codes[s].Value);
        public static implicit operator MODECODEEnum(MODECODE s) => Codes[s.Value].Code;
    }

    /// <summary>
    ///  W : Wait equipment’s Sampling Data
    ///  S : Lot start without equipment’s Sampling Data
    /// </summary>
    public partial class SAMPLINGFLAG : StringType
    {
        public enum SAMPLINGFLAGEnum { WAIT, START };
        static private Descs<SAMPLINGFLAGEnum> _codes = new Descs<SAMPLINGFLAGEnum>();

        static public Descs<SAMPLINGFLAGEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<SAMPLINGFLAGEnum>(SAMPLINGFLAGEnum.WAIT, "Wait data", "W"));
                    _codes.Items.Add(new Desc<SAMPLINGFLAGEnum>(SAMPLINGFLAGEnum.START, "Lot Start", "S"));
                }
                return _codes;
            }
        }

        public static implicit operator SAMPLINGFLAG(SAMPLINGFLAGEnum s) => new SAMPLINGFLAG((string)Codes[s].Value);
        public static implicit operator SAMPLINGFLAGEnum(SAMPLINGFLAG s) => Codes[s.Value].Code;
    }
    public partial class RECIPEIDTYPE : StringType
    {
        public enum RECIPEIDTYPEEnum 
        { 
            Equipment, 
            Machine,
            Unit,
            Special
        };
        static private Descs<RECIPEIDTYPEEnum> _codes = new Descs<RECIPEIDTYPEEnum>();

        static public Descs<RECIPEIDTYPEEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RECIPEIDTYPEEnum>(RECIPEIDTYPEEnum.Equipment, "Equipment Recipe", "E"));
                    _codes.Items.Add(new Desc<RECIPEIDTYPEEnum>(RECIPEIDTYPEEnum.Machine, "Machine Recipe", "M"));
                    _codes.Items.Add(new Desc<RECIPEIDTYPEEnum>(RECIPEIDTYPEEnum.Unit, "Unit Recipe", "U"));
                    _codes.Items.Add(new Desc<RECIPEIDTYPEEnum>(RECIPEIDTYPEEnum.Special, "Special Recipe", "1"));
                }
                return _codes;
            }
        }

        public static implicit operator RECIPEIDTYPE(RECIPEIDTYPEEnum s) => new RECIPEIDTYPE((string)Codes[s].Value);
        public static implicit operator RECIPEIDTYPEEnum(RECIPEIDTYPE s) => Codes[s.Value].Code;
    }

    public partial class RECIPEIDSTATUS : StringType
    {
        public enum RECIPEIDSTATUSEnum
        {
            Running,
            Idle,
            Disable,
        };
        static private Descs<RECIPEIDSTATUSEnum> _codes = new Descs<RECIPEIDSTATUSEnum>();

        static public Descs<RECIPEIDSTATUSEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RECIPEIDSTATUSEnum>(RECIPEIDSTATUSEnum.Running, "Using", "R"));
                    _codes.Items.Add(new Desc<RECIPEIDSTATUSEnum>(RECIPEIDSTATUSEnum.Idle, "Editable", "I"));
                    _codes.Items.Add(new Desc<RECIPEIDSTATUSEnum>(RECIPEIDSTATUSEnum.Disable, "Locked", "D"));
                }
                return _codes;
            }
        }

        public static implicit operator RECIPEIDSTATUS(RECIPEIDSTATUSEnum s) => new RECIPEIDSTATUS((string)Codes[s].Value);
        public static implicit operator RECIPEIDSTATUSEnum(RECIPEIDSTATUS s) => Codes[s.Value].Code;
    }

    public partial class RECIPEIDCMD : StringType
    {
        public enum RECIPEIDCMDEnum
        {
            Built,
            Change,
            Delete,
            Rename,
        };
        static private Descs<RECIPEIDCMDEnum> _codes = new Descs<RECIPEIDCMDEnum>();

        static public Descs<RECIPEIDCMDEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RECIPEIDCMDEnum>(RECIPEIDCMDEnum.Built, "Built", "B"));
                    _codes.Items.Add(new Desc<RECIPEIDCMDEnum>(RECIPEIDCMDEnum.Change, "Change", "C"));
                    _codes.Items.Add(new Desc<RECIPEIDCMDEnum>(RECIPEIDCMDEnum.Delete, "Delete", "D"));
                    _codes.Items.Add(new Desc<RECIPEIDCMDEnum>(RECIPEIDCMDEnum.Rename, "Rename", "R"));
                }
                return _codes;
            }
        }

        public static implicit operator RECIPEIDCMD(RECIPEIDCMDEnum s) => new RECIPEIDCMD((string)Codes[s].Value);
        public static implicit operator RECIPEIDCMDEnum(RECIPEIDCMD s) => Codes[s.Value].Code;
    }
    public partial class ENDFLAG : StringType
    {
        public enum ENDFLAGEnum
        {
            NormalEnd,
            OperatorEnd,
            LastProcess,
            Rework,
        };
        static private Descs<ENDFLAGEnum> _codes = new Descs<ENDFLAGEnum>();

        static public Descs<ENDFLAGEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<ENDFLAGEnum>(ENDFLAGEnum.NormalEnd, "NormalEnd", "N"));
                    _codes.Items.Add(new Desc<ENDFLAGEnum>(ENDFLAGEnum.OperatorEnd, "OperatorEnd", "P"));
                    _codes.Items.Add(new Desc<ENDFLAGEnum>(ENDFLAGEnum.OperatorEnd, "LastProcess", "E"));
                    _codes.Items.Add(new Desc<ENDFLAGEnum>(ENDFLAGEnum.OperatorEnd, "Rework", "R"));
                }
                return _codes;
            }
        }

        public static implicit operator ENDFLAG(ENDFLAGEnum s) => new ENDFLAG((string)Codes[s].Value);
        public static implicit operator ENDFLAGEnum(ENDFLAG s) => Codes[s.Value].Code;
    }

    public partial class RESERVECD : StringType
    {
        public enum RESERVECDEnum
        {
            Reservation,
            Start,
        };
        static private Descs<RESERVECDEnum> _codes = new Descs<RESERVECDEnum>();

        static public Descs<RESERVECDEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RESERVECDEnum>(RESERVECDEnum.Reservation, "Reservation", "R"));
                    _codes.Items.Add(new Desc<RESERVECDEnum>(RESERVECDEnum.Start, "Start", "S"));
                }
                return _codes;
            }
        }

        public static implicit operator RESERVECD(RESERVECDEnum s) => new RESERVECD((string)Codes[s].Value);
        public static implicit operator RESERVECDEnum(RESERVECD s) => Codes[s.Value].Code;
    }

    public partial class RCMD : StringType
    {
        public enum RCMDEnum
        {
            PAUSE,
            RESUME,
        };
        static private Descs<RCMDEnum> _codes = new Descs<RCMDEnum>();

        static public Descs<RCMDEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<RCMDEnum>(RCMDEnum.PAUSE, "PAUSE", "PAUSE"));
                    _codes.Items.Add(new Desc<RCMDEnum>(RCMDEnum.RESUME, "RESUME", "RESUME"));
                }
                return _codes;
            }
        }

        public static implicit operator RCMD(RCMDEnum s) => new RCMD((string)Codes[s].Value);
        public static implicit operator RCMDEnum(RCMD s) => Codes[s.Value].Code;
    }
    public partial class REQOPTION : StringType
    {
        public enum REQOPTIONEnum
        {
            GLSCODE,
            GLSID,
            ALL,
        };
        static private Descs<REQOPTIONEnum> _codes = new Descs<REQOPTIONEnum>();

        static public Descs<REQOPTIONEnum> Codes
        {
            get
            {
                if (_codes.IsEmpty)
                {
                    _codes.Items.Add(new Desc<REQOPTIONEnum>(REQOPTIONEnum.GLSCODE, "GLSCODE", "C"));
                    _codes.Items.Add(new Desc<REQOPTIONEnum>(REQOPTIONEnum.GLSID, "GLSID", "I"));
                    _codes.Items.Add(new Desc<REQOPTIONEnum>(REQOPTIONEnum.ALL, "ALL", "A"));
                }
                return _codes;
            }
        }

        public static implicit operator REQOPTION(REQOPTIONEnum s) => new REQOPTION((string)Codes[s].Value);
        public static implicit operator REQOPTIONEnum(REQOPTION s) => Codes[s.Value].Code;
    }

    public partial class ALCD : BinaryType
    {
        //bit 설정하여 사용함
        public enum ALCDEnum
        {
            NotUsed = 0,
            PersonalSafety,
            EQPSafety,
            ParamControlWarning,
            ParamControlError,
            IrrError,
            EQPStatusWarning,
            AttentionFlag,
            DataIntegrity = 8,
        };

        public static byte SetCode(ALCDEnum cd = ALCDEnum.NotUsed)
        {
            byte result = 0b_0000_0000;
            byte occur = 0b_1000_0000;
            byte code = Convert.ToByte(cd);

            result |= occur;
            result |= code;

            return result;
        }

        public static byte ReleaseCode(byte alarm)
        {
            byte occur = 0b_1000_0000;

            //이미 release된 알람이면 그냥 리턴
            var shift = alarm >> 7;
            if (shift == 0)
            {
                return alarm;
            }

            alarm ^= occur;
            return alarm;
        }
    }

    public partial class ACKC1 : BinaryType
    {
        public enum ACKC1Enum { Accept, Error };
    }

    /// <summary>
    /// 0 = Accept
    /// 1 = Process Condition is not ready
    /// 2 = The recipe is not exist in equipment’s recipe list 
    /// 3 = Glass data is not accepted
    /// 4 = Process is not ready to load this glass 
    /// 5 = A, B Cut Recipe Mismatching
    /// </summary>
    public partial class ACKC2 : BinaryType
    {
        public enum ACK2Enum { Accept, NotAccept, NoRecipe, NoGlassData, NotReady, RecipeMismatching, };
    }

    public partial class ACKC2A : BinaryType
    {
        public enum ACKC2AEnum { Accept, Error };
    }
    public partial class ACKC6 : BinaryType
    {
        public enum ACKC6Enum { Accept, Error, AlreadyRelease };
    }
    public partial class ACKC6A : BinaryType
    {
        public enum ACKC6AEnum { Accept, NoGlass, NoCassette, LOTIDError, Busy };
    }
    public partial class ACKC7 : BinaryType
    {
        public enum ACKC7enum { Accept, Error };    //1 이상은 모두 Error
    }
    public partial class ACKC7S : BinaryType
    {
        public enum ACKC7Senum { Accept, Error };
    }
    public partial class ACKC7M : BinaryType
    {
        public enum ACKC7MEnum { Accept, Error, ProcessError };
    }
    public partial class REACK : BinaryType
    {
        public enum REACKEnum { Accept, NoPermission, Using, EQBusy, RollBack, NoRecipeID, LengthError, ParamError, RecipeExist};
        //RollBack : Recipe Version Error
    }
    public partial class PHACK : BinaryType
    {
        public enum PHACKEnum { Accept, NoPermission, LengthError, NoRecipeID, ParamError, SavedOtherUse };
    }
    public partial class CSSSTATUS : BinaryType
    {
        public enum CSSSTATUSEnum { Release, Set };
    }

    public class Descs<T> where T : Enum 
    {
        public List<Desc<T>> Items { get; set; } = new List<Desc<T>>();
        public bool IsEmpty => Items.Count == 0;

        public Desc<T> this[T code] => Items.Find(i=>i.Code.Equals(code));
        //public Desc<T> this[string name] => Items.Find(i=>i.Name.Equals(name));
        public Desc<T> this[object value] => Items.Find(i=>i.Value.Equals(value));
    }

    public class Desc<T> where T : Enum
    {
        public Desc()
        {

        }

        public Desc(T code, string name, object value)
        {
            Code = code;
            Name = name;
            Value = value;
        }

        public T Code { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
