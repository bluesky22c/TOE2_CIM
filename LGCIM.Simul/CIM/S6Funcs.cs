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

        // [보완] S6F63: Unload Glass Data Report
        public void SendS6F63()
        {
            var func = new CIMS6F63();
            func.UNITID = serverStatus.UnitID;
            func.STAGEID = 1;

            // Log 분석 결과: UnLoadGlass 리스트에 데이터가 포함되어야 함
            
            // Dummy glass data 생성 (GemInfo.cs의 UnLoadGlass 및 GSD 타입 사용)
            var glassInfo = new UnLoadGlass();
            glassInfo.LOTID = "CETL5A001200";
            glassInfo.CSTID = "PXCCST100041";
            glassInfo.SLOTID = "24";
            glassInfo.GLSID = "6TL5900057A00";
            glassInfo.PNLIF = new string('G', 2000); // 2000자 더미 데이터
            glassInfo.SUBMDLIF = new string(' ', 2000);
            glassInfo.PRCID = "77490";
            glassInfo.DICODE = 0;
            glassInfo.PORTID = 2;
            glassInfo.RECIPEID = "123QDLS3";
            glassInfo.GLSJUDGE = "G";
            glassInfo.PARTNUM = "62084-0129A";
            glassInfo.PRODTYPE = "PRODUCTION";
            glassInfo.ATTRIBUTE = "CELL_GAP_G";
            glassInfo.GLSTYPE = "N";
            glassInfo.GLSPROC = "0";
            glassInfo.GLSCODE = 16152;
            glassInfo.DEST = 0;
            glassInfo.KEYID = "CETL5A001200_20251106152106";
            
            // sub-item list 보고 (GSD 타입)
            glassInfo.GSDS.Add(new GSD { GSDITEM = "CUT_TYPE", GSDVALUE = "A" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "DEPO_UNIT", GSDVALUE = "PXCSPT020305" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "DUMMY_COUNT", GSDVALUE = "0" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "GATE_ALIGN_EQP", GSDVALUE = "TPHT0650" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "LAYER_ALIGN_EQP", GSDVALUE = "TPHT2700" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "MATCH_SLOT", GSDVALUE = "00" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "ORT_FLAG", GSDVALUE = "0" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "PI_FLAG", GSDVALUE = "0" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "PPBOX_ID", GSDVALUE = "OX2EF8B420" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "PPBOX_SN", GSDVALUE = "W225199210" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "TFT_GATE_PHT_EQP", GSDVALUE = "TPHT2700" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "TFT_GLS_MAKER", GSDVALUE = "O" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "PAIRINFO", GSDVALUE = "Y" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "PAIR_GLASS_CODE", GSDVALUE = "16182" });
            glassInfo.GSDS.Add(new GSD { GSDITEM = "ROUTE_CHAMBER", GSDVALUE = "" });

            // 메인 리스트에 추가 (CIMS6F63의 리스트 이름은 UnLoadGlass 임)
            func.UnLoadGlass.Add(glassInfo);
            
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
