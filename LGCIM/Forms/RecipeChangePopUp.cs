using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LGCIM.Lib;

namespace LGCIM.Forms
{
    public partial class RecipeChangePopUp : Form
    {
        int timeLimit = 5000;

        public RecipeChangePopUp()
        {
            InitializeComponent();
        }

        private void RecipeChangePopUp_Load(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerLabel.Text = $"{timeLimit / timer1.Interval}초 뒤에 자동으로 닫힙니다.";
            timeLimit -= 1000;

            if (timeLimit < 0)
            {
                timer1.Stop();
                Close();
            }
        }

        public void UpdateUI(RecipeSet recipeSet, string title)
        {
            titleLabel.Text = title;
            recipeIDLabel.Text = $"Recipe ID : {recipeSet.RECIPEID}";
            descriptionLabel.Text = $"Desc : {recipeSet.DESC}";

            recipeBindingSource.DataSource = recipeSet.ScanRecipe;
            recipeBindingSource.ResetBindings(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
