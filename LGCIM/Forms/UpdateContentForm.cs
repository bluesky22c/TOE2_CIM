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

namespace LGCIM
{
    public partial class UpdateContentForm : Form
    {
        //GridView 선택 행 작업 번호
        public int SelectRowindex { get; set; } = 0;

        public UpdateContentForm(int selectRowNo)
        {
            InitializeComponent();;
            this.SelectRowindex = selectRowNo;
        }

        private void UpdateContectForm_Load(object sender, EventArgs e)
        {
            var Version = Vars.VerManager.versionHistory;

            descTextBox.Text = Version[SelectRowindex].UpdateContent;
            nameTextBox.Text = Version[SelectRowindex].Operator;
            versionTextBox.Text = Version[SelectRowindex].Version;
            DateTextBox.Text = Version[SelectRowindex].Date;
        }
    }
}
