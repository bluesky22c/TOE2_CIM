using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace LGCIMServer
{
    [SupportedOSPlatform("windows")]
    public class ScenarioProgressForm : Form
    {
        // 플로우 다이어그램 한 행을 표현하는 내부 클래스
        private class FlowStep
        {
            public int StepNumber { get; set; }
            public string MessageName { get; set; }
            public string Direction { get; set; }   // H->C, C->H, C->E, E->C
            public int ScenarioStepIndex { get; set; }  // ScenarioRunner StepIndex 매핑
            public Panel StepControl { get; set; }
            public Label Bubble { get; set; }
        }

        private Panel _flowPanel;
        private Label _statusLabel;
        private List<FlowStep> _currentFlowSteps = new List<FlowStep>();
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
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.MinimumSize = new Size(600, 400);
            this.TopMost = true;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                Padding = new Padding(0)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110)); // 시나리오 선택 버튼
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));  // RUN SCENARIO 버튼
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));  // Status 레이블
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // 플로우 패널

            // ── 시나리오 선택 버튼 ──
            var headerPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(4)
            };

            var scenarios = new (string id, string text)[]
            {
                ("A",      "Normal Flow"),
                ("C",      "Unload"),
                ("D",      "Unload Glass (S6F63)"),
                ("E",      "Lot End (S6F65)"),
                ("RECIPE", "Recipe"),
                ("B",      "Alarm")
            };

            foreach (var (id, text) in scenarios)
            {
                var btn = new Button
                {
                    Text = text,
                    Width = 148,
                    Height = 46,
                    Tag = id,
                    Margin = new Padding(2),
                    FlatStyle = FlatStyle.System
                };
                btn.Click += (s, e) => LoadScenario((string)((Button)s).Tag);
                headerPanel.Controls.Add(btn);
            }
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // ── RUN SCENARIO 버튼 ──
            var runBtn = new Button
            {
                Text = "RUN SCENARIO",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(80, 190, 200),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            runBtn.FlatAppearance.BorderSize = 0;
            runBtn.Click += (s, e) =>
            {
                if (_currentScenarioId != null)
                    _ = _runner.RunScenarioAsync(_currentScenarioId);
            };
            mainLayout.Controls.Add(runBtn, 0, 1);

            // ── Status 레이블 ──
            _statusLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Status: Ready",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Padding = new Padding(6, 0, 0, 0),
                BackColor = Color.White,
                ForeColor = Color.DimGray
            };
            mainLayout.Controls.Add(_statusLabel, 0, 2);

            // ── 플로우 다이어그램 패널 (스크롤) ──
            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };
            _flowPanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White
            };
            scrollPanel.Controls.Add(_flowPanel);
            mainLayout.Controls.Add(scrollPanel, 0, 3);

            this.Controls.Add(mainLayout);
        }

        // ─────────────────────────────────────────────────────
        // 시나리오 로드 (버튼 클릭 시)
        // ─────────────────────────────────────────────────────
        public void LoadScenario(string id)
        {
            _currentScenarioId = id;
            InitScenarioFlow(id.ToUpper());
            _statusLabel.Text = "Status: Ready";
            _statusLabel.BackColor = Color.White;
            _statusLabel.ForeColor = Color.DimGray;
        }

        // ─────────────────────────────────────────────────────
        // 플로우 다이어그램 초기화 (ECSSimulForm.InitScenarioFlow 참고)
        // ─────────────────────────────────────────────────────
        private void InitScenarioFlow(string id)
        {
            _flowPanel.Controls.Clear();
            _currentFlowSteps.Clear();

            // 3열 헤더
            _flowPanel.Controls.Add(MakeHeader("ECS Simulator", new Point(5,  5)));
            _flowPanel.Controls.Add(MakeHeader("CIM (Main)",    new Point(195, 5)));
            _flowPanel.Controls.Add(MakeHeader("EQP Simulator", new Point(365, 5)));

            var flow = GetFlowDefinition(id);

            int y = 32;
            for (int i = 0; i < flow.Count; i++)
            {
                var fs = new FlowStep
                {
                    StepNumber        = i + 1,
                    MessageName       = flow[i].msg,
                    Direction         = flow[i].dir,
                    ScenarioStepIndex = flow[i].stepIdx
                };

                Panel row = CreateStepRow(fs);
                row.Location = new Point(5, y);
                _flowPanel.Controls.Add(row);
                fs.StepControl = row;
                _currentFlowSteps.Add(fs);
                y += 50;
            }
        }

        private Label MakeHeader(string text, Point loc)
        {
            return new Label
            {
                Text      = text,
                Location  = loc,
                AutoSize  = true,
                Font      = new Font("Malgun Gothic", 8, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent
            };
        }

        // ─────────────────────────────────────────────────────
        // 시나리오별 플로우 정의 (사양서 기반)
        //
        // 방향 코드:
        //   H->C : ECS Simulator → CIM (Main)
        //   C->H : CIM (Main) → ECS Simulator
        //   C->E : CIM (Main) → EQP Simulator
        //   E->C : EQP Simulator → CIM (Main)
        //
        // stepIdx : ScenarioRunner.ScenarioStep 인덱스 (0-based)
        // ─────────────────────────────────────────────────────
        private List<(string msg, string dir, int stepIdx)> GetFlowDefinition(string id)
        {
            var f = new List<(string, string, int)>();

            switch (id)
            {
                // ── A: Normal Loading (LoadReady→LoadDone→InspectDone→UnloadReady→UnloadDone) ──
                case "A":
                    // LoadReady (step 0) ─────────────────────
                    f.Add(("S7F61 (Recipe ID Confirm Req)", "H->C", 0));
                    f.Add(("S7F62 (Ack)",                  "C->H", 0));
                    f.Add(("S2F87 (Load Glass Data)",       "H->C", 0));
                    f.Add(("S2F88 (Ack)",                  "C->H", 0));
                    f.Add(("Glass Move Req (LR)",           "C->E", 0));
                    f.Add(("S6F67 (Move Request: LR)",      "C->H", 0));
                    f.Add(("S6F68 (Ack)",                  "H->C", 0));
                    // LoadDone (step 1) ──────────────────────
                    f.Add(("CMD: LoadDone",                "E->C", 1));
                    f.Add(("S6F91 (Status Change: RUN)",   "C->H", 1));
                    f.Add(("S6F92 (Ack)",                  "H->C", 1));
                    f.Add(("S1F67 (All Glass Position)",   "C->H", 1));
                    f.Add(("S1F68 (Ack)",                  "H->C", 1));
                    // InspectDone (step 2) ───────────────────
                    f.Add(("CMD: InspectDone",             "E->C", 2));
                    f.Add(("S6F91 (Status Change: RUN)",   "C->H", 2));
                    f.Add(("S6F92 (Ack)",                  "H->C", 2));
                    // UnloadReady (step 3) ───────────────────
                    f.Add(("CMD: UnloadReady",             "E->C", 3));
                    f.Add(("S6F67 (Move Request: UR)",     "C->H", 3));
                    f.Add(("S6F68 (Ack)",                  "H->C", 3));
                    // UnloadDone (step 4) ────────────────────
                    f.Add(("CMD: UnloadDone",              "E->C", 4));
                    f.Add(("S6F91 (Status Change: IDLE)",  "C->H", 4));
                    f.Add(("S6F92 (Ack)",                  "H->C", 4));
                    f.Add(("S1F67 (All Glass Position)",   "C->H", 4));
                    f.Add(("S1F68 (Ack)",                  "H->C", 4));
                    break;

                // ── C: Unload (ExchangeReady→UnloadReady→UnloadDone) ──
                case "C":
                    // ExchangeReady (step 0) ─────────────────
                    f.Add(("CMD: ExchangeReady",           "E->C", 0));
                    f.Add(("S6F63 (Unload Glass Data)",    "C->H", 0));
                    f.Add(("S6F64 (Ack)",                  "H->C", 0));
                    f.Add(("S6F61 (Load Glass Data Req)",  "C->H", 0));
                    f.Add(("S6F62 (Ack)",                  "H->C", 0));
                    // UnloadReady (step 1) ───────────────────
                    f.Add(("CMD: UnloadReady",             "E->C", 1));
                    f.Add(("S6F67 (Move Request: UR)",     "C->H", 1));
                    f.Add(("S6F68 (Ack)",                  "H->C", 1));
                    // UnloadDone (step 2) ────────────────────
                    f.Add(("CMD: UnloadDone",              "E->C", 2));
                    f.Add(("S6F91 (Status Change: IDLE)",  "C->H", 2));
                    f.Add(("S6F92 (Ack)",                  "H->C", 2));
                    break;

                // ── D: Unload Glass Data (S6F63) ─────────────
                case "D":
                    f.Add(("S6F63 (Unload Glass Data)",   "C->H", 0));
                    f.Add(("S6F64 (Ack)",                 "H->C", 0));
                    break;

                // ── E: Lot End (S6F65) ────────────────────────
                case "E":
                    f.Add(("S6F65 (LOT End Data)",            "H->C", 0));
                    f.Add(("S6F66 (Ack)",                     "C->H", 0));
                    f.Add(("S6F83 (LOT Processing Data Rpt)", "C->H", 0));
                    f.Add(("S6F84 (Ack)",                     "H->C", 0));
                    break;

                // ── RECIPE: Recipe Create & Change ───────────
                case "RECIPE":
                    // RecipeCreate (step 0) ──────────────────
                    f.Add(("CMD: RecipeCreate",            "E->C", 0));
                    f.Add(("S7F61 (Recipe Confirm Req)",   "H->C", 0));
                    f.Add(("S7F62 (Ack)",                  "C->H", 0));
                    // RecipeChange (step 1) ──────────────────
                    f.Add(("CMD: RecipeChange",            "E->C", 1));
                    f.Add(("S7F69 (Recipe Data)",          "H->C", 1));
                    f.Add(("S7F70 (Ack)",                  "C->H", 1));
                    break;

                // ── B: Alarm Set & Clear ──────────────────────
                case "B":
                    // AlarmSet (step 0) ──────────────────────
                    f.Add(("CMD: AlarmSet",                "E->C", 0));
                    f.Add(("S5F1 (Alarm Report)",          "C->H", 0));
                    f.Add(("S5F2 (Ack)",                   "H->C", 0));
                    // AlarmClear (step 1) ────────────────────
                    f.Add(("CMD: AlarmClear",              "E->C", 1));
                    f.Add(("S5F1 (Alarm Clear Report)",    "C->H", 1));
                    f.Add(("S5F2 (Ack)",                   "H->C", 1));
                    break;
            }

            return f;
        }

        // ─────────────────────────────────────────────────────
        // 한 행(FlowStep) 패널 생성 (ECSSimulForm.CreateStepRow 참고)
        //
        // 열 레이아웃 (row 너비 510px):
        //   ECS 열  : 0 ~ 185px
        //   CIM 열  : 186 ~ 240px  (버블 중심 x=215)
        //   EQP 열  : 241 ~ 510px
        //
        //   H->C 화살표 : ECS → CIM  (x=35, 오른쪽 방향)
        //   C->H 화살표 : CIM → ECS  (x=35, 왼쪽 방향)
        //   C->E 화살표 : CIM → EQP  (x=240, 오른쪽 방향)
        //   E->C 화살표 : EQP → CIM  (x=240, 왼쪽 방향)
        // ─────────────────────────────────────────────────────
        private Panel CreateStepRow(FlowStep step)
        {
            Panel p = new Panel { Size = new Size(510, 45), BackColor = Color.Transparent };

            // 번호 버블 (원형)
            Label lblNum = new Label
            {
                Text      = step.StepNumber.ToString(),
                Size      = new Size(20, 20),
                Location  = new Point(215, 13),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 8, FontStyle.Bold)
            };
            step.Bubble = lblNum;
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, lblNum.Width, lblNum.Height);
            lblNum.Region = new Region(path);

            // 메시지 레이블
            Label lblMsg = new Label
            {
                Text        = step.MessageName,
                AutoSize    = true,
                Font        = new Font("Malgun Gothic", 8),
                ForeColor   = Color.FromArgb(64, 64, 64),
                BackColor   = Color.Transparent
            };

            // 화살표 레이블
            Label lblArrow = new Label
            {
                AutoSize  = true,
                Font      = new Font("Consolas", 8),
                ForeColor = Color.DarkGray,
                BackColor = Color.Transparent
            };

            switch (step.Direction)
            {
                case "H->C": // ECS → CIM
                    lblArrow.Text     = "──────────▶";
                    lblArrow.Location = new Point(35, 25);
                    lblMsg.Location   = new Point(2, 5);
                    lblMsg.MaximumSize = new Size(160, 0);
                    break;

                case "C->H": // CIM → ECS
                    lblArrow.Text     = "◀──────────";
                    lblArrow.Location = new Point(35, 25);
                    lblMsg.Location   = new Point(2, 5);
                    lblMsg.MaximumSize = new Size(160, 0);
                    break;

                case "C->E": // CIM → EQP
                    lblArrow.Text     = "──────────▶";
                    lblArrow.Location = new Point(240, 25);
                    lblMsg.Location   = new Point(360, 5);
                    lblMsg.MaximumSize = new Size(145, 0);
                    break;

                case "E->C": // EQP → CIM
                    lblArrow.Text     = "◀──────────";
                    lblArrow.Location = new Point(240, 25);
                    lblMsg.Location   = new Point(360, 5);
                    lblMsg.MaximumSize = new Size(145, 0);
                    break;
            }

            p.Controls.Add(lblNum);
            p.Controls.Add(lblMsg);
            p.Controls.Add(lblArrow);
            return p;
        }

        // ─────────────────────────────────────────────────────
        // 플로우 단계 하이라이트 (ECSSimulForm.HighlightStep 참고)
        //   scenarioStepIndex 이하의 항목은 모두 완료(LimeGreen) 처리
        // ─────────────────────────────────────────────────────
        private void HighlightFlowSteps(int scenarioStepIndex)
        {
            if (_flowPanel.InvokeRequired)
            {
                _flowPanel.Invoke(new Action(() => HighlightFlowSteps(scenarioStepIndex)));
                return;
            }

            foreach (var fs in _currentFlowSteps)
            {
                if (fs.Bubble == null) continue;

                if (fs.ScenarioStepIndex <= scenarioStepIndex)
                {
                    fs.Bubble.BackColor = Color.LimeGreen;
                    fs.Bubble.ForeColor = Color.White;
                }
            }
        }

        // ─────────────────────────────────────────────────────
        // ScenarioRunner 이벤트 핸들러
        // ─────────────────────────────────────────────────────
        private void Runner_ScenarioStarted(string name)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_ScenarioStarted(name))); return; }

            _statusLabel.Text      = "Status: Running - " + name;
            _statusLabel.BackColor = Color.Yellow;
            _statusLabel.ForeColor = Color.Black;

            // 모든 버블 초기화
            foreach (var fs in _currentFlowSteps)
            {
                if (fs.Bubble != null)
                {
                    fs.Bubble.BackColor = Color.FromArgb(200, 200, 200);
                    fs.Bubble.ForeColor = Color.White;
                }
            }
        }

        private void Runner_StepExecuting(string action, int index)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_StepExecuting(action, index))); return; }
            HighlightFlowSteps(index);
        }

        private void Runner_ScenarioCompleted(string name)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => Runner_ScenarioCompleted(name))); return; }

            _statusLabel.Text      = "Status: Completed - " + name;
            _statusLabel.BackColor = Color.LimeGreen;
            _statusLabel.ForeColor = Color.Black;

            // 모든 버블 완료 처리
            foreach (var fs in _currentFlowSteps)
            {
                if (fs.Bubble != null)
                {
                    fs.Bubble.BackColor = Color.LimeGreen;
                    fs.Bubble.ForeColor = Color.White;
                }
            }
        }
    }
}
