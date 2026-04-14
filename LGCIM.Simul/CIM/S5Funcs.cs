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
        void InitS5()
        {
            gem.OnS5F1Event += Gem_OnS5F1Event;
        }

        private void Gem_OnS5F1Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS5F2();
            func.UNITID = serverStatus.UnitID;
            gem.HSMSSend(func);
        }
    }
}
