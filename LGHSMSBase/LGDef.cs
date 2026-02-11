  
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// CSTID
    /// </summary>
    [Serializable]
    public partial class CSTID : StringType
    {
        public CSTID() : base(14, "CSTID") 
        {
            Value = "";
        }
        public CSTID(string value="") : base(14, "CSTID")
        {
            Value = value;
        }
        public static implicit operator string(CSTID s) => s.Value.Trim(' ');        
        public static implicit operator CSTID(string s) => new CSTID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTID
    /// </summary>
    [Serializable]
    public partial class LOTID : StringType
    {
        public LOTID() : base(16, "LOTID") 
        {
            Value = "";
        }
        public LOTID(string value="") : base(16, "LOTID")
        {
            Value = value;
        }
        public static implicit operator string(LOTID s) => s.Value.Trim(' ');        
        public static implicit operator LOTID(string s) => new LOTID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RECIPEID
    /// </summary>
    [Serializable]
    public partial class RECIPEID : StringType
    {
        public RECIPEID() : base(24, "RECIPEID") 
        {
            Value = "";
        }
        public RECIPEID(string value="") : base(24, "RECIPEID")
        {
            Value = value;
        }
        public static implicit operator string(RECIPEID s) => s.Value.Trim(' ');        
        public static implicit operator RECIPEID(string s) => new RECIPEID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSID
    /// </summary>
    [Serializable]
    public partial class GLSID : StringType
    {
        public GLSID() : base(16, "GLSID") 
        {
            Value = "";
        }
        public GLSID(string value="") : base(16, "GLSID")
        {
            Value = value;
        }
        public static implicit operator string(GLSID s) => s.Value.Trim(' ');        
        public static implicit operator GLSID(string s) => new GLSID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SLOTID
    /// </summary>
    [Serializable]
    public partial class SLOTID : StringType
    {
        public SLOTID() : base(3, "SLOTID") 
        {
            Value = "1";
        }
        public SLOTID(string value="1") : base(3, "SLOTID")
        {
            Value = value;
        }
        public static implicit operator string(SLOTID s) => s.Value.Trim(' ');        
        public static implicit operator SLOTID(string s) => new SLOTID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PORTID
    /// </summary>
    [Serializable]
    public partial class PORTID : BinaryType
    {
        public PORTID() : base(1, 1, "PORTID") { }
        public PORTID(byte value) : base(1, 1, "PORTID")
        {
            Value = value;
        }
        public static implicit operator byte(PORTID s) => s.Value;        
        public static implicit operator PORTID(byte s) => new PORTID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// UNITID
    /// </summary>
    [Serializable]
    public partial class UNITID : U2Type
    {
        public UNITID() : base(201, 1, "UNITID") { }
        public UNITID(ushort value) : base(201, 1, "UNITID")
        {
            Value = value;
        }
        public static implicit operator ushort(UNITID s) => s.Value;        
        public static implicit operator UNITID(ushort s) => new UNITID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SUBUNITID
    /// </summary>
    [Serializable]
    public partial class SUBUNITID : BinaryType
    {
        public SUBUNITID() : base(1, 1, "SUBUNITID") { }
        public SUBUNITID(byte value) : base(1, 1, "SUBUNITID")
        {
            Value = value;
        }
        public static implicit operator byte(SUBUNITID s) => s.Value;        
        public static implicit operator SUBUNITID(byte s) => new SUBUNITID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ALCD
    /// </summary>
    [Serializable]
    public partial class ALCD : BinaryType
    {
        public ALCD() : base(0, 1, "ALCD") { }
        public ALCD(byte value) : base(0, 1, "ALCD")
        {
            Value = value;
        }
        public static implicit operator byte(ALCD s) => s.Value;        
        public static implicit operator ALCD(byte s) => new ALCD(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ALID
    /// </summary>
    [Serializable]
    public partial class ALID : StringType
    {
        public ALID() : base(10, "ALID") 
        {
            Value = "0";
        }
        public ALID(string value="0") : base(10, "ALID")
        {
            Value = value;
        }
        public static implicit operator string(ALID s) => s.Value.Trim(' ');        
        public static implicit operator ALID(string s) => new ALID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ALTX
    /// </summary>
    [Serializable]
    public partial class ALTX : StringType
    {
        public ALTX() : base(120, "ALTX") 
        {
            Value = "";
        }
        public ALTX(string value="") : base(120, "ALTX")
        {
            Value = value;
        }
        public static implicit operator string(ALTX s) => s.Value.Trim(' ');        
        public static implicit operator ALTX(string s) => new ALTX(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// MDLN
    /// </summary>
    [Serializable]
    public partial class MDLN : StringType
    {
        public MDLN() : base(8, "MDLN") 
        {
            Value = "TOE";
        }
        public MDLN(string value="TOE") : base(8, "MDLN")
        {
            Value = value;
        }
        public static implicit operator string(MDLN s) => s.Value.Trim(' ');        
        public static implicit operator MDLN(string s) => new MDLN(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SOFTREV
    /// </summary>
    [Serializable]
    public partial class SOFTREV : StringType
    {
        public SOFTREV() : base(6, "SOFTREV") 
        {
            Value = "V100";
        }
        public SOFTREV(string value="V100") : base(6, "SOFTREV")
        {
            Value = value;
        }
        public static implicit operator string(SOFTREV s) => s.Value.Trim(' ');        
        public static implicit operator SOFTREV(string s) => new SOFTREV(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// EQPSTATUS
    /// </summary>
    [Serializable]
    public partial class EQPSTATUS : StringType
    {
        public EQPSTATUS() : base(1, "EQPSTATUS") 
        {
            Value = "I";
        }
        public EQPSTATUS(string value="I") : base(1, "EQPSTATUS")
        {
            Value = value;
        }
        public static implicit operator string(EQPSTATUS s) => s.Value.Trim(' ');        
        public static implicit operator EQPSTATUS(string s) => new EQPSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// IONIZERID
    /// </summary>
    [Serializable]
    public partial class IONIZERID : BinaryType
    {
        public IONIZERID() : base(1, 1, "IONIZERID") { }
        public IONIZERID(byte value) : base(1, 1, "IONIZERID")
        {
            Value = value;
        }
        public static implicit operator byte(IONIZERID s) => s.Value;        
        public static implicit operator IONIZERID(byte s) => new IONIZERID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// IONIZERSTATUS
    /// </summary>
    [Serializable]
    public partial class IONIZERSTATUS : StringType
    {
        public IONIZERSTATUS() : base(1, "IONIZERSTATUS") 
        {
            Value = "R";
        }
        public IONIZERSTATUS(string value="R") : base(1, "IONIZERSTATUS")
        {
            Value = value;
        }
        public static implicit operator string(IONIZERSTATUS s) => s.Value.Trim(' ');        
        public static implicit operator IONIZERSTATUS(string s) => new IONIZERSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC1
    /// </summary>
    [Serializable]
    public partial class ACKC1 : BinaryType
    {
        public ACKC1() : base(0, 1, "ACKC1") { }
        public ACKC1(byte value) : base(0, 1, "ACKC1")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC1 s) => s.Value;        
        public static implicit operator ACKC1(byte s) => new ACKC1(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC2
    /// </summary>
    [Serializable]
    public partial class ACKC2 : BinaryType
    {
        public ACKC2() : base(0, 1, "ACKC2") { }
        public ACKC2(byte value) : base(0, 1, "ACKC2")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC2 s) => s.Value;        
        public static implicit operator ACKC2(byte s) => new ACKC2(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC2A
    /// </summary>
    [Serializable]
    public partial class ACKC2A : BinaryType
    {
        public ACKC2A() : base(0, 1, "ACKC2A") { }
        public ACKC2A(byte value) : base(0, 1, "ACKC2A")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC2A s) => s.Value;        
        public static implicit operator ACKC2A(byte s) => new ACKC2A(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSCODE
    /// </summary>
    [Serializable]
    public partial class GLSCODE : U2Type
    {
        public GLSCODE() : base(2, 1, "GLSCODE") { }
        public GLSCODE(ushort value) : base(2, 1, "GLSCODE")
        {
            Value = value;
        }
        public static implicit operator ushort(GLSCODE s) => s.Value;        
        public static implicit operator GLSCODE(ushort s) => new GLSCODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// TIME
    /// </summary>
    [Serializable]
    public partial class TIME : StringType
    {
        public TIME() : base(14, "TIME") 
        {
            Value = "2.02106E+13";
        }
        public TIME(string value="2.02106E+13") : base(14, "TIME")
        {
            Value = value;
        }
        public static implicit operator string(TIME s) => s.Value.Trim(' ');        
        public static implicit operator TIME(string s) => new TIME(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RCS 0:Online Remote, 1: Online Local, 2: Offline
    /// </summary>
    [Serializable]
    public partial class RCS : StringType
    {
        public RCS() : base(1, "RCS 0:Online Remote, 1: Online Local, 2: Offline") 
        {
            Value = "2";
        }
        public RCS(string value="2") : base(1, "RCS 0:Online Remote, 1: Online Local, 2: Offline")
        {
            Value = value;
        }
        public static implicit operator string(RCS s) => s.Value.Trim(' ');        
        public static implicit operator RCS(string s) => new RCS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// OPMODE 1:Normal
    /// </summary>
    [Serializable]
    public partial class OPMODE : StringType
    {
        public OPMODE() : base(1, "OPMODE 1:Normal") 
        {
            Value = "1";
        }
        public OPMODE(string value="1") : base(1, "OPMODE 1:Normal")
        {
            Value = value;
        }
        public static implicit operator string(OPMODE s) => s.Value.Trim(' ');        
        public static implicit operator OPMODE(string s) => new OPMODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ENDFLAG N:Normal, P:OperatorEnd, E:LastProcess, R:Rework
    /// </summary>
    [Serializable]
    public partial class ENDFLAG : StringType
    {
        public ENDFLAG() : base(1, "ENDFLAG N:Normal, P:OperatorEnd, E:LastProcess, R:Rework") 
        {
            Value = "N";
        }
        public ENDFLAG(string value="N") : base(1, "ENDFLAG N:Normal, P:OperatorEnd, E:LastProcess, R:Rework")
        {
            Value = value;
        }
        public static implicit operator string(ENDFLAG s) => s.Value.Trim(' ');        
        public static implicit operator ENDFLAG(string s) => new ENDFLAG(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// CSSSTATUS
    /// </summary>
    [Serializable]
    public partial class CSSSTATUS : BinaryType
    {
        public CSSSTATUS() : base(0, 1, "CSSSTATUS") { }
        public CSSSTATUS(byte value) : base(0, 1, "CSSSTATUS")
        {
            Value = value;
        }
        public static implicit operator byte(CSSSTATUS s) => s.Value;        
        public static implicit operator CSSSTATUS(byte s) => new CSSSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// STAGEID
    /// </summary>
    [Serializable]
    public partial class STAGEID : BinaryType
    {
        public STAGEID() : base(1, 1, "STAGEID") { }
        public STAGEID(byte value) : base(1, 1, "STAGEID")
        {
            Value = value;
        }
        public static implicit operator byte(STAGEID s) => s.Value;        
        public static implicit operator STAGEID(byte s) => new STAGEID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// HOSTMSG
    /// </summary>
    [Serializable]
    public partial class HOSTMSG : StringType
    {
        public HOSTMSG() : base(80, "HOSTMSG") 
        {
            Value = "";
        }
        public HOSTMSG(string value="") : base(80, "HOSTMSG")
        {
            Value = value;
        }
        public static implicit operator string(HOSTMSG s) => s.Value.Trim(' ');        
        public static implicit operator HOSTMSG(string s) => new HOSTMSG(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// REQOPTION C:GlassCode, I:GlassID, A:Both
    /// </summary>
    [Serializable]
    public partial class REQOPTION : StringType
    {
        public REQOPTION() : base(1, "REQOPTION C:GlassCode, I:GlassID, A:Both") 
        {
            Value = "C";
        }
        public REQOPTION(string value="C") : base(1, "REQOPTION C:GlassCode, I:GlassID, A:Both")
        {
            Value = value;
        }
        public static implicit operator string(REQOPTION s) => s.Value.Trim(' ');        
        public static implicit operator REQOPTION(string s) => new REQOPTION(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PRCID
    /// </summary>
    [Serializable]
    public partial class PRCID : StringType
    {
        public PRCID() : base(8, "PRCID") 
        {
            Value = "";
        }
        public PRCID(string value="") : base(8, "PRCID")
        {
            Value = value;
        }
        public static implicit operator string(PRCID s) => s.Value.Trim(' ');        
        public static implicit operator PRCID(string s) => new PRCID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSODR 0:First, 1:Normal, 2:Last, 3:Only
    /// </summary>
    [Serializable]
    public partial class GLSODR : StringType
    {
        public GLSODR() : base(1, "GLSODR 0:First, 1:Normal, 2:Last, 3:Only") 
        {
            Value = "";
        }
        public GLSODR(string value="") : base(1, "GLSODR 0:First, 1:Normal, 2:Last, 3:Only")
        {
            Value = value;
        }
        public static implicit operator string(GLSODR s) => s.Value.Trim(' ');        
        public static implicit operator GLSODR(string s) => new GLSODR(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSJUDGE
    /// </summary>
    [Serializable]
    public partial class GLSJUDGE : StringType
    {
        public GLSJUDGE() : base(2, "GLSJUDGE") 
        {
            Value = "";
        }
        public GLSJUDGE(string value="") : base(2, "GLSJUDGE")
        {
            Value = value;
        }
        public static implicit operator string(GLSJUDGE s) => s.Value.Trim(' ');        
        public static implicit operator GLSJUDGE(string s) => new GLSJUDGE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PNLIF
    /// </summary>
    [Serializable]
    public partial class PNLIF : StringType
    {
        public PNLIF() : base(2000, "PNLIF") 
        {
            Value = "";
        }
        public PNLIF(string value="") : base(2000, "PNLIF")
        {
            Value = value;
        }
        public static implicit operator string(PNLIF s) => s.Value.Trim(' ');        
        public static implicit operator PNLIF(string s) => new PNLIF(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SUBMDLIF
    /// </summary>
    [Serializable]
    public partial class SUBMDLIF : StringType
    {
        public SUBMDLIF() : base(2000, "SUBMDLIF") 
        {
            Value = "";
        }
        public SUBMDLIF(string value="") : base(2000, "SUBMDLIF")
        {
            Value = value;
        }
        public static implicit operator string(SUBMDLIF s) => s.Value.Trim(' ');        
        public static implicit operator SUBMDLIF(string s) => new SUBMDLIF(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// REINPUT
    /// </summary>
    [Serializable]
    public partial class REINPUT : BinaryType
    {
        public REINPUT() : base(0, 1, "REINPUT") { }
        public REINPUT(byte value) : base(0, 1, "REINPUT")
        {
            Value = value;
        }
        public static implicit operator byte(REINPUT s) => s.Value;        
        public static implicit operator REINPUT(byte s) => new REINPUT(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSTHICK
    /// </summary>
    [Serializable]
    public partial class GLSTHICK : StringType
    {
        public GLSTHICK() : base(5, "GLSTHICK") 
        {
            Value = "";
        }
        public GLSTHICK(string value="") : base(5, "GLSTHICK")
        {
            Value = value;
        }
        public static implicit operator string(GLSTHICK s) => s.Value.Trim(' ');        
        public static implicit operator GLSTHICK(string s) => new GLSTHICK(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PARTNUM
    /// </summary>
    [Serializable]
    public partial class PARTNUM : StringType
    {
        public PARTNUM() : base(14, "PARTNUM") 
        {
            Value = "";
        }
        public PARTNUM(string value="") : base(14, "PARTNUM")
        {
            Value = value;
        }
        public static implicit operator string(PARTNUM s) => s.Value.Trim(' ');        
        public static implicit operator PARTNUM(string s) => new PARTNUM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PRODTYPE
    /// </summary>
    [Serializable]
    public partial class PRODTYPE : StringType
    {
        public PRODTYPE() : base(12, "PRODTYPE") 
        {
            Value = "";
        }
        public PRODTYPE(string value="") : base(12, "PRODTYPE")
        {
            Value = value;
        }
        public static implicit operator string(PRODTYPE s) => s.Value.Trim(' ');        
        public static implicit operator PRODTYPE(string s) => new PRODTYPE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ATTRIBUTE
    /// </summary>
    [Serializable]
    public partial class ATTRIBUTE : StringType
    {
        public ATTRIBUTE() : base(12, "ATTRIBUTE") 
        {
            Value = "";
        }
        public ATTRIBUTE(string value="") : base(12, "ATTRIBUTE")
        {
            Value = value;
        }
        public static implicit operator string(ATTRIBUTE s) => s.Value.Trim(' ');        
        public static implicit operator ATTRIBUTE(string s) => new ATTRIBUTE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSTYPE
    /// </summary>
    [Serializable]
    public partial class GLSTYPE : StringType
    {
        public GLSTYPE() : base(1, "GLSTYPE") 
        {
            Value = "";
        }
        public GLSTYPE(string value="") : base(1, "GLSTYPE")
        {
            Value = value;
        }
        public static implicit operator string(GLSTYPE s) => s.Value.Trim(' ');        
        public static implicit operator GLSTYPE(string s) => new GLSTYPE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// KEYID
    /// </summary>
    [Serializable]
    public partial class KEYID : StringType
    {
        public KEYID() : base(32, "KEYID") 
        {
            Value = "";
        }
        public KEYID(string value="") : base(32, "KEYID")
        {
            Value = value;
        }
        public static implicit operator string(KEYID s) => s.Value.Trim(' ');        
        public static implicit operator KEYID(string s) => new KEYID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LSDITEM
    /// </summary>
    [Serializable]
    public partial class LSDITEM : StringType
    {
        public LSDITEM() : base(24, "LSDITEM") 
        {
            Value = "";
        }
        public LSDITEM(string value="") : base(24, "LSDITEM")
        {
            Value = value;
        }
        public static implicit operator string(LSDITEM s) => s.Value.Trim(' ');        
        public static implicit operator LSDITEM(string s) => new LSDITEM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LSDVALUE
    /// </summary>
    [Serializable]
    public partial class LSDVALUE : StringType
    {
        public LSDVALUE() : base(40, "LSDVALUE") 
        {
            Value = "";
        }
        public LSDVALUE(string value="") : base(40, "LSDVALUE")
        {
            Value = value;
        }
        public static implicit operator string(LSDVALUE s) => s.Value.Trim(' ');        
        public static implicit operator LSDVALUE(string s) => new LSDVALUE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GSDITEM
    /// </summary>
    [Serializable]
    public partial class GSDITEM : StringType
    {
        public GSDITEM() : base(24, "GSDITEM") 
        {
            Value = "";
        }
        public GSDITEM(string value="") : base(24, "GSDITEM")
        {
            Value = value;
        }
        public static implicit operator string(GSDITEM s) => s.Value.Trim(' ');        
        public static implicit operator GSDITEM(string s) => new GSDITEM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GSDVALUE
    /// </summary>
    [Serializable]
    public partial class GSDVALUE : StringType
    {
        public GSDVALUE() : base(40, "GSDVALUE") 
        {
            Value = "";
        }
        public GSDVALUE(string value="") : base(40, "GSDVALUE")
        {
            Value = value;
        }
        public static implicit operator string(GSDVALUE s) => s.Value.Trim(' ');        
        public static implicit operator GSDVALUE(string s) => new GSDVALUE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ESDITEM
    /// </summary>
    [Serializable]
    public partial class ESDITEM : StringType
    {
        public ESDITEM() : base(24, "ESDITEM") 
        {
            Value = "";
        }
        public ESDITEM(string value="") : base(24, "ESDITEM")
        {
            Value = value;
        }
        public static implicit operator string(ESDITEM s) => s.Value.Trim(' ');        
        public static implicit operator ESDITEM(string s) => new ESDITEM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ESDVALUE
    /// </summary>
    [Serializable]
    public partial class ESDVALUE : StringType
    {
        public ESDVALUE() : base(40, "ESDVALUE") 
        {
            Value = "";
        }
        public ESDVALUE(string value="") : base(40, "ESDVALUE")
        {
            Value = value;
        }
        public static implicit operator string(ESDVALUE s) => s.Value.Trim(' ');        
        public static implicit operator ESDVALUE(string s) => new ESDVALUE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSMVSTATUS
    /// </summary>
    [Serializable]
    public partial class GLSMVSTATUS : StringType
    {
        public GLSMVSTATUS() : base(2, "GLSMVSTATUS") 
        {
            Value = "";
        }
        public GLSMVSTATUS(string value="") : base(2, "GLSMVSTATUS")
        {
            Value = value;
        }
        public static implicit operator string(GLSMVSTATUS s) => s.Value.Trim(' ');        
        public static implicit operator GLSMVSTATUS(string s) => new GLSMVSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSSTATUS
    /// </summary>
    [Serializable]
    public partial class GLSSTATUS : StringType
    {
        public GLSSTATUS() : base(1, "GLSSTATUS") 
        {
            Value = "";
        }
        public GLSSTATUS(string value="") : base(1, "GLSSTATUS")
        {
            Value = value;
        }
        public static implicit operator string(GLSSTATUS s) => s.Value.Trim(' ');        
        public static implicit operator GLSSTATUS(string s) => new GLSSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LEVEL
    /// </summary>
    [Serializable]
    public partial class LEVEL : StringType
    {
        public LEVEL() : base(1, "LEVEL") 
        {
            Value = "";
        }
        public LEVEL(string value="") : base(1, "LEVEL")
        {
            Value = value;
        }
        public static implicit operator string(LEVEL s) => s.Value.Trim(' ');        
        public static implicit operator LEVEL(string s) => new LEVEL(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// AACK
    /// </summary>
    [Serializable]
    public partial class AACK : BinaryType
    {
        public AACK() : base(0, 1, "AACK") { }
        public AACK(byte value) : base(0, 1, "AACK")
        {
            Value = value;
        }
        public static implicit operator byte(AACK s) => s.Value;        
        public static implicit operator AACK(byte s) => new AACK(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC10
    /// </summary>
    [Serializable]
    public partial class ACKC10 : BinaryType
    {
        public ACKC10() : base(0, 1, "ACKC10") { }
        public ACKC10(byte value) : base(0, 1, "ACKC10")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC10 s) => s.Value;        
        public static implicit operator ACKC10(byte s) => new ACKC10(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC5
    /// </summary>
    [Serializable]
    public partial class ACKC5 : BinaryType
    {
        public ACKC5() : base(0, 1, "ACKC5") { }
        public ACKC5(byte value) : base(0, 1, "ACKC5")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC5 s) => s.Value;        
        public static implicit operator ACKC5(byte s) => new ACKC5(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// CSTREADY
    /// </summary>
    [Serializable]
    public partial class CSTREADY : StringType
    {
        public CSTREADY() : base(1, "CSTREADY") 
        {
            Value = "";
        }
        public CSTREADY(string value="") : base(1, "CSTREADY")
        {
            Value = value;
        }
        public static implicit operator string(CSTREADY s) => s.Value.Trim(' ');        
        public static implicit operator CSTREADY(string s) => new CSTREADY(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// DACK
    /// </summary>
    [Serializable]
    public partial class DACK : BinaryType
    {
        public DACK() : base(0, 1, "DACK") { }
        public DACK(byte value) : base(0, 1, "DACK")
        {
            Value = value;
        }
        public static implicit operator byte(DACK s) => s.Value;        
        public static implicit operator DACK(byte s) => new DACK(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// DEST
    /// </summary>
    [Serializable]
    public partial class DEST : U2Type
    {
        public DEST() : base(1, 1, "DEST") { }
        public DEST(ushort value) : base(1, 1, "DEST")
        {
            Value = value;
        }
        public static implicit operator ushort(DEST s) => s.Value;        
        public static implicit operator DEST(ushort s) => new DEST(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// DICODE
    /// </summary>
    [Serializable]
    public partial class DICODE : BinaryType
    {
        public DICODE() : base(1, 1, "DICODE") { }
        public DICODE(byte value) : base(1, 1, "DICODE")
        {
            Value = value;
        }
        public static implicit operator byte(DICODE s) => s.Value;        
        public static implicit operator DICODE(byte s) => new DICODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// DVNAME
    /// </summary>
    [Serializable]
    public partial class DVNAME : StringType
    {
        public DVNAME() : base(14, "DVNAME") 
        {
            Value = "";
        }
        public DVNAME(string value="") : base(14, "DVNAME")
        {
            Value = value;
        }
        public static implicit operator string(DVNAME s) => s.Value.Trim(' ');        
        public static implicit operator DVNAME(string s) => new DVNAME(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// DVVAL
    /// </summary>
    [Serializable]
    public partial class DVVAL : StringType
    {
        public DVVAL() : base(14, "DVVAL") 
        {
            Value = "";
        }
        public DVVAL(string value="") : base(14, "DVVAL")
        {
            Value = value;
        }
        public static implicit operator string(DVVAL s) => s.Value.Trim(' ');        
        public static implicit operator DVVAL(string s) => new DVVAL(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// EQPINFO
    /// </summary>
    [Serializable]
    public partial class EQPINFO : StringType
    {
        public EQPINFO() : base(40, "EQPINFO") 
        {
            Value = "";
        }
        public EQPINFO(string value="") : base(40, "EQPINFO")
        {
            Value = value;
        }
        public static implicit operator string(EQPINFO s) => s.Value.Trim(' ');        
        public static implicit operator EQPINFO(string s) => new EQPINFO(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// EQPMSG
    /// </summary>
    [Serializable]
    public partial class EQPMSG : StringType
    {
        public EQPMSG() : base(180, "EQPMSG") 
        {
            Value = "";
        }
        public EQPMSG(string value="") : base(180, "EQPMSG")
        {
            Value = value;
        }
        public static implicit operator string(EQPMSG s) => s.Value.Trim(' ');        
        public static implicit operator EQPMSG(string s) => new EQPMSG(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// EUNITID
    /// </summary>
    [Serializable]
    public partial class EUNITID : U2Type
    {
        public EUNITID() : base(1, 1, "EUNITID") { }
        public EUNITID(ushort value) : base(1, 1, "EUNITID")
        {
            Value = value;
        }
        public static implicit operator ushort(EUNITID s) => s.Value;        
        public static implicit operator EUNITID(ushort s) => new EUNITID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSDVNAME
    /// </summary>
    [Serializable]
    public partial class GLSDVNAME : StringType
    {
        public GLSDVNAME() : base(14, "GLSDVNAME") 
        {
            Value = "";
        }
        public GLSDVNAME(string value="") : base(14, "GLSDVNAME")
        {
            Value = value;
        }
        public static implicit operator string(GLSDVNAME s) => s.Value.Trim(' ');        
        public static implicit operator GLSDVNAME(string s) => new GLSDVNAME(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSDVVAL
    /// </summary>
    [Serializable]
    public partial class GLSDVVAL : StringType
    {
        public GLSDVVAL() : base(14, "GLSDVVAL") 
        {
            Value = "";
        }
        public GLSDVVAL(string value="") : base(14, "GLSDVVAL")
        {
            Value = value;
        }
        public static implicit operator string(GLSDVVAL s) => s.Value.Trim(' ');        
        public static implicit operator GLSDVVAL(string s) => new GLSDVVAL(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSEXIST 0:NotExist, 1:Exist
    /// </summary>
    [Serializable]
    public partial class GLSEXIST : BinaryType
    {
        public GLSEXIST() : base(1, 1, "GLSEXIST 0:NotExist, 1:Exist") { }
        public GLSEXIST(byte value) : base(1, 1, "GLSEXIST 0:NotExist, 1:Exist")
        {
            Value = value;
        }
        public static implicit operator byte(GLSEXIST s) => s.Value;        
        public static implicit operator GLSEXIST(byte s) => new GLSEXIST(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSPRDATA
    /// </summary>
    [Serializable]
    public partial class GLSPRDATA : StringType
    {
        public GLSPRDATA() : base(450, "GLSPRDATA") 
        {
            Value = "";
        }
        public GLSPRDATA(string value="") : base(450, "GLSPRDATA")
        {
            Value = value;
        }
        public static implicit operator string(GLSPRDATA s) => s.Value.Trim(' ');        
        public static implicit operator GLSPRDATA(string s) => new GLSPRDATA(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSPRITEM
    /// </summary>
    [Serializable]
    public partial class GLSPRITEM : StringType
    {
        public GLSPRITEM() : base(288, "GLSPRITEM") 
        {
            Value = "";
        }
        public GLSPRITEM(string value="") : base(288, "GLSPRITEM")
        {
            Value = value;
        }
        public static implicit operator string(GLSPRITEM s) => s.Value.Trim(' ');        
        public static implicit operator GLSPRITEM(string s) => new GLSPRITEM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSPROC
    /// </summary>
    [Serializable]
    public partial class GLSPROC : StringType
    {
        public GLSPROC() : base(1, "GLSPROC") 
        {
            Value = "";
        }
        public GLSPROC(string value="") : base(1, "GLSPROC")
        {
            Value = value;
        }
        public static implicit operator string(GLSPROC s) => s.Value.Trim(' ');        
        public static implicit operator GLSPROC(string s) => new GLSPROC(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GLSSELECTED
    /// </summary>
    [Serializable]
    public partial class GLSSELECTED : BinaryType
    {
        public GLSSELECTED() : base(1, 1, "GLSSELECTED") { }
        public GLSSELECTED(byte value) : base(1, 1, "GLSSELECTED")
        {
            Value = value;
        }
        public static implicit operator byte(GLSSELECTED s) => s.Value;        
        public static implicit operator GLSSELECTED(byte s) => new GLSSELECTED(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LDGLSCODE_A
    /// </summary>
    [Serializable]
    public partial class LDGLSCODE_A : U2Type
    {
        public LDGLSCODE_A() : base(0, 1, "LDGLSCODE_A") { }
        public LDGLSCODE_A(ushort value) : base(0, 1, "LDGLSCODE_A")
        {
            Value = value;
        }
        public static implicit operator ushort(LDGLSCODE_A s) => s.Value;        
        public static implicit operator LDGLSCODE_A(ushort s) => new LDGLSCODE_A(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LDGLSCODE_B
    /// </summary>
    [Serializable]
    public partial class LDGLSCODE_B : U2Type
    {
        public LDGLSCODE_B() : base(0, 1, "LDGLSCODE_B") { }
        public LDGLSCODE_B(ushort value) : base(0, 1, "LDGLSCODE_B")
        {
            Value = value;
        }
        public static implicit operator ushort(LDGLSCODE_B s) => s.Value;        
        public static implicit operator LDGLSCODE_B(ushort s) => new LDGLSCODE_B(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// UDGLSCODE_A
    /// </summary>
    [Serializable]
    public partial class UDGLSCODE_A : U2Type
    {
        public UDGLSCODE_A() : base(0, 1, "UDGLSCODE_A") { }
        public UDGLSCODE_A(ushort value) : base(0, 1, "UDGLSCODE_A")
        {
            Value = value;
        }
        public static implicit operator ushort(UDGLSCODE_A s) => s.Value;        
        public static implicit operator UDGLSCODE_A(ushort s) => new UDGLSCODE_A(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// UDGLSCODE_B
    /// </summary>
    [Serializable]
    public partial class UDGLSCODE_B : U2Type
    {
        public UDGLSCODE_B() : base(0, 1, "UDGLSCODE_B") { }
        public UDGLSCODE_B(ushort value) : base(0, 1, "UDGLSCODE_B")
        {
            Value = value;
        }
        public static implicit operator ushort(UDGLSCODE_B s) => s.Value;        
        public static implicit operator UDGLSCODE_B(ushort s) => new UDGLSCODE_B(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTDVNAME
    /// </summary>
    [Serializable]
    public partial class LOTDVNAME : StringType
    {
        public LOTDVNAME() : base(14, "LOTDVNAME") 
        {
            Value = "";
        }
        public LOTDVNAME(string value="") : base(14, "LOTDVNAME")
        {
            Value = value;
        }
        public static implicit operator string(LOTDVNAME s) => s.Value.Trim(' ');        
        public static implicit operator LOTDVNAME(string s) => new LOTDVNAME(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTDVVAL
    /// </summary>
    [Serializable]
    public partial class LOTDVVAL : StringType
    {
        public LOTDVVAL() : base(14, "LOTDVVAL") 
        {
            Value = "";
        }
        public LOTDVVAL(string value="") : base(14, "LOTDVVAL")
        {
            Value = value;
        }
        public static implicit operator string(LOTDVVAL s) => s.Value.Trim(' ');        
        public static implicit operator LOTDVVAL(string s) => new LOTDVVAL(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTJUDGE
    /// </summary>
    [Serializable]
    public partial class LOTJUDGE : StringType
    {
        public LOTJUDGE() : base(2, "LOTJUDGE") 
        {
            Value = "";
        }
        public LOTJUDGE(string value="") : base(2, "LOTJUDGE")
        {
            Value = value;
        }
        public static implicit operator string(LOTJUDGE s) => s.Value.Trim(' ');        
        public static implicit operator LOTJUDGE(string s) => new LOTJUDGE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTPRDATA
    /// </summary>
    [Serializable]
    public partial class LOTPRDATA : StringType
    {
        public LOTPRDATA() : base(450, "LOTPRDATA") 
        {
            Value = "";
        }
        public LOTPRDATA(string value="") : base(450, "LOTPRDATA")
        {
            Value = value;
        }
        public static implicit operator string(LOTPRDATA s) => s.Value.Trim(' ');        
        public static implicit operator LOTPRDATA(string s) => new LOTPRDATA(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOTPRITEM
    /// </summary>
    [Serializable]
    public partial class LOTPRITEM : StringType
    {
        public LOTPRITEM() : base(288, "LOTPRITEM") 
        {
            Value = "";
        }
        public LOTPRITEM(string value="") : base(288, "LOTPRITEM")
        {
            Value = value;
        }
        public static implicit operator string(LOTPRITEM s) => s.Value.Trim(' ');        
        public static implicit operator LOTPRITEM(string s) => new LOTPRITEM(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOWERRECIPEID
    /// </summary>
    [Serializable]
    public partial class LOWERRECIPEID : StringType
    {
        public LOWERRECIPEID() : base(24, "LOWERRECIPEID") 
        {
            Value = "";
        }
        public LOWERRECIPEID(string value="") : base(24, "LOWERRECIPEID")
        {
            Value = value;
        }
        public static implicit operator string(LOWERRECIPEID s) => s.Value.Trim(' ');        
        public static implicit operator LOWERRECIPEID(string s) => new LOWERRECIPEID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// LOWERRECIPEIDVER
    /// </summary>
    [Serializable]
    public partial class LOWERRECIPEIDVER : StringType
    {
        public LOWERRECIPEIDVER() : base(12, "LOWERRECIPEIDVER") 
        {
            Value = "";
        }
        public LOWERRECIPEIDVER(string value="") : base(12, "LOWERRECIPEIDVER")
        {
            Value = value;
        }
        public static implicit operator string(LOWERRECIPEIDVER s) => s.Value.Trim(' ');        
        public static implicit operator LOWERRECIPEIDVER(string s) => new LOWERRECIPEIDVER(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// MAP
    /// </summary>
    [Serializable]
    public partial class MAP : StringType
    {
        public MAP() : base(40, "MAP") 
        {
            Value = "";
        }
        public MAP(string value="") : base(40, "MAP")
        {
            Value = value;
        }
        public static implicit operator string(MAP s) => s.Value.Trim(' ');        
        public static implicit operator MAP(string s) => new MAP(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// MULTIFLAG
    /// </summary>
    [Serializable]
    public partial class MULTIFLAG : StringType
    {
        public MULTIFLAG() : base(1, "MULTIFLAG") 
        {
            Value = "";
        }
        public MULTIFLAG(string value="") : base(1, "MULTIFLAG")
        {
            Value = value;
        }
        public static implicit operator string(MULTIFLAG s) => s.Value.Trim(' ');        
        public static implicit operator MULTIFLAG(string s) => new MULTIFLAG(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PHACK
    /// </summary>
    [Serializable]
    public partial class PHACK : BinaryType
    {
        public PHACK() : base(0, 1, "PHACK") { }
        public PHACK(byte value) : base(0, 1, "PHACK")
        {
            Value = value;
        }
        public static implicit operator byte(PHACK s) => s.Value;        
        public static implicit operator PHACK(byte s) => new PHACK(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// PROCUNITID
    /// </summary>
    [Serializable]
    public partial class PROCUNITID : U2Type
    {
        public PROCUNITID() : base(1, 1, "PROCUNITID") { }
        public PROCUNITID(ushort value) : base(1, 1, "PROCUNITID")
        {
            Value = value;
        }
        public static implicit operator ushort(PROCUNITID s) => s.Value;        
        public static implicit operator PROCUNITID(ushort s) => new PROCUNITID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// REACK
    /// </summary>
    [Serializable]
    public partial class REACK : BinaryType
    {
        public REACK() : base(0, 1, "REACK") { }
        public REACK(byte value) : base(0, 1, "REACK")
        {
            Value = value;
        }
        public static implicit operator byte(REACK s) => s.Value;        
        public static implicit operator REACK(byte s) => new REACK(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RECIPEIDCMD B:Built, C:Change, D:Delete, R:Rename
    /// </summary>
    [Serializable]
    public partial class RECIPEIDCMD : StringType
    {
        public RECIPEIDCMD() : base(1, "RECIPEIDCMD B:Built, C:Change, D:Delete, R:Rename") 
        {
            Value = "";
        }
        public RECIPEIDCMD(string value="") : base(1, "RECIPEIDCMD B:Built, C:Change, D:Delete, R:Rename")
        {
            Value = value;
        }
        public static implicit operator string(RECIPEIDCMD s) => s.Value.Trim(' ');        
        public static implicit operator RECIPEIDCMD(string s) => new RECIPEIDCMD(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RECIPEIDSTATUS R:Using, I:Editable, D:Disable
    /// </summary>
    [Serializable]
    public partial class RECIPEIDSTATUS : StringType
    {
        public RECIPEIDSTATUS() : base(1, "RECIPEIDSTATUS R:Using, I:Editable, D:Disable") 
        {
            Value = "";
        }
        public RECIPEIDSTATUS(string value="") : base(1, "RECIPEIDSTATUS R:Using, I:Editable, D:Disable")
        {
            Value = value;
        }
        public static implicit operator string(RECIPEIDSTATUS s) => s.Value.Trim(' ');        
        public static implicit operator RECIPEIDSTATUS(string s) => new RECIPEIDSTATUS(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RECIPEIDVER
    /// </summary>
    [Serializable]
    public partial class RECIPEIDVER : StringType
    {
        public RECIPEIDVER() : base(12, "RECIPEIDVER") 
        {
            Value = "";
        }
        public RECIPEIDVER(string value="") : base(12, "RECIPEIDVER")
        {
            Value = value;
        }
        public static implicit operator string(RECIPEIDVER s) => s.Value.Trim(' ');        
        public static implicit operator RECIPEIDVER(string s) => new RECIPEIDVER(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RPARMNAME
    /// </summary>
    [Serializable]
    public partial class RPARMNAME : StringType
    {
        public RPARMNAME() : base(24, "RPARMNAME") 
        {
            Value = "";
        }
        public RPARMNAME(string value="") : base(24, "RPARMNAME")
        {
            Value = value;
        }
        public static implicit operator string(RPARMNAME s) => s.Value.Trim(' ');        
        public static implicit operator RPARMNAME(string s) => new RPARMNAME(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RPARMVAL
    /// </summary>
    [Serializable]
    public partial class RPARMVAL : StringType
    {
        public RPARMVAL() : base(24, "RPARMVAL") 
        {
            Value = "";
        }
        public RPARMVAL(string value="") : base(24, "RPARMVAL")
        {
            Value = value;
        }
        public static implicit operator string(RPARMVAL s) => s.Value.Trim(' ');        
        public static implicit operator RPARMVAL(string s) => new RPARMVAL(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SCRPCODE
    /// </summary>
    [Serializable]
    public partial class SCRPCODE : StringType
    {
        public SCRPCODE() : base(4, "SCRPCODE") 
        {
            Value = "";
        }
        public SCRPCODE(string value="") : base(4, "SCRPCODE")
        {
            Value = value;
        }
        public static implicit operator string(SCRPCODE s) => s.Value.Trim(' ');        
        public static implicit operator SCRPCODE(string s) => new SCRPCODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SCRTYPE
    /// </summary>
    [Serializable]
    public partial class SCRTYPE : StringType
    {
        public SCRTYPE() : base(1, "SCRTYPE") 
        {
            Value = "";
        }
        public SCRTYPE(string value="") : base(1, "SCRTYPE")
        {
            Value = value;
        }
        public static implicit operator string(SCRTYPE s) => s.Value.Trim(' ');        
        public static implicit operator SCRTYPE(string s) => new SCRTYPE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SELECTED
    /// </summary>
    [Serializable]
    public partial class SELECTED : StringType
    {
        public SELECTED() : base(1, "SELECTED") 
        {
            Value = "";
        }
        public SELECTED(string value="") : base(1, "SELECTED")
        {
            Value = value;
        }
        public static implicit operator string(SELECTED s) => s.Value.Trim(' ');        
        public static implicit operator SELECTED(string s) => new SELECTED(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SLTLOTID
    /// </summary>
    [Serializable]
    public partial class SLTLOTID : StringType
    {
        public SLTLOTID() : base(16, "SLTLOTID") 
        {
            Value = "";
        }
        public SLTLOTID(string value="") : base(16, "SLTLOTID")
        {
            Value = value;
        }
        public static implicit operator string(SLTLOTID s) => s.Value.Trim(' ');        
        public static implicit operator SLTLOTID(string s) => new SLTLOTID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SRECIPEID
    /// </summary>
    [Serializable]
    public partial class SRECIPEID : StringType
    {
        public SRECIPEID() : base(24, "SRECIPEID") 
        {
            Value = "";
        }
        public SRECIPEID(string value="") : base(24, "SRECIPEID")
        {
            Value = value;
        }
        public static implicit operator string(SRECIPEID s) => s.Value.Trim(' ');        
        public static implicit operator SRECIPEID(string s) => new SRECIPEID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// STEP
    /// </summary>
    [Serializable]
    public partial class STEP : StringType
    {
        public STEP() : base(14, "STEP") 
        {
            Value = "";
        }
        public STEP(string value="") : base(14, "STEP")
        {
            Value = value;
        }
        public static implicit operator string(STEP s) => s.Value.Trim(' ');        
        public static implicit operator STEP(string s) => new STEP(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// TEXT
    /// </summary>
    [Serializable]
    public partial class TEXT : StringType
    {
        public TEXT() : base(80, "TEXT") 
        {
            Value = "";
        }
        public TEXT(string value="") : base(80, "TEXT")
        {
            Value = value;
        }
        public static implicit operator string(TEXT s) => s.Value.Trim(' ');        
        public static implicit operator TEXT(string s) => new TEXT(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// TID
    /// </summary>
    [Serializable]
    public partial class TID : BinaryType
    {
        public TID() : base(0, 1, "TID") { }
        public TID(byte value) : base(0, 1, "TID")
        {
            Value = value;
        }
        public static implicit operator byte(TID s) => s.Value;        
        public static implicit operator TID(byte s) => new TID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// USEMODE
    /// </summary>
    [Serializable]
    public partial class USEMODE : StringType
    {
        public USEMODE() : base(8, "USEMODE") 
        {
            Value = "";
        }
        public USEMODE(string value="") : base(8, "USEMODE")
        {
            Value = value;
        }
        public static implicit operator string(USEMODE s) => s.Value.Trim(' ');        
        public static implicit operator USEMODE(string s) => new USEMODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// MODECODE
    /// </summary>
    [Serializable]
    public partial class MODECODE : StringType
    {
        public MODECODE() : base(1, "MODECODE") 
        {
            Value = "";
        }
        public MODECODE(string value="") : base(1, "MODECODE")
        {
            Value = value;
        }
        public static implicit operator string(MODECODE s) => s.Value.Trim(' ');        
        public static implicit operator MODECODE(string s) => new MODECODE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RECIPEIDTYPE E:Equipment, M:Machine, U:Unit
    /// </summary>
    [Serializable]
    public partial class RECIPEIDTYPE : StringType
    {
        public RECIPEIDTYPE() : base(1, "RECIPEIDTYPE E:Equipment, M:Machine, U:Unit") 
        {
            Value = "";
        }
        public RECIPEIDTYPE(string value="") : base(1, "RECIPEIDTYPE E:Equipment, M:Machine, U:Unit")
        {
            Value = value;
        }
        public static implicit operator string(RECIPEIDTYPE s) => s.Value.Trim(' ');        
        public static implicit operator RECIPEIDTYPE(string s) => new RECIPEIDTYPE(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// SAMPLINGFLAG W:Wait, S:Start
    /// </summary>
    [Serializable]
    public partial class SAMPLINGFLAG : StringType
    {
        public SAMPLINGFLAG() : base(1, "SAMPLINGFLAG W:Wait, S:Start") 
        {
            Value = "";
        }
        public SAMPLINGFLAG(string value="") : base(1, "SAMPLINGFLAG W:Wait, S:Start")
        {
            Value = value;
        }
        public static implicit operator string(SAMPLINGFLAG s) => s.Value.Trim(' ');        
        public static implicit operator SAMPLINGFLAG(string s) => new SAMPLINGFLAG(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC6
    /// </summary>
    [Serializable]
    public partial class ACKC6 : BinaryType
    {
        public ACKC6() : base(0, 1, "ACKC6") { }
        public ACKC6(byte value) : base(0, 1, "ACKC6")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC6 s) => s.Value;        
        public static implicit operator ACKC6(byte s) => new ACKC6(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC6A
    /// </summary>
    [Serializable]
    public partial class ACKC6A : BinaryType
    {
        public ACKC6A() : base(0, 1, "ACKC6A") { }
        public ACKC6A(byte value) : base(0, 1, "ACKC6A")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC6A s) => s.Value;        
        public static implicit operator ACKC6A(byte s) => new ACKC6A(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC7
    /// </summary>
    [Serializable]
    public partial class ACKC7 : BinaryType
    {
        public ACKC7() : base(0, 1, "ACKC7") { }
        public ACKC7(byte value) : base(0, 1, "ACKC7")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC7 s) => s.Value;        
        public static implicit operator ACKC7(byte s) => new ACKC7(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ACKC7S
    /// </summary>
    [Serializable]
    public partial class ACKC7S : BinaryType
    {
        public ACKC7S() : base(0, 1, "ACKC7S") { }
        public ACKC7S(byte value) : base(0, 1, "ACKC7S")
        {
            Value = value;
        }
        public static implicit operator byte(ACKC7S s) => s.Value;        
        public static implicit operator ACKC7S(byte s) => new ACKC7S(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// MEXP
    /// </summary>
    [Serializable]
    public partial class MEXP : StringType
    {
        public MEXP() : base(6, "MEXP") 
        {
            Value = "MEXP";
        }
        public MEXP(string value="MEXP") : base(6, "MEXP")
        {
            Value = value;
        }
        public static implicit operator string(MEXP s) => s.Value.Trim(' ');        
        public static implicit operator MEXP(string s) => new MEXP(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// EDID
    /// </summary>
    [Serializable]
    public partial class EDID : StringType
    {
        public EDID() : base(14, "EDID") 
        {
            Value = "EDID";
        }
        public EDID(string value="EDID") : base(14, "EDID")
        {
            Value = value;
        }
        public static implicit operator string(EDID s) => s.Value.Trim(' ');        
        public static implicit operator EDID(string s) => new EDID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RESERVECD R:Reservation, S:Start
    /// </summary>
    [Serializable]
    public partial class RESERVECD : StringType
    {
        public RESERVECD() : base(1, "RESERVECD R:Reservation, S:Start") 
        {
            Value = "R";
        }
        public RESERVECD(string value="R") : base(1, "RESERVECD R:Reservation, S:Start")
        {
            Value = value;
        }
        public static implicit operator string(RESERVECD s) => s.Value.Trim(' ');        
        public static implicit operator RESERVECD(string s) => new RESERVECD(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// RCMD
    /// </summary>
    [Serializable]
    public partial class RCMD : StringType
    {
        public RCMD() : base(6, "RCMD") 
        {
            Value = "PAUSE";
        }
        public RCMD(string value="PAUSE") : base(6, "RCMD")
        {
            Value = value;
        }
        public static implicit operator string(RCMD s) => s.Value.Trim(' ');        
        public static implicit operator RCMD(string s) => new RCMD(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// TARGETID
    /// </summary>
    [Serializable]
    public partial class TARGETID : U2Type
    {
        public TARGETID() : base(102, 1, "TARGETID") { }
        public TARGETID(ushort value) : base(102, 1, "TARGETID")
        {
            Value = value;
        }
        public static implicit operator ushort(TARGETID s) => s.Value;        
        public static implicit operator TARGETID(ushort s) => new TARGETID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// GROUPID
    /// </summary>
    [Serializable]
    public partial class GROUPID : StringType
    {
        public GROUPID() : base(12, "GROUPID") 
        {
            Value = "GROUPID";
        }
        public GROUPID(string value="GROUPID") : base(12, "GROUPID")
        {
            Value = value;
        }
        public static implicit operator string(GROUPID s) => s.Value.Trim(' ');        
        public static implicit operator GROUPID(string s) => new GROUPID(s);
    }
    // 이 코드는 자동으로 생성된 코드임
    /// <summary>
    /// ABS
    /// </summary>
    [Serializable]
    public partial class ABS : BinaryType
    {
        public ABS() : base(new byte[] {1,2,3,4,5,6,7,8,9,10}, 10, "ABS") { }
        public ABS(byte value) : base(new byte[] {1,2,3,4,5,6,7,8,9,10}, 10, "ABS")
        {
            Value = value;
        }
        public static implicit operator byte(ABS s) => s.Value;        
        public static implicit operator ABS(byte s) => new ABS(s);
    }
}
