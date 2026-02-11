using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCIMServer
{
    public partial class ServerCIM
    {
        void InitS10()
        {

        }

        public void SendS10F3(string text)
        {
            var func = new CIMS10F3();
            func.TID = 0;
            func.TEXT = text;

            gem.HSMSSend(func);
        }
    }
}
