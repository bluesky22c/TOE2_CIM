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
    public partial class GLSMVSTATUSForm : Form
    {
        public GLSMVSTATUS.GLSMVSTATUSEnum GLSMVSTATUS { get; set; }

        public GLSMVSTATUSForm()
        {
            InitializeComponent();
        }

        private void GLSMVSTATUSForm_Load(object sender, EventArgs e)
        {
            
        }

        private void UWButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UW;
        }

        private void CRButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.CR;
        }

        private void LWButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LW;
        }

        private void LRButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.LR;
        }

        private void URButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.UR;
        }

        private void ERButton_Click(object sender, EventArgs e)
        {
            GLSMVSTATUS = LGHSMSBase.GLSMVSTATUS.GLSMVSTATUSEnum.ER;
        }
    }
}
