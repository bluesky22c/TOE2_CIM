using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOECIMLib;

namespace LGCIMServer.Forms
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class RecipeSearchForm : Form
    {
        public ServerCIM server;
        RecipeSet CurRecipe { get; set; }
        List<RecipeSet> AllRecipe => server.serverStatus.CIMRecipes;


        public RecipeSearchForm()
        {
            InitializeComponent();
        }

        private void RecipeSearchForm_Load(object sender, EventArgs e)
        {
            Update_AllRecipe();
        }

        private void UpdateUI_CurRecipe()
        {
            if (CurRecipe != null)
            {
                curReipceLabel.Text = $"CurRecipe : {CurRecipe.RECIPEID}";
            }
        }

        public void Update_AllRecipe()
        {            
            recipeSetBindingSource.DataSource = AllRecipe;
            recipeSetBindingSource.ResetBindings(false);
        }

        private void recipSetDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            RecipeSet recipeSet = (RecipeSet)recipeSetBindingSource.Current;
            scanRecipePropertyGrid.SelectedObject = recipeSet;
            recipeBindingSource.DataSource = recipeSet.ScanRecipe;
            recipeBindingSource.ResetBindings(false);

            CurRecipe = recipeSet;
            UpdateUI_CurRecipe();
        }


        //보고------------------------------------------------------------------------
        private void 레시피조회S15F71ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.SendS15F71();
            Update_AllRecipe();
        }

        private void 하위레시피조회S15F73ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("선택된 레시피가 없습니다.");
                return;
            }
            var recipeID = CurRecipe.RECIPEID;
            server.SendS15F73(recipeID);
        }

        private void 레시피파라미터조회S15F75ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("선택된 레시피가 없습니다.");
                return;
            }
            var recipeID = CurRecipe.RECIPEID;
            server.SendS15F75(recipeID);
        }

        private void bulitButton_Click(object sender, EventArgs e)
        {
            var recipeSet = new RecipeSet();
            recipeSet.RECIPEID = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            for (int i = 0; i < 3; i++) 
            {
                recipeSet.ScanRecipe.Add(new CIMRecipe { ScanNo = i, PARAM_A = $"A{i}", PARAM_B = $"B{i}", PARAM_C = $"C{i}" });
            }
            AllRecipe.Add(recipeSet);
            Update_AllRecipe();
            server.SendS15F77(recipeSet, RECIPEIDCMD.RECIPEIDCMDEnum.Built);
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("변경할 레시피를 선택해주세요.");
                return;
            }

            Update_AllRecipe();
            server.SendS15F77(CurRecipe , RECIPEIDCMD.RECIPEIDCMDEnum.Change);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(CurRecipe == null)
            {
                MessageBox.Show("삭제할 레시피를 선택해주세요.");
                return;
            }

            AllRecipe.Remove(CurRecipe);
            Update_AllRecipe();
            server.SendS15F77(CurRecipe, RECIPEIDCMD.RECIPEIDCMDEnum.Delete);
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("변경할 레시피를 선택해주세요.");
                return;
            }

            Update_AllRecipe();
            server.SendS15F77(CurRecipe, RECIPEIDCMD.RECIPEIDCMDEnum.Rename);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 현재RecipeIDList조회S15F65ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.SendS15F65();
        }

        private void ePPD조회S7F19ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            streamTextBox.Text = "7";
            functionTextBox.Text = "19";
            textBox1.Text = server.MakeS7F19();
        }

        private void ePPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.SendS7F67();
        }

        private void groupInRecipeID조회S7F69ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.SendS7F69();
        }

        private void recipeBody조회S7F77ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("조회할 레시피를 선택해주세요.");
                return;
            }

            var recipeID = CurRecipe.RECIPEID;
            server.SendS7F77(recipeID);
        }

        private void recipeConfirmButton_Click(object sender, EventArgs e)
        {
            if (CurRecipe == null)
            {
                MessageBox.Show("삭제할 레시피를 선택해주세요.");
                return;
            }

            server.SendS7F61(1, CurRecipe.RECIPEID);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var stream = int.Parse(streamTextBox.Text);
            var function = int.Parse(functionTextBox.Text);
            HSMSMessage msg = new HSMSMessage();

            msg.SetHSMS(stream, function, 0, true, textBox1.Text);
            server.gem.HSMSSend(msg);
        }
    }
}
