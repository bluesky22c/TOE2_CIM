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

namespace LGCIMServer.EQPSimul
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class RecipeSimulForm : Form
    {
        RecipeSimulator simul = new RecipeSimulator();
        EQPSimulator eqpSimul;
        
        string recipePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Simul", "Recipe");
        RecipeSet selected;

        public RecipeSimulForm(EQPSimulator eqpSimul = null)
        {
            InitializeComponent();
            this.eqpSimul = eqpSimul;
        }

        private void ConsoleSimulator_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void ConsoleSimulator_OnConsoleEvent(CimCommData data)
        {
            var e = data.ToCIM;

            switch (e.Command)
            {
                case EQPtoCIM.None:
                    break;

                case EQPtoCIM.RecipeCreate:
                    simul.RecipeCreate(e.RecipeItem);
                    break;

                case EQPtoCIM.RecipeChange:
                    simul.RecipeChange(e.RecipeItem);
                    break;

                case EQPtoCIM.RecipeDelete:
                    simul.RecipeDelete(e.RecipeItem);
                    break;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var recipeSet = new RecipeSet();
            recipeSet.RECIPEID = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            recipeSet.Save(recipePath, recipeSet.RECIPEID);
            UpdateUI();

            if (eqpSimul != null) simul.SyncStatus(eqpSimul.Data);

            ConsoleSimulator_OnConsoleEvent(new CimCommData 
            {
                ToCIM = new EQPtoCIMData {
                    Command = EQPtoCIM.RecipeCreate,
                    RecipeItem = recipeSet,
                }
            });
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("삭제할 레시피가 없습니다.");
                return;
            }

            if (eqpSimul != null) simul.SyncStatus(eqpSimul.Data);

            ConsoleSimulator_OnConsoleEvent(new CimCommData
            {
                ToCIM = new EQPtoCIMData {
                    Command = EQPtoCIM.RecipeDelete,
                    RecipeItem = selected,
                }
            });

            var delete = Path.Combine(recipePath, selected.RECIPEID);
            try
            {
                if (Directory.Exists(delete))
                {
                    Directory.Delete(delete, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"레시피 폴더 삭제 실패: {ex.Message}");
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            recipeListDataGridView.Rows.Clear();

            if (!Directory.Exists(recipePath))
            {
                Directory.CreateDirectory(recipePath);
            }

            var recipeFileList = Directory.GetDirectories(recipePath);
            foreach (string dir in recipeFileList)
            {
                string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();

                object[] row = { recipeName, "" };
                recipeListDataGridView.Rows.Add(row);
            }
        }

        private void recipeListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var recipeID = recipeListDataGridView.Rows[e.RowIndex].Cells[0].Value;

            var recipeFileList = Directory.GetDirectories(recipePath);
            foreach (string dir in recipeFileList)
            {
                string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                if(recipeID.ToString() == recipeName)
                {
                    selected = RecipeSet.Load(recipePath, recipeName);
                }
            }

            recipeParamPropertyGrid.SelectedObject = selected;
            recipeBindingSource.DataSource = selected.ScanRecipe;
            recipeBindingSource.ResetBindings(false);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(selected == null)
            {
                MessageBox.Show("저장할 레시피가 없습니다.");
                return;
            }

            if (eqpSimul != null) simul.SyncStatus(eqpSimul.Data);

            ConsoleSimulator_OnConsoleEvent(new CimCommData
            {
                ToCIM = new EQPtoCIMData {
                    Command = EQPtoCIM.RecipeChange,
                    RecipeItem = selected,
                }
            });
            selected.Save(recipePath, selected.RECIPEID);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
