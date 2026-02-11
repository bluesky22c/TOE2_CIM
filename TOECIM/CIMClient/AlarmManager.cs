using DBMulti;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM
{
    public class AlarmManager : DBMulti.DBComm<TOECIMLib.AlarmInfo>
    {
        public AlarmManager() : base()
        {

        }

        public void SetAlarm(TOECIMLib.AlarmInfo alarm)
        {
            //try
            //{
            //    using (var db = dbFactory.Open())
            //    {
            //        if (alarm == null)
            //            return;

            //        db.Insert(alarm);

            //        var findAlarm = db.Single<TOECIMLib.AlarmInfo>("SELECT MAX(ID) FROM AlarmInfo");
            //        if (findAlarm == null)
            //            return;
                
            //        //DB에서 쓰는 unique ID 복사
            //        alarm.ID = findAlarm.ID;
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        public void ReleaseAlarm(TOECIMLib.AlarmInfo alarm)
        {
            //try
            //{
            //    using (var db = dbFactory.Open())
            //    {
            //        if (alarm == null)
            //            return;

            //        var findAlarm = db.Single<TOECIMLib.AlarmInfo>(d => d.ID == alarm.ID);
            //        if (findAlarm == null)
            //            return;

            //        alarm.IsRelease = true;
            //        findAlarm.IsRelease = true;
            //        findAlarm.ALCD = alarm.ALCD;
            //        db.Update(findAlarm, d => d.ID == alarm.ID);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        public List<TOECIMLib.AlarmInfo> ReadAllAlarm()
        {
            return Select();
        }

        public List<TOECIMLib.AlarmInfo> SearchAlarm(string sql)
        {
            return Select(sql);
        }
    }
}
