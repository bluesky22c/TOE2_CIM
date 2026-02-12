using Engion.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM.Forms
{
    public partial class RecipeSimulator : System.Windows.Forms.Form
    {
        static public int CONSOLEServerPort = 3501;
        CimCommData consoleData { get; set; } = new CimCommData();

        internal void SendToCIM(CimCommData consoleData)
        {
            try
            {
                var client = new JsonClient<CimCommData>(this, "127.0.0.1", Program.SystemSetup.ConsolePortNo, null);

                CimCommData data = Common.Clone<CimCommData>(consoleData);
                data.WaitResponse = false;

                client.Send(data);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        internal void RecipeCreate(RecipeSet recipe)
        {
            consoleData.ToCIM.Command = EQPtoCIM.RecipeCreate;
            consoleData.ToCIM.RecipeItem = recipe;

            SendToCIM(consoleData);
        }

        internal void RecipeChange(RecipeSet recipe)
        {
            consoleData.ToCIM.Command = EQPtoCIM.RecipeChange;
            consoleData.ToCIM.RecipeItem = recipe;

            SendToCIM(consoleData);
        }

        internal void RecipeDelete(RecipeSet recipe)
        {
            consoleData.ToCIM.Command = EQPtoCIM.RecipeDelete;
            consoleData.ToCIM.RecipeItem = recipe;

            SendToCIM(consoleData);
        }

        internal void RecipeRename(RecipeSet recipe)
        {
            consoleData.ToCIM.Command = EQPtoCIM.RecipeRename;
            consoleData.ToCIM.RecipeItem = recipe;

            SendToCIM(consoleData);
        }

        private void deleteButton_Click(object sender, EventArgs e) { }
        private void recipeListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void addButton_Click(object sender, EventArgs e) { }
        private void cancelButton_Click(object sender, EventArgs e) { }
        private void saveButton_Click(object sender, EventArgs e) { }
        private void ConsoleSimulator_Load(object sender, EventArgs e) { }
    }
}
