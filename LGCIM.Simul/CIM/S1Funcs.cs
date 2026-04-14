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
        void InitS1()
        {
            gem.OnS1F1Event += Gem_OnS1F1Event;
            gem.OnS1F61Event += Gem_OnS1F61Event;
            gem.OnS1F67Event += Gem_OnS1F67Event;
            gem.OnS1F75Event += Gem_OnS1F75Event;
        }


        private void Gem_OnS1F61Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS1F62();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS1F1Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS1F2();
            func.MDLN = serverStatus.EQModelType;
            gem.HSMSSend(func);
        }
        
        private void Gem_OnS1F67Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS1F68();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
            //SendS6F65();
        }

        private void Gem_OnS1F75Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS1F76();
            func.UNITID = serverStatus.UnitID;
            func.ACKC1 = (byte)ACKC1.ACKC1Enum.Accept;
            gem.HSMSSend(func);
        }
    }
}
