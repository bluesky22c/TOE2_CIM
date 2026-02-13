using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIMLib
{
    public enum AlarmType 
    {
        HeavyAlarm,
        LightAlarm,
    }

    [Serializable]
    public class AlarmInfo
    {
        [AutoIncrement]
        public int ID { get; set; }
        public byte ALCD { get; set; } = 0;
        public string ALID { get; set; } = "0";
        public AlarmType AlarmType { get; set; } = AlarmType.HeavyAlarm;
        public string ALTX { get; set; } = "";
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// enum 으로 정의됨 알람 이름
        /// </summary>
        public string Name { get; set; } = "No";
        /// <summary>
        /// 해제됐는지 확인
        /// </summary>
        public bool IsRelease { get; set; } = false;
        public string Desc { get; set; } = "";
        static void Check()
        {
        }

        static public AlarmInfo? GetError(string code, List<AlarmInfo> list)
        {
            Check();
            return list.Find(l => l.ALID == code || l.Name == code);
        }

        /// <summary>
        /// 보고할 알람을 찾는다.
        /// 찾는 순서는 
        /// 1. Heavy Alarm중 마지막.
        /// 2. Light Alarm중 마지막.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public AlarmInfo? GetReportAlarm(List<AlarmInfo> list)
        {
            if (list.Count == 0)
                return null;
            Check();
            var alarm = list.LastOrDefault(l => l.AlarmType == AlarmType.HeavyAlarm);
            if (alarm == null)
            {
                alarm = list.LastOrDefault(l => l.AlarmType == AlarmType.LightAlarm);
            }
            return alarm;
        }

    }
}
