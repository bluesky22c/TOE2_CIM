
namespace LGCIMServer
{
    partial class ECSSimulForm
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
            rtbLog = new RichTextBox();
            dateTimeSetButton = new Button();
            s1f63Button = new Button();
            recipeButton = new Button();
            ECSStartButton = new Button();
            LOTENDButton = new Button();
            SERVERpropertyGrid = new PropertyGrid();
            AGlasspropertyGrid = new PropertyGrid();
            BGlasspropertyGrid = new PropertyGrid();
            menuStrip1 = new MenuStrip();
            testToolStripMenuItem = new ToolStripMenuItem();
            unloadDataRequsetToolStripMenuItem = new ToolStripMenuItem();
            sendTextToCIMToolStripMenuItem = new ToolStripMenuItem();
            pDDownToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            groupBox1 = new GroupBox();
            noGlassButton = new Button();
            portCountTextBox = new TextBox();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            clearBGlassInfoButton = new Button();
            clearAGlassInfoButton = new Button();
            setBGlassInfoButton = new Button();
            setAGlassInfoButton = new Button();
            lblPortStatus = new Label();
            tabPage2 = new TabPage();
            comboBox1 = new ComboBox();
            label4 = new Label();
            propertyGrid1 = new PropertyGrid();
            sendButton = new Button();
            treeView1 = new TreeView();
            label3 = new Label();
            label2 = new Label();
            functionTextBox = new TextBox();
            streamTextBox = new TextBox();
            hsmsMessageTextBox = new TextBox();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.Location = new Point(483, 43);
            rtbLog.Margin = new Padding(3, 4, 3, 4);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(300, 874);
            rtbLog.TabIndex = 20;
            rtbLog.Text = "";
            // 
            // dateTimeSetButton
            // 
            dateTimeSetButton.Location = new Point(6, 20);
            dateTimeSetButton.Margin = new Padding(3, 4, 3, 4);
            dateTimeSetButton.Name = "dateTimeSetButton";
            dateTimeSetButton.Size = new Size(108, 54);
            dateTimeSetButton.TabIndex = 5;
            dateTimeSetButton.Text = "S2F31\nDateTime Set";
            dateTimeSetButton.UseVisualStyleBackColor = true;
            dateTimeSetButton.Click += dateTimeSetButton_Click;
            // 
            // s1f63Button
            // 
            s1f63Button.Location = new Point(6, 142);
            s1f63Button.Margin = new Padding(3, 4, 3, 4);
            s1f63Button.Name = "s1f63Button";
            s1f63Button.Size = new Size(108, 54);
            s1f63Button.TabIndex = 0;
            s1f63Button.Text = "S1F63\nOffline Req";
            s1f63Button.UseVisualStyleBackColor = true;
            s1f63Button.Click += s1f63Button_Click;
            // 
            // recipeButton
            // 
            recipeButton.Location = new Point(6, 203);
            recipeButton.Margin = new Padding(3, 4, 3, 4);
            recipeButton.Name = "recipeButton";
            recipeButton.Size = new Size(108, 54);
            recipeButton.TabIndex = 1;
            recipeButton.Text = "S15F71\nRecipe Req";
            recipeButton.UseVisualStyleBackColor = true;
            recipeButton.Click += recipeButton_Click;
            // 
            // ECSStartButton
            // 
            ECSStartButton.Location = new Point(6, 81);
            ECSStartButton.Margin = new Padding(3, 4, 3, 4);
            ECSStartButton.Name = "ECSStartButton";
            ECSStartButton.Size = new Size(108, 54);
            ECSStartButton.TabIndex = 5;
            ECSStartButton.Text = "S7F61\nRecipe Confirm";
            ECSStartButton.UseVisualStyleBackColor = true;
            ECSStartButton.Click += ECSStartButton_Click;
            // 
            // LOTENDButton
            // 
            LOTENDButton.Location = new Point(17, 278);
            LOTENDButton.Margin = new Padding(3, 4, 3, 4);
            LOTENDButton.Name = "LOTENDButton";
            LOTENDButton.Size = new Size(96, 54);
            LOTENDButton.TabIndex = 6;
            LOTENDButton.Text = "S6F65\nLOT END";
            LOTENDButton.UseVisualStyleBackColor = true;
            LOTENDButton.Click += LOTENDButton_Click;
            // 
            // SERVERpropertyGrid
            // 
            SERVERpropertyGrid.Location = new Point(9, 473);
            SERVERpropertyGrid.Margin = new Padding(3, 4, 3, 4);
            SERVERpropertyGrid.Name = "SERVERpropertyGrid";
            SERVERpropertyGrid.Size = new Size(203, 312);
            SERVERpropertyGrid.TabIndex = 7;
            // 
            // AGlasspropertyGrid
            // 
            AGlasspropertyGrid.Location = new Point(266, 20);
            AGlasspropertyGrid.Margin = new Padding(3, 4, 3, 4);
            AGlasspropertyGrid.Name = "AGlasspropertyGrid";
            AGlasspropertyGrid.Size = new Size(190, 368);
            AGlasspropertyGrid.TabIndex = 8;
            // 
            // BGlasspropertyGrid
            // 
            BGlasspropertyGrid.Location = new Point(265, 473);
            BGlasspropertyGrid.Margin = new Padding(3, 4, 3, 4);
            BGlasspropertyGrid.Name = "BGlasspropertyGrid";
            BGlasspropertyGrid.Size = new Size(191, 343);
            BGlasspropertyGrid.TabIndex = 9;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(795, 24);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { unloadDataRequsetToolStripMenuItem, sendTextToCIMToolStripMenuItem, pDDownToolStripMenuItem });
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(40, 20);
            testToolStripMenuItem.Text = "Test";
            // 
            // unloadDataRequsetToolStripMenuItem
            // 
            unloadDataRequsetToolStripMenuItem.Name = "unloadDataRequsetToolStripMenuItem";
            unloadDataRequsetToolStripMenuItem.Size = new Size(264, 22);
            unloadDataRequsetToolStripMenuItem.Text = "Send Unload Data Requset (S2F69)";
            unloadDataRequsetToolStripMenuItem.Click += unloadDataRequsetToolStripMenuItem_Click;
            // 
            // sendTextToCIMToolStripMenuItem
            // 
            sendTextToCIMToolStripMenuItem.Name = "sendTextToCIMToolStripMenuItem";
            sendTextToCIMToolStripMenuItem.Size = new Size(264, 22);
            sendTextToCIMToolStripMenuItem.Text = "Send Text to CIM (S10F3)";
            sendTextToCIMToolStripMenuItem.Click += sendTextToCIMToolStripMenuItem_Click;
            // 
            // pDDownToolStripMenuItem
            // 
            pDDownToolStripMenuItem.Name = "pDDownToolStripMenuItem";
            pDDownToolStripMenuItem.Size = new Size(264, 22);
            pDDownToolStripMenuItem.Text = "Send PD Down (S2F99)";
            pDDownToolStripMenuItem.Click += pdDownButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(17, 32);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(96, 54);
            button1.TabIndex = 12;
            button1.Text = "S2F87\nFirst Glass";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(17, 94);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(96, 54);
            button2.TabIndex = 13;
            button2.Text = "S2F87\nNormal Glass";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(17, 155);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(96, 54);
            button3.TabIndex = 14;
            button3.Text = "S2F87\nLast Glass";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(noGlassButton);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(LOTENDButton);
            groupBox1.Location = new Point(123, 20);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(132, 346);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "S2F87 Glass Data";
            // 
            // noGlassButton
            // 
            noGlassButton.Location = new Point(17, 216);
            noGlassButton.Margin = new Padding(3, 4, 3, 4);
            noGlassButton.Name = "noGlassButton";
            noGlassButton.Size = new Size(96, 54);
            noGlassButton.TabIndex = 15;
            noGlassButton.Text = "S2F87\nNo Glass";
            noGlassButton.UseVisualStyleBackColor = true;
            noGlassButton.Click += noGlassButton_Click;
            // 
            // portCountTextBox
            // 
            portCountTextBox.Location = new Point(11, 408);
            portCountTextBox.Margin = new Padding(3, 4, 3, 4);
            portCountTextBox.Name = "portCountTextBox";
            portCountTextBox.Size = new Size(44, 23);
            portCountTextBox.TabIndex = 16;
            portCountTextBox.Text = "1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 356);
            label1.Name = "label1";
            label1.Size = new Size(270, 45);
            label1.TabIndex = 17;
            label1.Text = "Port Count :\r\nS7F61 (Process Program Load Inquire) 메시지를 \r\n전송할 때 *처리할 포트의 개수* 설정";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(8, 36);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(475, 889);
            tabControl1.TabIndex = 18;
            //
            // tabPage1
            // 
            tabPage1.Controls.Add(clearBGlassInfoButton);
            tabPage1.Controls.Add(clearAGlassInfoButton);
            tabPage1.Controls.Add(setBGlassInfoButton);
            tabPage1.Controls.Add(setAGlassInfoButton);
            tabPage1.Controls.Add(dateTimeSetButton);
            tabPage1.Controls.Add(s1f63Button);
            tabPage1.Controls.Add(recipeButton);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(portCountTextBox);
            tabPage1.Controls.Add(ECSStartButton);
            tabPage1.Controls.Add(lblPortStatus);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(BGlasspropertyGrid);
            tabPage1.Controls.Add(SERVERpropertyGrid);
            tabPage1.Controls.Add(AGlasspropertyGrid);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 4, 3, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 4, 3, 4);
            tabPage1.Size = new Size(467, 861);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Normal";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // clearBGlassInfoButton
            // 
            clearBGlassInfoButton.Location = new Point(247, 824);
            clearBGlassInfoButton.Margin = new Padding(3, 4, 3, 4);
            clearBGlassInfoButton.Name = "clearBGlassInfoButton";
            clearBGlassInfoButton.Size = new Size(75, 29);
            clearBGlassInfoButton.TabIndex = 19;
            clearBGlassInfoButton.Text = "Clear Info";
            clearBGlassInfoButton.UseVisualStyleBackColor = true;
            clearBGlassInfoButton.Click += clearBGlassInfoButton_Click;
            // 
            // clearAGlassInfoButton
            // 
            clearAGlassInfoButton.Location = new Point(273, 435);
            clearAGlassInfoButton.Margin = new Padding(3, 4, 3, 4);
            clearAGlassInfoButton.Name = "clearAGlassInfoButton";
            clearAGlassInfoButton.Size = new Size(75, 29);
            clearAGlassInfoButton.TabIndex = 19;
            clearAGlassInfoButton.Text = "Clear Info";
            clearAGlassInfoButton.UseVisualStyleBackColor = true;
            clearAGlassInfoButton.Click += clearAGlassInfoButton_Click;
            // 
            // setBGlassInfoButton
            // 
            setBGlassInfoButton.Location = new Point(328, 824);
            setBGlassInfoButton.Margin = new Padding(3, 4, 3, 4);
            setBGlassInfoButton.Name = "setBGlassInfoButton";
            setBGlassInfoButton.Size = new Size(75, 29);
            setBGlassInfoButton.TabIndex = 18;
            setBGlassInfoButton.Text = "Set Info";
            setBGlassInfoButton.UseVisualStyleBackColor = true;
            setBGlassInfoButton.Click += setBGlassInfoButton_Click;
            // 
            // setAGlassInfoButton
            // 
            setAGlassInfoButton.Location = new Point(354, 435);
            setAGlassInfoButton.Margin = new Padding(3, 4, 3, 4);
            setAGlassInfoButton.Name = "setAGlassInfoButton";
            setAGlassInfoButton.Size = new Size(75, 29);
            setAGlassInfoButton.TabIndex = 18;
            setAGlassInfoButton.Text = "Set Info";
            setAGlassInfoButton.UseVisualStyleBackColor = true;
            setAGlassInfoButton.Click += setAGlassInfoButton_Click;
            // 
            // lblPortStatus
            // 
            lblPortStatus.AutoSize = true;
            lblPortStatus.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblPortStatus.Location = new Point(11, 438);
            lblPortStatus.Name = "lblPortStatus";
            lblPortStatus.Size = new Size(103, 20);
            lblPortStatus.TabIndex = 18;
            lblPortStatus.Text = "Port Status: -";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(propertyGrid1);
            tabPage2.Controls.Add(sendButton);
            tabPage2.Controls.Add(treeView1);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(functionTextBox);
            tabPage2.Controls.Add(streamTextBox);
            tabPage2.Controls.Add(hsmsMessageTextBox);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new Size(467, 861);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Message";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "False", "True" });
            comboBox1.Location = new Point(240, 817);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(68, 23);
            comboBox1.TabIndex = 31;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(197, 821);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 30;
            label4.Text = "Wait:";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Location = new Point(209, 505);
            propertyGrid1.Margin = new Padding(3, 4, 3, 4);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(226, 295);
            propertyGrid1.TabIndex = 29;
            propertyGrid1.SelectedGridItemChanged += propertyGrid1_SelectedGridItemChanged;
            // 
            // sendButton
            // 
            sendButton.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            sendButton.Location = new Point(642, 809);
            sendButton.Margin = new Padding(3, 4, 3, 4);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(117, 40);
            sendButton.TabIndex = 28;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView1.Location = new Point(20, 14);
            treeView1.Margin = new Padding(3, 4, 3, 4);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(183, 785);
            treeView1.TabIndex = 27;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("굴림", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(332, 822);
            label3.Name = "label3";
            label3.Size = new Size(20, 13);
            label3.TabIndex = 26;
            label3.Text = "F:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("굴림", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(96, 821);
            label2.Name = "label2";
            label2.Size = new Size(22, 13);
            label2.TabIndex = 25;
            label2.Text = "S:";
            // 
            // functionTextBox
            // 
            functionTextBox.Location = new Point(358, 814);
            functionTextBox.Margin = new Padding(3, 4, 3, 4);
            functionTextBox.Name = "functionTextBox";
            functionTextBox.Size = new Size(67, 23);
            functionTextBox.TabIndex = 24;
            // 
            // streamTextBox
            // 
            streamTextBox.Location = new Point(124, 814);
            streamTextBox.Margin = new Padding(3, 4, 3, 4);
            streamTextBox.Name = "streamTextBox";
            streamTextBox.Size = new Size(65, 23);
            streamTextBox.TabIndex = 23;
            // 
            // hsmsMessageTextBox
            // 
            hsmsMessageTextBox.Location = new Point(220, 14);
            hsmsMessageTextBox.Margin = new Padding(3, 4, 3, 4);
            hsmsMessageTextBox.Multiline = true;
            hsmsMessageTextBox.Name = "hsmsMessageTextBox";
            hsmsMessageTextBox.Size = new Size(215, 483);
            hsmsMessageTextBox.TabIndex = 22;
            hsmsMessageTextBox.WordWrap = false;
            // 
            // ECSSimulForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 928);
            Controls.Add(rtbLog);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ECSSimulForm";
            Text = "ECS Simulator";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dateTimeSetButton;
        private System.Windows.Forms.Button s1f63Button;
        private System.Windows.Forms.Button recipeButton;
        private System.Windows.Forms.Button ECSStartButton;
        private System.Windows.Forms.Button LOTENDButton;
        private System.Windows.Forms.PropertyGrid SERVERpropertyGrid;
        private System.Windows.Forms.PropertyGrid AGlasspropertyGrid;
        private System.Windows.Forms.PropertyGrid BGlasspropertyGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadDataRequsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendTextToCIMToolStripMenuItem;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem pDDownToolStripMenuItem;
        private System.Windows.Forms.Button noGlassButton;
        private System.Windows.Forms.TextBox portCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox functionTextBox;
        private System.Windows.Forms.TextBox streamTextBox;
        private System.Windows.Forms.TextBox hsmsMessageTextBox;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button setBGlassInfoButton;
        private System.Windows.Forms.Button setAGlassInfoButton;
        private System.Windows.Forms.Button clearBGlassInfoButton;
        private System.Windows.Forms.Button clearAGlassInfoButton;
        private System.Windows.Forms.Label lblPortStatus;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}

