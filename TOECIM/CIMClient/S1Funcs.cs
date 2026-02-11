using Engion;
using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM
{
    public partial class ClientCIM
    {
        void InitS1()
        {
            gem.OnS1F1Event += Gem_OnS1F1Event;
            gem.OnS1F2Event += Gem_OnS1F2Event;
            gem.OnS1F62Event += Gem_OnS1F62Event;
            gem.OnS1F63Event += Gem_OnS1F63Event;
            gem.OnS1F68Event += Gem_OnS1F68Event;
        }

        private void Gem_OnS1F1Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            var func = new CIMS1F2();
            func.MDLN = cimStatus.EQModelType;

            gem.HSMSSend(func);
        }

        private void Gem_OnS1F2Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            SendS2F61(cimStatus.RCS);
        }

        private void Gem_OnS1F63Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            SendS1F64();
        }

        private void SendS1F1()
        {
            gem.HSMSSend(new CIMS1F1());
        }

        public void SendS1F61()
        {
            var func = new CIMS1F61();

            ReqEQData(this, new CimCommData());

            func.EQPReport.UNITID = cimStatus.UnitID;
            func.EQPReport.EQPStatus = cimStatus.EQPStatus;

            if (cimStatus.CurAlarm != null)
            {
                func.EQPReport.ALID = cimStatus.CurAlarm.ALID;
                func.EQPReport.ALTX = cimStatus.CurAlarm.ALTX;
                if (cimStatus.CurAlarm.AlarmType == AlarmType.HeavyAlarm)
                    func.EQPReport.EQPStatus = EQPSTATUS.DOWN;

                //    func.EQPReport.ALID = cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.RUN ||
                //                          cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.IDLE ?
                //                          "0" : cimStatus.CurAlarm.ALID;
            }

            for (int i = 0; i < 3; i++)
            {
                var item = new IONIZERRPT();                
                item.IONIZERID = cimStatus.IonizerInfos[i].ID;
                item.IONIZERSTATUS = cimStatus.IonizerInfos[i].Status;
                func.EQPReport.IONIZERRPTS.Add(item);
            }
            
            gem.HSMSSend(func);
        }

        private void Gem_OnS1F62Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS1F62 cim = (CIMS1F62)e.cimClass;
            switch ((ACKC1.ACKC1Enum)cim.ACKC1.Value)
            {
                case ACKC1.ACKC1Enum.Accept:
                    //RCS 모드 변경할때로 이동
                    //if ((cimStatus.CimCommData.ToCIM.Command == ConsoletoCIM.LoadReadyDone ||
                    //    cimStatus.CimCommData.ToCIM.Command == ConsoletoCIM.UnloadReadyDone) &&
                    //    cimStatus.RCS == RCS.RCSEnum.ONLineRemote)
                    //{
                    //    SendS6F61();
                    //}
                    break;

                case ACKC1.ACKC1Enum.Error:
                    log.AddLogMessage(LogType.Error, 0, $"[OnS1F62Event] Equipment Status Report Error.");
                    break;
            }
        }
        private void SendS1F64()
        {
            var func = new CIMS1F64();
            func.EQPReport.UNITID = cimStatus.UnitID;
            func.EQPReport.EQPStatus = cimStatus.EQPStatus;

            if (cimStatus.CurAlarm != null)
            {
                func.EQPReport.ALID = cimStatus.CurAlarm.ALID;
                func.EQPReport.ALTX = cimStatus.CurAlarm.ALTX;

                //    func.EQPReport.ALID = cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.RUN ||
                //                          cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.IDLE ?
                //                          "0" : cimStatus.CurAlarm.ALID;
            }

            ReqEQData(this, new CimCommData());
            for (int i = 0; i < 3; i++)
            {
                var item = new IONIZERRPT();
                item.IONIZERID = cimStatus.IonizerInfos[i].ID;
                item.IONIZERSTATUS = cimStatus.IonizerInfos[i].Status;
                func.EQPReport.IONIZERRPTS.Add(item);
            }

            gem.HSMSSend(func);
        }

        /// <summary>
        /// 글래스 Handshake가 끝난 후 보고한다.
        /// </summary>
        public void SendS1F67()
        {
            var func = new CIMS1F67();

            //foreach (var s in SystemSetup.SubUnitID)
            {
                var su = new SUBUNIT();
                su.UNITID = cimStatus.UnitID;
                //su.SUBUNITID = Program.SystemSetup.SubUnitID[0];

                su.GLSCODES = new List<GLSCODE>();
                //if (cimStatus.LoadGlass != null && cimStatus.EQGlassIn == true)
                //{
                //    su.GLSCODES.Add(cimStatus.LoadGlass.AGlass.GLSCODE);
                //    su.GLSCODES.Add(cimStatus.LoadGlass.BGlass.GLSCODE);
                //}
                if (cimStatus.UnloadGlass != null)
                {
                    su.GLSCODES.Add(cimStatus.UnloadGlass.AGlass.GLSCODE);
                    su.GLSCODES.Add(cimStatus.UnloadGlass.BGlass.GLSCODE);
                }

                func.SUBUNITS.Add(su);
            }
            gem.HSMSSend(func);

            //cimStatus.LoadGlassEQ = true;
        }


        private void Gem_OnS1F68Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            
        }

        //ionizer
        public void SendS1F75()
        {
            ReqEQData(this, new CimCommData());

            var func = new CIMS1F75();

            func.UNITID = cimStatus.UnitID;
            for (int i = 0; i < 3; i++)
            {
                var item = new IONIZERRPT();
                item.IONIZERID = cimStatus.IonizerInfos[i].ID;
                item.IONIZERSTATUS = cimStatus.IonizerInfos[i].Status;
                func.IONIZERRPTS.Add(item);
            }
            gem.HSMSSend(func);
        }
    }
}
