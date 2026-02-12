using System;
using System.Windows.Forms;
using TOECIM.Forms;

namespace TOECIM
{
    public partial class SimulatorForm : Form
    {
        private EQPSimulForm eqpSimulForm;
        private RecipeSimulForm consoleSimulForm;
        private TabControl tabControl;
        private TabPage eqpTabPage;
        private TabPage consoleTabPage;

        public SimulatorForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            SuspendLayout();

            Text = "Simulator";
            Size = new System.Drawing.Size(1200, 850);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("맑은 고딕", 9F)
            };

            // Tab 1: EQP Simulator
            eqpTabPage = new TabPage("EQP Simulator");
            eqpSimulForm = new EQPSimulForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            eqpTabPage.Controls.Add(eqpSimulForm);
            eqpSimulForm.Show();

            // Tab 2: Console Simulator
            consoleTabPage = new TabPage("Console Simulator");
            consoleSimulForm = new RecipeSimulForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            consoleTabPage.Controls.Add(consoleSimulForm);
            consoleSimulForm.Show();

            tabControl.TabPages.Add(eqpTabPage);
            tabControl.TabPages.Add(consoleTabPage);
            Controls.Add(tabControl);

            ResumeLayout(false);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 서버 정지 후 숨김
                eqpSimulForm?.StopSimulServer();
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SimulatorForm
            // 
            ClientSize = new Size(1155, 800);
            Name = "SimulatorForm";
            ResumeLayout(false);

        }

        /// <summary>
        /// 지정된 탭 인덱스로 전환 (0: EQP, 1: Console)
        /// </summary>
        public void ShowTab(int tabIndex)
        {
            if (tabIndex >= 0 && tabIndex < tabControl.TabPages.Count)
            {
                tabControl.SelectedIndex = tabIndex;
            }
            Show();
            BringToFront();
        }

        /// <summary>
        /// EQP Simulator 폼 인스턴스 접근
        /// </summary>
        public EQPSimulForm EqpSimulForm => eqpSimulForm;
    }
}
