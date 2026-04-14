namespace LGCIM.Forms
{
    partial class AlarmForm
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
            this.components = new System.ComponentModel.Container();
            this.OKbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.curAlarmDataGridView = new System.Windows.Forms.DataGridView();
            this.clearButton = new System.Windows.Forms.Button();
            this.alarmInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aLCDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aLTXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReleaseDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.curAlarmDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alarmInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // OKbutton
            // 
            this.OKbutton.BackColor = System.Drawing.Color.Gainsboro;
            this.OKbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OKbutton.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OKbutton.Location = new System.Drawing.Point(515, 328);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(144, 48);
            this.OKbutton.TabIndex = 60;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = false;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(236, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 65);
            this.label1.TabIndex = 62;
            this.label1.Text = "ALARM";
            // 
            // curAlarmDataGridView
            // 
            this.curAlarmDataGridView.AllowUserToAddRows = false;
            this.curAlarmDataGridView.AllowUserToDeleteRows = false;
            this.curAlarmDataGridView.AutoGenerateColumns = false;
            this.curAlarmDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.curAlarmDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.aLCDDataGridViewTextBoxColumn,
            this.aLIDDataGridViewTextBoxColumn,
            this.alarmTypeDataGridViewTextBoxColumn,
            this.aLTXDataGridViewTextBoxColumn,
            this.dateTimeDataGridViewTextBoxColumn,
            this.isReleaseDataGridViewCheckBoxColumn});
            this.curAlarmDataGridView.DataSource = this.alarmInfoBindingSource;
            this.curAlarmDataGridView.Location = new System.Drawing.Point(12, 85);
            this.curAlarmDataGridView.Name = "curAlarmDataGridView";
            this.curAlarmDataGridView.ReadOnly = true;
            this.curAlarmDataGridView.RowTemplate.Height = 23;
            this.curAlarmDataGridView.Size = new System.Drawing.Size(647, 215);
            this.curAlarmDataGridView.TabIndex = 63;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.Gainsboro;
            this.clearButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearButton.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.clearButton.ForeColor = System.Drawing.Color.Red;
            this.clearButton.Location = new System.Drawing.Point(365, 328);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(144, 48);
            this.clearButton.TabIndex = 64;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // alarmInfoBindingSource
            // 
            this.alarmInfoBindingSource.DataSource = typeof(LGCIM.Lib.AlarmInfo);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 40;
            // 
            // aLCDDataGridViewTextBoxColumn
            // 
            this.aLCDDataGridViewTextBoxColumn.DataPropertyName = "ALCD";
            this.aLCDDataGridViewTextBoxColumn.HeaderText = "ALCD";
            this.aLCDDataGridViewTextBoxColumn.Name = "aLCDDataGridViewTextBoxColumn";
            this.aLCDDataGridViewTextBoxColumn.ReadOnly = true;
            this.aLCDDataGridViewTextBoxColumn.Width = 60;
            // 
            // aLIDDataGridViewTextBoxColumn
            // 
            this.aLIDDataGridViewTextBoxColumn.DataPropertyName = "ALID";
            this.aLIDDataGridViewTextBoxColumn.HeaderText = "ALID";
            this.aLIDDataGridViewTextBoxColumn.Name = "aLIDDataGridViewTextBoxColumn";
            this.aLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // alarmTypeDataGridViewTextBoxColumn
            // 
            this.alarmTypeDataGridViewTextBoxColumn.DataPropertyName = "AlarmType";
            this.alarmTypeDataGridViewTextBoxColumn.HeaderText = "AlarmType";
            this.alarmTypeDataGridViewTextBoxColumn.Name = "alarmTypeDataGridViewTextBoxColumn";
            this.alarmTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.alarmTypeDataGridViewTextBoxColumn.Width = 90;
            // 
            // aLTXDataGridViewTextBoxColumn
            // 
            this.aLTXDataGridViewTextBoxColumn.DataPropertyName = "ALTX";
            this.aLTXDataGridViewTextBoxColumn.HeaderText = "ALTX";
            this.aLTXDataGridViewTextBoxColumn.Name = "aLTXDataGridViewTextBoxColumn";
            this.aLTXDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            this.dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            this.dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
            this.dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            this.dateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateTimeDataGridViewTextBoxColumn.Width = 110;
            // 
            // isReleaseDataGridViewCheckBoxColumn
            // 
            this.isReleaseDataGridViewCheckBoxColumn.DataPropertyName = "IsRelease";
            this.isReleaseDataGridViewCheckBoxColumn.HeaderText = "IsRelease";
            this.isReleaseDataGridViewCheckBoxColumn.Name = "isReleaseDataGridViewCheckBoxColumn";
            this.isReleaseDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // AlarmForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(671, 388);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.curAlarmDataGridView);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "AlarmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alarm";
            this.Load += new System.EventHandler(this.AlarmForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.curAlarmDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alarmInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView curAlarmDataGridView;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLCDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarmTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLTXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isReleaseDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource alarmInfoBindingSource;
    }
}