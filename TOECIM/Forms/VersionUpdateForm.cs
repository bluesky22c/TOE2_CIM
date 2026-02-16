using Elp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOECIM
{
    public partial class VersionUpdateForm : Form
    {
        public VersionUpdateForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var addForm = new AddVersionForm();
            addForm.ShowDialog();

            if (addForm.DialogResult == DialogResult.OK)
            {
                UpdateList();

                Vars.VerManager.Save();
            }
            else if(addForm.DialogResult != DialogResult.OK)
            {
                return;
            }
        }

        private void VersionUpdateForm_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        void UpdateList()
        {
            versionDataGridView.Rows.Clear();

            var versionItem = Vars.VerManager.versionHistory;
            
            if (versionItem != null)
            {
                foreach (var ver in versionItem)
                {
                    var updateContentTitle = ver.Version + " " + ver.UpdateContentTitle;
                    object[] row = { ver.No, ver.Date, ver.Version, ver.Operator, updateContentTitle };
                    versionDataGridView.Rows.Add(row);
                }
            }
        }

        private void versionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var rowdata = versionDataGridView.SelectedRows[0];
            int rowDataNo = (int)rowdata.Cells[0].Value;
            var versionItem = Vars.VerManager.versionHistory;
            var findNo = versionItem.FindIndex(v => (v.No == rowDataNo));

            if(findNo == -1)
            {
                return;
            }

            var updateContentForm = new UpdateContentForm(findNo);

            updateContentForm.ShowDialog();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (versionDataGridView.RowCount == 0)
            {
                MessageBox.Show("기존 데이터가 없습니다");
                return;
            }

            var rowdata = versionDataGridView.SelectedRows[0];
            int rowDataNo = (int)rowdata.Cells[0].Value;
            var versionItem = Vars.VerManager.versionHistory;
            var findNo = versionItem.FindIndex(v => (v.No == rowDataNo));

            if (versionItem != null) 
            {
                if(MessageBox.Show("선택한 작업 내용을 삭제 하겠습니까?","YesOrNo",MessageBoxButtons.YesNo) == DialogResult.Yes) 
                {
                    versionItem.RemoveAt(findNo);
                    Vars.VerManager.Save();
                    UpdateList();
                }
                else
                {
                    return;
                }
            }
        }

        private void SerchButton_Click(object sender, EventArgs e)
        {
            versionDataGridView.Rows.Clear();

            var versionItem = Vars.VerManager.SearchVersion(startDatePicker.Value, EndDatePicker.Value);

            if (versionItem != null)
            {
                foreach (var ver in versionItem)
                {
                    var updateContentTitle = ver.Version + " " + ver.UpdateContentTitle;
                    object[] row = { ver.No, ver.Date, ver.Version, ver.Operator, updateContentTitle };
                    versionDataGridView.Rows.Add(row);
                }
            }
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
