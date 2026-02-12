using Engion;
using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TOECIMLib;
//using TOELib;

namespace TOECIM
{
    public partial class ClientCIM
    {
        void InitS6()
        {
            gem.OnS6F62Event += Gem_OnS6F62Event;
            gem.OnS6F64Event += Gem_OnS6F64Event;
            gem.OnS6F65Event += Gem_OnS6F65Event;
            gem.OnS6F68Event += Gem_OnS6F68Event;
            gem.OnS6F70Event += Gem_OnS6F70Event;
            gem.OnS6F74Event += Gem_OnS6F74Event;
            gem.OnS6F84Event += Gem_OnS6F84Event;
            gem.OnS6F92Event += Gem_OnS6F92Event;
        }


        public void SendS6F61() 
        {
            if(cimStatus.RCS == RCS.RCSEnum.OffLine)
            {
                log.AddLogMessage(LogType.Warning, 0, $"현재 {cimStatus.RCS} 모드입니다.");
                return;
            }

            //Load Glass 초기화
            //cimStatus.LoadGlass = null;
            //cimStatus.LoadGlassEQ = false;

            var func = new CIMS6F61();
            func.UNITID = cimStatus.UnitID;
            func.STAGEID = cimStatus.StageID;

            gem.HSMSSend(func);
        }

        private void Gem_OnS6F62Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F62 cim = (CIMS6F62)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    //Glass data 요청 후 Timeout 등록
                    timeout.RegNewTimeout(ETimeout.toGlassDataLoad, Program.SystemSetup.Timeout * 1000);
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F62Event] Load Glass Data Request Error.");
                    break;
            }
        }

        //Unload
        public void SendS6F63()
        {
            try
            {
                var func = new CIMS6F63();

                func.UNITID = cimStatus.UnitID;
                func.STAGEID = cimStatus.StageID;

                if (cimStatus.UnloadGlass == null)
                {
                    gem.HSMSSend(func);
                    return;
                }

                func.UnLoadGlass = new List<UnLoadGlass>();

                //A Glass
                UnLoadGlass ToUnloadGlass(GlassInfo gls)
                {
                    var cimUnload_A = new UnLoadGlass();
                    cimUnload_A.GLSODR = gls.GLSODR;
                    cimUnload_A.LOTID = gls.LOTID;
                    if (gls.GlassInfoExist == false)
                    {
                        cimUnload_A.GLSODR = "";
                        cimUnload_A.LOTID = "";
                    }
                    cimUnload_A.CSTID = gls.CSTID;
                    cimUnload_A.SLOTID = gls.SLOTID;
                    cimUnload_A.GLSID = gls.GLSID;
                    cimUnload_A.PNLIF = gls.PNLIF;
                    cimUnload_A.SUBMDLIF = gls.SUBMDLIF;
                    cimUnload_A.PRCID = gls.PRCID;
                    cimUnload_A.DICODE = 0;   //일단 기본값
                    cimUnload_A.PORTID = gls.PORTID;
                    cimUnload_A.RECIPEID = gls.RECIPEID;
                    cimUnload_A.GLSJUDGE = GetGlassJudgeCode(gls.GLSJUDGE, cimStatus.CimCommData.ToCIM.GlassJudgeCode);
                    cimUnload_A.PARTNUM = gls.PARTNUM;
                    cimUnload_A.PRODTYPE = gls.PRODTYPE;
                    cimUnload_A.ATTRIBUTE = gls.ATTRIBUTE;
                    cimUnload_A.GLSTYPE = gls.GLSTYPE;
                    cimUnload_A.GLSPROC = "0";   //일단 기본값
                    cimUnload_A.GLSCODE = gls.GLSCODE;
                    cimUnload_A.DEST = 0;
                    cimUnload_A.KEYID = gls.KEYID;

                    cimUnload_A.GSDS = new List<LGHSMSBase.GSD>();
                    foreach (var gsd in gls.GSDS)
                    {
                        var g = new LGHSMSBase.GSD { GSDITEM = gsd.GSDITEM, GSDVALUE = gsd.GSDVALUE };
                        cimUnload_A.GSDS.Add(g);
                    }
                    return cimUnload_A;
                }
                var unloadGlassA = ToUnloadGlass(cimStatus.UnloadGlass.AGlass);
                var unloadGlassB = ToUnloadGlass(cimStatus.UnloadGlass.BGlass);

                func.UnLoadGlass.Add(unloadGlassA);
                func.UnLoadGlass.Add(unloadGlassB);

                gem.HSMSSend(func);

                ////Unload Glass에 값 복사
                //MoveLoadtoUnload();

                UpdateUI(this, null);

            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, ee.ToString());
            }
        }

        public void MoveLoadtoUnload()
        {
            if(cimStatus.LoadGlass == null)
            {
                cimStatus.UnloadGlass = null;
                return;
            }

            //Unload Glass에 값 복사
            cimStatus.UnloadGlass = cimStatus.LoadGlass;

            cimStatus.UnloadGlass.AGlass.ReportType = ReportType.Unload;
            cimStatus.UnloadGlass.AGlass.DICODE = 0;
            cimStatus.UnloadGlass.AGlass.GLSPROC = "0";
            cimStatus.UnloadGlass.AGlass.DEST = 0;

            //Unload Glass에 값 복사
            cimStatus.UnloadGlass.BGlass.ReportType = ReportType.Unload;
            cimStatus.UnloadGlass.BGlass.DICODE = 0;
            cimStatus.UnloadGlass.BGlass.GLSPROC = "0";
            cimStatus.UnloadGlass.BGlass.DEST = 0;

            cimStatus.LoadGlass = null;
        }

        /// <summary>
        /// oldJudge : ECS에서 Glass Data 로드할때 지정된 Judge 값
        /// newJudge : Console에서 검사 후 판정한 Judge 값
        /// </summary>
        /// <param name="oldJudge"></param>
        /// <param name="newJudge"></param>
        /// <returns></returns>
        public string GetGlassJudgeCode(string oldJudge, string newJudge)
        {
            //todo : glass judge 관련 스펙이 있으면 작업 필요
            return newJudge;
        }

        private void Gem_OnS6F64Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F64 cim = (CIMS6F64)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    //pause 요청 시 UW, UR 보고
                    if (cimStatus.Pause == true)
                    {
                        //cimStatus.LoadGlass = null;
                        //cimStatus.CstGlassExist = false;
                        NextGlassProcess();
                        return;
                    }

                    // Load Glass Data Requset
                    //if (cimStatus.CimCommData.ToCIM.Command == EQPtoCIM.InspectDone)
                    //{
                    //    SendS6F61();
                    //}
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F64Event] UnLoad Glass Data Send Error.");
                    break;
            }
        }

        private void Gem_OnS6F65Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS6F65 cim = (CIMS6F65)e.cimClass;

                var func_66 = new CIMS6F66();
                func_66.UNITID = cim.UNITID;
                func_66.PORTID = cim.PORTID;
                func_66.LOTID = cim.LOTID;

                string temp_lot = cim.LOTID;
                var temp_cst = cim.CSTID;
                var temp_recipe = cim.RECIPEID;
                var temp_prc = cim.PRCID;
                var temp_unitID = cim.UNITID;
                var temp_key = $"{temp_lot}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                gem.HSMSSend(func_66);

                var func_83 = new CIMS6F83();
                func_83.LOTID = temp_lot;
                func_83.CSTID = temp_cst;
                func_83.RECIPEID = temp_recipe;
                func_83.PRCID = temp_prc;
                func_83.UNITID = temp_unitID;
                func_83.KEYID = temp_key;
                func_83.LOTJUDGE = "G";  //어떻게 판정하는지 확인 필요

                if (cimStatus.LoadGlass != null)
                {
                    func_83.LSDS = new List<LGHSMSBase.LSD>();

                    foreach (var lsd in cimStatus.LoadGlass.AGlass.LSDS)
                    {
                        var l = new LGHSMSBase.LSD { LSDITEM = lsd.LSDITEM, LSDVALUE = lsd.LSDVALUE };
                        func_83.LSDS.Add(l);
                    }
                }

                //console에서 받아와야 함
                //cimStatus.APDIItems = new List<APDInfo>();
                //cimStatus.APDIItems.Add(new APDInfo { APDName = "APD1", APDValue = "1" });
                //cimStatus.APDIItems.Add(new APDInfo { APDName = "APD2", APDValue = "2" });
                //cimStatus.APDIItems.Add(new APDInfo { APDName = "APD3", APDValue = "3" });

                //func_83.DVITEMS = new List<DVItem>();
                //foreach (var apd in cimStatus.APDIItems)
                //{
                //    var dv = new DVItem();
                //    dv.DVNAME = apd.APDName;
                //    dv.DVVAL = apd.APDValue;
                //    func_83.DVITEMS.Add(dv);
                //}

                gem.HSMSSend(func_83);
            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, ee.ToString());
            }
        }

        public void SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum glsMv)
        {
            if (cimStatus.CurAlarm != null && glsMv != GLSMVSTATUS.GLSMVSTATUSEnum.CR)
            {
                log.Error($"장비가 Down 상태여서 Glass Move Request {glsMv}를 요청하지 않습니다");
                return;
            }
            log.Information($"Send Move Req: {glsMv}");
            var func = new CIMS6F67();
            func.GLSMVSTATUS = glsMv;
            func.UNITID = cimStatus.UnitID;
            func.STAGEID = cimStatus.StageID;

            if (cimStatus.LoadGlass != null)
            {
                func.LDGLSCODE_A = cimStatus.LoadGlass.AGlass.GLSCODE;
                func.LDGLSCODE_B = cimStatus.LoadGlass.BGlass.GLSCODE;
            }
            if (cimStatus.UnloadGlass != null && glsMv != GLSMVSTATUS.GLSMVSTATUSEnum.LR)
            {
                func.UDGLSCODE_A = cimStatus.UnloadGlass.AGlass.GLSCODE;
                func.UDGLSCODE_B = cimStatus.UnloadGlass.BGlass.GLSCODE;
            }
            cimStatus.LastReportMV = glsMv;
            gem.HSMSSend(func);
        }

        private void Gem_OnS6F68Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F68 cim = (CIMS6F68)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    OnGLSMVChanged(this, new CIMArgs());
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F62Event] Load Glass Data Request Error.");
                    break;
            }

        }

        public void SendS6F69()
        {
            var func = new CIMS6F69();
            func.UNITID = cimStatus.UnitID;
            gem.HSMSSend(func);
        }

        private void Gem_OnS6F70Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F70 cim = (CIMS6F70)e.cimClass;

            var args = new CassetteArgs();
            args.CassetteMap.AddRange(cim.CassetteMAP);            
            ShowCassetteMAP(this, args);
        }

        public void SendS6F71(SLOTITEM_SAMPLE slotItem)
        {
            var func = new CIMS6F71();
            func.UNITID = cimStatus.UnitID;
            func.STAGEID = cimStatus.StageID;
            func.SLOTITEM_SAMPLE = slotItem;
            gem.HSMSSend(func);

            log.AddLogMessage(LogType.Information, 0, $"[S6F71] Sample Slot 요청됨. PortID : {slotItem.PORTID.Value}, SlotID : {slotItem.SLOTID.Value}");
        }

        public void SendS6F73()
        {
            try
            {
                //if (cimStatus.UnloadGlass == null)
                //{
                //    var func = new CIMS6F73();
                //    log.AddLogMessage(LogType.Error, 0, $"[SendS6F73] No Unload Glass Data.");
                //    gem.HSMSSend(func);
                //    return;
                //}

                // 보내기전에 Console 한테 받아오기
                var res = Common.ToConsole(new CIMtoEQPData { Command = CIMtoEQP.APDReq });
                var Aapd = res.AcutAPD;
                var bapd = res.BcutAPD;

                //A Glass 보고----------------------------------------------------------------
                if (cimStatus.UnloadGlass != null && cimStatus.UnloadGlass.AGlass.GlassInfoExist)
                {
                    var func_A = new CIMS6F73();
                    func_A.LOTID = cimStatus.UnloadGlass.AGlass.LOTID;
                    func_A.CSTID = cimStatus.UnloadGlass.AGlass.CSTID;
                    func_A.RECIPEID = cimStatus.UnloadGlass.AGlass.RECIPEID;
                    func_A.PRCID = cimStatus.UnloadGlass.AGlass.PRCID;
                    func_A.SLOTID = cimStatus.UnloadGlass.AGlass.SLOTID;
                    func_A.UNITID = cimStatus.UnitID;

                    func_A.GLSPRCS.GLSID = cimStatus.UnloadGlass.AGlass.GLSID;
                    func_A.GLSPRCS.GLSJUDGE = cimStatus.UnloadGlass.AGlass.GLSJUDGE;
                    func_A.GLSPRCS.PNLIF = cimStatus.UnloadGlass.AGlass.PNLIF;
                    func_A.GLSPRCS.SUBMDLIF = cimStatus.UnloadGlass.AGlass.SUBMDLIF;
                    func_A.GLSPRCS.GLSCODE = cimStatus.UnloadGlass.AGlass.GLSCODE;

                    func_A.GLSPRCS.GSDS = new List<LGHSMSBase.GSD>();
                    foreach (var g in cimStatus.UnloadGlass.AGlass.GSDS)
                    {
                        var gsd = new LGHSMSBase.GSD();
                        gsd.GSDITEM = g.GSDITEM;
                        gsd.GSDVALUE = g.GSDVALUE;
                        func_A.GLSPRCS.GSDS.Add(gsd);
                    }

                    // Console에서 받아온 APD 구성
                    cimStatus.APDIItems = new List<APDInfo>();
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "GLS_START_TIME", APDValue = Aapd.GLS_START_TIME });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "GLS_END_TIME", APDValue = Aapd.GLS_END_TIME });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_LOT_ID", APDValue = Aapd.A_LOT_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_LOT_ID", APDValue = Aapd.B_LOT_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_GLASS_ID", APDValue = Aapd.A_GLASS_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_GLASS_ID", APDValue = Aapd.B_GLASS_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_0_ALIGN", APDValue = Aapd.A_0_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_0_ALIGN", APDValue = Aapd.B_0_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_90_ALIGN", APDValue = Aapd.A_90_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_90_ALIGN", APDValue = Aapd.B_90_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_180_ALIGN", APDValue = Aapd.A_180_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_180_ALIGN", APDValue = Aapd.B_180_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_270_ALIGN", APDValue = Aapd.A_270_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_270_ALIGN", APDValue = Aapd.B_270_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_0_RATE", APDValue = Aapd.A_0_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_0_RATE", APDValue = Aapd.B_0_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_90_RATE", APDValue = Aapd.A_90_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_90_RATE", APDValue = Aapd.B_90_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_180_RATE", APDValue = Aapd.A_180_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_180_RATE", APDValue = Aapd.B_180_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_270_RATE", APDValue = Aapd.A_270_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_270_RATE", APDValue = Aapd.B_270_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "SCAN_SPEED", APDValue = Aapd.SCAN_SPEED });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_V_DEFECT", APDValue = Aapd.A_V_DEFECT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_V_DEFECT", APDValue = Aapd.B_V_DEFECT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_REVIEW_CNT", APDValue = Aapd.A_REVIEW_CNT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_REVIEW_CNT", APDValue = Aapd.B_REVIEW_CNT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "AA_SCAN_ILLU", APDValue = Aapd.AA_SCAN_ILLU });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "PAD_SCAN_ILLU", APDValue = Aapd.PAD_SCAN_ILLU });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "TACT_TIME", APDValue = Aapd.TACT_TIME });
                    func_A.DVS = new List<DVItem>();
                    foreach (var apd in cimStatus.APDIItems)
                    {
                        var dv = new DVItem();
                        dv.DVNAME = apd.APDName;
                        dv.DVVAL = apd.APDValue;
                        func_A.DVS.Add(dv);
                    }

                    gem.HSMSSend(func_A);
                }

                //B Glass 보고----------------------------------------------------------------
                if (cimStatus.UnloadGlass != null && cimStatus.UnloadGlass.BGlass.GlassInfoExist)
                {

                    var func_B = new CIMS6F73();
                    func_B.LOTID = cimStatus.UnloadGlass.BGlass.LOTID;
                    func_B.CSTID = cimStatus.UnloadGlass.BGlass.CSTID;
                    func_B.RECIPEID = cimStatus.UnloadGlass.BGlass.RECIPEID;
                    func_B.PRCID = cimStatus.UnloadGlass.BGlass.PRCID;
                    func_B.SLOTID = cimStatus.UnloadGlass.BGlass.SLOTID;
                    func_B.UNITID = cimStatus.UnitID;

                    func_B.GLSPRCS.GLSID = cimStatus.UnloadGlass.BGlass.GLSID;
                    func_B.GLSPRCS.GLSJUDGE = cimStatus.UnloadGlass.BGlass.GLSJUDGE;
                    func_B.GLSPRCS.PNLIF = cimStatus.UnloadGlass.BGlass.PNLIF;
                    func_B.GLSPRCS.SUBMDLIF = cimStatus.UnloadGlass.BGlass.SUBMDLIF;
                    func_B.GLSPRCS.GLSCODE = cimStatus.UnloadGlass.BGlass.GLSCODE;

                    func_B.GLSPRCS.GSDS = new List<LGHSMSBase.GSD>();
                    foreach (var g in cimStatus.UnloadGlass.BGlass.GSDS)
                    {
                        var gsd = new LGHSMSBase.GSD();
                        gsd.GSDITEM = g.GSDITEM;
                        gsd.GSDVALUE = g.GSDVALUE;
                        func_B.GLSPRCS.GSDS.Add(gsd);
                    }

                    // Console에서 받아온 APD 구성
                    cimStatus.APDIItems = new List<APDInfo>();
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "GLS_START_TIME", APDValue = Aapd.GLS_START_TIME });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "GLS_END_TIME", APDValue = Aapd.GLS_END_TIME });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_LOT_ID", APDValue = Aapd.A_LOT_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_LOT_ID", APDValue = Aapd.B_LOT_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_GLASS_ID", APDValue = Aapd.A_GLASS_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_GLASS_ID", APDValue = Aapd.B_GLASS_ID });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_0_ALIGN", APDValue = Aapd.A_0_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_0_ALIGN", APDValue = Aapd.B_0_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_90_ALIGN", APDValue = Aapd.A_90_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_90_ALIGN", APDValue = Aapd.B_90_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_180_ALIGN", APDValue = Aapd.A_180_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_180_ALIGN", APDValue = Aapd.B_180_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_270_ALIGN", APDValue = Aapd.A_270_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_270_ALIGN", APDValue = Aapd.B_270_ALIGN });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_0_RATE", APDValue = Aapd.A_0_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_0_RATE", APDValue = Aapd.B_0_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_90_RATE", APDValue = Aapd.A_90_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_90_RATE", APDValue = Aapd.B_90_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_180_RATE", APDValue = Aapd.A_180_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_180_RATE", APDValue = Aapd.B_180_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_270_RATE", APDValue = Aapd.A_270_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_270_RATE", APDValue = Aapd.B_270_RATE });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "SCAN_SPEED", APDValue = Aapd.SCAN_SPEED });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_V_DEFECT", APDValue = Aapd.A_V_DEFECT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_V_DEFECT", APDValue = Aapd.B_V_DEFECT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "A_REVIEW_CNT", APDValue = Aapd.A_REVIEW_CNT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "B_REVIEW_CNT", APDValue = Aapd.B_REVIEW_CNT });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "AA_SCAN_ILLU", APDValue = Aapd.AA_SCAN_ILLU });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "PAD_SCAN_ILLU", APDValue = Aapd.PAD_SCAN_ILLU });
                    cimStatus.APDIItems.Add(new APDInfo { APDName = "TACT_TIME", APDValue = Aapd.TACT_TIME });
                    func_B.DVS = new List<DVItem>();
                    foreach (var apd in cimStatus.APDIItems)
                    {
                        var dv = new DVItem();
                        dv.DVNAME = apd.APDName;
                        dv.DVVAL = apd.APDValue;
                        func_B.DVS.Add(dv);
                    }

                    gem.HSMSSend(func_B);
                }

                //Glass Unload Data 초기화 
                //cimStatus.UnloadGlass = null;
            }
            catch(Exception e)
            {
                log.AddLogMessage(LogType.Error, 0, e.ToString());
            }
        }


        private void Gem_OnS6F74Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F74 cim = (CIMS6F74)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F74Event] Unload Glass 보고 실패");
                    break;
            }
        }

        private void Gem_OnS6F84Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F84 cim = (CIMS6F84)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    //if(cimStatus.GlassIn == false && cimStatus.EQtoCIMData.CMD == EQtoCIM_CMD.LoadReady)
                    //{
                    //    SendS6F61();
                    //}
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F84Event] LOT Actual Ack Fail");
                    break;
            }
        }

        public void SendS6F85()
        {
            gem.HSMSSend(new CIMS6F85 
            {
                UNITID = cimStatus.UnitID
            });
        }

        public void SendS6F89(int slotCount)
        {
            var func = new CIMS6F89();
            func.UNITID = cimStatus.UnitID;
            //func.LOTID = 
            func.RESERVECD = RESERVECD.RESERVECDEnum.Reservation;

            for(int i = 0; i < slotCount; i++)
            {
                var slot = new REWORK_ITEM();
                slot.SLOTID = $"{i + 1}";
                slot.SELECTED = "0";
                func.REWORK_ITEMS.Add(slot);
            }

            gem.HSMSSend(func);
        }

        void SendS6F91(EQPSTATUS.EQPSTATUSEnum eqpStatus)
        {
            var func = new CIMS6F91();
            func.UNITID = cimStatus.UnitID;

            if (cimStatus.CurAlarm != null)
            {
                //func.ALID = cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.RUN ||
                //            cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.IDLE ?
                //            "0" : cimStatus.CurAlarm.ALID;
                //func.ALTX = cimStatus.CurAlarm.ALTX;
                func.ALID = cimStatus.CurAlarm.ALID;
                func.ALTX = cimStatus.CurAlarm.ALTX;
                if (cimStatus.CurAlarm.AlarmType == AlarmType.HeavyAlarm)
                    eqpStatus = EQPSTATUS.EQPSTATUSEnum.DOWN;
            }
            else if (cimStatus.IsAutoRun)
            {
                if (cimStatus.EQGlassIn == true)
                {
                    var attr = cimStatus.Attribute;
                    if (string.IsNullOrEmpty(attr))
                    {
                        eqpStatus = EQPSTATUS.EQPSTATUSEnum.RUN;

                    }
                    else if (attr.Contains("EXPERIMENT"))
                    {
                        eqpStatus = EQPSTATUS.EQPSTATUSEnum.Experiment;
                    }
                    else if (attr.Contains("DEVELOPMENT"))
                    {
                        eqpStatus = EQPSTATUS.EQPSTATUSEnum.Development;

                    }
                    else if (attr.Contains("MONITOR"))
                    {
                        eqpStatus = EQPSTATUS.EQPSTATUSEnum.Monitoring;

                    }
                    else
                    {
                        eqpStatus = EQPSTATUS.EQPSTATUSEnum.RUN;

                    }
                }
                else
                    eqpStatus = EQPSTATUS.EQPSTATUSEnum.IDLE;
            }
            else
            {
                //AlarmOccur(new List<AlarmInfo> { CIMAlarm.SystemNotReadyAlarm() });
                //return;
                eqpStatus = EQPSTATUS.EQPSTATUSEnum.DOWN;
            }
            // 상태가 바뀔때만 보고
            if (eqpStatus == cimStatus.EQPStatus)
            {
                return;
            }

            //if (eqpStatus == EQPSTATUS.EQPSTATUSEnum.DOWN)
            //{
            //    if (cimStatus.LastReportMV == GLSMVSTATUS.GLSMVSTATUSEnum.LR ||
            //        cimStatus.LastReportMV == GLSMVSTATUS.GLSMVSTATUSEnum.UR ||
            //        cimStatus.LastReportMV == GLSMVSTATUS.GLSMVSTATUSEnum.ER)
            //        SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.CR);

            //}

            func.EQPStatus = eqpStatus;
            cimStatus.EQPStatus = eqpStatus;
            gem.HSMSSend(func);
        }

        private void Gem_OnS6F92Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS6F92 cim = (CIMS6F92)e.cimClass;
            switch ((ACKC6.ACKC6Enum)cim.ACKC6.Value)
            {
                case ACKC6.ACKC6Enum.Accept:
                    //cimStatus.EQPStatus = cimStatus.TryEQPStatus;
                    OnEQStatusChanged(this, new CIMArgs());
                    break;

                case ACKC6.ACKC6Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS6F92Event] EQStatus 바꾸기 실패");
                    break;
            }
        }

        public void SendS6F93(string code)
        {
            if(cimStatus.UnloadGlass == null)
            {
                log.Warning("현재 로드된 글라스 없음. Glass Scrap 종료.");
                return;
            }

            var func = new CIMS6F93();
            if (cimStatus.IsAGlass)
            {
                func.LOTID = cimStatus.UnloadGlass.AGlass.LOTID;
                func.GLSID = cimStatus.UnloadGlass.AGlass.GLSID;
           }
            else
            {
                func.LOTID = cimStatus.UnloadGlass.BGlass.LOTID;
                func.GLSID = cimStatus.UnloadGlass.BGlass.GLSID;
            }

            func.UNITID = cimStatus.UnitID;

            if (cimStatus.SCRTYPE == "P")
                func.SCRTYPE = "P";
            else
                func.SCRTYPE = "G";

            // LG가 정의한 scrap code
            func.SCRPCODE = "CODE";

            gem.HSMSSend(func);
        }
    }
}
