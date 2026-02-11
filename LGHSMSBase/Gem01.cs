using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMISoft.HSMS;
using System.Runtime.InteropServices;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS1F0 : CimBasicClass
    {
        public CIMS1F0()
        {
            Title = "Abort Transaction";
            Stream = 1;
            Function = 0;
            Wait = false;
        }
    }

    [Serializable]
    public class CIMS1F1 : CimBasicClass
    {
        public CIMS1F1()
        {
            Title = "Are You Threr Request";
            Stream = 1;
            Function = 1;
            Wait = true;
        }
    }

    [Serializable]
    public class CIMS1F2 : CimBasicClass
    {
        public CIMS1F2()
        {
            Title = "On Line Data";
            Stream = 1;
            Function = 2;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public MDLN MDLN { get; set; }
        public SOFTREV SoftRev { get; set; }
    }


    [Serializable]
    public class CIMS1F61 : CimBasicClass
    {
        public CIMS1F61()
        {
            Title = "All Equipment Status Report";
            Stream = 1;
            Function = 61;
            Wait = true;

            //L1_2 = new ListType(2);
            //L2_1 = new ListType(1);
            //L3_4 = new ListType(4);
            AutoCreateInstance();
        }
        public EQPReport EQPReport { get; set; }
        //public ListType L1_2 { get; set; }
        //public ListType L2_1 { get; set; }
        //public ListType L3_4 { get; set; }
        //public UNITID UNITID { get; set; }
        //public StringType EQPStatus { get; set; } = new StringType(1);
        //public ALID ALID { get; set; }
        //public ALTX ALTX { get; set; }

        ////public ListType L4_0 { get; set; } = new ListType(0);
        //public List<IONIZERRPT> IONIZERRPTs { get; set; } = new List<IONIZERRPT>();
    }

    [Serializable]
    public class CIMS1F62 : CimBasicClass
    {
        public CIMS1F62()
        {
            Title = "All Equipment Status Report Acknowledge";
            Stream = 1;
            Function = 62;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public BinaryType ACKC1 { get; set; }
    }

    [Serializable]
    public class CIMS1F63 : CimBasicClass
    {
        public CIMS1F63()
        {
            Title = "All Equipment Status Request";
            Stream = 1;
            Function = 63;
            Wait = true;
            AutoCreateInstance();
        }
    }

    [Serializable]
    public class CIMS1F64 : CimBasicClass
    {
        public CIMS1F64()
        {
            Title = "All Equipment Status Data";
            Stream = 1;
            Function = 64;
            Wait = false;

            AutoCreateInstance();
        }
        public EQPReport EQPReport { get; set; }
    }

    [Serializable]
    public class CIMS1F67 : CimBasicClass
    {
        public CIMS1F67()
        {
            Title = "All Glass Position Data Report";
            Stream = 1;
            Function = 67;
            Wait = true;

            AutoCreateInstance();
        }

        public List<SUBUNIT> SUBUNITS { get; set; } = new List<SUBUNIT>();
    }

    [Serializable]
    public class CIMS1F68 : CimBasicClass
    {
        public CIMS1F68()
        {
            Title = "All Glass Position Data Report Acknowledge";
            Stream = 1;
            Function = 68;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC1 ACKC1 { get; set; }
    }

    public class CIMS1F75 : CimBasicClass
    {
        public CIMS1F75()
        {
            Title = "Ionizer Status Report";
            Stream = 1;
            Function = 75;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<IONIZERRPT> IONIZERRPTS { get; set; } = new List<IONIZERRPT>();
    }

    public class CIMS1F76 : CimBasicClass
    {
        public CIMS1F76()
        {
            Title = "Ionizer Status Report Acknowledge";
            Stream = 1;
            Function = 76;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC1 ACKC1 { get; set; }
    }
}
