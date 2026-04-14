using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGCIM.Lib;

namespace LGCIM
{
    public enum CIMErrorNames
    {
        SYSTEM_IS_NOT_READY = 900,
    }
    public class CIMAlarm
    {
        public static AlarmInfo SystemNotReadyAlarm()
        {

            return new AlarmInfo
            {
                ALID = ((int)(CIMErrorNames.SYSTEM_IS_NOT_READY)).ToString(),
                AlarmType = AlarmType.HeavyAlarm,
                ALTX = "System is not ready",
                Name = CIMErrorNames.SYSTEM_IS_NOT_READY.ToString(),
            };
        }
    }
}
