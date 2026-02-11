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
    public partial class OPModeForm : Form
    {
        public OPMODE.OPMODEEnum OPMODE { get; set; }
        public OPModeForm()
        {
            InitializeComponent();
        }

        private void OPModeForm_Load(object sender, EventArgs e)
        {

        }

        private void SequenceTransferButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.SequenceTransfer;
        }

        private void RestorationButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.Restoration;
        }

        private void CleanOutButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.CleanOut;
        }

        private void normalButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.Normal;
        }

        private void ParticleCheckButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.ParticleCheck;
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.Transfer;
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.Test;
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.Manual;
        }

        private void RepairButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.RepairMode;
        }

        private void ReverseButton_Click(object sender, EventArgs e)
        {
            OPMODE = LGHSMSBase.OPMODE.OPMODEEnum.ReverseMode;
        }

    }
}
