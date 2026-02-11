using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{

    public class CIMS2F0 : CimBasicClass
    {
        public CIMS2F0()
        {
            Title = "Abort Transaction";
            Stream = 2;
            Function = 0;
            Wait = false;

            AutoCreateInstance();
        }
    }

    public class CIMS2F17 : CimBasicClass
    {
        public CIMS2F17()
        {
            Title = "Date and Time Request";
            Stream = 2;
            Function = 17;
            Wait = true;

            AutoCreateInstance();
        }

        public UNITID UNITID { get; set; }
    }

    public class CIMS2F18 : CimBasicClass
    {
        public CIMS2F18()
        {
            Title = "Date and Time Data";
            Stream = 2;
            Function = 18;
            Wait = false;

            AutoCreateInstance();
        }
        public TIME TIME { get; set; }
    }

    public class CIMS2F25 : CimBasicClass
    {
        public CIMS2F25()
        {
            Title = "Loopback Diagnostic Request";
            Stream = 2;
            Function = 25;
            Wait = true;

            AutoCreateInstance();
        }
        public ABS ABS { get; set; }
    }

    public class CIMS2F26 : CimBasicClass
    {
        public CIMS2F26()
        {
            Title = "Loopback Diagnostic Data";
            Stream = 2;
            Function = 26;
            Wait = false;

            AutoCreateInstance();
        }
        public ABS ABS { get; set; }
    }

    public class CIMS2F31 : CimBasicClass
    {
        public CIMS2F31()
        {
            Title = "Date and Time Data Send";
            Stream = 2;
            Function = 31;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public TIME TIME { get; set; }
    }

    public class CIMS2F32 : CimBasicClass
    {
        public CIMS2F32()
        {
            Title = "Date and Time Acknowledge";
            Stream = 2;
            Function = 32;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }

    public class CIMS2F61 : CimBasicClass
    {
        public CIMS2F61()
        {
            Title = "Control State Change Request";
            Stream = 2;
            Function = 61;
            Wait = true;

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public RCS RCS { get;set;}
    }

    public class CIMS2F62 : CimBasicClass
    {
        public CIMS2F62()
        {
            Title = "Control State Change Request Acknowledge";
            Stream = 2;
            Function = 62;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public ACKC2A ACKC2A { get; set; }
    }
    public class CIMS2F63 : CimBasicClass
    {
        public CIMS2F63()
        {
            Title = "Equipment Operation Mode Report";
            Stream = 2;
            Function = 63;
            Wait = true;

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public OPMODE OPMODE { get; set; }
    }

    public class CIMS2F64 : CimBasicClass
    {
        public CIMS2F64()
        {
            Title = "Equipment Operation Mode Report Acknowledge";
            Stream = 2;
            Function = 64;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public ACKC2A ACKC2A { get; set; }
    }

    public class CIMS2F65 : CimBasicClass
    {
        public CIMS2F65()
        {
            Title = "Local LOT Status Change Request";
            Stream = 2;
            Function = 65;
            Wait = true;

            AutoCreateInstance();
        }

        public ListType L1_4 { get; set; } = new ListType(4);
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public LOTID LOTID { get; set; }
        public ENDFLAG ENDFLAG { get; set; }
    }

    public class CIMS2F66 : CimBasicClass
    {
        public CIMS2F66()
        {
            Title = "Local LOT Status Change Request Acknowledge";
            Stream = 2;
            Function = 66;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }

    public class CIMS2F67 : CimBasicClass
    {
        public CIMS2F67()
        {
            Title = "Cycle Stop Status Report";
            Stream = 2;
            Function = 67;
            Wait = true;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public CSSSTATUS CSSSTATUS { get; set; }
    }

    public class CIMS2F68 : CimBasicClass
    {
        public CIMS2F68()
        {
            Title = "Cycle Stop Status Report Acknowledge";
            Stream = 2;
            Function = 68;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }

    public class CIMS2F69 : CimBasicClass
    {
        public CIMS2F69()
        {
            Title = "Unload Glass Data Request";
            Stream = 2;
            Function = 69;
            Wait = true;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public HOSTMSG HOSTMSG { get; set; }
    }
    public class CIMS2F70 : CimBasicClass
    {
        public CIMS2F70()
        {
            Title = "Unload Glass Data Request Acknowledge";
            Stream = 2;
            Function = 70;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }
    public class CIMS2F77 : CimBasicClass
    {
        public CIMS2F77()
        {
            Title = "Glass Call Request";
            Stream = 2;
            Function = 77;
            Wait = true;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public TARGETID TARGETID { get; set; }
    }
    public class CIMS2F78 : CimBasicClass
    {
        public CIMS2F78()
        {
            Title = "Glass Call Request Acknowledge";
            Stream = 2;
            Function = 78;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }
    public class CIMS2F79 : CimBasicClass
    {
        public CIMS2F79()
        {
            Title = "Glass Data Request";
            Stream = 2;
            Function = 79;
            Wait = true;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }

        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public GLSID GLSID { get; set; }
        public GLSCODE GLSCODE { get; set; }
        public REQOPTION REQOPTION { get; set; }
    }
    public class CIMS2F80 : CimBasicClass
    {
        public CIMS2F80()
        {
            Title = "Glass Data";
            Stream = 2;
            Function = 80;
            Wait = false;

            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC2 ACKC2 { get; set; }
        public LoadGlass LoadGlass { get; set; }
    }

    public class CIMS2F87 : CimBasicClass
    {
        public CIMS2F87()
        {
            Title = "Load Glass Data Send";
            Stream = 2;
            Function = 87;
            Wait = true;

            L1_4 = new ListType(4);
            LoadGlass = new List<LoadGlass>();

            AutoCreateInstance();
        }

        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public GLSSTATUS GLSSTATUS { get; set; }
        public List<LoadGlass> LoadGlass { get; set; }
    }

    public class CIMS2F88 : CimBasicClass
    {
        public CIMS2F88()
        {
            Title = "Load Glass Data Send Acknowledge";
            Stream = 2;
            Function = 88;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; } = new ListType(3);
        public UNITID UNITID { get; set; }
        public STAGEID STAGEID { get; set; }
        public ACKC2 ACKC2 { get; set; }
    }
    public class CIMS2F99 : CimBasicClass
    {
        public CIMS2F99()
        {
            Title = "Equipment PD Down Request";
            Stream = 2;
            Function = 99;
            Wait = true;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public RCMD RCMD { get; set; }
        public HOSTMSG HOSTMSG { get; set; }
    }

    public class CIMS2F100 : CimBasicClass
    {
        public CIMS2F100()
        {
            Title = "Equipment PD Down Request Acknowledge";
            Stream = 2;
            Function = 100;
            Wait = false;

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; } = new ListType(2);
        public UNITID UNITID { get; set; }
        public ACKC6 ACKC6 { get; set; }
    }
}
