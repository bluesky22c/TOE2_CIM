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

namespace LGCIMServer.Forms
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class PDDownForm : Form
    {
        public RCMD.RCMDEnum RCMD { get; set; }

        public PDDownForm()
        {
            InitializeComponent();
        }

        private void PDDownForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Enum.GetNames(typeof(RCMD.RCMDEnum)).Length; i++)
            {
                var rcmds = Enum.GetValues(typeof(RCMD.RCMDEnum));
                rcmdComboBox.Items.Add(rcmds.GetValue(i));
            }
            rcmdComboBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RCMD = (RCMD.RCMDEnum)rcmdComboBox.SelectedItem;
        }
    }
}
