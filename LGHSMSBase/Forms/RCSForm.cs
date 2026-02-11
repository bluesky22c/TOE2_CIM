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
    public partial class RCSForm : Form
    {
        public RCS.RCSEnum RCS { get; set; }
        public RCSForm()
        {
            InitializeComponent();
        }

        private void offlineButton_Click(object sender, EventArgs e)
        {
            RCS = LGHSMSBase.RCS.RCSEnum.OffLine;
        }

        private void localButton_Click(object sender, EventArgs e)
        {
            RCS = LGHSMSBase.RCS.RCSEnum.OnLineLocal;
        }

        private void remoteButton_Click(object sender, EventArgs e)
        {
            RCS = LGHSMSBase.RCS.RCSEnum.ONLineRemote;
        }

    }
}
