using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTest
{
    public partial class Form1 : Form
    {
        List<AlarmInfo> alarmList = new List<AlarmInfo>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            alarmList.Add(new AlarmInfo { ALID = "001", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            alarmList.Add(new AlarmInfo { ALID = "002", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            alarmList.Add(new AlarmInfo { ALID = "003", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            alarmList.Add(new AlarmInfo { ALID = "004", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            alarmList.Add(new AlarmInfo { ALID = "005", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            alarmList.Add(new AlarmInfo { ALID = "006", ALTX = "ALARM", DateTime = DateTime.Now.ToString("yyyyMMdd_hhmmss") });
            var dbFactory = new OrmLiteConnectionFactory(
                "DataBase=Test",
                MySqlDialect.Provider);
            using (var db = dbFactory.Open())
            {
                db.DropAndCreateTable<AlarmInfo>();
                db.Insert<AlarmInfo>(alarmList.ToArray());

                var list = db.Select<AlarmInfo>().ToList();
                var alarm = db.Single<AlarmInfo>(d => d.ALID == "003");

                if (alarm == null)
                {
                }
                else
                {
                    alarm.Id = 0;
                    alarm.ALTX = "This is updated alarm";
                    db.Update<AlarmInfo>(alarm, d => d.ALID == "003");
                }
            }
        }
    }

    public enum AlarmType
    {
        HeavyAlarm,
        LightAlarm,
    }

    public class AlarmInfo
    {
        [AutoIncrement]
        public int Id { get; set; }
        public uint ALCD { get; set; } = 0;
        public string ALID { get; set; } = "0";
        public AlarmType AlarmType { get; set; } = AlarmType.LightAlarm;
        public string ALTX { get; set; } = "";
        public string DateTime { get; set; } = "";

        /// <summary>
        /// 해제됐는지 확인 (DB표시용)
        /// </summary>
        public bool IsRelease { get; set; } = false;
    }
}
