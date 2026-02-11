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
using TOECIMLib;

namespace TOECIM.Forms
{
    public partial class AlarmSimulForm : Form
    {
        public AlarmInfo AlarmInfo { get; set; }

        public AlarmSimulForm()
        {
            InitializeComponent();
        }

        private void AddAlarmForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Enum.GetNames(typeof(ALCD.ALCDEnum)).Length; i++)
            {
                var alarmCodes = Enum.GetValues(typeof(ALCD.ALCDEnum));
                ALCDComboBox.Items.Add(alarmCodes.GetValue(i));
            }
            ALCDComboBox.SelectedIndex = 0;

            for (int i = 0; i < Enum.GetNames(typeof(AlarmType)).Length; i++)
            {
                var alarmType = Enum.GetValues(typeof(AlarmType));
                ALTypeComboBox.Items.Add(alarmType.GetValue(i));
            }
            ALTypeComboBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            AlarmInfo = new AlarmInfo();

            AlarmInfo.ALID = ALIDTextBox.Text;
            AlarmInfo.ALTX = ALTXTextBox.Text;
            AlarmInfo.AlarmType = (AlarmType)ALTypeComboBox.SelectedItem;
            AlarmInfo.DateTime = DateTime.Now;
            AlarmInfo.IsRelease = false;
            AlarmInfo.ALCD = ALCD.SetCode((ALCD.ALCDEnum)ALCDComboBox.SelectedIndex);
        }
    }
}
