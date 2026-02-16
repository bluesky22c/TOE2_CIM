using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LGCIMServer.Define;

namespace LGCIMServer
{
    public class ScenarioProgressForm : Form
    {
        private FlowLayoutPanel _stepPanel;
        private Label _statusLabel;
        private List<Label> _stepLabels = new List<Label>();
        private ScenarioRunner _runner;
        private string _currentScenarioId;

        public ScenarioProgressForm(ScenarioRunner runner)
        {
            _runner = runner;
            InitializeComponent();
            _runner.ScenarioStarted += Runner_ScenarioStarted;
            _runner.StepExecuting += Runner_StepExecuting;
            _runner.ScenarioCompleted += Runner_ScenarioCompleted;
            
            this.FormClosing += (s, e) => 
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.Hide();
                }
            };
        }

        private void InitializeComponent()
        {
            this.Text = "Scenario Progress";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130)); // Header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Steps
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Status

            // Header (Scenario Selector)
            var headerPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };

            var scenarios = new[] {
                new { ID = "A", Text = "Normal Flow" },
                new { ID = "C", Text = "Unload" },
                new { ID = "D", Text = "Unload Glass (S6F63)" },
                new { ID = "E", Text = "Lot End (S6F65)" },
                new { ID = "RECIPE", Text = "Recipe" },
                new { ID = "B", Text = "Alarm" }
            };

            foreach (var sc in scenarios)
            {
                var btn = new Button { Text = sc.Text, Width = 130, Height = 50, Tag = sc.ID };
                btn.Click += (s, e) => LoadScenario((string)((Button)s).Tag);
                headerPanel.Controls.Add(btn);
            }
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // Steps Panel
            _stepPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            mainLayout.Controls.Add(_stepPanel, 0, 1);

            // Status Label
            _statusLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Ready",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightGray
            };
            mainLayout.Controls.Add(_statusLabel, 0, 2);

            this.Controls.Add(mainLayout);
        }

        public void LoadScenario(string id)
        {
            _currentScenarioId = id;
            var scenario = _runner.GetScenario(id);
            if (scenario == null) return;

            _stepPanel.Controls.Clear();
            _stepLabels.Clear();

            _statusLabel.Text = $"Loaded: {scenario.Name}";

            int stepNum = 1;
            foreach (var step in scenario.Steps)
            {
                var lbl = new Label
                {
                    Text = $"{stepNum++}. {step.Action} " + (step.Params != null ? string.Join(", ", step.Params.Values) : ""),
                    AutoSize = true,
                    Padding = new Padding(5),
                    Margin = new Padding(3),
                    Font = new Font("Consolas", 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 350
                };
                _stepPanel.Controls.Add(lbl);
                _stepLabels.Add(lbl);
            }
            
            // Add Run Button at the bottom of steps
            var runBtn = new Button { Text = "RUN SCENARIO", Width = 350, Height = 40, BackColor = Color.LightBlue };
            runBtn.Click += (s, e) => _ = _runner.RunScenarioAsync(id);
            _stepPanel.Controls.Add(runBtn);
        }

        private void Runner_ScenarioStarted(string name)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_ScenarioStarted(name))); return; }
            _statusLabel.Text = "Running: " + name;
            _statusLabel.BackColor = Color.Yellow;
            
            foreach(var lbl in _stepLabels)
            {
                lbl.BackColor = Color.White;
                lbl.ForeColor = Color.Black;
            }
        }

        private void Runner_StepExecuting(string action, int index)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_StepExecuting(action, index))); return; }
            
            if (index >= 0 && index < _stepLabels.Count)
            {
                // Reset previous
                if (index > 0)
                {
                    _stepLabels[index - 1].BackColor = Color.LightGreen;
                }

                // Highlight current
                _stepLabels[index].BackColor = Color.Orange;
                _stepLabels[index].ForeColor = Color.White;
                _stepPanel.ScrollControlIntoView(_stepLabels[index]);
            }
        }

        private void Runner_ScenarioCompleted(string name)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_ScenarioCompleted(name))); return; }
            _statusLabel.Text = "Completed: " + name;
            _statusLabel.BackColor = Color.LimeGreen;

            // Mark all as done
            foreach (var lbl in _stepLabels)
            {
                lbl.BackColor = Color.LightGreen;
                lbl.ForeColor = Color.Black;
            }
        }
    }
}
