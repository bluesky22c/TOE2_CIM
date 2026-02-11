using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS5F0 : CimBasicClass
    {
        public CIMS5F0()
        {
            Title = "Abort Transaction";
            Stream = 5;
            Function = 0;
            Wait = false;

            AutoCreateInstance();
        }
    }

    [Serializable]
    public class CIMS5F1 : CimBasicClass
    {
        public CIMS5F1()
        {
            Title = "Alarm Report";
            Stream = 5;
            Function = 1;
            Wait = true;

            L1_4 = new ListType(4);

            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public ALCD ALCD { get; set; }
        public ALID ALID { get; set; }
        public ALTX ALTX { get; set; }

    }

    [Serializable]
    public class CIMS5F2 : CimBasicClass
    {
        public CIMS5F2()
        {
            Title = "Alarm Report Acknowledge";
            Stream = 5;
            Function = 2;
            Wait = false;

            L1_2 = new ListType(2);

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public ACKC5 ACKC5 { get; set; }
    }
}
