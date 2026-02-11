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
        void InitS9()
        {

        }

        //Timeout
        public void SendS9F13(string message)
        {
            var func = new CIMS9F13();
            func.MEXP = message;
            //func.EDID = "";

            gem.HSMSSend(func);
        }
    }
}
