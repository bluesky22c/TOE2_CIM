using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGHSMSBase.Forms
{
    public partial class EQPStatusForm : Form
    {
        public EQPSTATUS.EQPSTATUSEnum EQPStatus { get; set; }

        public EQPStatusForm()
        {
            InitializeComponent();
        }

        private void ExperimentButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.Experiment;
        }

        private void ProcessDownButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.ProcessDown;
        }

        private void DOWNButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.DOWN;
        }

        private void IDLEButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.IDLE;
        }

        private void MonitoringButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.Monitoring;
        }

        private void ModelChangeButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.ModelChange;
        }

        private void DevelopmentButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.Development;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            EQPStatus = EQPSTATUS.EQPSTATUSEnum.RUN;
        }
    }
}
