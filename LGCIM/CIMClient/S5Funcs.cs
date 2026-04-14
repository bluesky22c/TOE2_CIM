using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGHSMSBase;
using LGCIM.Lib;
using static LGHSMSBase.CIMS5F0;

namespace LGCIM
{
    public partial class ClientCIM
    {
        void InitS5()
        {
            gem.OnS5F2Event += Gem_OnS5F2Event;
        }

        public void SendS5F1(AlarmInfo alarm, bool occur)
        {
            var func = new CIMS5F1();

            if (alarm != null)
            {
                if (occur)
                {
                    alarm.ALCD |= 0b1000_0000;
                }
                else
                {
                    alarm.ALCD &= 0b0111_1111;
                }
                func.UNITID = cimStatus.UnitID;
                func.ALID = alarm.ALID;
                func.ALCD = alarm.ALCD;
                func.ALTX = alarm.ALTX;
                bool sendResult = gem.HSMSSend(func);
                log.AddLogMessage(Elp.LogType.Information, 0, $"[S5F1] Alarm Report attempt. ALID : {func.ALID.Value}, ALCD : {func.ALCD.Value}, Online: {cimStatus.IsOnline}, HSMSState: {gem.ConnectMode}, SendSuccess: {sendResult}");

                //10.14 eqpStatus는 eq에서 직접 받아서 보고
                //heavy 알람 발생 시 설비 상태 down으로 변경
                //if(alarm.IsRelease == false && alarm.AlarmType == AlarmType.HeavyAlarm)
                //{
                //    cimStatus.PreStatus = cimStatus.EQPStatus;
                //    if (cimStatus.EQPStatus == EQPSTATUS.EQPSTATUSEnum.DOWN)
                //        cimStatus.PreStatus = EQPSTATUS.EQPSTATUSEnum.IDLE;

                //    ChangeEQPStatus(this, new EQPArgs 
                //    { 
                //        CurStatus = cimStatus.EQPStatus,
                //        ChangeStatus = EQPSTATUS.EQPSTATUSEnum.DOWN
                //    });
                //}

                ////heavy 알람 해제 시 설비 상태 원복
                //if (alarm.IsRelease == true && alarm.AlarmType == AlarmType.HeavyAlarm)
                //{
                //    cimStatus.CurAlarm = null;

                //    if (cimStatus.PreStatus == EQPSTATUS.EQPSTATUSEnum.DOWN)
                //        cimStatus.PreStatus = EQPSTATUS.EQPSTATUSEnum.IDLE;

                //    ChangeEQPStatus(this, new EQPArgs
                //    {
                //        CurStatus = cimStatus.EQPStatus,
                //        ChangeStatus = cimStatus.PreStatus
                //    });
                //}
            }
        }

        private void Gem_OnS5F2Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            // S5F2: Host Alarm Report ACK
            log.AddLogMessage(Elp.LogType.Information, 0, $"[S5F2] Alarm Report ACK 수신.");
        }
    }
}
