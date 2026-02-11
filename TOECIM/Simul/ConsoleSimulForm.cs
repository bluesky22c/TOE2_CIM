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

namespace TOECIM.Forms
{
    public partial class ConsoleSimulForm : Form
    {
        ConsoleSimulator simul = new ConsoleSimulator();
        
        string recipePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Simul", "Recipe");
        RecipeSet selected;
        //public event EventHandler<CONSOLEtoCIMData> OnConsoleEvent = delegate { };    //EQ to Console 이벤트

        public ConsoleSimulForm()
        {
            InitializeComponent();
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
                case ConsoletoCIM.None:
                    break;

                case ConsoletoCIM.RecipeCreate:
                    simul.RecipeCreate(e.RecipeItem);
                    break;

                case ConsoletoCIM.RecipeChange:
                    simul.RecipeChange(e.RecipeItem);
                    break;

                case ConsoletoCIM.RecipeDelete:
                    simul.RecipeDelete(e.RecipeItem);
                    break;
            }

            //OnConsoleEvent(this, e);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var recipeSet = new RecipeSet();
            recipeSet.RECIPEID = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            recipeSet.Save(recipePath, recipeSet.RECIPEID);
            UpdateUI();

            ConsoleSimulator_OnConsoleEvent(new CimCommData 
            {
                ToCIM = new ConsoletoCIMData {
                    Command = ConsoletoCIM.RecipeCreate,
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

            ConsoleSimulator_OnConsoleEvent(new CimCommData
            {
                ToCIM = new ConsoletoCIMData {
                    Command = ConsoletoCIM.RecipeCreate,
                    RecipeItem = selected,
                }
            });

            var delete = Path.Combine(recipePath, selected.RECIPEID);
            DirectoryInfo di = new DirectoryInfo(delete);
            di.Delete(true);

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

            ConsoleSimulator_OnConsoleEvent(new CimCommData
            {
                ToCIM = new ConsoletoCIMData {
                    Command = ConsoletoCIM.RecipeChange,
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
