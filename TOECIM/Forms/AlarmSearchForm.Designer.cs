
namespace TOECIM.Forms
{
    partial class AlarmSearchForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.alarmTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.IsReleaseComboBox = new System.Windows.Forms.ComboBox();
            this.ALIDTextBox = new System.Windows.Forms.TextBox();
            this.ALCDComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.alarmdataGridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsRelease = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alarmdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.alarmTypeComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.IsReleaseComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.ALIDTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.ALCDComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.startDatePicker);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.endDatePicker);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSearch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(837, 511);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.TabIndex = 0;
            // 
            // alarmTypeComboBox
            // 
            this.alarmTypeComboBox.FormattingEnabled = true;
            this.alarmTypeComboBox.Location = new System.Drawing.Point(573, 43);
            this.alarmTypeComboBox.Name = "alarmTypeComboBox";
            this.alarmTypeComboBox.Size = new System.Drawing.Size(121, 20);
            this.alarmTypeComboBox.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(486, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "Alarm Type :";
            // 
            // IsReleaseComboBox
            // 
            this.IsReleaseComboBox.FormattingEnabled = true;
            this.IsReleaseComboBox.Location = new System.Drawing.Point(573, 12);
            this.IsReleaseComboBox.Name = "IsReleaseComboBox";
            this.IsReleaseComboBox.Size = new System.Drawing.Size(121, 20);
            this.IsReleaseComboBox.TabIndex = 23;
            // 
            // ALIDTextBox
            // 
            this.ALIDTextBox.Location = new System.Drawing.Point(348, 42);
            this.ALIDTextBox.Name = "ALIDTextBox";
            this.ALIDTextBox.Size = new System.Drawing.Size(121, 21);
            this.ALIDTextBox.TabIndex = 22;
            // 
            // ALCDComboBox
            // 
            this.ALCDComboBox.FormattingEnabled = true;
            this.ALCDComboBox.Location = new System.Drawing.Point(348, 13);
            this.ALCDComboBox.Name = "ALCDComboBox";
            this.ALCDComboBox.Size = new System.Drawing.Size(121, 20);
            this.ALCDComboBox.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "Alarm ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "해제 여부 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Alarm Code :";
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "yyyy-MM-dd, HH:mm:ss";
            this.startDatePicker.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(77, 13);
            this.startDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(160, 23);
            this.startDatePicker.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "시작일자:";
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "yyyy-MM-dd, HH:mm:ss";
            this.endDatePicker.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(77, 42);
            this.endDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(160, 23);
            this.endDatePicker.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "종료일자:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonSearch.Location = new System.Drawing.Point(729, 9);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(101, 64);
            this.buttonSearch.TabIndex = 13;
            this.buttonSearch.Text = "검색";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click+= new System.EventHandler(this.buttonSearch_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.alarmdataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new System.Drawing.Size(837, 421);
            this.splitContainer2.SplitterDistance = 365;
            this.splitContainer2.TabIndex = 0;
            // 
            // alarmdataGridView
            // 
            this.alarmdataGridView.AllowUserToAddRows = false;
            this.alarmdataGridView.AllowUserToDeleteRows = false;
            this.alarmdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.alarmdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ALCD,
            this.ALID,
            this.AlarmType,
            this.ALTX,
            this.DateTime,
            this.IsRelease});
            this.alarmdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmdataGridView.Location = new System.Drawing.Point(0, 0);
            this.alarmdataGridView.Name = "alarmdataGridView";
            this.alarmdataGridView.ReadOnly = true;
            this.alarmdataGridView.RowTemplate.Height = 23;
            this.alarmdataGridView.Size = new System.Drawing.Size(837, 365);
            this.alarmdataGridView.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // ALCD
            // 
            this.ALCD.HeaderText = "ALCD";
            this.ALCD.Name = "ALCD";
            this.ALCD.ReadOnly = true;
            this.ALCD.Width = 60;
            // 
            // ALID
            // 
            this.ALID.HeaderText = "ALID";
            this.ALID.Name = "ALID";
            this.ALID.ReadOnly = true;
            // 
            // AlarmType
            // 
            this.AlarmType.HeaderText = "AlarmType";
            this.AlarmType.Name = "AlarmType";
            this.AlarmType.ReadOnly = true;
            // 
            // ALTX
            // 
            this.ALTX.HeaderText = "ALTX";
            this.ALTX.Name = "ALTX";
            this.ALTX.ReadOnly = true;
            this.ALTX.Width = 200;
            // 
            // DateTime
            // 
            this.DateTime.HeaderText = "DateTime";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Width = 160;
            // 
            // IsRelease
            // 
            this.IsRelease.HeaderText = "IsRelease";
            this.IsRelease.Name = "IsRelease";
            this.IsRelease.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(695, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 47);
            this.button1.TabIndex = 24;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AlarmSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 511);
            this.Controls.Add(this.splitContainer1);
            this.Name = "AlarmSearchForm";
            this.Text = "AlarmSearchForm";
            this.Load += new System.EventHandler(this.AlarmSearchForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.alarmdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView alarmdataGridView;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox ALCDComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox IsReleaseComboBox;
        private System.Windows.Forms.TextBox ALIDTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsRelease;
        private System.Windows.Forms.ComboBox alarmTypeComboBox;
        private System.Windows.Forms.Label label6;
    }
}