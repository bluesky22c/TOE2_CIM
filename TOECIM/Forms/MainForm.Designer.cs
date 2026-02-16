
namespace TOECIM
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            materialCardRCS = new MaterialSkin.Controls.MaterialCard();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            rcsButton = new MaterialSkin.Controls.MaterialButton();
            materialCardOPMode = new MaterialSkin.Controls.MaterialCard();
            materialLabelOPMode = new MaterialSkin.Controls.MaterialLabel();
            opModeButton = new MaterialSkin.Controls.MaterialButton();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            materialCardScenario = new MaterialSkin.Controls.MaterialCard();
            scenarioPrereqLabel = new Label();
            materialLabelScenario = new MaterialSkin.Controls.MaterialLabel();
            scrapReportButton = new MaterialSkin.Controls.MaterialButton();
            unloadButton = new MaterialSkin.Controls.MaterialButton();
            glassDataReqButton = new MaterialSkin.Controls.MaterialButton();
            LoadButton = new MaterialSkin.Controls.MaterialButton();
            materialCardEQPStatus = new MaterialSkin.Controls.MaterialCard();
            materialLabelEQPStatus = new MaterialSkin.Controls.MaterialLabel();
            autoRunLabel = new Label();
            glassInlabel = new Label();
            glsmvLabel = new Label();
            alarmStatusButton = new Button();
            consoleStatusLabel = new Label();
            EQStatusLabel = new Label();
            tabControl1 = new TOECIM.Controls.DarkTabControl();
            tabPage1 = new TabPage();
            splitContainer3 = new SplitContainer();
            materialCardGlassStatus = new MaterialSkin.Controls.MaterialCard();
            materialLabelGlassStatus = new MaterialSkin.Controls.MaterialLabel();
            refreshCosoleStatusButton = new MaterialSkin.Controls.MaterialButton();
            GLSStatusTextBox = new TextBox();
            label12 = new MaterialSkin.Controls.MaterialLabel();
            cstidTextBox = new TextBox();
            materialCardBGlassInfo = new MaterialSkin.Controls.MaterialCard();
            materialLabelBGlassInfo = new MaterialSkin.Controls.MaterialLabel();
            bGlassOdrTextBox = new TextBox();
            label8 = new MaterialSkin.Controls.MaterialLabel();
            bGlassCodeTextBox = new TextBox();
            bGlassIDTextBox = new TextBox();
            label4 = new MaterialSkin.Controls.MaterialLabel();
            label5 = new MaterialSkin.Controls.MaterialLabel();
            label9 = new MaterialSkin.Controls.MaterialLabel();
            materialCardAGlassInfo = new MaterialSkin.Controls.MaterialCard();
            materialLabelAGlassInfo = new MaterialSkin.Controls.MaterialLabel();
            aGlassOdrTextBox = new TextBox();
            label1 = new MaterialSkin.Controls.MaterialLabel();
            aGlassCodeTextBox = new TextBox();
            aGlassIDTextBox = new TextBox();
            label6 = new MaterialSkin.Controls.MaterialLabel();
            label7 = new MaterialSkin.Controls.MaterialLabel();
            lotidTextBox = new TextBox();
            stageIDTextBox = new TextBox();
            portidTextBox = new TextBox();
            label10 = new MaterialSkin.Controls.MaterialLabel();
            unitIDTextBox = new TextBox();
            label11 = new MaterialSkin.Controls.MaterialLabel();
            label3 = new MaterialSkin.Controls.MaterialLabel();
            label2 = new MaterialSkin.Controls.MaterialLabel();
            tabControl2 = new TOECIM.Controls.DarkTabControl();
            tabPage4 = new TabPage();
            splitContainer4 = new SplitContainer();
            aLoadPropertyGrid = new PropertyGrid();
            bLoadPropertyGrid = new PropertyGrid();
            tabPage5 = new TabPage();
            splitContainer5 = new SplitContainer();
            aUnLoadPropertyGrid = new PropertyGrid();
            bUnLoadPropertyGrid = new PropertyGrid();
            tabPage2 = new TabPage();
            allAlarmClearButton = new MaterialSkin.Controls.MaterialButton();
            RefreshButton = new MaterialSkin.Controls.MaterialButton();
            selectedAlarmClearButton = new MaterialSkin.Controls.MaterialButton();
            alarmHistoryButton = new MaterialSkin.Controls.MaterialButton();
            curAlarmButton = new MaterialSkin.Controls.MaterialButton();
            alarmDataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            _ALID = new DataGridViewTextBoxColumn();
            _ALCD = new DataGridViewTextBoxColumn();
            _AlarmType = new DataGridViewTextBoxColumn();
            _ALTX = new DataGridViewTextBoxColumn();
            _DateTime = new DataGridViewTextBoxColumn();
            _IsRelease = new DataGridViewTextBoxColumn();
            tabPage3 = new TabPage();
            materialCardGlassClearOpt = new MaterialSkin.Controls.MaterialCard();
            materialLabelGlassClearOpt = new MaterialSkin.Controls.MaterialLabel();
            glassdataresetButton = new MaterialSkin.Controls.MaterialButton();
            GlassCodeCheckBox = new CheckBox();
            materialCardGlassType = new MaterialSkin.Controls.MaterialCard();
            materialLabelGlassType = new MaterialSkin.Controls.MaterialLabel();
            Aglass = new RadioButton();
            Bglass = new RadioButton();
            GlassIDCheckBox = new CheckBox();
            glassIDTextBox = new TextBox();
            glassCodeTextBox = new TextBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            systemSetupToolStripMenuItem = new ToolStripMenuItem();
            testToolStripMenuItem = new ToolStripMenuItem();
            loopBackToolStripMenuItem = new ToolStripMenuItem();
            timeCheckToolStripMenuItem = new ToolStripMenuItem();
            gLSMVStatusToolStripMenuItem = new ToolStripMenuItem();
            eQPStatusToolStripMenuItem = new ToolStripMenuItem();
            eQPSimulatorToolStripMenuItem = new ToolStripMenuItem();
            alarmToolStripMenuItem = new ToolStripMenuItem();
            glassData초기화ToolStripMenuItem = new ToolStripMenuItem();
            eTCToolStripMenuItem = new ToolStripMenuItem();
            messageViewToolStripMenuItem = new ToolStripMenuItem();
            recipeViewToolStripMenuItem = new ToolStripMenuItem();
            cassetteMAPRequestToolStripMenuItem = new ToolStripMenuItem();
            reworkToolStripMenuItem = new ToolStripMenuItem();
            ionizerToolStripMenuItem = new ToolStripMenuItem();
            configToolStripMenuItem = new ToolStripMenuItem();
            sW변경이력ToolStripMenuItem = new ToolStripMenuItem();
            logViewer1 = new Elp.LogViewer(components);
            materialCardRCS.SuspendLayout();
            materialCardOPMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            materialCardScenario.SuspendLayout();
            materialCardEQPStatus.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            materialCardGlassStatus.SuspendLayout();
            materialCardBGlassInfo.SuspendLayout();
            materialCardAGlassInfo.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)alarmDataGridView).BeginInit();
            tabPage3.SuspendLayout();
            materialCardGlassClearOpt.SuspendLayout();
            materialCardGlassType.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // materialCardRCS
            // 
            materialCardRCS.BackColor = Color.FromArgb(255, 255, 255);
            materialCardRCS.Controls.Add(materialLabel1);
            materialCardRCS.Depth = 0;
            materialCardRCS.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardRCS.Location = new Point(3, 2);
            materialCardRCS.Margin = new Padding(14);
            materialCardRCS.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardRCS.Name = "materialCardRCS";
            materialCardRCS.Padding = new Padding(14);
            materialCardRCS.Size = new Size(120, 92);
            materialCardRCS.TabIndex = 0;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(10, 5);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(31, 19);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "RCS";
            // 
            // rcsButton
            // 
            rcsButton.AutoSize = false;
            rcsButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rcsButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            rcsButton.Depth = 0;
            rcsButton.HighEmphasis = false;
            rcsButton.Icon = null;
            rcsButton.Location = new Point(4, 27);
            rcsButton.Margin = new Padding(0);
            rcsButton.MouseState = MaterialSkin.MouseState.HOVER;
            rcsButton.Name = "rcsButton";
            rcsButton.NoAccentTextColor = Color.Empty;
            rcsButton.Size = new Size(115, 38);
            rcsButton.TabIndex = 0;
            rcsButton.Text = "Offline";
            rcsButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            rcsButton.UseAccentColor = false;
            rcsButton.UseVisualStyleBackColor = true;
            rcsButton.Click += rcsButton_Click;
            // 
            // materialCardOPMode
            // 
            materialCardOPMode.BackColor = Color.FromArgb(255, 255, 255);
            materialCardOPMode.Controls.Add(materialLabelOPMode);
            materialCardOPMode.Controls.Add(opModeButton);
            materialCardOPMode.Depth = 0;
            materialCardOPMode.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardOPMode.Location = new Point(605, 2);
            materialCardOPMode.Margin = new Padding(14);
            materialCardOPMode.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardOPMode.Name = "materialCardOPMode";
            materialCardOPMode.Padding = new Padding(14);
            materialCardOPMode.Size = new Size(132, 95);
            materialCardOPMode.TabIndex = 1;
            // 
            // materialLabelOPMode
            // 
            materialLabelOPMode.AutoSize = true;
            materialLabelOPMode.Depth = 0;
            materialLabelOPMode.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelOPMode.Location = new Point(10, 5);
            materialLabelOPMode.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelOPMode.Name = "materialLabelOPMode";
            materialLabelOPMode.Size = new Size(62, 19);
            materialLabelOPMode.TabIndex = 1;
            materialLabelOPMode.Text = "OPMode";
            // 
            // opModeButton
            // 
            opModeButton.AutoSize = false;
            opModeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            opModeButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            opModeButton.Depth = 0;
            opModeButton.HighEmphasis = false;
            opModeButton.Icon = null;
            opModeButton.Location = new Point(7, 29);
            opModeButton.Margin = new Padding(0);
            opModeButton.MouseState = MaterialSkin.MouseState.HOVER;
            opModeButton.Name = "opModeButton";
            opModeButton.NoAccentTextColor = Color.White;
            opModeButton.Size = new Size(117, 52);
            opModeButton.TabIndex = 0;
            opModeButton.Text = "Normal";
            opModeButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            opModeButton.UseAccentColor = false;
            opModeButton.UseVisualStyleBackColor = true;
            opModeButton.Click += opModeButton_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 64);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer1.Panel2.Controls.Add(logViewer1);
            splitContainer1.Size = new Size(1323, 898);
            splitContainer1.SplitterDistance = 747;
            splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            splitContainer2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 24);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer2.Panel1.Controls.Add(materialCardScenario);
            splitContainer2.Panel1.Controls.Add(materialCardOPMode);
            splitContainer2.Panel1.Controls.Add(materialCardEQPStatus);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer2.Panel2.Controls.Add(tabControl1);
            splitContainer2.Size = new Size(1323, 723);
            splitContainer2.SplitterDistance = 96;
            splitContainer2.SplitterWidth = 2;
            splitContainer2.TabIndex = 7;
            // 
            // materialCardScenario
            // 
            materialCardScenario.BackColor = Color.FromArgb(255, 255, 255);
            materialCardScenario.Controls.Add(scenarioPrereqLabel);
            materialCardScenario.Controls.Add(materialLabelScenario);
            materialCardScenario.Controls.Add(scrapReportButton);
            materialCardScenario.Controls.Add(unloadButton);
            materialCardScenario.Controls.Add(glassDataReqButton);
            materialCardScenario.Controls.Add(LoadButton);
            materialCardScenario.Controls.Add(rcsButton);
            materialCardScenario.Depth = 0;
            materialCardScenario.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardScenario.Location = new Point(3, 2);
            materialCardScenario.Margin = new Padding(14);
            materialCardScenario.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardScenario.Name = "materialCardScenario";
            materialCardScenario.Padding = new Padding(14);
            materialCardScenario.Size = new Size(600, 115);
            materialCardScenario.TabIndex = 0;
            // 
            // scenarioPrereqLabel
            // 
            scenarioPrereqLabel.AutoSize = true;
            scenarioPrereqLabel.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            scenarioPrereqLabel.ForeColor = Color.IndianRed;
            scenarioPrereqLabel.Location = new Point(10, 70);
            scenarioPrereqLabel.Name = "scenarioPrereqLabel";
            scenarioPrereqLabel.Size = new Size(368, 17);
            scenarioPrereqLabel.TabIndex = 6;
            scenarioPrereqLabel.Text = "※ 시나리오 시작 전 필수 조건을 시뮬레이터에서 설정하세요.";
            // 
            // materialLabelScenario
            // 
            materialLabelScenario.AutoSize = true;
            materialLabelScenario.Depth = 0;
            materialLabelScenario.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelScenario.Location = new Point(10, 5);
            materialLabelScenario.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelScenario.Name = "materialLabelScenario";
            materialLabelScenario.Size = new Size(136, 19);
            materialLabelScenario.TabIndex = 5;
            materialLabelScenario.Text = "Scenario Sequence";
            // 
            // scrapReportButton
            // 
            scrapReportButton.AutoSize = false;
            scrapReportButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrapReportButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            scrapReportButton.Depth = 0;
            scrapReportButton.HighEmphasis = false;
            scrapReportButton.Icon = null;
            scrapReportButton.Location = new Point(480, 27);
            scrapReportButton.Margin = new Padding(0);
            scrapReportButton.MouseState = MaterialSkin.MouseState.HOVER;
            scrapReportButton.Name = "scrapReportButton";
            scrapReportButton.NoAccentTextColor = Color.Empty;
            scrapReportButton.Size = new Size(115, 38);
            scrapReportButton.TabIndex = 4;
            scrapReportButton.Text = "Scrap Report";
            scrapReportButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            scrapReportButton.UseAccentColor = false;
            scrapReportButton.UseVisualStyleBackColor = true;
            scrapReportButton.Click += scrapReportButton_Click;
            // 
            // unloadButton
            // 
            unloadButton.AutoSize = false;
            unloadButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            unloadButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            unloadButton.Depth = 0;
            unloadButton.HighEmphasis = false;
            unloadButton.Icon = null;
            unloadButton.Location = new Point(361, 27);
            unloadButton.Margin = new Padding(0);
            unloadButton.MouseState = MaterialSkin.MouseState.HOVER;
            unloadButton.Name = "unloadButton";
            unloadButton.NoAccentTextColor = Color.Empty;
            unloadButton.Size = new Size(115, 38);
            unloadButton.TabIndex = 3;
            unloadButton.Text = "Unload Done";
            unloadButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            unloadButton.UseAccentColor = false;
            unloadButton.UseVisualStyleBackColor = true;
            unloadButton.Click += unloadButton_Click;
            // 
            // glassDataReqButton
            // 
            glassDataReqButton.AutoSize = false;
            glassDataReqButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            glassDataReqButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            glassDataReqButton.Depth = 0;
            glassDataReqButton.HighEmphasis = false;
            glassDataReqButton.Icon = null;
            glassDataReqButton.Location = new Point(242, 27);
            glassDataReqButton.Margin = new Padding(0);
            glassDataReqButton.MouseState = MaterialSkin.MouseState.HOVER;
            glassDataReqButton.Name = "glassDataReqButton";
            glassDataReqButton.NoAccentTextColor = Color.Empty;
            glassDataReqButton.Size = new Size(115, 38);
            glassDataReqButton.TabIndex = 2;
            glassDataReqButton.Text = "Data Request";
            glassDataReqButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            glassDataReqButton.UseAccentColor = false;
            glassDataReqButton.UseVisualStyleBackColor = true;
            glassDataReqButton.Click += glassDataReqButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.AutoSize = false;
            LoadButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            LoadButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            LoadButton.Depth = 0;
            LoadButton.HighEmphasis = false;
            LoadButton.Icon = null;
            LoadButton.Location = new Point(123, 27);
            LoadButton.Margin = new Padding(0);
            LoadButton.MouseState = MaterialSkin.MouseState.HOVER;
            LoadButton.Name = "LoadButton";
            LoadButton.NoAccentTextColor = Color.Empty;
            LoadButton.Size = new Size(115, 38);
            LoadButton.TabIndex = 1;
            LoadButton.Text = "Load Done";
            LoadButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            LoadButton.UseAccentColor = false;
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // materialCardEQPStatus
            // 
            materialCardEQPStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialCardEQPStatus.BackColor = Color.FromArgb(255, 255, 255);
            materialCardEQPStatus.Controls.Add(materialLabelEQPStatus);
            materialCardEQPStatus.Controls.Add(autoRunLabel);
            materialCardEQPStatus.Controls.Add(glassInlabel);
            materialCardEQPStatus.Controls.Add(glsmvLabel);
            materialCardEQPStatus.Controls.Add(alarmStatusButton);
            materialCardEQPStatus.Controls.Add(consoleStatusLabel);
            materialCardEQPStatus.Controls.Add(EQStatusLabel);
            materialCardEQPStatus.Depth = 0;
            materialCardEQPStatus.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardEQPStatus.Location = new Point(740, 2);
            materialCardEQPStatus.Margin = new Padding(14);
            materialCardEQPStatus.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardEQPStatus.Name = "materialCardEQPStatus";
            materialCardEQPStatus.Padding = new Padding(14);
            materialCardEQPStatus.Size = new Size(583, 95);
            materialCardEQPStatus.TabIndex = 2;
            // 
            // materialLabelEQPStatus
            // 
            materialLabelEQPStatus.AutoSize = true;
            materialLabelEQPStatus.Depth = 0;
            materialLabelEQPStatus.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelEQPStatus.Location = new Point(10, 5);
            materialLabelEQPStatus.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelEQPStatus.Name = "materialLabelEQPStatus";
            materialLabelEQPStatus.Size = new Size(77, 19);
            materialLabelEQPStatus.TabIndex = 11;
            materialLabelEQPStatus.Text = "EQPStatus";
            // 
            // autoRunLabel
            // 
            autoRunLabel.BorderStyle = BorderStyle.FixedSingle;
            autoRunLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            autoRunLabel.Location = new Point(315, 27);
            autoRunLabel.Name = "autoRunLabel";
            autoRunLabel.Size = new Size(79, 56);
            autoRunLabel.TabIndex = 10;
            autoRunLabel.Text = "AutoRun OFF";
            autoRunLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // glassInlabel
            // 
            glassInlabel.BorderStyle = BorderStyle.FixedSingle;
            glassInlabel.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            glassInlabel.Location = new Point(400, 27);
            glassInlabel.Name = "glassInlabel";
            glassInlabel.Size = new Size(86, 56);
            glassInlabel.TabIndex = 9;
            glassInlabel.Text = "Glass IN";
            glassInlabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // glsmvLabel
            // 
            glsmvLabel.BorderStyle = BorderStyle.FixedSingle;
            glsmvLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            glsmvLabel.Location = new Point(492, 27);
            glsmvLabel.Name = "glsmvLabel";
            glsmvLabel.Size = new Size(80, 56);
            glsmvLabel.TabIndex = 8;
            glsmvLabel.Text = "LW";
            glsmvLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // alarmStatusButton
            // 
            alarmStatusButton.FlatStyle = FlatStyle.Flat;
            alarmStatusButton.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            alarmStatusButton.Location = new Point(15, 27);
            alarmStatusButton.Name = "alarmStatusButton";
            alarmStatusButton.Size = new Size(101, 56);
            alarmStatusButton.TabIndex = 1;
            alarmStatusButton.Text = "Alarm";
            alarmStatusButton.UseVisualStyleBackColor = true;
            alarmStatusButton.Click += alarmStatusLabel_Click;
            // 
            // consoleStatusLabel
            // 
            consoleStatusLabel.BorderStyle = BorderStyle.FixedSingle;
            consoleStatusLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            consoleStatusLabel.ForeColor = Color.Green;
            consoleStatusLabel.Location = new Point(123, 27);
            consoleStatusLabel.Name = "consoleStatusLabel";
            consoleStatusLabel.Size = new Size(94, 56);
            consoleStatusLabel.TabIndex = 1;
            consoleStatusLabel.Text = "CONSOLE";
            consoleStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EQStatusLabel
            // 
            EQStatusLabel.BorderStyle = BorderStyle.FixedSingle;
            EQStatusLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            EQStatusLabel.Location = new Point(223, 27);
            EQStatusLabel.Name = "EQStatusLabel";
            EQStatusLabel.Size = new Size(86, 56);
            EQStatusLabel.TabIndex = 0;
            EQStatusLabel.Text = "DOWN";
            EQStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.DarkBackColor = Color.FromArgb(25, 38, 52);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.HighlightColor = Color.FromArgb(129, 212, 250);
            tabControl1.ItemSize = new Size(100, 35);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1323, 625);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer3);
            tabPage1.Location = new Point(4, 39);
            tabPage1.Margin = new Padding(3, 4, 3, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 4, 3, 4);
            tabPage1.Size = new Size(1315, 582);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Status";
            // 
            // splitContainer3
            // 
            splitContainer3.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 4);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer3.Panel1.Controls.Add(materialCardGlassStatus);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer3.Panel2.Controls.Add(tabControl2);
            splitContainer3.Size = new Size(1309, 574);
            splitContainer3.SplitterDistance = 434;
            splitContainer3.SplitterWidth = 2;
            splitContainer3.TabIndex = 3;
            // 
            // materialCardGlassStatus
            // 
            materialCardGlassStatus.BackColor = Color.FromArgb(255, 255, 255);
            materialCardGlassStatus.Controls.Add(materialLabelGlassStatus);
            materialCardGlassStatus.Controls.Add(refreshCosoleStatusButton);
            materialCardGlassStatus.Controls.Add(GLSStatusTextBox);
            materialCardGlassStatus.Controls.Add(label12);
            materialCardGlassStatus.Controls.Add(cstidTextBox);
            materialCardGlassStatus.Controls.Add(materialCardBGlassInfo);
            materialCardGlassStatus.Controls.Add(label9);
            materialCardGlassStatus.Controls.Add(materialCardAGlassInfo);
            materialCardGlassStatus.Controls.Add(lotidTextBox);
            materialCardGlassStatus.Controls.Add(stageIDTextBox);
            materialCardGlassStatus.Controls.Add(portidTextBox);
            materialCardGlassStatus.Controls.Add(label10);
            materialCardGlassStatus.Controls.Add(unitIDTextBox);
            materialCardGlassStatus.Controls.Add(label11);
            materialCardGlassStatus.Controls.Add(label3);
            materialCardGlassStatus.Controls.Add(label2);
            materialCardGlassStatus.Depth = 0;
            materialCardGlassStatus.Dock = DockStyle.Fill;
            materialCardGlassStatus.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardGlassStatus.Location = new Point(0, 0);
            materialCardGlassStatus.Margin = new Padding(14);
            materialCardGlassStatus.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardGlassStatus.Name = "materialCardGlassStatus";
            materialCardGlassStatus.Padding = new Padding(14);
            materialCardGlassStatus.Size = new Size(434, 574);
            materialCardGlassStatus.TabIndex = 3;
            // 
            // materialLabelGlassStatus
            // 
            materialLabelGlassStatus.AutoSize = true;
            materialLabelGlassStatus.Depth = 0;
            materialLabelGlassStatus.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelGlassStatus.Location = new Point(10, 15);
            materialLabelGlassStatus.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelGlassStatus.Name = "materialLabelGlassStatus";
            materialLabelGlassStatus.Size = new Size(142, 19);
            materialLabelGlassStatus.TabIndex = 21;
            materialLabelGlassStatus.Text = "Current Load Status";
            // 
            // refreshCosoleStatusButton
            // 
            refreshCosoleStatusButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            refreshCosoleStatusButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            refreshCosoleStatusButton.Depth = 0;
            refreshCosoleStatusButton.HighEmphasis = true;
            refreshCosoleStatusButton.Icon = null;
            refreshCosoleStatusButton.Location = new Point(234, 495);
            refreshCosoleStatusButton.Margin = new Padding(3, 4, 3, 4);
            refreshCosoleStatusButton.MouseState = MaterialSkin.MouseState.HOVER;
            refreshCosoleStatusButton.Name = "refreshCosoleStatusButton";
            refreshCosoleStatusButton.NoAccentTextColor = Color.Empty;
            refreshCosoleStatusButton.Size = new Size(84, 36);
            refreshCosoleStatusButton.TabIndex = 20;
            refreshCosoleStatusButton.Text = "Refresh";
            refreshCosoleStatusButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            refreshCosoleStatusButton.UseAccentColor = false;
            refreshCosoleStatusButton.UseVisualStyleBackColor = false;
            refreshCosoleStatusButton.Click += refreshCosoleStatusButton_Click_1;
            // 
            // GLSStatusTextBox
            // 
            GLSStatusTextBox.BorderStyle = BorderStyle.FixedSingle;
            GLSStatusTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            GLSStatusTextBox.Location = new Point(173, 195);
            GLSStatusTextBox.Name = "GLSStatusTextBox";
            GLSStatusTextBox.ReadOnly = true;
            GLSStatusTextBox.Size = new Size(136, 23);
            GLSStatusTextBox.TabIndex = 19;
            // 
            // label12
            // 
            label12.Depth = 0;
            label12.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label12.Location = new Point(52, 197);
            label12.MouseState = MaterialSkin.MouseState.HOVER;
            label12.Name = "label12";
            label12.Size = new Size(115, 23);
            label12.TabIndex = 18;
            label12.Text = "GLSSTATUS :";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cstidTextBox
            // 
            cstidTextBox.BorderStyle = BorderStyle.FixedSingle;
            cstidTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            cstidTextBox.Location = new Point(173, 165);
            cstidTextBox.Name = "cstidTextBox";
            cstidTextBox.ReadOnly = true;
            cstidTextBox.Size = new Size(136, 23);
            cstidTextBox.TabIndex = 17;
            // 
            // materialCardBGlassInfo
            // 
            materialCardBGlassInfo.BackColor = Color.FromArgb(255, 255, 255);
            materialCardBGlassInfo.Controls.Add(materialLabelBGlassInfo);
            materialCardBGlassInfo.Controls.Add(bGlassOdrTextBox);
            materialCardBGlassInfo.Controls.Add(label8);
            materialCardBGlassInfo.Controls.Add(bGlassCodeTextBox);
            materialCardBGlassInfo.Controls.Add(bGlassIDTextBox);
            materialCardBGlassInfo.Controls.Add(label4);
            materialCardBGlassInfo.Controls.Add(label5);
            materialCardBGlassInfo.Depth = 0;
            materialCardBGlassInfo.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardBGlassInfo.Location = new Point(28, 358);
            materialCardBGlassInfo.Margin = new Padding(14);
            materialCardBGlassInfo.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardBGlassInfo.Name = "materialCardBGlassInfo";
            materialCardBGlassInfo.Padding = new Padding(14);
            materialCardBGlassInfo.Size = new Size(289, 115);
            materialCardBGlassInfo.TabIndex = 11;
            // 
            // materialLabelBGlassInfo
            // 
            materialLabelBGlassInfo.AutoSize = true;
            materialLabelBGlassInfo.Depth = 0;
            materialLabelBGlassInfo.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelBGlassInfo.Location = new Point(10, 5);
            materialLabelBGlassInfo.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelBGlassInfo.Name = "materialLabelBGlassInfo";
            materialLabelBGlassInfo.Size = new Size(55, 19);
            materialLabelBGlassInfo.TabIndex = 12;
            materialLabelBGlassInfo.Text = "B Glass";
            // 
            // bGlassOdrTextBox
            // 
            bGlassOdrTextBox.BorderStyle = BorderStyle.FixedSingle;
            bGlassOdrTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            bGlassOdrTextBox.Location = new Point(145, 85);
            bGlassOdrTextBox.Name = "bGlassOdrTextBox";
            bGlassOdrTextBox.ReadOnly = true;
            bGlassOdrTextBox.Size = new Size(122, 23);
            bGlassOdrTextBox.TabIndex = 11;
            // 
            // label8
            // 
            label8.Depth = 0;
            label8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label8.Location = new Point(6, 88);
            label8.MouseState = MaterialSkin.MouseState.HOVER;
            label8.Name = "label8";
            label8.Size = new Size(142, 23);
            label8.TabIndex = 10;
            label8.Text = "GLASSODR :";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // bGlassCodeTextBox
            // 
            bGlassCodeTextBox.BorderStyle = BorderStyle.FixedSingle;
            bGlassCodeTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            bGlassCodeTextBox.Location = new Point(145, 55);
            bGlassCodeTextBox.Name = "bGlassCodeTextBox";
            bGlassCodeTextBox.ReadOnly = true;
            bGlassCodeTextBox.Size = new Size(122, 23);
            bGlassCodeTextBox.TabIndex = 9;
            // 
            // bGlassIDTextBox
            // 
            bGlassIDTextBox.BorderStyle = BorderStyle.FixedSingle;
            bGlassIDTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            bGlassIDTextBox.Location = new Point(145, 25);
            bGlassIDTextBox.Name = "bGlassIDTextBox";
            bGlassIDTextBox.ReadOnly = true;
            bGlassIDTextBox.Size = new Size(122, 23);
            bGlassIDTextBox.TabIndex = 8;
            // 
            // label4
            // 
            label4.Depth = 0;
            label4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label4.Location = new Point(6, 59);
            label4.MouseState = MaterialSkin.MouseState.HOVER;
            label4.Name = "label4";
            label4.Size = new Size(142, 23);
            label4.TabIndex = 3;
            label4.Text = "GLASSCODE :";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Depth = 0;
            label5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label5.Location = new Point(6, 29);
            label5.MouseState = MaterialSkin.MouseState.HOVER;
            label5.Name = "label5";
            label5.Size = new Size(142, 23);
            label5.TabIndex = 2;
            label5.Text = "GLASSID :";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.Depth = 0;
            label9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label9.Location = new Point(52, 167);
            label9.MouseState = MaterialSkin.MouseState.HOVER;
            label9.Name = "label9";
            label9.Size = new Size(115, 23);
            label9.TabIndex = 16;
            label9.Text = "CSTID :";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialCardAGlassInfo
            // 
            materialCardAGlassInfo.BackColor = Color.FromArgb(255, 255, 255);
            materialCardAGlassInfo.Controls.Add(materialLabelAGlassInfo);
            materialCardAGlassInfo.Controls.Add(aGlassOdrTextBox);
            materialCardAGlassInfo.Controls.Add(label1);
            materialCardAGlassInfo.Controls.Add(aGlassCodeTextBox);
            materialCardAGlassInfo.Controls.Add(aGlassIDTextBox);
            materialCardAGlassInfo.Controls.Add(label6);
            materialCardAGlassInfo.Controls.Add(label7);
            materialCardAGlassInfo.Depth = 0;
            materialCardAGlassInfo.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardAGlassInfo.Location = new Point(28, 238);
            materialCardAGlassInfo.Margin = new Padding(14);
            materialCardAGlassInfo.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardAGlassInfo.Name = "materialCardAGlassInfo";
            materialCardAGlassInfo.Padding = new Padding(14);
            materialCardAGlassInfo.Size = new Size(289, 115);
            materialCardAGlassInfo.TabIndex = 10;
            // 
            // materialLabelAGlassInfo
            // 
            materialLabelAGlassInfo.AutoSize = true;
            materialLabelAGlassInfo.Depth = 0;
            materialLabelAGlassInfo.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelAGlassInfo.Location = new Point(10, 5);
            materialLabelAGlassInfo.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelAGlassInfo.Name = "materialLabelAGlassInfo";
            materialLabelAGlassInfo.Size = new Size(55, 19);
            materialLabelAGlassInfo.TabIndex = 12;
            materialLabelAGlassInfo.Text = "A Glass";
            // 
            // aGlassOdrTextBox
            // 
            aGlassOdrTextBox.BorderStyle = BorderStyle.FixedSingle;
            aGlassOdrTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            aGlassOdrTextBox.Location = new Point(145, 85);
            aGlassOdrTextBox.Name = "aGlassOdrTextBox";
            aGlassOdrTextBox.ReadOnly = true;
            aGlassOdrTextBox.Size = new Size(122, 23);
            aGlassOdrTextBox.TabIndex = 11;
            // 
            // label1
            // 
            label1.Depth = 0;
            label1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.Location = new Point(6, 88);
            label1.MouseState = MaterialSkin.MouseState.HOVER;
            label1.Name = "label1";
            label1.Size = new Size(142, 23);
            label1.TabIndex = 10;
            label1.Text = "GLASSODR :";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // aGlassCodeTextBox
            // 
            aGlassCodeTextBox.BorderStyle = BorderStyle.FixedSingle;
            aGlassCodeTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            aGlassCodeTextBox.Location = new Point(145, 55);
            aGlassCodeTextBox.Name = "aGlassCodeTextBox";
            aGlassCodeTextBox.ReadOnly = true;
            aGlassCodeTextBox.Size = new Size(122, 23);
            aGlassCodeTextBox.TabIndex = 9;
            // 
            // aGlassIDTextBox
            // 
            aGlassIDTextBox.BorderStyle = BorderStyle.FixedSingle;
            aGlassIDTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            aGlassIDTextBox.Location = new Point(145, 25);
            aGlassIDTextBox.Name = "aGlassIDTextBox";
            aGlassIDTextBox.ReadOnly = true;
            aGlassIDTextBox.Size = new Size(122, 23);
            aGlassIDTextBox.TabIndex = 8;
            // 
            // label6
            // 
            label6.Depth = 0;
            label6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label6.Location = new Point(6, 59);
            label6.MouseState = MaterialSkin.MouseState.HOVER;
            label6.Name = "label6";
            label6.Size = new Size(142, 23);
            label6.TabIndex = 3;
            label6.Text = "GLASSCODE :";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Depth = 0;
            label7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label7.Location = new Point(6, 29);
            label7.MouseState = MaterialSkin.MouseState.HOVER;
            label7.Name = "label7";
            label7.Size = new Size(142, 23);
            label7.TabIndex = 2;
            label7.Text = "GLASSID :";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lotidTextBox
            // 
            lotidTextBox.BorderStyle = BorderStyle.FixedSingle;
            lotidTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lotidTextBox.Location = new Point(173, 136);
            lotidTextBox.Name = "lotidTextBox";
            lotidTextBox.ReadOnly = true;
            lotidTextBox.Size = new Size(136, 23);
            lotidTextBox.TabIndex = 15;
            // 
            // stageIDTextBox
            // 
            stageIDTextBox.BorderStyle = BorderStyle.FixedSingle;
            stageIDTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            stageIDTextBox.Location = new Point(173, 77);
            stageIDTextBox.Name = "stageIDTextBox";
            stageIDTextBox.ReadOnly = true;
            stageIDTextBox.Size = new Size(136, 23);
            stageIDTextBox.TabIndex = 7;
            // 
            // portidTextBox
            // 
            portidTextBox.BorderStyle = BorderStyle.FixedSingle;
            portidTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            portidTextBox.Location = new Point(173, 106);
            portidTextBox.Name = "portidTextBox";
            portidTextBox.ReadOnly = true;
            portidTextBox.Size = new Size(136, 23);
            portidTextBox.TabIndex = 14;
            // 
            // label10
            // 
            label10.Depth = 0;
            label10.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label10.Location = new Point(52, 138);
            label10.MouseState = MaterialSkin.MouseState.HOVER;
            label10.Name = "label10";
            label10.Size = new Size(115, 23);
            label10.TabIndex = 13;
            label10.Text = "LOTID :";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // unitIDTextBox
            // 
            unitIDTextBox.BorderStyle = BorderStyle.FixedSingle;
            unitIDTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            unitIDTextBox.Location = new Point(173, 48);
            unitIDTextBox.Name = "unitIDTextBox";
            unitIDTextBox.ReadOnly = true;
            unitIDTextBox.Size = new Size(136, 23);
            unitIDTextBox.TabIndex = 6;
            // 
            // label11
            // 
            label11.Depth = 0;
            label11.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label11.Location = new Point(52, 108);
            label11.MouseState = MaterialSkin.MouseState.HOVER;
            label11.Name = "label11";
            label11.Size = new Size(115, 23);
            label11.TabIndex = 12;
            label11.Text = "PORTID :";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Depth = 0;
            label3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.Location = new Point(52, 79);
            label3.MouseState = MaterialSkin.MouseState.HOVER;
            label3.Name = "label3";
            label3.Size = new Size(115, 23);
            label3.TabIndex = 1;
            label3.Text = "STAGEID :";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Depth = 0;
            label2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label2.Location = new Point(52, 50);
            label2.MouseState = MaterialSkin.MouseState.HOVER;
            label2.Name = "label2";
            label2.Size = new Size(115, 23);
            label2.TabIndex = 22;
            label2.Text = "UnitID :";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Controls.Add(tabPage5);
            tabControl2.DarkBackColor = Color.FromArgb(25, 38, 52);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl2.HighlightColor = Color.FromArgb(129, 212, 250);
            tabControl2.ItemSize = new Size(100, 30);
            tabControl2.Location = new Point(0, 0);
            tabControl2.Margin = new Padding(3, 4, 3, 4);
            tabControl2.Multiline = true;
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(873, 574);
            tabControl2.SizeMode = TabSizeMode.Fixed;
            tabControl2.TabIndex = 2;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(splitContainer4);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Margin = new Padding(3, 4, 3, 4);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3, 4, 3, 4);
            tabPage4.Size = new Size(865, 536);
            tabPage4.TabIndex = 0;
            tabPage4.Text = "LoadGlass";
            // 
            // splitContainer4
            // 
            splitContainer4.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(3, 4);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer4.Panel1.Controls.Add(aLoadPropertyGrid);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer4.Panel2.Controls.Add(bLoadPropertyGrid);
            splitContainer4.Size = new Size(859, 528);
            splitContainer4.SplitterDistance = 429;
            splitContainer4.SplitterWidth = 2;
            splitContainer4.TabIndex = 9;
            // 
            // aLoadPropertyGrid
            // 
            aLoadPropertyGrid.Dock = DockStyle.Fill;
            aLoadPropertyGrid.Location = new Point(0, 0);
            aLoadPropertyGrid.Margin = new Padding(3, 4, 3, 4);
            aLoadPropertyGrid.Name = "aLoadPropertyGrid";
            aLoadPropertyGrid.Size = new Size(429, 528);
            aLoadPropertyGrid.TabIndex = 9;
            // 
            // bLoadPropertyGrid
            // 
            bLoadPropertyGrid.Dock = DockStyle.Fill;
            bLoadPropertyGrid.Location = new Point(0, 0);
            bLoadPropertyGrid.Margin = new Padding(3, 4, 3, 4);
            bLoadPropertyGrid.Name = "bLoadPropertyGrid";
            bLoadPropertyGrid.Size = new Size(428, 528);
            bLoadPropertyGrid.TabIndex = 11;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(splitContainer5);
            tabPage5.Location = new Point(4, 34);
            tabPage5.Margin = new Padding(3, 4, 3, 4);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3, 4, 3, 4);
            tabPage5.Size = new Size(865, 536);
            tabPage5.TabIndex = 1;
            tabPage5.Text = "UnloadGlass";
            // 
            // splitContainer5
            // 
            splitContainer5.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.Location = new Point(3, 4);
            splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer5.Panel1.Controls.Add(aUnLoadPropertyGrid);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.BackColor = Color.FromArgb(25, 38, 52);
            splitContainer5.Panel2.Controls.Add(bUnLoadPropertyGrid);
            splitContainer5.Size = new Size(859, 528);
            splitContainer5.SplitterDistance = 429;
            splitContainer5.SplitterWidth = 2;
            splitContainer5.TabIndex = 4;
            // 
            // aUnLoadPropertyGrid
            // 
            aUnLoadPropertyGrid.Dock = DockStyle.Fill;
            aUnLoadPropertyGrid.Location = new Point(0, 0);
            aUnLoadPropertyGrid.Margin = new Padding(3, 4, 3, 4);
            aUnLoadPropertyGrid.Name = "aUnLoadPropertyGrid";
            aUnLoadPropertyGrid.Size = new Size(429, 528);
            aUnLoadPropertyGrid.TabIndex = 4;
            // 
            // bUnLoadPropertyGrid
            // 
            bUnLoadPropertyGrid.Dock = DockStyle.Fill;
            bUnLoadPropertyGrid.Location = new Point(0, 0);
            bUnLoadPropertyGrid.Margin = new Padding(3, 4, 3, 4);
            bUnLoadPropertyGrid.Name = "bUnLoadPropertyGrid";
            bUnLoadPropertyGrid.Size = new Size(428, 528);
            bUnLoadPropertyGrid.TabIndex = 6;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(allAlarmClearButton);
            tabPage2.Controls.Add(RefreshButton);
            tabPage2.Controls.Add(selectedAlarmClearButton);
            tabPage2.Controls.Add(alarmHistoryButton);
            tabPage2.Controls.Add(curAlarmButton);
            tabPage2.Controls.Add(alarmDataGridView);
            tabPage2.Location = new Point(4, 39);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new Size(1315, 582);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Alarm";
            // 
            // allAlarmClearButton
            // 
            allAlarmClearButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            allAlarmClearButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            allAlarmClearButton.Depth = 0;
            allAlarmClearButton.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            allAlarmClearButton.HighEmphasis = true;
            allAlarmClearButton.Icon = null;
            allAlarmClearButton.Location = new Point(651, 134);
            allAlarmClearButton.Margin = new Padding(3, 4, 3, 4);
            allAlarmClearButton.MouseState = MaterialSkin.MouseState.HOVER;
            allAlarmClearButton.Name = "allAlarmClearButton";
            allAlarmClearButton.NoAccentTextColor = Color.Empty;
            allAlarmClearButton.Size = new Size(149, 36);
            allAlarmClearButton.TabIndex = 5;
            allAlarmClearButton.Text = "All Alarm Clear";
            allAlarmClearButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            allAlarmClearButton.UseAccentColor = false;
            allAlarmClearButton.UseVisualStyleBackColor = true;
            allAlarmClearButton.Click += allAlarmClearButton_Click;
            // 
            // RefreshButton
            // 
            RefreshButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            RefreshButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            RefreshButton.Depth = 0;
            RefreshButton.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            RefreshButton.HighEmphasis = true;
            RefreshButton.Icon = null;
            RefreshButton.Location = new Point(632, 538);
            RefreshButton.Margin = new Padding(3, 4, 3, 4);
            RefreshButton.MouseState = MaterialSkin.MouseState.HOVER;
            RefreshButton.Name = "RefreshButton";
            RefreshButton.NoAccentTextColor = Color.Empty;
            RefreshButton.Size = new Size(84, 36);
            RefreshButton.TabIndex = 4;
            RefreshButton.Text = "Refresh";
            RefreshButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            RefreshButton.UseAccentColor = false;
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // selectedAlarmClearButton
            // 
            selectedAlarmClearButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            selectedAlarmClearButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            selectedAlarmClearButton.Depth = 0;
            selectedAlarmClearButton.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            selectedAlarmClearButton.HighEmphasis = true;
            selectedAlarmClearButton.Icon = null;
            selectedAlarmClearButton.Location = new Point(651, 90);
            selectedAlarmClearButton.Margin = new Padding(3, 4, 3, 4);
            selectedAlarmClearButton.MouseState = MaterialSkin.MouseState.HOVER;
            selectedAlarmClearButton.Name = "selectedAlarmClearButton";
            selectedAlarmClearButton.NoAccentTextColor = Color.Empty;
            selectedAlarmClearButton.Size = new Size(194, 36);
            selectedAlarmClearButton.TabIndex = 3;
            selectedAlarmClearButton.Text = "Selected Alarm Clear";
            selectedAlarmClearButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            selectedAlarmClearButton.UseAccentColor = false;
            selectedAlarmClearButton.UseVisualStyleBackColor = true;
            selectedAlarmClearButton.Click += alarmClearButton_Click;
            // 
            // alarmHistoryButton
            // 
            alarmHistoryButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            alarmHistoryButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            alarmHistoryButton.Depth = 0;
            alarmHistoryButton.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            alarmHistoryButton.HighEmphasis = true;
            alarmHistoryButton.Icon = null;
            alarmHistoryButton.Location = new Point(651, 182);
            alarmHistoryButton.Margin = new Padding(3, 4, 3, 4);
            alarmHistoryButton.MouseState = MaterialSkin.MouseState.HOVER;
            alarmHistoryButton.Name = "alarmHistoryButton";
            alarmHistoryButton.NoAccentTextColor = Color.Empty;
            alarmHistoryButton.Size = new Size(135, 36);
            alarmHistoryButton.TabIndex = 2;
            alarmHistoryButton.Text = "Alarm History";
            alarmHistoryButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            alarmHistoryButton.UseAccentColor = false;
            alarmHistoryButton.UseVisualStyleBackColor = true;
            alarmHistoryButton.Click += alarmHistoryButton_Click;
            // 
            // curAlarmButton
            // 
            curAlarmButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            curAlarmButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            curAlarmButton.Depth = 0;
            curAlarmButton.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            curAlarmButton.HighEmphasis = true;
            curAlarmButton.Icon = null;
            curAlarmButton.Location = new Point(651, 44);
            curAlarmButton.Margin = new Padding(3, 4, 3, 4);
            curAlarmButton.MouseState = MaterialSkin.MouseState.HOVER;
            curAlarmButton.Name = "curAlarmButton";
            curAlarmButton.NoAccentTextColor = Color.Empty;
            curAlarmButton.Size = new Size(140, 36);
            curAlarmButton.TabIndex = 1;
            curAlarmButton.Text = "Current Alarm";
            curAlarmButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            curAlarmButton.UseAccentColor = false;
            curAlarmButton.UseVisualStyleBackColor = true;
            curAlarmButton.Click += curAlarmButton_Click;
            // 
            // alarmDataGridView
            // 
            alarmDataGridView.AllowUserToDeleteRows = false;
            alarmDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            alarmDataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, _ALID, _ALCD, _AlarmType, _ALTX, _DateTime, _IsRelease });
            alarmDataGridView.Location = new Point(6, 8);
            alarmDataGridView.Margin = new Padding(3, 4, 3, 4);
            alarmDataGridView.Name = "alarmDataGridView";
            alarmDataGridView.ReadOnly = true;
            alarmDataGridView.RowTemplate.Height = 23;
            alarmDataGridView.Size = new Size(620, 581);
            alarmDataGridView.TabIndex = 0;
            alarmDataGridView.CellClick += alarmDataGridView_CellClick;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 30;
            // 
            // _ALID
            // 
            _ALID.HeaderText = "ALID";
            _ALID.Name = "_ALID";
            _ALID.ReadOnly = true;
            _ALID.Width = 50;
            // 
            // _ALCD
            // 
            _ALCD.HeaderText = "ALCD";
            _ALCD.Name = "_ALCD";
            _ALCD.ReadOnly = true;
            _ALCD.Width = 50;
            // 
            // _AlarmType
            // 
            _AlarmType.HeaderText = "AlarmType";
            _AlarmType.Name = "_AlarmType";
            _AlarmType.ReadOnly = true;
            // 
            // _ALTX
            // 
            _ALTX.HeaderText = "ALTX";
            _ALTX.Name = "_ALTX";
            _ALTX.ReadOnly = true;
            _ALTX.Width = 170;
            // 
            // _DateTime
            // 
            _DateTime.HeaderText = "DateTime";
            _DateTime.Name = "_DateTime";
            _DateTime.ReadOnly = true;
            _DateTime.Width = 140;
            // 
            // _IsRelease
            // 
            _IsRelease.HeaderText = "IsRelease";
            _IsRelease.Name = "_IsRelease";
            _IsRelease.ReadOnly = true;
            _IsRelease.Width = 80;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(materialCardGlassClearOpt);
            tabPage3.Location = new Point(4, 39);
            tabPage3.Margin = new Padding(3, 4, 3, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3, 4, 3, 4);
            tabPage3.Size = new Size(1315, 582);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "ETC";
            // 
            // materialCardGlassClearOpt
            // 
            materialCardGlassClearOpt.BackColor = Color.FromArgb(255, 255, 255);
            materialCardGlassClearOpt.Controls.Add(materialLabelGlassClearOpt);
            materialCardGlassClearOpt.Controls.Add(glassdataresetButton);
            materialCardGlassClearOpt.Controls.Add(GlassCodeCheckBox);
            materialCardGlassClearOpt.Controls.Add(materialCardGlassType);
            materialCardGlassClearOpt.Controls.Add(GlassIDCheckBox);
            materialCardGlassClearOpt.Controls.Add(glassIDTextBox);
            materialCardGlassClearOpt.Controls.Add(glassCodeTextBox);
            materialCardGlassClearOpt.Depth = 0;
            materialCardGlassClearOpt.Dock = DockStyle.Fill;
            materialCardGlassClearOpt.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardGlassClearOpt.Location = new Point(3, 4);
            materialCardGlassClearOpt.Margin = new Padding(14);
            materialCardGlassClearOpt.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardGlassClearOpt.Name = "materialCardGlassClearOpt";
            materialCardGlassClearOpt.Padding = new Padding(14);
            materialCardGlassClearOpt.Size = new Size(1309, 574);
            materialCardGlassClearOpt.TabIndex = 18;
            // 
            // materialLabelGlassClearOpt
            // 
            materialLabelGlassClearOpt.AutoSize = true;
            materialLabelGlassClearOpt.Depth = 0;
            materialLabelGlassClearOpt.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelGlassClearOpt.Location = new Point(10, 5);
            materialLabelGlassClearOpt.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelGlassClearOpt.Name = "materialLabelGlassClearOpt";
            materialLabelGlassClearOpt.Size = new Size(110, 19);
            materialLabelGlassClearOpt.TabIndex = 34;
            materialLabelGlassClearOpt.Text = "Glass Data Req";
            // 
            // glassdataresetButton
            // 
            glassdataresetButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            glassdataresetButton.BackColor = Color.Red;
            glassdataresetButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            glassdataresetButton.Depth = 0;
            glassdataresetButton.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            glassdataresetButton.ForeColor = Color.Yellow;
            glassdataresetButton.HighEmphasis = true;
            glassdataresetButton.Icon = null;
            glassdataresetButton.Location = new Point(26, 167);
            glassdataresetButton.Margin = new Padding(3, 4, 3, 4);
            glassdataresetButton.MouseState = MaterialSkin.MouseState.HOVER;
            glassdataresetButton.Name = "glassdataresetButton";
            glassdataresetButton.NoAccentTextColor = Color.Empty;
            glassdataresetButton.Size = new Size(156, 36);
            glassdataresetButton.TabIndex = 33;
            glassdataresetButton.Text = "Glass Data Reset";
            glassdataresetButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            glassdataresetButton.UseAccentColor = false;
            glassdataresetButton.UseVisualStyleBackColor = false;
            glassdataresetButton.Click += glassdataresetButton_Click;
            // 
            // GlassCodeCheckBox
            // 
            GlassCodeCheckBox.Checked = true;
            GlassCodeCheckBox.CheckState = CheckState.Checked;
            GlassCodeCheckBox.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GlassCodeCheckBox.Location = new Point(180, 110);
            GlassCodeCheckBox.Margin = new Padding(3, 4, 3, 4);
            GlassCodeCheckBox.Name = "GlassCodeCheckBox";
            GlassCodeCheckBox.Size = new Size(142, 39);
            GlassCodeCheckBox.TabIndex = 29;
            GlassCodeCheckBox.Text = "GlassCode";
            GlassCodeCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            GlassCodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // materialCardGlassType
            // 
            materialCardGlassType.BackColor = Color.FromArgb(255, 255, 255);
            materialCardGlassType.Controls.Add(materialLabelGlassType);
            materialCardGlassType.Controls.Add(Aglass);
            materialCardGlassType.Controls.Add(Bglass);
            materialCardGlassType.Depth = 0;
            materialCardGlassType.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCardGlassType.Location = new Point(26, 48);
            materialCardGlassType.Margin = new Padding(14);
            materialCardGlassType.MouseState = MaterialSkin.MouseState.HOVER;
            materialCardGlassType.Name = "materialCardGlassType";
            materialCardGlassType.Padding = new Padding(14);
            materialCardGlassType.Size = new Size(135, 101);
            materialCardGlassType.TabIndex = 18;
            // 
            // materialLabelGlassType
            // 
            materialLabelGlassType.AutoSize = true;
            materialLabelGlassType.Depth = 0;
            materialLabelGlassType.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabelGlassType.Location = new Point(10, 5);
            materialLabelGlassType.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabelGlassType.Name = "materialLabelGlassType";
            materialLabelGlassType.Size = new Size(41, 19);
            materialLabelGlassType.TabIndex = 2;
            materialLabelGlassType.Text = "Glass";
            // 
            // Aglass
            // 
            Aglass.Checked = true;
            Aglass.Location = new Point(6, 25);
            Aglass.Margin = new Padding(3, 4, 3, 4);
            Aglass.Name = "Aglass";
            Aglass.Size = new Size(123, 34);
            Aglass.TabIndex = 10;
            Aglass.TabStop = true;
            Aglass.Text = "A Glass";
            Aglass.TextAlign = ContentAlignment.MiddleCenter;
            Aglass.UseVisualStyleBackColor = true;
            Aglass.CheckedChanged += Aglass_CheckedChanged;
            // 
            // Bglass
            // 
            Bglass.Location = new Point(6, 57);
            Bglass.Margin = new Padding(3, 4, 3, 4);
            Bglass.Name = "Bglass";
            Bglass.Size = new Size(123, 34);
            Bglass.TabIndex = 11;
            Bglass.Text = "B Glass";
            Bglass.TextAlign = ContentAlignment.MiddleCenter;
            Bglass.UseVisualStyleBackColor = true;
            Bglass.CheckedChanged += Bglass_CheckedChanged;
            // 
            // GlassIDCheckBox
            // 
            GlassIDCheckBox.Checked = true;
            GlassIDCheckBox.CheckState = CheckState.Checked;
            GlassIDCheckBox.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GlassIDCheckBox.Location = new Point(180, 48);
            GlassIDCheckBox.Margin = new Padding(3, 4, 3, 4);
            GlassIDCheckBox.Name = "GlassIDCheckBox";
            GlassIDCheckBox.Size = new Size(142, 39);
            GlassIDCheckBox.TabIndex = 28;
            GlassIDCheckBox.Text = "GlassID";
            GlassIDCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            GlassIDCheckBox.UseVisualStyleBackColor = true;
            // 
            // glassIDTextBox
            // 
            glassIDTextBox.BorderStyle = BorderStyle.FixedSingle;
            glassIDTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            glassIDTextBox.ForeColor = Color.Black;
            glassIDTextBox.Location = new Point(328, 56);
            glassIDTextBox.Name = "glassIDTextBox";
            glassIDTextBox.Size = new Size(132, 23);
            glassIDTextBox.TabIndex = 30;
            glassIDTextBox.Text = "1111";
            glassIDTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // glassCodeTextBox
            // 
            glassCodeTextBox.BorderStyle = BorderStyle.FixedSingle;
            glassCodeTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            glassCodeTextBox.ForeColor = Color.Black;
            glassCodeTextBox.Location = new Point(328, 116);
            glassCodeTextBox.Name = "glassCodeTextBox";
            glassCodeTextBox.Size = new Size(132, 23);
            glassCodeTextBox.TabIndex = 32;
            glassCodeTextBox.Text = "1111";
            glassCodeTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, testToolStripMenuItem, eTCToolStripMenuItem, configToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1323, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { systemSetupToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(39, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // systemSetupToolStripMenuItem
            // 
            systemSetupToolStripMenuItem.Name = "systemSetupToolStripMenuItem";
            systemSetupToolStripMenuItem.Size = new Size(147, 22);
            systemSetupToolStripMenuItem.Text = "SystemSetup";
            systemSetupToolStripMenuItem.Click += systemSetupToolStripMenuItem_Click;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loopBackToolStripMenuItem, timeCheckToolStripMenuItem, gLSMVStatusToolStripMenuItem, eQPStatusToolStripMenuItem, eQPSimulatorToolStripMenuItem, alarmToolStripMenuItem, glassData초기화ToolStripMenuItem });
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(42, 20);
            testToolStripMenuItem.Text = "Test";
            // 
            // loopBackToolStripMenuItem
            // 
            loopBackToolStripMenuItem.Name = "loopBackToolStripMenuItem";
            loopBackToolStripMenuItem.Size = new Size(175, 22);
            loopBackToolStripMenuItem.Text = "LoopBack";
            loopBackToolStripMenuItem.Click += loopBackToolStripMenuItem_Click;
            // 
            // timeCheckToolStripMenuItem
            // 
            timeCheckToolStripMenuItem.Name = "timeCheckToolStripMenuItem";
            timeCheckToolStripMenuItem.Size = new Size(175, 22);
            timeCheckToolStripMenuItem.Text = "Time Check";
            timeCheckToolStripMenuItem.Click += timeCheckToolStripMenuItem_Click;
            // 
            // gLSMVStatusToolStripMenuItem
            // 
            gLSMVStatusToolStripMenuItem.Name = "gLSMVStatusToolStripMenuItem";
            gLSMVStatusToolStripMenuItem.Size = new Size(175, 22);
            gLSMVStatusToolStripMenuItem.Text = "GLSMVStatus";
            gLSMVStatusToolStripMenuItem.Click += gLSMVStatusToolStripMenuItem_Click;
            // 
            // eQPStatusToolStripMenuItem
            // 
            eQPStatusToolStripMenuItem.Name = "eQPStatusToolStripMenuItem";
            eQPStatusToolStripMenuItem.Size = new Size(175, 22);
            eQPStatusToolStripMenuItem.Text = "EQPStatus";
            eQPStatusToolStripMenuItem.Click += eQPStatusToolStripMenuItem_Click;
            // 
            // eQPSimulatorToolStripMenuItem
            // 
            eQPSimulatorToolStripMenuItem.Name = "eQPSimulatorToolStripMenuItem";
            eQPSimulatorToolStripMenuItem.Size = new Size(175, 22);
            eQPSimulatorToolStripMenuItem.Text = "EQP Simulator";
            eQPSimulatorToolStripMenuItem.Click += eQPSimulatorToolStripMenuItem_Click;
            // 
            // alarmToolStripMenuItem
            // 
            alarmToolStripMenuItem.Name = "alarmToolStripMenuItem";
            alarmToolStripMenuItem.Size = new Size(175, 22);
            alarmToolStripMenuItem.Text = "Alarm";
            alarmToolStripMenuItem.Click += alarmToolStripMenuItem_Click;
            // 
            // glassData초기화ToolStripMenuItem
            // 
            glassData초기화ToolStripMenuItem.Name = "glassData초기화ToolStripMenuItem";
            glassData초기화ToolStripMenuItem.Size = new Size(175, 22);
            glassData초기화ToolStripMenuItem.Text = "Glass Data 초기화";
            glassData초기화ToolStripMenuItem.Click += glassData초기화ToolStripMenuItem_Click;
            // 
            // eTCToolStripMenuItem
            // 
            eTCToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { messageViewToolStripMenuItem, recipeViewToolStripMenuItem, cassetteMAPRequestToolStripMenuItem, reworkToolStripMenuItem, ionizerToolStripMenuItem });
            eTCToolStripMenuItem.Name = "eTCToolStripMenuItem";
            eTCToolStripMenuItem.Size = new Size(43, 20);
            eTCToolStripMenuItem.Text = "ETC";
            // 
            // messageViewToolStripMenuItem
            // 
            messageViewToolStripMenuItem.Name = "messageViewToolStripMenuItem";
            messageViewToolStripMenuItem.Size = new Size(154, 22);
            messageViewToolStripMenuItem.Text = "Message View";
            messageViewToolStripMenuItem.Click += messageViewToolStripMenuItem_Click;
            // 
            // recipeViewToolStripMenuItem
            // 
            recipeViewToolStripMenuItem.Name = "recipeViewToolStripMenuItem";
            recipeViewToolStripMenuItem.Size = new Size(154, 22);
            recipeViewToolStripMenuItem.Text = "Recipe View";
            recipeViewToolStripMenuItem.Click += recipeViewToolStripMenuItem_Click;
            // 
            // cassetteMAPRequestToolStripMenuItem
            // 
            cassetteMAPRequestToolStripMenuItem.Name = "cassetteMAPRequestToolStripMenuItem";
            cassetteMAPRequestToolStripMenuItem.Size = new Size(154, 22);
            cassetteMAPRequestToolStripMenuItem.Text = "Cassette MAP";
            cassetteMAPRequestToolStripMenuItem.Click += cassetteMAPRequestToolStripMenuItem_Click;
            // 
            // reworkToolStripMenuItem
            // 
            reworkToolStripMenuItem.Name = "reworkToolStripMenuItem";
            reworkToolStripMenuItem.Size = new Size(154, 22);
            reworkToolStripMenuItem.Text = "Rework";
            // 
            // ionizerToolStripMenuItem
            // 
            ionizerToolStripMenuItem.Name = "ionizerToolStripMenuItem";
            ionizerToolStripMenuItem.Size = new Size(154, 22);
            ionizerToolStripMenuItem.Text = "Ionizer";
            ionizerToolStripMenuItem.Click += ionizerToolStripMenuItem_Click;
            // 
            // configToolStripMenuItem
            // 
            configToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sW변경이력ToolStripMenuItem });
            configToolStripMenuItem.Name = "configToolStripMenuItem";
            configToolStripMenuItem.Size = new Size(55, 20);
            configToolStripMenuItem.Text = "Config";
            // 
            // sW변경이력ToolStripMenuItem
            // 
            sW변경이력ToolStripMenuItem.Name = "sW변경이력ToolStripMenuItem";
            sW변경이력ToolStripMenuItem.Size = new Size(147, 22);
            sW변경이력ToolStripMenuItem.Text = "SW 변경 이력";
            sW변경이력ToolStripMenuItem.Click += sW변경이력ToolStripMenuItem_Click;
            // 
            // logViewer1
            // 
            logViewer1.BackColor = Color.FromArgb(25, 38, 52);
            logViewer1.Dock = DockStyle.Fill;
            logViewer1.ForeColor = Color.Gainsboro;
            logViewer1.Format = "[{0:MM-dd HH:mm:ss.fff}] {3}";
            logViewer1.Location = new Point(0, 0);
            logViewer1.Log = null;
            logViewer1.Margin = new Padding(3, 4, 3, 4);
            logViewer1.Name = "logViewer1";
            logViewer1.ReadOnly = true;
            logViewer1.Size = new Size(1323, 147);
            logViewer1.TabIndex = 0;
            logViewer1.Text = "";
            logViewer1.WordWrap = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1329, 965);
            Controls.Add(splitContainer1);
            Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1335, 965);
            MinimumSize = new Size(1300, 965);
            Name = "MainForm";
            StartPosition = FormStartPosition.Manual;
            Text = "TOE CIM";
            FormClosing += MainForm_FormClosing;
            Load += FormMain_Load;
            materialCardRCS.ResumeLayout(false);
            materialCardRCS.PerformLayout();
            materialCardOPMode.ResumeLayout(false);
            materialCardOPMode.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            materialCardScenario.ResumeLayout(false);
            materialCardScenario.PerformLayout();
            materialCardEQPStatus.ResumeLayout(false);
            materialCardEQPStatus.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            materialCardGlassStatus.ResumeLayout(false);
            materialCardGlassStatus.PerformLayout();
            materialCardBGlassInfo.ResumeLayout(false);
            materialCardBGlassInfo.PerformLayout();
            materialCardAGlassInfo.ResumeLayout(false);
            materialCardAGlassInfo.PerformLayout();
            tabControl2.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)alarmDataGridView).EndInit();
            tabPage3.ResumeLayout(false);
            materialCardGlassClearOpt.ResumeLayout(false);
            materialCardGlassClearOpt.PerformLayout();
            materialCardGlassType.ResumeLayout(false);
            materialCardGlassType.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCardRCS;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton rcsButton;
        private MaterialSkin.Controls.MaterialCard materialCardScenario;
        private MaterialSkin.Controls.MaterialLabel materialLabelScenario;
        private MaterialSkin.Controls.MaterialButton scrapReportButton;
        private MaterialSkin.Controls.MaterialButton unloadButton;
        private MaterialSkin.Controls.MaterialButton LoadButton;
        private System.Windows.Forms.Label scenarioPrereqLabel;
        private MaterialSkin.Controls.MaterialCard materialCardOPMode;
        private MaterialSkin.Controls.MaterialLabel materialLabelOPMode;
        private MaterialSkin.Controls.MaterialButton opModeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Elp.LogViewer logViewer1;
        private MaterialSkin.Controls.MaterialCard materialCardEQPStatus;
        private MaterialSkin.Controls.MaterialLabel materialLabelEQPStatus;
        private System.Windows.Forms.Label EQStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eQPStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gLSMVStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemSetupToolStripMenuItem;
        private TOECIM.Controls.DarkTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView alarmDataGridView;
        private MaterialSkin.Controls.MaterialButton curAlarmButton;
        private MaterialSkin.Controls.MaterialButton alarmHistoryButton;
        private MaterialSkin.Controls.MaterialButton selectedAlarmClearButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem eQPSimulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmToolStripMenuItem;
        private MaterialSkin.Controls.MaterialButton RefreshButton;
        private MaterialSkin.Controls.MaterialButton allAlarmClearButton;
        private MaterialSkin.Controls.MaterialCard materialCardGlassType;
        private MaterialSkin.Controls.MaterialLabel materialLabelGlassType;
        private System.Windows.Forms.RadioButton Aglass;
        private System.Windows.Forms.RadioButton Bglass;
        private System.Windows.Forms.ToolStripMenuItem eTCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messageViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cassetteMAPRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ionizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem glassData초기화ToolStripMenuItem;
        private System.Windows.Forms.TextBox glassCodeTextBox;
        private System.Windows.Forms.TextBox glassIDTextBox;
        private MaterialSkin.Controls.MaterialButton glassDataReqButton;
        private MaterialSkin.Controls.MaterialCard materialCardGlassClearOpt;
        private MaterialSkin.Controls.MaterialLabel materialLabelGlassClearOpt;
        private System.Windows.Forms.CheckBox GlassIDCheckBox;
        private System.Windows.Forms.CheckBox GlassCodeCheckBox;
        private System.Windows.Forms.ToolStripMenuItem loopBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeCheckToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ALID;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ALCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn _AlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ALTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn _DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn _IsRelease;
        private System.Windows.Forms.Button alarmStatusButton;
        private System.Windows.Forms.Label consoleStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MaterialSkin.Controls.MaterialButton glassdataresetButton;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sW변경이력ToolStripMenuItem;
        private System.Windows.Forms.Label autoRunLabel;
        private System.Windows.Forms.Label glassInlabel;
        private System.Windows.Forms.Label glsmvLabel;
        private SplitContainer splitContainer3;
        private MaterialSkin.Controls.MaterialCard materialCardGlassStatus;
        private MaterialSkin.Controls.MaterialLabel materialLabelGlassStatus;
        private MaterialSkin.Controls.MaterialButton refreshCosoleStatusButton;
        private TextBox GLSStatusTextBox;
        private MaterialSkin.Controls.MaterialLabel label12;
        private TextBox cstidTextBox;
        private MaterialSkin.Controls.MaterialCard materialCardBGlassInfo;
        private MaterialSkin.Controls.MaterialLabel materialLabelBGlassInfo;
        private TextBox bGlassOdrTextBox;
        private MaterialSkin.Controls.MaterialLabel label8;
        private TextBox bGlassCodeTextBox;
        private TextBox bGlassIDTextBox;
        private MaterialSkin.Controls.MaterialLabel label4;
        private MaterialSkin.Controls.MaterialLabel label5;
        private MaterialSkin.Controls.MaterialLabel label9;
        private MaterialSkin.Controls.MaterialCard materialCardAGlassInfo;
        private MaterialSkin.Controls.MaterialLabel materialLabelAGlassInfo;
        private TextBox aGlassOdrTextBox;
        private MaterialSkin.Controls.MaterialLabel label1;
        private TextBox aGlassCodeTextBox;
        private TextBox aGlassIDTextBox;
        private MaterialSkin.Controls.MaterialLabel label6;
        private MaterialSkin.Controls.MaterialLabel label7;
        private TextBox lotidTextBox;
        private TextBox stageIDTextBox;
        private TextBox portidTextBox;
        private MaterialSkin.Controls.MaterialLabel label10;
        private TextBox unitIDTextBox;
        private MaterialSkin.Controls.MaterialLabel label11;
        private MaterialSkin.Controls.MaterialLabel label3;
        private MaterialSkin.Controls.MaterialLabel label2;
        private TOECIM.Controls.DarkTabControl tabControl2;
        private TabPage tabPage4;
        private SplitContainer splitContainer4;
        private PropertyGrid aLoadPropertyGrid;
        private PropertyGrid bLoadPropertyGrid;
        private TabPage tabPage5;
        private SplitContainer splitContainer5;
        private PropertyGrid aUnLoadPropertyGrid;
        private PropertyGrid bUnLoadPropertyGrid;
    }
}

