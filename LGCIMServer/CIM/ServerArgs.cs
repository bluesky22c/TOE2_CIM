using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCIMServer
{
    public class ChangeUI : EventArgs
    {
        public GLSODR GLSODR { get; set; } = "0";
        public GLSSTATUS GLSSTATUS { get; set; } = "1";
    }
}
