using MMISoft.HSMS;
using System;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS10F0 : CimBasicClass
    {
        public CIMS10F0()
        {
            Title = "Abort Transaction";
            Stream = 10;
            Function = 0;

            Wait = false;
        }
    }

    public class CIMS10F3 : CimBasicClass
    {
        public CIMS10F3()
        {
            Title = "Terminal Display, Single";
            Stream = 10;
            Function = 3;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public TID TID { get; set; }
        public TEXT TEXT { get; set; }
    }

    public class CIMS10F4 : CimBasicClass
    {
        public CIMS10F4()
        {
            Title = "Terminal Display, Single Acknowledge";
            Stream = 10;
            Function = 4;
            Wait = false;

            AutoCreateInstance();
        }

        public ACKC10 ACKC10 { get; set; }
    }
}
