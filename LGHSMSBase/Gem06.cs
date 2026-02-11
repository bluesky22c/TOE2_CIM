using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS6F0 : CimBasicClass
    {
        public CIMS6F0()
        {
            Title = "Abort Transaction";
            Stream = 6;
            Function = 0;
            Wait = false;
        }
    }

    [Serializable]
    public class CIMS6F61 : CimBasicClass
    {
        public CIMS6F61()
        {
            Title = "Load Glass Data Request";
            Stream = 6;
            Function = 61;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
    }

    [Serializable]
    public class CIMS6F62 : CimBasicClass
    {
        public CIMS6F62()
        {
            Title = "Load Glass Data Request Acknowledge";
            Stream = 6;
            Function = 62;
            Wait = false;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F63 : CimBasicClass
    {
        public CIMS6F63()
        {
            Title = "Unload Glass Data Report";
            Stream = 6;
            Function = 63;
            Wait = true;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }
        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public List<UnLoadGlass> UnLoadGlass { get; set; }
    }
    [Serializable]
    public class CIMS6F64 : CimBasicClass
    {
        public CIMS6F64()
        {
            Title = "Unload Glass Data Report Acknowledge";
            Stream = 6;
            Function = 64;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F65 : CimBasicClass
    {
        public CIMS6F65()
        {
            Title = "LOT End Data Send";
            Stream = 6;
            Function = 65;
            Wait = true;

            L1_9 = new ListType(9);
            AutoCreateInstance();
        }
        public ListType L1_9 { get; set; }
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public ENDFLAG ENDFLAG { get; set; }
        public MULTIFLAG MULTIFLAG { get; set; }
        public List<SLOTITEM_LOTEND>  SLOTITEM_LOTENDS { get; set; }
    }
    [Serializable]
    public class CIMS6F66 : CimBasicClass
    {
        public CIMS6F66()
        {
            Title = "LOT End Data Send Acknowledge";
            Stream = 6;
            Function = 66;
            Wait = false;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public LOTID LOTID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }

    [Serializable]
    public class CIMS6F67 : CimBasicClass
    {
        public CIMS6F67()
        {
            Title = "Glass Move Request";
            Stream = 6;
            Function = 67;
            Wait = true;

            L1_7 = new ListType(7);
            AutoCreateInstance();
        }

        public ListType L1_7 { get; set; }
        public GLSMVSTATUS GLSMVSTATUS { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public LDGLSCODE_A LDGLSCODE_A { get; set; }
        public LDGLSCODE_B LDGLSCODE_B { get; set; }
        public UDGLSCODE_A UDGLSCODE_A { get; set; }
        public UDGLSCODE_B UDGLSCODE_B { get; set; }

    }
    [Serializable]
    public class CIMS6F68 : CimBasicClass
    {
        public CIMS6F68()
        {
            Title = "Glass Move Request Acknowledge";
            Stream = 6;
            Function = 68;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F69 : CimBasicClass
    {
        public CIMS6F69()
        {
            Title = "Cassette Map Request";
            Stream = 6;
            Function = 69;
            Wait = true;

            L1_1 = new ListType(1);
            AutoCreateInstance();
        }

        public ListType L1_1 { get; set; }
        public UNITID UNITID { get; set; }
    }
    [Serializable]
    public class CIMS6F70 : CimBasicClass
    {
        public CIMS6F70()
        {
            Title = "Cassette Map Request Acknowledge";
            Stream = 6;
            Function = 70;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<CassetteMAP> CassetteMAP { get; set; }
    }
    [Serializable]
    public class CIMS6F71 : CimBasicClass
    {
        public CIMS6F71()
        {
            Title = "Sampling Slot Request";
            Stream = 6;
            Function = 71;
            Wait = true;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public SLOTITEM_SAMPLE SLOTITEM_SAMPLE { get; set; }
    }

    [Serializable]
    public class CIMS6F72 : CimBasicClass
    {
        public CIMS6F72()
        {
            Title = "Sampling Slot Request Acknowledge";
            Stream = 6;
            Function = 72;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6A ACKC6A { get; set; }
    }
    [Serializable]
    public class CIMS6F73 : CimBasicClass
    {
        public CIMS6F73()
        {
            Title = "Glass Actual Processing Data Report";
            Stream = 6;
            Function = 73;
            Wait = true;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }

        public ListType L1_8 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public SLOTID SLOTID { get; set; }
        public UNITID UNITID { get; set; }
        public GLSPRCItem GLSPRCS { get; set; }
        public List<DVItem> DVS { get; set; }
    }
    [Serializable]
    public class CIMS6F74 : CimBasicClass
    {
        public CIMS6F74()
        {
            Title = "Glass Actual Processing Data Report Acknowledge";
            Stream = 6;
            Function = 74;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F83 : CimBasicClass
    {
        public CIMS6F83()
        {
            Title = "LOT Actual Processing Data Report";
            Stream = 6;
            Function = 83;
            Wait = true;

            L1_9 = new ListType(9);
            AutoCreateInstance();
        }

        public ListType L1_9 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public UNITID UNITID { get; set; }
        public KEYID KEYID { get; set; }
        public LOTJUDGE LOTJUDGE { get; set; }
        public List<LSD> LSDS { get; set; }
        public List<DVItem> DVITEMS { get; set; }
    }
    [Serializable]
    public class CIMS6F84 : CimBasicClass
    {
        public CIMS6F84()
        {
            Title = "LOT Actual Processing Data Report Acknowledge";
            Stream = 6;
            Function = 84;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F85 : CimBasicClass
    {
        public CIMS6F85()
        {
            Title = "Dummy Lot Request";
            Stream = 6;
            Function = 85;
            Wait = true;

            AutoCreateInstance();
        }

        public UNITID UNITID { get; set; }
    }
    [Serializable]
    public class CIMS6F86 : CimBasicClass
    {
        public CIMS6F86()
        {
            Title = "Dummy Lot Request Acknowledge";
            Stream = 6;
            Function = 86;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
    [Serializable]
    public class CIMS6F89 : CimBasicClass
    {
        public CIMS6F89()
        {
            Title = "Rework Request";
            Stream = 6;
            Function = 89;
            Wait = true;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }

        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public LOTID LOTID { get; set; }
        public RESERVECD RESERVECD { get; set; }
        public List<REWORK_ITEM> REWORK_ITEMS { get; set; }
    }

    [Serializable]
    public class CIMS6F90: CimBasicClass
    {
        public CIMS6F90()
        {
            Title = "Rework Request Acknowledge";
            Stream = 6;
            Function = 90;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }


    [Serializable]
    public class CIMS6F91 : CimBasicClass
    {
        public CIMS6F91()
        {
            Title = "Equipment Status Change Report";
            Stream = 6;
            Function = 91;
            Wait = true;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public EQPSTATUS EQPStatus { get; set; }
        public ALID ALID { get; set; }
        public ALTX ALTX { get; set; }
    }

    [Serializable]
    public class CIMS6F92 : CimBasicClass
    {
        public CIMS6F92()
        {
            Title = "Equipment Status Change Report Acknowledge";
            Stream = 6;
            Function = 92;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }

    [Serializable]
    public class CIMS6F93 : CimBasicClass
    {
        public CIMS6F93()
        {
            Title = "Scrap Data Report";
            Stream = 6;
            Function = 93;
            Wait = true;

            L1_5 = new ListType(5);
            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public LOTID LOTID { get; set; }
        public GLSID GLSID { get; set; }
        public UNITID UNITID { get; set; }
        public SCRTYPE SCRTYPE { get; set; }
        public SCRPCODE SCRPCODE { get; set; }
    }

    [Serializable]
    public class CIMS6F94 : CimBasicClass
    {
        public CIMS6F94()
        {
            Title = "Scrap Data Report Acknowledge";
            Stream = 6;
            Function = 94;
            Wait = false;
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
}
