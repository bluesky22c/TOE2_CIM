
namespace LGCIM
{
    partial class VersionUpdateForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.versionDataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.SerchButton = new System.Windows.Forms.Button();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.versionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // versionDataGridView
            // 
            this.versionDataGridView.AllowUserToAddRows = false;
            this.versionDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.versionDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.versionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.versionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.versionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Date,
            this.Version,
            this.Operator,
            this.UpdateContent});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.versionDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.versionDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.versionDataGridView.Location = new System.Drawing.Point(0, 122);
            this.versionDataGridView.MultiSelect = false;
            this.versionDataGridView.Name = "versionDataGridView";
            this.versionDataGridView.ReadOnly = true;
            this.versionDataGridView.RowHeadersVisible = false;
            this.versionDataGridView.RowTemplate.Height = 23;
            this.versionDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.versionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.versionDataGridView.Size = new System.Drawing.Size(939, 474);
            this.versionDataGridView.TabIndex = 0;
            this.versionDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.versionDataGridView_CellMouseDoubleClick);
            // 
            // No
            // 
            this.No.HeaderText = "작업 번호";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Date
            // 
            this.Date.HeaderText = "작업 일자";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Date.Width = 200;
            // 
            // Version
            // 
            this.Version.HeaderText = "작업 버전";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Version.Width = 150;
            // 
            // Operator
            // 
            this.Operator.HeaderText = "작업자";
            this.Operator.Name = "Operator";
            this.Operator.ReadOnly = true;
            this.Operator.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Operator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operator.Width = 120;
            // 
            // UpdateContent
            // 
            this.UpdateContent.HeaderText = "작업내용";
            this.UpdateContent.Name = "UpdateContent";
            this.UpdateContent.ReadOnly = true;
            this.UpdateContent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UpdateContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UpdateContent.Width = 370;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(734, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 50);
            this.button1.TabIndex = 30;
            this.button1.Text = "닫  기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(734, 11);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 50);
            this.addButton.TabIndex = 31;
            this.addButton.Text = "추  가";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(834, 11);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 50);
            this.deleteButton.TabIndex = 32;
            this.deleteButton.Text = "삭  제";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // startDatePicker
            // 
            this.startDatePicker.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(72, 13);
            this.startDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(170, 23);
            this.startDatePicker.TabIndex = 33;
            this.startDatePicker.Value = new System.DateTime(2023, 7, 18, 0, 0, 0, 0);
            // 
            // EndDatePicker
            // 
            this.EndDatePicker.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.EndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDatePicker.Location = new System.Drawing.Point(72, 38);
            this.EndDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.EndDatePicker.MinDate = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            this.EndDatePicker.Name = "EndDatePicker";
            this.EndDatePicker.Size = new System.Drawing.Size(170, 23);
            this.EndDatePicker.TabIndex = 34;
            this.EndDatePicker.Value = new System.DateTime(2023, 7, 18, 0, 0, 0, 0);
            // 
            // SerchButton
            // 
            this.SerchButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerchButton.Location = new System.Drawing.Point(248, 12);
            this.SerchButton.Name = "SerchButton";
            this.SerchButton.Size = new System.Drawing.Size(150, 50);
            this.SerchButton.TabIndex = 35;
            this.SerchButton.Text = "검색";
            this.SerchButton.UseVisualStyleBackColor = true;
            this.SerchButton.Click += new System.EventHandler(this.SerchButton_Click);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshbutton.Location = new System.Drawing.Point(248, 63);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(150, 50);
            this.refreshbutton.TabIndex = 36;
            this.refreshbutton.Text = "새로 고침";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 37;
            this.label4.Text = "시작일자:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "종료일자:";
            // 
            // VersionUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 596);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.SerchButton);
            this.Controls.Add(this.EndDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.versionDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "VersionUpdateForm";
            this.Text = "VersionUpdateForm";
            this.Load += new System.EventHandler(this.VersionUpdateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.versionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView versionDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker EndDatePicker;
        private System.Windows.Forms.Button SerchButton;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}