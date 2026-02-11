using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIM
{
    public partial class ClientCIM
    {
        void InitS10()
        {
            gem.OnS10F3Event += Gem_OnS10F3Event;
        }

        private void Gem_OnS10F3Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS10F3 cim = (CIMS10F3)e.cimClass;
            HostMessagePopup(this, new CIMArgs { Message = cim.TEXT });

            gem.HSMSSend(new CIMS10F4());
        }
    }
}
