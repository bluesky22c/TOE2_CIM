using LGCIMServer.EQPSimul;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Versioning;

namespace LGCIMServer
{
    [SupportedOSPlatform("windows")]
    public partial class LGCIMSimulForm : Form
    {
        ECSSimulForm ecsSimulForm;
        EQPSimulForm eqpSimulForm;
        RecipeSimulForm recipeSimulForm;
        ScenarioRunner _runner;

        public LGCIMSimulForm()
        {
            InitializeComponent();
            InitializeComponents();
            
            // 시작 위치 설정 (-1920, 0)
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(-1920, 0);
            // this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponents()
        {
            mainLayoutPanel.BackColor = Color.White;
            
            // MenuStrip 추가 (Scenario Trigger)
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem scenarioMenu = new ToolStripMenuItem("Scenarios");
            menu.Items.Add(scenarioMenu);
            this.Controls.Add(menu);
            this.MainMenuStrip = menu;

            // Title Styling
            var titleFont = new Font("맑은 고딕", 10F, FontStyle.Bold);
            var titleBackColor = Color.FromArgb(45, 45, 45);
            var titleForeColor = Color.White;

            Label CreateTitle(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = titleFont,
                BackColor = titleBackColor,
                ForeColor = titleForeColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };

            var ecsTitle = CreateTitle("ECS     ---->");
            mainLayoutPanel.Controls.Add(ecsTitle, 0, 0);

            var toeCimTitle = CreateTitle("<----    TOECIM    ---->");
            mainLayoutPanel.Controls.Add(toeCimTitle, 1, 0);
            mainLayoutPanel.SetColumnSpan(toeCimTitle, 2);

            var eqpTitle = CreateTitle("<----     EQP");
            mainLayoutPanel.Controls.Add(eqpTitle, 3, 0);

            // ECS Form
            ecsSimulForm = new ECSSimulForm();
            ecsSimulForm.TopLevel = false;
            ecsSimulForm.FormBorderStyle = FormBorderStyle.None;
            ecsSimulForm.Dock = DockStyle.Fill;
            ecsSimulForm.Visible = true;
            mainLayoutPanel.Controls.Add(ecsSimulForm, 0, 1);
            mainLayoutPanel.SetColumnSpan(ecsSimulForm, 2);

            // EQP 영역을 탭 구조로 변경
            TabControl eqpTabControl = new TabControl { Dock = DockStyle.Fill };
            mainLayoutPanel.Controls.Add(eqpTabControl, 2, 1);
            mainLayoutPanel.SetColumnSpan(eqpTabControl, 2);

            // 1. EQP 탭
            TabPage eqpTabPage = new TabPage("EQP");
            eqpSimulForm = new EQPSimulForm();
            eqpSimulForm.TopLevel = false;
            eqpSimulForm.FormBorderStyle = FormBorderStyle.None;
            eqpSimulForm.Dock = DockStyle.Fill;
            eqpSimulForm.Visible = true;
            eqpTabPage.Controls.Add(eqpSimulForm);
            eqpTabControl.TabPages.Add(eqpTabPage);

            // 2. Recipe 탭
            TabPage recipeTabPage = new TabPage("Recipe");
            recipeSimulForm = new RecipeSimulForm(eqpSimulForm.Simulator);
            recipeSimulForm.TopLevel = false;
            recipeSimulForm.FormBorderStyle = FormBorderStyle.None;
            recipeSimulForm.Dock = DockStyle.Fill;
            recipeSimulForm.Visible = true;
            recipeTabPage.Controls.Add(recipeSimulForm);
            eqpTabControl.TabPages.Add(recipeTabPage);

            // Init Scenario Runner
            _runner = new ScenarioRunner(eqpSimulForm.Simulator, ecsSimulForm.Server);
            
            ScenarioProgressForm progressForm = new ScenarioProgressForm(_runner);

            // Add Menu Items (Show Progress Form and load scenario)
            void ShowScenarioForm()
            {
                if (progressForm.IsDisposed) progressForm = new ScenarioProgressForm(_runner);
                progressForm.Show();
                progressForm.BringToFront();
            }

            scenarioMenu.DropDownItems.Add("Scenario Auto Test", null, (s, e) => ShowScenarioForm());


            FormClosing += SimulatorForm_FormClosing;
        }


        private void SimulatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (eqpSimulForm != null) eqpSimulForm.StopSimulServer();
        }

    }
}
