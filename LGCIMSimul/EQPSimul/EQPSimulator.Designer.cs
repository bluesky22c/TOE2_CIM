
namespace LGCIMServer.EQPSimul
{
    partial class EQPSimulator
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
            this.eqpControlPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadGlassOrdertextBox = new System.Windows.Forms.TextBox();
            this.loadGlassCodetextBox = new System.Windows.Forms.TextBox();
            this.loadGlassIDtextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.unloadGlassOrdertextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.unloadGlassCodetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.unloadGlassIDtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uwLabel = new System.Windows.Forms.Label();
            this.alarmSetButton = new System.Windows.Forms.Button();
            this.alarmClearButton = new System.Windows.Forms.Button();
            this.autoRunCheckBox = new System.Windows.Forms.CheckBox();
            this.alarmComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // eqpControlPanel
            // 
            this.eqpControlPanel.Location = new System.Drawing.Point(27, 32);
            this.eqpControlPanel.Name = "eqpControlPanel";
            this.eqpControlPanel.Size = new System.Drawing.Size(130, 241);
            this.eqpControlPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "GlassCode : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "GlassID : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadGlassOrdertextBox);
            this.groupBox1.Controls.Add(this.loadGlassCodetextBox);
            this.groupBox1.Controls.Add(this.loadGlassIDtextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(255, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 126);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Glass Data";
            // 
            // loadGlassOrdertextBox
            // 
            this.loadGlassOrdertextBox.Location = new System.Drawing.Point(108, 91);
            this.loadGlassOrdertextBox.Name = "loadGlassOrdertextBox";
            this.loadGlassOrdertextBox.Size = new System.Drawing.Size(241, 21);
            this.loadGlassOrdertextBox.TabIndex = 6;
            // 
            // loadGlassCodetextBox
            // 
            this.loadGlassCodetextBox.Location = new System.Drawing.Point(108, 57);
            this.loadGlassCodetextBox.Name = "loadGlassCodetextBox";
            this.loadGlassCodetextBox.Size = new System.Drawing.Size(241, 21);
            this.loadGlassCodetextBox.TabIndex = 5;
            // 
            // loadGlassIDtextBox
            // 
            this.loadGlassIDtextBox.Location = new System.Drawing.Point(108, 25);
            this.loadGlassIDtextBox.Name = "loadGlassIDtextBox";
            this.loadGlassIDtextBox.Size = new System.Drawing.Size(241, 21);
            this.loadGlassIDtextBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "GlassOrder : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.unloadGlassOrdertextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.unloadGlassCodetextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.unloadGlassIDtextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(255, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 125);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unload Glass";
            // 
            // unloadGlassOrdertextBox
            // 
            this.unloadGlassOrdertextBox.Location = new System.Drawing.Point(109, 89);
            this.unloadGlassOrdertextBox.Name = "unloadGlassOrdertextBox";
            this.unloadGlassOrdertextBox.Size = new System.Drawing.Size(241, 21);
            this.unloadGlassOrdertextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "GlassID : ";
            // 
            // unloadGlassCodetextBox
            // 
            this.unloadGlassCodetextBox.Location = new System.Drawing.Point(109, 55);
            this.unloadGlassCodetextBox.Name = "unloadGlassCodetextBox";
            this.unloadGlassCodetextBox.Size = new System.Drawing.Size(241, 21);
            this.unloadGlassCodetextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "GlassCode : ";
            // 
            // unloadGlassIDtextBox
            // 
            this.unloadGlassIDtextBox.Location = new System.Drawing.Point(109, 23);
            this.unloadGlassIDtextBox.Name = "unloadGlassIDtextBox";
            this.unloadGlassIDtextBox.Size = new System.Drawing.Size(241, 21);
            this.unloadGlassIDtextBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "GlassOrder : ";
            // 
            // uwLabel
            // 
            this.uwLabel.AutoSize = true;
            this.uwLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uwLabel.ForeColor = System.Drawing.Color.Red;
            this.uwLabel.Location = new System.Drawing.Point(497, 208);
            this.uwLabel.Name = "uwLabel";
            this.uwLabel.Size = new System.Drawing.Size(112, 12);
            this.uwLabel.TabIndex = 5;
            this.uwLabel.Text = "Unload Waiting..";
            // 
            // alarmSetButton
            // 
            this.alarmSetButton.Location = new System.Drawing.Point(416, 375);
            this.alarmSetButton.Name = "alarmSetButton";
            this.alarmSetButton.Size = new System.Drawing.Size(87, 34);
            this.alarmSetButton.TabIndex = 6;
            this.alarmSetButton.Text = "Alarm Set";
            this.alarmSetButton.UseVisualStyleBackColor = true;
            this.alarmSetButton.Click += new System.EventHandler(this.alarmSetButton_Click);
            // 
            // alarmClearButton
            // 
            this.alarmClearButton.Location = new System.Drawing.Point(509, 375);
            this.alarmClearButton.Name = "alarmClearButton";
            this.alarmClearButton.Size = new System.Drawing.Size(87, 34);
            this.alarmClearButton.TabIndex = 7;
            this.alarmClearButton.Text = "Alarm Clear";
            this.alarmClearButton.UseVisualStyleBackColor = true;
            this.alarmClearButton.Click += new System.EventHandler(this.alarmClearButton_Click);
            // 
            // autoRunCheckBox
            // 
            this.autoRunCheckBox.AutoSize = true;
            this.autoRunCheckBox.Checked = true;
            this.autoRunCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRunCheckBox.Location = new System.Drawing.Point(539, 32);
            this.autoRunCheckBox.Name = "autoRunCheckBox";
            this.autoRunCheckBox.Size = new System.Drawing.Size(71, 16);
            this.autoRunCheckBox.TabIndex = 8;
            this.autoRunCheckBox.Text = "AutoRun";
            this.autoRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // alarmComboBox
            // 
            this.alarmComboBox.FormattingEnabled = true;
            this.alarmComboBox.Location = new System.Drawing.Point(299, 383);
            this.alarmComboBox.Name = "alarmComboBox";
            this.alarmComboBox.Size = new System.Drawing.Size(111, 20);
            this.alarmComboBox.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(626, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // EQPSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 423);
            this.Controls.Add(this.alarmComboBox);
            this.Controls.Add(this.autoRunCheckBox);
            this.Controls.Add(this.alarmClearButton);
            this.Controls.Add(this.alarmSetButton);
            this.Controls.Add(this.uwLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.eqpControlPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EQPSimulator";
            this.Text = "EQPSimulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EQPSimulator_FormClosing);
            this.Load += new System.EventHandler(this.EQPSimulator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}