
namespace TOECIM
{
    partial class EQPSimulForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            eqpControlPanel = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            loadGlassOrdertextBox = new TextBox();
            loadGlassCodetextBox = new TextBox();
            loadGlassIDtextBox = new TextBox();
            label5 = new Label();
            groupBox2 = new GroupBox();
            unloadGlassOrdertextBox = new TextBox();
            label4 = new Label();
            unloadGlassCodetextBox = new TextBox();
            label6 = new Label();
            unloadGlassIDtextBox = new TextBox();
            label3 = new Label();
            uwLabel = new Label();
            alarmSetButton = new Button();
            alarmClearButton = new Button();
            autoRunCheckBox = new CheckBox();
            alarmComboBox = new ComboBox();
            glassInCheckBox = new CheckBox();
            eqpStatusLabel = new Label();
            statusComboBox = new ComboBox();
            statusChangeButton = new Button();
            readyStatusComboBox = new ComboBox();
            readyStatusChangeButton = new Button();
            simulLogListBox = new ListBox();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // eqpControlPanel
            // 
            eqpControlPanel.FlowDirection = FlowDirection.TopDown;
            eqpControlPanel.Location = new Point(12, 36);
            eqpControlPanel.Margin = new Padding(3, 4, 3, 4);
            eqpControlPanel.Name = "eqpControlPanel";
            eqpControlPanel.Size = new Size(180, 540);
            eqpControlPanel.TabIndex = 0;
            eqpControlPanel.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 75);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 1;
            label1.Text = "GlassCode : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 35);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 2;
            label2.Text = "GlassID : ";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(loadGlassOrdertextBox);
            groupBox1.Controls.Add(loadGlassCodetextBox);
            groupBox1.Controls.Add(loadGlassIDtextBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(267, 99);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(355, 158);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Load Glass Data";
            // 
            // loadGlassOrdertextBox
            // 
            loadGlassOrdertextBox.Location = new Point(108, 114);
            loadGlassOrdertextBox.Margin = new Padding(3, 4, 3, 4);
            loadGlassOrdertextBox.Name = "loadGlassOrdertextBox";
            loadGlassOrdertextBox.Size = new Size(241, 23);
            loadGlassOrdertextBox.TabIndex = 6;
            // 
            // loadGlassCodetextBox
            // 
            loadGlassCodetextBox.Location = new Point(108, 71);
            loadGlassCodetextBox.Margin = new Padding(3, 4, 3, 4);
            loadGlassCodetextBox.Name = "loadGlassCodetextBox";
            loadGlassCodetextBox.Size = new Size(241, 23);
            loadGlassCodetextBox.TabIndex = 5;
            // 
            // loadGlassIDtextBox
            // 
            loadGlassIDtextBox.Location = new Point(108, 31);
            loadGlassIDtextBox.Margin = new Padding(3, 4, 3, 4);
            loadGlassIDtextBox.Name = "loadGlassIDtextBox";
            loadGlassIDtextBox.Size = new Size(241, 23);
            loadGlassIDtextBox.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 118);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 3;
            label5.Text = "GlassOrder : ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(unloadGlassOrdertextBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(unloadGlassCodetextBox);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(unloadGlassIDtextBox);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(267, 306);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(355, 156);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Unload Glass";
            // 
            // unloadGlassOrdertextBox
            // 
            unloadGlassOrdertextBox.Location = new Point(109, 111);
            unloadGlassOrdertextBox.Margin = new Padding(3, 4, 3, 4);
            unloadGlassOrdertextBox.Name = "unloadGlassOrdertextBox";
            unloadGlassOrdertextBox.Size = new Size(241, 23);
            unloadGlassOrdertextBox.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 32);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 8;
            label4.Text = "GlassID : ";
            // 
            // unloadGlassCodetextBox
            // 
            unloadGlassCodetextBox.Location = new Point(109, 69);
            unloadGlassCodetextBox.Margin = new Padding(3, 4, 3, 4);
            unloadGlassCodetextBox.Name = "unloadGlassCodetextBox";
            unloadGlassCodetextBox.Size = new Size(241, 23);
            unloadGlassCodetextBox.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 72);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 7;
            label6.Text = "GlassCode : ";
            // 
            // unloadGlassIDtextBox
            // 
            unloadGlassIDtextBox.Location = new Point(109, 29);
            unloadGlassIDtextBox.Margin = new Padding(3, 4, 3, 4);
            unloadGlassIDtextBox.Name = "unloadGlassIDtextBox";
            unloadGlassIDtextBox.Size = new Size(241, 23);
            unloadGlassIDtextBox.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 115);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 9;
            label3.Text = "GlassOrder : ";
            // 
            // uwLabel
            // 
            uwLabel.AutoSize = true;
            uwLabel.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            uwLabel.ForeColor = Color.Red;
            uwLabel.Location = new Point(509, 276);
            uwLabel.Name = "uwLabel";
            uwLabel.Size = new Size(112, 12);
            uwLabel.TabIndex = 5;
            uwLabel.Text = "Unload Waiting..";
            uwLabel.Visible = false;
            // 
            // alarmSetButton
            // 
            alarmSetButton.Location = new Point(440, 480);
            alarmSetButton.Margin = new Padding(3, 4, 3, 4);
            alarmSetButton.Name = "alarmSetButton";
            alarmSetButton.Size = new Size(87, 42);
            alarmSetButton.TabIndex = 6;
            alarmSetButton.Text = "Alarm Set";
            alarmSetButton.UseVisualStyleBackColor = true;
            alarmSetButton.Click += alarmSetButton_Click;
            // 
            // alarmClearButton
            // 
            alarmClearButton.Location = new Point(533, 480);
            alarmClearButton.Margin = new Padding(3, 4, 3, 4);
            alarmClearButton.Name = "alarmClearButton";
            alarmClearButton.Size = new Size(87, 42);
            alarmClearButton.TabIndex = 7;
            alarmClearButton.Text = "Alarm Clear";
            alarmClearButton.UseVisualStyleBackColor = true;
            alarmClearButton.Click += alarmClearButton_Click;
            // 
            // autoRunCheckBox
            // 
            autoRunCheckBox.AutoSize = true;
            autoRunCheckBox.Checked = true;
            autoRunCheckBox.CheckState = CheckState.Checked;
            autoRunCheckBox.Location = new Point(551, 50);
            autoRunCheckBox.Margin = new Padding(3, 4, 3, 4);
            autoRunCheckBox.Name = "autoRunCheckBox";
            autoRunCheckBox.Size = new Size(73, 19);
            autoRunCheckBox.TabIndex = 8;
            autoRunCheckBox.Text = "AutoRun";
            autoRunCheckBox.UseVisualStyleBackColor = true;
            autoRunCheckBox.CheckedChanged += autoRunCheckBox_CheckedChanged;
            // 
            // alarmComboBox
            // 
            alarmComboBox.FormattingEnabled = true;
            alarmComboBox.Location = new Point(314, 490);
            alarmComboBox.Margin = new Padding(3, 4, 3, 4);
            alarmComboBox.Name = "alarmComboBox";
            alarmComboBox.Size = new Size(120, 23);
            alarmComboBox.TabIndex = 9;
            // 
            // glassInCheckBox
            // 
            glassInCheckBox.AutoSize = true;
            glassInCheckBox.Location = new Point(469, 50);
            glassInCheckBox.Margin = new Padding(3, 4, 3, 4);
            glassInCheckBox.Name = "glassInCheckBox";
            glassInCheckBox.Size = new Size(63, 19);
            glassInCheckBox.TabIndex = 11;
            glassInCheckBox.Text = "GlassIn";
            glassInCheckBox.UseVisualStyleBackColor = true;
            glassInCheckBox.CheckedChanged += glassInCheckBox_CheckedChanged;
            // 
            // eqpStatusLabel
            // 
            eqpStatusLabel.AutoSize = true;
            eqpStatusLabel.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            eqpStatusLabel.ForeColor = Color.Blue;
            eqpStatusLabel.Location = new Point(284, 273);
            eqpStatusLabel.Name = "eqpStatusLabel";
            eqpStatusLabel.Size = new Size(102, 15);
            eqpStatusLabel.TabIndex = 7;
            eqpStatusLabel.Text = "EQPStatus : IDLE";
            // 
            // statusComboBox
            // 
            statusComboBox.FormattingEnabled = true;
            statusComboBox.Location = new Point(314, 529);
            statusComboBox.Name = "statusComboBox";
            statusComboBox.Size = new Size(120, 23);
            statusComboBox.TabIndex = 12;
            // 
            // statusChangeButton
            // 
            statusChangeButton.Location = new Point(440, 529);
            statusChangeButton.Name = "statusChangeButton";
            statusChangeButton.Size = new Size(126, 23);
            statusChangeButton.TabIndex = 13;
            statusChangeButton.Text = "Change Status";
            statusChangeButton.UseVisualStyleBackColor = true;
            statusChangeButton.Click += statusChangeButton_Click;
            // 
            // readyStatusComboBox
            // 
            readyStatusComboBox.FormattingEnabled = true;
            readyStatusComboBox.Location = new Point(314, 558);
            readyStatusComboBox.Name = "readyStatusComboBox";
            readyStatusComboBox.Size = new Size(120, 23);
            readyStatusComboBox.TabIndex = 14;
            // 
            // readyStatusChangeButton
            // 
            readyStatusChangeButton.Location = new Point(440, 558);
            readyStatusChangeButton.Name = "readyStatusChangeButton";
            readyStatusChangeButton.Size = new Size(130, 23);
            readyStatusChangeButton.TabIndex = 15;
            readyStatusChangeButton.Text = "Change ReadyStatus";
            readyStatusChangeButton.UseVisualStyleBackColor = true;
            readyStatusChangeButton.Click += readyStatusChangeButton_Click;
            // 
            // simulLogListBox
            // 
            simulLogListBox.FormattingEnabled = true;
            simulLogListBox.HorizontalScrollbar = true;
            simulLogListBox.ItemHeight = 15;
            simulLogListBox.Location = new Point(637, 36);
            simulLogListBox.Name = "simulLogListBox";
            simulLogListBox.Size = new Size(500, 544);
            simulLogListBox.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label7.Location = new Point(639, 15);
            label7.Name = "label7";
            label7.Size = new Size(121, 15);
            label7.TabIndex = 17;
            label7.Text = "Communication Log";
            // 
            // EQPSimulForm
            // 
            ClientSize = new Size(1150, 600);
            Controls.Add(label7);
            Controls.Add(simulLogListBox);
            Controls.Add(readyStatusChangeButton);
            Controls.Add(readyStatusComboBox);
            Controls.Add(statusChangeButton);
            Controls.Add(statusComboBox);
            Controls.Add(eqpStatusLabel);
            Controls.Add(glassInCheckBox);
            Controls.Add(alarmComboBox);
            Controls.Add(autoRunCheckBox);
            Controls.Add(alarmClearButton);
            Controls.Add(alarmSetButton);
            Controls.Add(uwLabel);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(eqpControlPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EQPSimulForm";
            Text = "EQPSimulator";
            FormClosing += EQPSimulator_FormClosing;
            Load += EQPSimulator_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel eqpControlPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox loadGlassOrdertextBox;
        private System.Windows.Forms.TextBox loadGlassCodetextBox;
        private System.Windows.Forms.TextBox loadGlassIDtextBox;
        private System.Windows.Forms.TextBox unloadGlassOrdertextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox unloadGlassCodetextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox unloadGlassIDtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label uwLabel;
        private System.Windows.Forms.Button alarmSetButton;
        private System.Windows.Forms.Button alarmClearButton;
        private System.Windows.Forms.CheckBox autoRunCheckBox;
        private System.Windows.Forms.ComboBox alarmComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.CheckBox glassInCheckBox;
        private System.Windows.Forms.Label eqpStatusLabel;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button statusChangeButton;
        private System.Windows.Forms.ComboBox readyStatusComboBox;
        private System.Windows.Forms.Button readyStatusChangeButton;
        private System.Windows.Forms.ListBox simulLogListBox;
        private System.Windows.Forms.Label label7;
    }
}