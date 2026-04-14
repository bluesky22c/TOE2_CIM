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

namespace LGCIMServer.EQPSimul.Forms
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class IonizerTestForm : Form
    {
        public IonizerInfo[] IonItems { get; set; } = new IonizerInfo[3];

        public IonizerTestForm()
        {
            InitializeComponent();
        }

        private void IonizerTestForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            MakeIonItem();
            RefreshUI();
        }

        private void MakeIonItem()
        {
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var item = new IonizerInfo();
                item.ID = (byte)rnd.Next(0, 255);
                item.Status = "R";
                IonItems[i] = item;
            }
        }

        private void RefreshUI()
        {
            ionizerInfoBindingSource.DataSource = IonItems; 
            ionizerInfoBindingSource.ResetBindings(false);
        }
    }
}
