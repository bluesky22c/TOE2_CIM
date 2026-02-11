using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using TOELib;

namespace TOECIM
{
    public partial class AddVersionForm : Form
    {
        public VersionHistory VersionHistory { get; set; }

        public AddVersionForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if ((IsNullCheck() == false) || (IsNoCheck() == true))
            {
                this.DialogResult= DialogResult.None;
                return;
            }

            var Version = Vars.VerManager.versionHistory;
            
            VersionHistory = new VersionHistory()
            {
                No = int.Parse(noTextBox.Text),
                Date = DatePicker.Value.ToString("yyyy-MM-dd"),
                Version = versionTextBox.Text,
                Operator = nameTextBox.Text,
                UpdateContent = descTextBox.Text,
            };

            Version.Add(VersionHistory);
        }

        private bool IsNullCheck()
        {
            if(string.IsNullOrEmpty(noTextBox.Text))
            {
                MessageBox.Show("작업 번호 입력 필요");
                return false;
            }

            if(string.IsNullOrEmpty(versionTextBox.Text))
            {
                MessageBox.Show("작업 버전 입력 필요");
                return false;
            }

            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("작업자 입력 필요");
                return false;
            }

            if (string.IsNullOrEmpty(descTextBox.Text))
            {
                MessageBox.Show("작업 내용 입력 필요");
                return false;
            }

            return true;
        }

        private bool IsNoCheck()
        {
            var version = Vars.VerManager.versionHistory;

            int textNo = int.Parse(noTextBox.Text);

            for(int i =0; i< version.Count; i++) 
            {
                if (version[i].No == textNo)
                {
                    MessageBox.Show("작업 번호 중복 입력");
                    return true;
                }
            }

            return false;
        }
    }
}
