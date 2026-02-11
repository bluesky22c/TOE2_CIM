using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOECIM.Forms
{
    public partial class SystemSetupForm : Form
    {
        public SystemSetup systemSetup { get; set; }

        public SystemSetupForm()
        {
            InitializeComponent();
        }

        private void SystemSetupForm_Load(object sender, EventArgs e)
        {
            systemSetupPropertyGrid.SelectedObject = systemSetup;
            configPropertyGrid.SelectedObject = systemSetup.Config;
        }
    }
}
