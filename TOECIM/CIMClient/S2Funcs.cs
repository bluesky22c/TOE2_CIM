using Elp;
using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TOECIMLib;
//using TOELib;
using static LGHSMSBase.Win32;

namespace TOECIM
{

    public partial class ClientCIM
    {
        public SYSTEMTIME SYSTEMTIME = new SYSTEMTIME();

        void InitS2()
        {
            gem.OnS2F25Event += Gem_OnS2F25Event;
            gem.OnS2F31Event += Gem_OnS2F31Event;
            gem.OnS2F62Event += Gem_OnS2F62Event;
            gem.OnS2F64Event += Gem_OnS2F64Event;
            gem.OnS2F68Event += Gem_OnS2F68Event;
            gem.OnS2F69Event += Gem_OnS2F69Event;
            gem.OnS2F78Event += Gem_OnS2F78Event;
            gem.OnS2F80Event += Gem_OnS2F80Event;
            gem.OnS2F87Event += Gem_OnS2F87Event;
            gem.OnS2F99Event += Gem_OnS2F99Event;
        }


        public void SendS2F17()
        {
            var func = new CIMS2F17 { UNITID = cimStatus.UnitID };
            gem.HSMSSend(func);
        }

        private void Gem_OnS2F31Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F31 cim = (CIMS2F31)e.cimClass;
            var func = new CIMS2F32();
            func.UNITID = cim.UNITID;

            try
            {
                SYSTEMTIME.wYear = ushort.Parse(cim.TIME.Value.ToString().Substring(0, 4));
                SYSTEMTIME.wMonth = ushort.Parse(cim.TIME.Value.ToString().Substring(4, 2));
                SYSTEMTIME.wDay = ushort.Parse(cim.TIME.Value.ToString().Substring(6, 2));
                SYSTEMTIME.wHour = ushort.Parse(cim.TIME.Value.ToString().Substring(8, 2));
                SYSTEMTIME.wMinute = ushort.Parse(cim.TIME.Value.ToString().Substring(10, 2));
                SYSTEMTIME.wSecond = ushort.Parse(cim.TIME.Value.ToString().Substring(12, 2));
                SYSTEMTIME.wMilliseconds = 0;
                Win32.SetLocalTime(ref SYSTEMTIME);

                func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;
                gem.HSMSSend(func);
            }
            catch (Exception ee)
            {
                func.ACKC2 = (byte)ACKC2.ACK2Enum.NotAccept;
                gem.HSMSSend(func);

                log.AddLogMessage(LogType.Error, 0, ee.ToString());
            }
        }

        //loopBack
        private void Gem_OnS2F25Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS2F26();
            CIMS2F25 cim = (CIMS2F25)e.cimClass;
            func.ABS = cim.ABS;
            gem.HSMSSend(func);
        }
        //loopBack
        public void SendS2F25()
        {
            var func = new CIMS2F25();
            gem.HSMSSend(func);
        }

        private void SendS2F61(RCS.RCSEnum rcs)
        {
            var func = new CIMS2F61();
            func.RCS = rcs;
            gem.HSMSSend(func);
        }

        private void Gem_OnS2F62Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F62 cim = (CIMS2F62)e.cimClass;

            //Console이 연결되어 있지 않으면 보고하지 않음
            var toCim = cimStatus.GetStatusFromConsole();
            //if(toCim.Error == true)
            //{
            //    log.Error("Console 연결되지 않음.");
            //    return;
            //}

            switch ((ACKC2A.ACKC2AEnum)cim.ACKC2A.Value)
            {
                case ACKC2A.ACKC2AEnum.Accept:
                    //ECS 재가동할때
                    if (cimStatus.ChangeRCS == false)
                    {
                        OnRCSModeChanged(this, new CIMArgs());

                        if (cimStatus.IsOnline)
                        {
                            SendS2F63(cimStatus.OPMode);    //opmode report
                            SendS2F17();    //date, time report
                            SendS1F61();    //equipment status report
                            SendS1F67();    //all glass position report

                            if (toCim != null && 
                                toCim.Error == false &&
                                cimStatus.TryRCS == RCS.RCSEnum.ONLineRemote &&
                                toCim.EqpReadyStatus != EQPReadyStatus.None &&
                                toCim.EqpReadyStatus.IsReady() &&
                                toCim.AutoRun == true)
                            {
                                SendS6F61();
                            }
                        }
                    }

                    //RCS 변경할때
                    else
                    {
                        //online -> offline
                        if (cimStatus.RCS != RCS.RCSEnum.OffLine && cimStatus.TryRCS == RCS.RCSEnum.OffLine)
                        {
                            cimStatus.RCS = cimStatus.TryRCS;
                            OnRCSModeChanged(this, new CIMArgs());

                            cimStatus.ChangeRCS = false;
                            break;
                        }

                        //online -> online
                        if (cimStatus.RCS != RCS.RCSEnum.OffLine && cimStatus.TryRCS != RCS.RCSEnum.OffLine)
                        {
                            cimStatus.RCS = cimStatus.TryRCS;
                            OnRCSModeChanged(this, new CIMArgs());

                            SendS2F63(cimStatus.OPMode);    //opmode report

                            cimStatus.ChangeRCS = false;
                            if (cimStatus.TryRCS == RCS.RCSEnum.ONLineRemote &&
                                toCim != null &&
                                toCim.EqpReadyStatus != EQPReadyStatus.None &&
                                toCim.EqpReadyStatus.IsReady() &&
                                toCim.AutoRun == true)
                            {
                                SendS6F61();
                            }
                            break;
                        }

                        //offline -> online
                        cimStatus.RCS = cimStatus.TryRCS;
                        OnRCSModeChanged(this, new CIMArgs());

                        SendS2F63(cimStatus.OPMode);    //opmode report
                        SendS2F17();    //date, time report
                        SendS1F61();    //equipment status report
                        SendS1F67();    //all glass position report

                        cimStatus.ChangeRCS = false;

                        if (cimStatus.TryRCS == RCS.RCSEnum.ONLineRemote &&
                            toCim != null &&
                            toCim.EqpReadyStatus != EQPReadyStatus.None &&
                            toCim.EqpReadyStatus.IsReady() &&
                            toCim.AutoRun == true)
                        {
                            SendS6F61();
                        }
                    }
                    break;

                case ACKC2A.ACKC2AEnum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS2F62Event] RCS 바꾸기 실패 {cimStatus.RCS}=>{cimStatus.TryRCS}");
                    break;
            }
        }

        private void SendS2F63(OPMODE.OPMODEEnum opMode)
        {
            var func = new CIMS2F63 { UNITID = cimStatus.UnitID };
            func.OPMODE = opMode;
            gem.HSMSSend(func);
        }


        private void Gem_OnS2F64Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F64 cim = (CIMS2F64)e.cimClass;
            switch ((ACKC2A.ACKC2AEnum)cim.ACKC2A.Value)
            {
                case ACKC2A.ACKC2AEnum.Accept:
                    cimStatus.OPMode = cimStatus.TryOPMode;
                    OnOPModeChanged(this, new CIMArgs());
                    break;

                case ACKC2A.ACKC2AEnum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS2F64Event] OPMODE 바꾸기 실패 {cimStatus.OPMode}=>{cimStatus.TryOPMode}");
                    break;
            }
        }

        //Lot End
        public void SendS2F65()
        {
            var func = new CIMS2F65();
            func.UNITID = cimStatus.UnitID;
            func.PORTID = cimStatus.LotData.PORTID;
            func.LOTID = cimStatus.LotData.LOTID;
            func.ENDFLAG = ENDFLAG.ENDFLAGEnum.OperatorEnd;

            gem.HSMSSend(func);
        }

        //cycle stop
        public void SendS2F67(CSSSTATUS.CSSSTATUSEnum cssStatus)
        {
            var func = new CIMS2F67();
            func.UNITID = cimStatus.UnitID;
            func.CSSSTATUS = (byte)cssStatus;
            gem.HSMSSend(func);
        }


        private void Gem_OnS2F68Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F68 cim = (CIMS2F68)e.cimClass;

            if (cimStatus.CycleStop == false)
            {
                if (cim.ACKC2 == (byte)ACKC2.ACK2Enum.Accept)
                {
                    SendS6F61();
                }
            }
        }

        private void Gem_OnS2F69Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F69 cim = (CIMS2F69)e.cimClass;
            log.AddLogMessage(LogType.Verbose, 0, $"[Gem_OnS2F69Event] HOST Message : {cim.HOSTMSG.Value}");

            var func = new CIMS2F70();
            func.UNITID = cim.UNITID;
            if (cimStatus.UnloadGlass == null)
            {
                func.ACKC2 = (byte)ACKC2.ACK2Enum.NoRecipe;   //사양서대로
                gem.HSMSSend(func);
                return;
            }

            func.ACKC2 = (byte)ACKC2.ACK2Enum.Accept;
            gem.HSMSSend(func);
            
            //UI에서 Unload Glass Data 확인해서 보내기
        }

        public void SendS2F77()
        {
            var func = new CIMS2F77();
            func.UNITID = cimStatus.UnitID;
            func.STAGEID = cimStatus.StageID;
            func.TARGETID = (ushort)cimStatus.UnitID;
            gem.HSMSSend(func);
        }

        private void Gem_OnS2F78Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            SendS6F61();
        }

        public void SendS2F79(REQOPTION.REQOPTIONEnum reqOption, string glassID, ushort glassCode)
        {
            var func = new CIMS2F79();
            func.UNITID = cimStatus.UnitID;

            switch (reqOption)
            {
                case REQOPTION.REQOPTIONEnum.GLSID:
                    func.GLSID = glassID;
                    func.REQOPTION = REQOPTION.REQOPTIONEnum.GLSID;
                    break;

                case REQOPTION.REQOPTIONEnum.GLSCODE:
                    func.GLSCODE = glassCode;
                    func.REQOPTION = REQOPTION.REQOPTIONEnum.GLSCODE;
                    break;

                case REQOPTION.REQOPTIONEnum.ALL:
                    func.GLSID = glassID;
                    func.GLSCODE = glassCode;
                    func.REQOPTION = REQOPTION.REQOPTIONEnum.ALL;
                    break;
            }

            gem.HSMSSend(func);
        }

        private void Gem_OnS2F80Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F80 cim = (CIMS2F80)e.cimClass;
            var g = new GlassInfo();

            g.ReportType = ReportType.Load;
            g.LOTID = cim.LoadGlass.LOTID;
            g.CSTID = cim.LoadGlass.CSTID;
            g.SLOTID = cim.LoadGlass.SLOTID;
            g.RECIPEID = cim.LoadGlass.RECIPEID;
            g.PRCID = cim.LoadGlass.PRCID;
            g.GLSODR = cim.LoadGlass.GLSODR;
            g.GLSID = cim.LoadGlass.GLSID;
            g.GLSJUDGE = cim.LoadGlass.GLSJUDGE;
            g.PNLIF = cim.LoadGlass.PNLIF;
            g.SUBMDLIF = cim.LoadGlass.SUBMDLIF;
            g.PORTID = cim.LoadGlass.PORTID;
            g.GLSCODE = cim.LoadGlass.GLSCODE;
            g.REINPUT = cim.LoadGlass.REINPUT;
            g.GLSTHICK = cim.LoadGlass.GLSTHICK;
            g.PARTNUM = cim.LoadGlass.PARTNUM;
            g.PRODTYPE = cim.LoadGlass.PRODTYPE;
            g.ATTRIBUTE = cim.LoadGlass.ATTRIBUTE;
            g.GLSTYPE = cim.LoadGlass.GLSTYPE;
            g.KEYID = cim.LoadGlass.KEYID;

            foreach (var cl in cim.LoadGlass.LSDS)
            {
                var lsd = new TOECIMLib.LSD();
                lsd.LSDITEM = cl.LSDITEM;
                lsd.LSDVALUE = cl.LSDVALUE;
                g.LSDS.Add(lsd);
            }

            foreach (var gl in cim.LoadGlass.GSDS)
            {
                var gsd = new TOECIMLib.GSD();
                gsd.GSDITEM = gl.GSDITEM;
                gsd.GSDVALUE = gl.GSDVALUE;
                g.GSDS.Add(gsd);
            }

            if (cimStatus.UnloadGlass is null)
                cimStatus.UnloadGlass = new ECSData();

            if (cimStatus.IsAGlass == true)
                cimStatus.UnloadGlass.AGlass = g;
            else
            {
                cimStatus.UnloadGlass.BGlass = g;

                var commData = new CimCommData
                {
                    ToConsole = new CIMtoEQPData
                    {
                        Command = CIMtoEQP.GlassDataReq,
                    }
                };

                commData.ToConsole.ContainGlass = cimStatus.CstGlassExist;
                commData.ToConsole.GlassInfo[0] = Common.Clone(Common.ConvertCIMtoCommonGlassData(cimStatus.UnloadGlass.AGlass));
                commData.ToConsole.GlassInfo[0].StartTime = DateTime.Now;
                commData.ToConsole.GlassInfo[1] = Common.Clone(Common.ConvertCIMtoCommonGlassData(cimStatus.UnloadGlass.BGlass));
                commData.ToConsole.GlassInfo[1].StartTime = DateTime.Now;
                Common.ToConsole(commData.ToConsole);
            }
                
            UpdateUI(this, null);

            // SendS6F73(); // 사양서 미정의 메시지로 주석 처리
        }


        private void Gem_OnS2F87Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            timeout.ReleaseTimeout(ETimeout.toGlassDataLoad);

            //var eqData = cimStatus.GetStatusFromConsole();

            //// 현재 장비 상태 업데이트
            ReqEQData(this, new CimCommData());

            CIMS2F87 cim = (CIMS2F87)e.cimClass;
            cimStatus.StageID = cim.STAGEID;

            log.AddLogMessage(LogType.Warning, 0, $"[Debug] S2F87 Received. Current RCS: {cimStatus.RCS}");
            if (cimStatus.RCS == RCS.RCSEnum.OffLine)
            {
                log.AddLogMessage(LogType.Warning, 0, $"현재 OffLine 모드입니다.");
                gem.HSMSSend(new CIMS2F0());
                return;
            }

            cimStatus.CstGlassExist = (cim.GLSSTATUS == "1") ? true : false;

            // 데이터 처리 결과
            // nak 작업하기 !
            // 장비가 Autorun 상태가 아니면 Nak
            ACKC2.ACK2Enum ack = ACKC2.ACK2Enum.Accept;
            var eqData = cimStatus.CimCommData.ToCIM;
            if (cimStatus.IsTransferReady == false)
            {
                ack = ACKC2.ACK2Enum.NotAccept;

                log.AddLogMessage(LogType.Warning, 0, $"IsAutoRun = {cimStatus.IsAutoRun}, RunState = {cimStatus.ConsoleData.EqpReadyStatus}");
            }

            if (cimStatus.CstGlassExist == false)
            {
                cimStatus.LoadGlass = null;
            }
            else
            {
                cimStatus.LoadGlass = new ECSData();
            }

            //Glass Load
            GlassInfo[] temp = new GlassInfo[2];
            for (int i = 0; i < cim.LoadGlass.Count; i++)
            {
                var g = new GlassInfo();

                g.ReportType = ReportType.Load;
                g.LOTID = cim.LoadGlass[i].LOTID;
                g.CSTID = cim.LoadGlass[i].CSTID;
                g.SLOTID = cim.LoadGlass[i].SLOTID;
                g.RECIPEID = cim.LoadGlass[i].RECIPEID;
                g.PRCID = cim.LoadGlass[i].PRCID;
                g.GLSODR = cim.LoadGlass[i].GLSODR;
                g.GLSID = cim.LoadGlass[i].GLSID;
                g.GLSJUDGE = cim.LoadGlass[i].GLSJUDGE;
                g.PNLIF = cim.LoadGlass[i].PNLIF;
                g.SUBMDLIF = cim.LoadGlass[i].SUBMDLIF;
                g.PORTID = cim.LoadGlass[i].PORTID;
                g.GLSCODE = cim.LoadGlass[i].GLSCODE;
                g.REINPUT = cim.LoadGlass[i].REINPUT;
                g.GLSTHICK = cim.LoadGlass[i].GLSTHICK;
                g.PARTNUM = cim.LoadGlass[i].PARTNUM;
                g.PRODTYPE = cim.LoadGlass[i].PRODTYPE;
                g.ATTRIBUTE = cim.LoadGlass[i].ATTRIBUTE;
                g.GLSTYPE = cim.LoadGlass[i].GLSTYPE;
                g.KEYID = cim.LoadGlass[i].KEYID;
                cimStatus.Attribute = cim.LoadGlass[i].PRODTYPE;

                foreach (var cl in cim.LoadGlass[i].LSDS)
                {
                    var lsd = new TOECIMLib.LSD();
                    lsd.LSDITEM = cl.LSDITEM;
                    lsd.LSDVALUE = cl.LSDVALUE;
                    g.LSDS.Add(lsd);
                }
                foreach (var gl in cim.LoadGlass[i].GSDS)
                {
                    var gsd = new TOECIMLib.GSD();
                    gsd.GSDITEM = gl.GSDITEM;
                    gsd.GSDVALUE = gl.GSDVALUE;
                    g.GSDS.Add(gsd);
                }

                temp[i] = g;
            }

            //Glass 데이터, Glass Order 업데이트----------------------------------
            if (temp[0] != null)
            {
                cimStatus.LoadGlass.AGlass = temp[0];

            }
            if (temp[1] != null)
            {
                cimStatus.LoadGlass.BGlass = temp[1];

            }

            //CIMS2F87에 대한 응답
            SendS2F88(cim.GLSSTATUS, ack);

            UpdateUI(this, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glsStatus"></param>
        /// <param name="ack">장비가 Autorun 상태가 아니면 Nak</param>
        public void SendS2F88(GLSSTATUS glsStatus, ACKC2.ACK2Enum ack = ACKC2.ACK2Enum.Accept)
        {
            var func = new CIMS2F88();
            func.UNITID = cimStatus.UnitID;
            func.STAGEID = cimStatus.StageID;
            
            func.ACKC2.Value = (byte)ack;
            gem.HSMSSend(func);

            //EQ에 Glass Data 전달
            var commData = new CimCommData
            {
                ToConsole = new CIMtoEQPData
                {
                    Command = CIMtoEQP.GlassDataSend,
                }
            };

            // 다음 Glass 시퀀스 진행
            if (ack == ACKC2.ACK2Enum.Accept)
            {
                if (NextGlassProcess())
                {
                    if (cimStatus.CstGlassExist)
                    {
                        commData.ToConsole.ContainGlass = cimStatus.CstGlassExist;
                        commData.ToConsole.GlassInfo[0] = Common.Clone(Common.ConvertCIMtoCommonGlassData(cimStatus.LoadGlass.AGlass));
                        commData.ToConsole.GlassInfo[1] = Common.Clone(Common.ConvertCIMtoCommonGlassData(cimStatus.LoadGlass.BGlass));
                        commData.ToConsole.GlassInfo[0].StartTime = DateTime.Now;
                        commData.ToConsole.GlassInfo[1].StartTime = DateTime.Now;

                        Common.ToConsole(commData.ToConsole);

                        //LotInfo Update
                        cimStatus.LotData.LOTID = cimStatus.LoadGlass.AGlass.LOTID;
                        cimStatus.LotData.CSTID = cimStatus.LoadGlass.AGlass.CSTID;
                        cimStatus.LotData.PORTID = cimStatus.LoadGlass.AGlass.PORTID;
                        cimStatus.LotData.Glasses.Add(cimStatus.LoadGlass);
                        log.Information($"Current Glass Count : {cimStatus.LotData.Glasses.Count}, LOTID : {cimStatus.LotData.LOTID.Value}");
                    }
                    else  //Load Glass가 없으면 GLSStatus = 0으로 보냄
                    {
                        commData.ToConsole.ContainGlass = cimStatus.CstGlassExist;
                        Common.ToConsole(commData.ToConsole);
                    }
                }

                cimStatus.CimCommData.ToConsole = Common.Clone(commData.ToConsole);

            }
            else
            {
                //todo : Cim 자체 알람 사용 여부
                //AutoRun 아니면 알람 띄우고 nak
                //Accept가 아닐 경우 장비는 LoadDone 상태로 진행되면 안됨. 장비로 Error 전달
                //cimStatus.LoadGlass = null;
                AlarmOccur(new List<AlarmInfo> { CIMAlarm.SystemNotReadyAlarm() });

                //commData.ToConsole.Command = CIMtoEQP.Error;
                //Common.ToConsole(commData.ToConsole);

                log.Warning($"[S2F88] 장비가 Down이거나 글래스를 받을 수 있는 상태가 아님. ACK : {ack}");

                //var alarm = new AlarmInfo();
                //alarm.ALID = "001"; //ID 뭔지 모르겠음
                //alarm.ALCD = ALCD.SetCode(ALCD.ALCDEnum.EQPStatusWarning);  //코드 뭔지 모르겠음
                //alarm.ALTX = "Glass Data Not Accept.";
                //alarm.AlarmType = AlarmType.HeavyAlarm;
                //CimAlarmSet(this, new AlarmArgs
                //{
                //    Alarm = alarm
                //});
            }
        }

        /// <summary>
        /// LR, ER, UR 신호를 보낸다. 
        /// </summary>
        /// <returns></returns>
        public bool NextGlassProcess()
        {
            log.AddLogMessage(LogType.Information, 0, $"Next GlassMove Process Start. EQ Glass In : {cimStatus.EQGlassIn}, GLSStatus : {cimStatus.CstGlassExist}");

            //Lot에 로드할 글라스가 없음(GLSSTATUS = 0)
            if (cimStatus.CstGlassExist == false)
            {
                // 양쪽다 glass 없으면 그냥 대기
                if (cimStatus.EQGlassIn == false)
                    return false;
                // glass 있으면 unload 요구
                if (cimStatus.EQGlassIn == true)
                {
                    SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.UW);
                    Thread.Sleep(500);
                    SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.UR);
                    // SendS6F73(); // 사양서 미정의 메시지로 주석 처리

                    Vars.curGLSM = GLSMVSTATUS.GLSMVSTATUSEnum.UR;
                    //GlassMoveEvent(this, new GLSMVArgs
                    //{
                    //    CurStatus = cimStatus.GLSMVSTATUS,
                    //    ChangeStatus = GLSMVSTATUS.GLSMVSTATUSEnum.UW
                    //});
                    //GlassMoveEvent(this, new GLSMVArgs
                    //{
                    //    CurStatus = cimStatus.GLSMVSTATUS,
                    //    ChangeStatus = GLSMVSTATUS.GLSMVSTATUSEnum.UR
                    //});
                    //cimStatus.PreGlassOrder = GlassOrder.None;
                }
            }

            //Lot에 로드할 글라스가 있음
            else
            {
                if (cimStatus.EQGlassIn == false)
                {
                    SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.LR);
                    //GlassMoveEvent(this, new GLSMVArgs
                    //{
                    //    CurStatus = cimStatus.GLSMVSTATUS,
                    //    ChangeStatus = GLSMVSTATUS.GLSMVSTATUSEnum.LR
                    //});
                }
                else
                {
                    SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.UW);
                    SendS6F67(GLSMVSTATUS.GLSMVSTATUSEnum.ER);
                    // SendS6F73(); // 사양서 미정의 메시지로 주석 처리

                    Vars.curGLSM = GLSMVSTATUS.GLSMVSTATUSEnum.ER;
                    //GlassMoveEvent(this, new GLSMVArgs
                    //{
                    //    CurStatus = cimStatus.GLSMVSTATUS,
                    //    ChangeStatus = GLSMVSTATUS.GLSMVSTATUSEnum.UW
                    //});
                    //GlassMoveEvent(this, new GLSMVArgs
                    //{
                    //    CurStatus = cimStatus.GLSMVSTATUS,
                    //    ChangeStatus = GLSMVSTATUS.GLSMVSTATUSEnum.ER
                    //});
                }
            }
            return true;
        }

        private void Gem_OnS2F99Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS2F99 cim = (CIMS2F99)e.cimClass;
            var func = new CIMS2F100();
            func.UNITID = cimStatus.UnitID;

            RCMD.RCMDEnum rcmd = cim.RCMD;
            switch (rcmd)
            {
                case RCMD.RCMDEnum.PAUSE:
                    var commData = new CimCommData { ToConsole = new CIMtoEQPData { Command = CIMtoEQP.Pause } };
                    Common.ToConsole(commData.ToConsole);
                    break;

                case RCMD.RCMDEnum.RESUME:
                    if (cimStatus.Pause == false)
                    {
                        log.Verbose("pause가 이미 해제됨.");
                        func.ACKC6 = (byte)ACKC6.ACKC6Enum.AlreadyRelease;
                        gem.HSMSSend(func);
                        return;
                    }

                    cimStatus.Pause = false;
                    if (cimStatus.EQGlassIn == true)
                        ReportEQPStatus(EQPSTATUS.EQPSTATUSEnum.RUN);
                    else
                        ReportEQPStatus(EQPSTATUS.EQPSTATUSEnum.IDLE
                            );

                    if (cimStatus.CimCommData.ToCIM.Command == EQPtoCIM.LoadReadyDone)
                        //|| cimStatus.CimCommData.ToCIM.Command == EQPtoCIM.UnloadReadyDone)
                    {
                        SendS6F61();
                    }
                    break;
            }

            func.ACKC6 = (byte)ACKC6.ACKC6Enum.Accept;
            gem.HSMSSend(func);
        }
    }
}
