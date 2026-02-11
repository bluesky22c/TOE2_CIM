using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS9F0 : CimBasicClass
    {
        public CIMS9F0()
        {
            Title = "Abort Transaction";
            Stream = 9;
            Function = 0;
            Wait = false;
        }
    }

    [Serializable]
    public class CIMS9F13 : CimBasicClass
    {
        public CIMS9F13()
        {
            Title = "Conversation Timeout";
            Stream = 9;
            Function = 13;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public MEXP MEXP { get; set; }
        public EDID EDID { get; set; }
    }
}
