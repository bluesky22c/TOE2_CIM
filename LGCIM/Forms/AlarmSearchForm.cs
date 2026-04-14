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
    public partial class AlarmSearchForm : Form
    {
        public ClientCIM cim { get; set; }
        public List<AlarmInfo> AlarmItems { get; set; } = new List<AlarmInfo>();

        public AlarmSearchForm()
        {
            InitializeComponent();
        }

        private void AlarmSearchForm_Load(object sender, EventArgs e)
        {

        }


        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //일단 다 불러오는걸로 하고 나중에 꼭 필터링 작업하기 !!
            AlarmItems.Clear();
            AlarmItems.AddRange(cim.AlarmDBManager.ReadAllAlarm());
            UpdateDataGridView();
        }

        void UpdateDataGridView()
        {
            alarmdataGridView.Rows.Clear();

            foreach (var alarm in AlarmItems)
            {
                object[] row = { alarm.ID, alarm.ALCD, alarm.ALID, alarm.AlarmType, alarm.ALTX, alarm.DateTime, alarm.IsRelease };
                alarmdataGridView.Rows.Add(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
