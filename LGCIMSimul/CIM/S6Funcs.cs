using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LGCIMServer
{
    public partial class ServerCIM
    {
        void InitS6()
        {
            gem.OnS6F91Event += Gem_OnS6F91Event;
            gem.OnS6F61Event += Gem_OnS6F61Event;
            gem.OnS6F63Event += Gem_OnS6F63Event;
            gem.OnS6F66Event += Gem_OnS6F66Event;
            gem.OnS6F67Event += Gem_OnS6F67Event;
            gem.OnS6F69Event += Gem_OnS6F69Event;
            gem.OnS6F71Event += Gem_OnS6F71Event;
            gem.OnS6F73Event += Gem_OnS6F73Event;
            gem.OnS6F83Event += Gem_OnS6F83Event;
            gem.OnS6F85Event += Gem_OnS6F85Event;
            gem.OnS6F89Event += Gem_OnS6F89Event;
            gem.OnS6F93Event += Gem_OnS6F93Event;
        }


        private void Gem_OnS6F61Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            gem.HSMSSend(new CIMS6F62());

            // [보완] Load Glass Data(S6F61) 수신 후 New Glass Data(S2F87)를 보고하여 시나리오 진행
            SendS2F87(serverStatus.GLSSTATUS, 1); 
        }

        private void Gem_OnS6F63Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F63 cim = (CIMS6F63)e.cimClass;

            var func = new CIMS6F64();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        // LOT END
        public void SendS6F65()
        {
            var func = new CIMS6F65();
            func.UNITID = serverStatus.UnitID;
            //func.LOTID = $"{serverStatus.LOTENDPNLINFOs.Count}EA012345678";

            //rework 요청 받았을 때
            if (serverStatus.ReworkFlag == true)
                func.ENDFLAG = ENDFLAG.ENDFLAGEnum.Rework;

            foreach(var s in serverStatus.LOTENDPNLINFOs)
            {
                func.SLOTITEM_LOTENDS.Add(s);
            }

            // dummy
            if (serverStatus.LOTENDPNLINFOs.Count == 0)
            {
                func.SLOTITEM_LOTENDS.Add(new SLOTITEM_LOTEND());
                func.SLOTITEM_LOTENDS.Add(new SLOTITEM_LOTEND());
                func.SLOTITEM_LOTENDS.Add(new SLOTITEM_LOTEND());
            }
            gem.HSMSSend(func);
        }

        private void Gem_OnS6F66Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F66 cim = (CIMS6F66)e.cimClass;
            serverStatus.LOTENDPNLINFOs.Clear();
        }

        private void Gem_OnS6F67Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F67 cim = (CIMS6F67)e.cimClass;

            var func = new CIMS6F68();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F69Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F70();

            func.UNITID = serverStatus.UnitID;
            func.CassetteMAP = new List<CassetteMAP>();
            int portNum = 4;
            for(int i = 0; i < portNum; i++)
            {
                var cstMap = new CassetteMAP();
                cstMap.LOTID = $"LOTID_{i + 1}";
                cstMap.CSTID = $"CSTID_{i + 1}";
                cstMap.PORTID = (PORTID)i;
                cstMap.MAP = "1000000000000000000100000000000000000001";
                cstMap.CSTREADY = "1";

                func.CassetteMAP.Add(cstMap);
            }

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F71Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F72();
            func.UNITID = serverStatus.UnitID;
            func.ACKC6A = (byte)ACKC6A.ACKC6AEnum.Accept;
            gem.HSMSSend(func);
        }

        private void Gem_OnS6F73Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F74();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F83Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F84();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F85Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F85 cim = (CIMS6F85)e.cimClass;
            var func = new CIMS6F86();
            func.UNITID = cim.UNITID;
            func.ACKC6 = (byte)ACKC6.ACKC6Enum.Accept;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F89Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F90();
            func.UNITID = serverStatus.UnitID;
            func.ACKC6 = (byte)ACKC6.ACKC6Enum.Accept;
            gem.HSMSSend(func);

            serverStatus.ReworkFlag = true;
        }

        private void Gem_OnS6F91Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F92();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F93Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS6F94();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }
    }
}
