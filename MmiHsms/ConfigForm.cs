using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMISoft.HSMS
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class ConfigForm : Form
    {
        public HSMSConfig Config { get; set; }
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Config; 
        }
    }
}
