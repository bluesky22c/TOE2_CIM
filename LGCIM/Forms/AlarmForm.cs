using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LGCIM.Lib;

namespace LGCIM.Forms
{
    public partial class AlarmForm : Form
    {
        public List<AlarmInfo> Alarms { get; set; } = new List<AlarmInfo>();
        public ClientCIM cim;
        public CIMStatus cimStatus;

        public AlarmForm()
        {
            InitializeComponent();
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            alarmInfoBindingSource.DataSource = Alarms;
            alarmInfoBindingSource.ResetBindings(false);
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            var consoleData = Common.ToConsole(new CIMtoEQPData { Command = CIMtoEQP.AlarmClear });
            if (consoleData.Error == true)
            {
                MessageBox.Show("전송 실패로 알람을 해지하지 못했습니다.");
            }

            if (Alarms == null || Alarms.Count == 0)
            {
                Close();
                return;
            }

            foreach (var alarm in Alarms.ToList())
            {
                cim.AlarmDBManager.ReleaseAlarm(alarm);
                alarm.ALCD = ALCD.ReleaseCode(alarm.ALCD);
                cim.SendS5F1(alarm, false);
            }

            Alarms.Clear();
            cimStatus.AlarmList.Clear();
            cim.RefreshUI();
            Close();
        }

        public void UpdateCurAlarmView()
        {
            alarmInfoBindingSource.ResetBindings(false);
        }
    }
}
