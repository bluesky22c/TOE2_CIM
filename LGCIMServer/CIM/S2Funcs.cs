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
        int glsIDnum = 0;
        public event EventHandler<EventArgs> propertyRefresh = delegate { };

        void InitS2()
        {
            gem.OnS2F17Event += Gem_OnS2F17Event;
            gem.OnS2F25Event += Gem_OnS2F25Event;
            gem.OnS2F61Event += Gem_OnS2F61Event;
            gem.OnS2F63Event += Gem_OnS2F63Event;
            gem.OnS2F65Event += Gem_OnS2F65Event;
            gem.OnS2F67Event += Gem_OnS2F67Event;
            gem.OnS2F77Event += Gem_OnS2F77Event;
            gem.OnS2F79Event += Gem_OnS2F79Event;
        }

        private void Gem_OnS2F25Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS2F26();
            CIMS2F25 cim = (CIMS2F25)e.cimClass;

            func.ABS = cim.ABS;

            gem.HSMSSend(func);
        }

        private void Gem_OnS2F17Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            gem.HSMSSend(new CIMS2F18 { TIME = DateTime.Now });
            // test
            //gem.HSMSSend(new CIMS2F18 { TIME = "20200823200000" });
        }

        public void SendS2F31()
        {
            gem.HSMSSend(new CIMS2F31 
            {
                UNITID = serverStatus.UnitID,
                TIME = DateTime.Now.ToString("yyyyMMddhhmmss"),
            });
        }

        private void Gem_OnS2F61Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F61 cim = (CIMS2F61)e.cimClass;
            serverStatus.UnitID = cim.UNITID;
            propertyRefresh(this, null);

            var func = new CIMS2F62();
            func.UNITID = serverStatus.UnitID;
            func.ACKC2A = (byte)ACKC2A.ACKC2AEnum.Accept;

            gem.HSMSSend(func);
        }

        private void Gem_OnS2F63Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS2F64();
            func.UNITID = serverStatus.UnitID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS2F67Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS2F68();
            func.UNITID = serverStatus.UnitID;
            func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;
            gem.HSMSSend(func);
        }

        public void SendS2F69()
        {
            var func = new CIMS2F69();
            func.UNITID = serverStatus.UnitID;
            func.HOSTMSG = $"Request Unload Data.";
            gem.HSMSSend(func);
        }

        public void SendS2F87(string glsStatus, int glsODR)
        {
            var func = new CIMS2F87();

            func.UNITID = serverStatus.UnitID;
            func.GLSSTATUS = glsStatus;

            if (func.GLSSTATUS == "1")      // EXIST
            {
                glsIDnum += 1111;
                
                var AGlass = new LoadGlass();
                AGlass.LOTID = serverStatus.AGlass.LOTID;
                AGlass.CSTID = serverStatus.AGlass.CSTID;
                AGlass.SLOTID = serverStatus.AGlass.SLOTID;
                AGlass.RECIPEID = serverStatus.AGlass.RECIPEID;
                AGlass.PRCID = serverStatus.AGlass.PRCID;
                AGlass.GLSODR = glsODR.ToString();
                AGlass.GLSID = serverStatus.AGlass.GLSID;
                AGlass.GLSJUDGE = serverStatus.AGlass.GLSJUDGE;
                AGlass.PNLIF = serverStatus.AGlass.PNLIF;
                AGlass.SUBMDLIF = serverStatus.AGlass.SUBMDLIF;
                AGlass.PORTID = serverStatus.AGlass.PORTID;
                AGlass.GLSCODE = serverStatus.AGlass.GlassInfoExist ? (ushort)glsIDnum : (ushort)0;
                AGlass.REINPUT = serverStatus.AGlass.REINPUT;
                AGlass.GLSTHICK = serverStatus.AGlass.GLSTHICK;
                AGlass.PARTNUM = serverStatus.AGlass.PARTNUM;
                AGlass.PRODTYPE = serverStatus.AGlass.PRODTYPE;
                AGlass.ATTRIBUTE = serverStatus.AGlass.ATTRIBUTE;
                AGlass.GLSTYPE = serverStatus.AGlass.GLSTYPE;
                AGlass.KEYID = serverStatus.AGlass.KEYID;
                //AGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("FROM_CST_QTY"), LSDVALUE = new LSDVALUE("60") });
                //AGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("LOT_JUDGE"), LSDVALUE = new LSDVALUE("G") });
                //AGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("LOT_TYPE"), LSDVALUE = new LSDVALUE("G") });
                //AGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("MODEL_ATTR"), LSDVALUE = new LSDVALUE("OLED") });
                //AGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("PAIRGLS"), LSDVALUE = new LSDVALUE("Y") });
                AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("CUT_TYPE"), GSDVALUE = new GSDVALUE("A") });
                AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIRINFO"), GSDVALUE = new GSDVALUE("Y") });
                AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIR_GLASS_CODE"), GSDVALUE = new GSDVALUE("13604") });
                AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("ROUTE_CHAMBER"), GSDVALUE = new GSDVALUE("") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("DUMMY_COUNT"), GSDVALUE = new GSDVALUE("0") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("GATE_ALIGN_EQP"), GSDVALUE = new GSDVALUE("A4TPHT1010") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("LAYER_ALIGN_EQP"), GSDVALUE = new GSDVALUE("A4TPHT0310") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("MATCH_SLOT"), GSDVALUE = new GSDVALUE("00") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("ORT_FLAG"), GSDVALUE = new GSDVALUE("0") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PI_FLAG"), GSDVALUE = new GSDVALUE("0") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PPBOX_ID"), GSDVALUE = new GSDVALUE("AZ2IF9E611") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PPBOX_SN"), GSDVALUE = new GSDVALUE("J20L96T611B0") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("TFT_GATE_PHT_EQP"), GSDVALUE = new GSDVALUE("A4TPHT0310") });
                //AGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("TFT_GLS_MAKER"), GSDVALUE = new GSDVALUE("A") });
                func.LoadGlass.Add(AGlass);

                glsIDnum += 1111;

                var BGlass = new LoadGlass();
                BGlass.LOTID = serverStatus.BGlass.LOTID;
                BGlass.CSTID = serverStatus.BGlass.CSTID;
                BGlass.SLOTID = serverStatus.BGlass.SLOTID;
                BGlass.RECIPEID = serverStatus.BGlass.RECIPEID;
                BGlass.PRCID = serverStatus.BGlass.PRCID;
                BGlass.GLSODR = glsODR.ToString();
                BGlass.GLSID = serverStatus.BGlass.GLSID;
                BGlass.GLSJUDGE = serverStatus.BGlass.GLSJUDGE;
                BGlass.PNLIF = serverStatus.BGlass.PNLIF;
                BGlass.SUBMDLIF = serverStatus.BGlass.SUBMDLIF;
                BGlass.PORTID = serverStatus.BGlass.PORTID;
                BGlass.GLSCODE = serverStatus.BGlass.GlassInfoExist ? (ushort)glsIDnum : (ushort)0;
                BGlass.REINPUT = serverStatus.BGlass.REINPUT;
                BGlass.GLSTHICK = serverStatus.BGlass.GLSTHICK;
                BGlass.PARTNUM = serverStatus.BGlass.PARTNUM;
                BGlass.PRODTYPE = serverStatus.BGlass.PRODTYPE;
                BGlass.ATTRIBUTE = serverStatus.BGlass.ATTRIBUTE;
                BGlass.GLSTYPE = serverStatus.BGlass.GLSTYPE;
                BGlass.KEYID = serverStatus.BGlass.KEYID;
                BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("CUT_TYPE"), GSDVALUE = new GSDVALUE("B") });
                BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIRINFO"), GSDVALUE = new GSDVALUE("Y") });
                BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIR_GLASS_CODE"), GSDVALUE = new GSDVALUE("13604") });
                BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("ROUTE_CHAMBER"), GSDVALUE = new GSDVALUE("") });
                //BGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("FROM_CST_QTY"), LSDVALUE = new LSDVALUE("60") });
                //BGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("LOT_JUDGE"), LSDVALUE = new LSDVALUE("G") });
                //BGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("LOT_TYPE"), LSDVALUE = new LSDVALUE("G") });
                //BGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("MODEL_ATTR"), LSDVALUE = new LSDVALUE("OLED") });
                //BGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("PAIRGLS"), LSDVALUE = new LSDVALUE("Y") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("CUT_TYPE"), GSDVALUE = new GSDVALUE("A") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("DUMMY_COUNT"), GSDVALUE = new GSDVALUE("0") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("GATE_ALIGN_EQP"), GSDVALUE = new GSDVALUE("A4TPHT1010") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("LAYER_ALIGN_EQP"), GSDVALUE = new GSDVALUE("A4TPHT0310") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("MATCH_SLOT"), GSDVALUE = new GSDVALUE("00") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("ORT_FLAG"), GSDVALUE = new GSDVALUE("0") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PI_FLAG"), GSDVALUE = new GSDVALUE("0") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PPBOX_ID"), GSDVALUE = new GSDVALUE("AZ2IF9E611") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PPBOX_SN"), GSDVALUE = new GSDVALUE("J20L96T611B0") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("TFT_GATE_PHT_EQP"), GSDVALUE = new GSDVALUE("A4TPHT0310") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("TFT_GLS_MAKER"), GSDVALUE = new GSDVALUE("A") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIRINFO"), GSDVALUE = new GSDVALUE("Y") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("PAIR_GLASS_CODE"), GSDVALUE = new GSDVALUE("13604") });
                //BGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("ROUTE_CHAMBER"), GSDVALUE = new GSDVALUE("") });
                func.LoadGlass.Add(BGlass);

                serverStatus.LOTENDPNLINFOs.Add(new SLOTITEM_LOTEND
                { 
                    GLSODR = BGlass.GLSODR, 
                    SLOTID = BGlass.SLOTID,
                    GLSID = BGlass.GLSID,
                    RECIPEID = BGlass.RECIPEID,
                    GLSEXIST = 1,
                    GLSSELECTED = 1,
                    PROCUNITID = (PROCUNITID)serverStatus.UnitID,
                    GLSJUDGE = BGlass.GLSJUDGE
                });
            }
            gem.HSMSSend(func);
        }


        private void Gem_OnS2F65Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS2F66();
            func.UNITID = serverStatus.UnitID;
            func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;
            gem.HSMSSend(func);

            Thread.Sleep(1000);

            SendS6F65();
        }


        private void Gem_OnS2F77Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F77 cim = (CIMS2F77)e.cimClass;
            var func = new CIMS2F78();
            func.UNITID = cim.UNITID;
            func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;

            gem.HSMSSend(func);
        }

        private void Gem_OnS2F79Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F79 cim = (CIMS2F79)e.cimClass;
            var func = new CIMS2F80();

            func.UNITID = cim.UNITID;
            if (serverStatus.GLSSTATUS == "1")      // EXIST
            {
                var sendGlass = new LoadGlass();
                sendGlass.LOTID = serverStatus.AGlass.LOTID;
                sendGlass.CSTID = serverStatus.AGlass.CSTID;
                sendGlass.SLOTID = serverStatus.AGlass.SLOTID;
                sendGlass.RECIPEID = serverStatus.AGlass.RECIPEID;
                sendGlass.PRCID = serverStatus.AGlass.PRCID;
                sendGlass.GLSODR = "0";
                sendGlass.GLSID = cim.GLSID;
                sendGlass.GLSJUDGE = serverStatus.AGlass.GLSJUDGE;
                sendGlass.PNLIF = serverStatus.AGlass.PNLIF;
                sendGlass.SUBMDLIF = serverStatus.AGlass.SUBMDLIF;
                sendGlass.PORTID = serverStatus.AGlass.PORTID;
                sendGlass.GLSCODE = cim.GLSCODE;
                sendGlass.REINPUT = serverStatus.AGlass.REINPUT;
                sendGlass.GLSTHICK = serverStatus.AGlass.GLSTHICK;
                sendGlass.PARTNUM = serverStatus.AGlass.PARTNUM;
                sendGlass.PRODTYPE = serverStatus.AGlass.PRODTYPE;
                sendGlass.ATTRIBUTE = serverStatus.AGlass.ATTRIBUTE;
                sendGlass.GLSTYPE = serverStatus.AGlass.GLSTYPE;
                sendGlass.KEYID = serverStatus.AGlass.KEYID;
                sendGlass.LSDS.Add(new LSD { LSDITEM = new LSDITEM("LotItemA1"), LSDVALUE = new LSDVALUE("LotValueA1") });
                sendGlass.GSDS.Add(new GSD { GSDITEM = new GSDITEM("GlassItemA1"), GSDVALUE = new GSDVALUE("GlassValueA1") });
                func.LoadGlass = sendGlass;
            }

            func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;

            gem.HSMSSend(func);
        }

        public void SendS2F99(RCMD.RCMDEnum rcmd)
        {
            var func = new CIMS2F99();
            func.RCMD = rcmd;
            func.HOSTMSG = $"{rcmd}";

            gem.HSMSSend(func);
        }
    }
}
