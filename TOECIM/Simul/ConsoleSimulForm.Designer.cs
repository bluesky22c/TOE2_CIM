
namespace TOECIM.Forms
{
    partial class ConsoleSimulForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.deleteButton = new System.Windows.Forms.Button();
            this.recipeListDataGridView = new System.Windows.Forms.DataGridView();
            this.RecipeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.scanRecipedataGridView = new System.Windows.Forms.DataGridView();
            this.scanNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.recipeParamPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.eqpStatusLabel = new System.Windows.Forms.Label();
            this.glassInCheckBox = new System.Windows.Forms.CheckBox();
            this.alarmComboBox = new System.Windows.Forms.ComboBox();
            this.autoRunCheckBox = new System.Windows.Forms.CheckBox();
            this.alarmClearButton = new System.Windows.Forms.Button();
            this.alarmSetButton = new System.Windows.Forms.Button();
            this.uwLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.unloadGlassOrdertextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.unloadGlassCodetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.unloadGlassIDtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadGlassOrdertextBox = new System.Windows.Forms.TextBox();
            this.loadGlassCodetextBox = new System.Windows.Forms.TextBox();
            this.loadGlassIDtextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.eqpControlPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeListDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanRecipedataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 516);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 490);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Recipe";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel1.Controls.Add(this.recipeListDataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.addButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scanRecipedataGridView);
            this.splitContainer1.Panel2.Controls.Add(this.cancelButton);
            this.splitContainer1.Panel2.Controls.Add(this.saveButton);
            this.splitContainer1.Panel2.Controls.Add(this.recipeParamPropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(758, 484);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.deleteButton.Location = new System.Drawing.Point(109, 435);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(103, 42);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // recipeListDataGridView
            // 
            this.recipeListDataGridView.AllowUserToAddRows = false;
            this.recipeListDataGridView.AllowUserToDeleteRows = false;
            this.recipeListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipeListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecipeName,
            this.Desc});
            this.recipeListDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.recipeListDataGridView.Location = new System.Drawing.Point(0, 0);
            this.recipeListDataGridView.Name = "recipeListDataGridView";
            this.recipeListDataGridView.ReadOnly = true;
            this.recipeListDataGridView.RowTemplate.Height = 23;
            this.recipeListDataGridView.Size = new System.Drawing.Size(325, 431);
            this.recipeListDataGridView.TabIndex = 0;
            this.recipeListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.recipeListDataGridView_CellClick);
            // 
            // RecipeName
            // 
            this.RecipeName.HeaderText = "RecipeName";
            this.RecipeName.Name = "RecipeName";
            this.RecipeName.ReadOnly = true;
            this.RecipeName.Width = 120;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Desc";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.addButton.Location = new System.Drawing.Point(0, 435);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(103, 42);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // scanRecipedataGridView
            // 
            this.scanRecipedataGridView.AutoGenerateColumns = false;
            this.scanRecipedataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scanRecipedataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.scanNoDataGridViewTextBoxColumn,
            this.pARAMADataGridViewTextBoxColumn,
            this.pARAMBDataGridViewTextBoxColumn,
            this.pARAMCDataGridViewTextBoxColumn});
            this.scanRecipedataGridView.DataSource = this.recipeBindingSource;
            this.scanRecipedataGridView.Location = new System.Drawing.Point(2, 236);
            this.scanRecipedataGridView.Name = "scanRecipedataGridView";
            this.scanRecipedataGridView.RowTemplate.Height = 23;
            this.scanRecipedataGridView.Size = new System.Drawing.Size(427, 195);
            this.scanRecipedataGridView.TabIndex = 3;
            // 
            // scanNoDataGridViewTextBoxColumn
            // 
            this.scanNoDataGridViewTextBoxColumn.DataPropertyName = "ScanNo";
            this.scanNoDataGridViewTextBoxColumn.HeaderText = "ScanNo";
            this.scanNoDataGridViewTextBoxColumn.Name = "scanNoDataGridViewTextBoxColumn";
            this.scanNoDataGridViewTextBoxColumn.Width = 60;
            // 
            // pARAMADataGridViewTextBoxColumn
            // 
            this.pARAMADataGridViewTextBoxColumn.DataPropertyName = "PARAM_A";
            this.pARAMADataGridViewTextBoxColumn.HeaderText = "PARAM_A";
            this.pARAMADataGridViewTextBoxColumn.Name = "pARAMADataGridViewTextBoxColumn";
            // 
            // pARAMBDataGridViewTextBoxColumn
            // 
            this.pARAMBDataGridViewTextBoxColumn.DataPropertyName = "PARAM_B";
            this.pARAMBDataGridViewTextBoxColumn.HeaderText = "PARAM_B";
            this.pARAMBDataGridViewTextBoxColumn.Name = "pARAMBDataGridViewTextBoxColumn";
            // 
            // pARAMCDataGridViewTextBoxColumn
            // 
            this.pARAMCDataGridViewTextBoxColumn.DataPropertyName = "PARAM_C";
            this.pARAMCDataGridViewTextBoxColumn.HeaderText = "PARAM_C";
            this.pARAMCDataGridViewTextBoxColumn.Name = "pARAMCDataGridViewTextBoxColumn";
            // 
            // recipeBindingSource
            // 
            this.recipeBindingSource.DataSource = typeof(TOECIMLib.CIMRecipe);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelButton.Location = new System.Drawing.Point(324, 435);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(103, 42);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveButton.Location = new System.Drawing.Point(215, 435);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(103, 42);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // recipeParamPropertyGrid
            // 
            this.recipeParamPropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.recipeParamPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.recipeParamPropertyGrid.Name = "recipeParamPropertyGrid";
            this.recipeParamPropertyGrid.Size = new System.Drawing.Size(429, 230);
            this.recipeParamPropertyGrid.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.eqpStatusLabel);
            this.tabPage2.Controls.Add(this.glassInCheckBox);
            this.tabPage2.Controls.Add(this.alarmComboBox);
            this.tabPage2.Controls.Add(this.autoRunCheckBox);
            this.tabPage2.Controls.Add(this.alarmClearButton);
            this.tabPage2.Controls.Add(this.alarmSetButton);
            this.tabPage2.Controls.Add(this.uwLabel);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.eqpControlPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Flow";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // eqpStatusLabel
            // 
            this.eqpStatusLabel.AutoSize = true;
            this.eqpStatusLabel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.eqpStatusLabel.ForeColor = System.Drawing.Color.Blue;
            this.eqpStatusLabel.Location = new System.Drawing.Point(205, 29);
            this.eqpStatusLabel.Name = "eqpStatusLabel";
            this.eqpStatusLabel.Size = new System.Drawing.Size(102, 15);
            this.eqpStatusLabel.TabIndex = 17;
            this.eqpStatusLabel.Text = "EQPStatus : IDLE";
            // 
            // glassInCheckBox
            // 
            this.glassInCheckBox.AutoSize = true;
            this.glassInCheckBox.Location = new System.Drawing.Point(411, 30);
            this.glassInCheckBox.Name = "glassInCheckBox";
            this.glassInCheckBox.Size = new System.Drawing.Size(67, 16);
            this.glassInCheckBox.TabIndex = 21;
            this.glassInCheckBox.Text = "GlassIn";
            this.glassInCheckBox.UseVisualStyleBackColor = true;
            // 
            // alarmComboBox
            // 
            this.alarmComboBox.FormattingEnabled = true;
            this.alarmComboBox.Location = new System.Drawing.Point(253, 398);
            this.alarmComboBox.Name = "alarmComboBox";
            this.alarmComboBox.Size = new System.Drawing.Size(111, 20);
            this.alarmComboBox.TabIndex = 20;
            // 
            // autoRunCheckBox
            // 
            this.autoRunCheckBox.AutoSize = true;
            this.autoRunCheckBox.Checked = true;
            this.autoRunCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRunCheckBox.Location = new System.Drawing.Point(493, 30);
            this.autoRunCheckBox.Name = "autoRunCheckBox";
            this.autoRunCheckBox.Size = new System.Drawing.Size(71, 16);
            this.autoRunCheckBox.TabIndex = 19;
            this.autoRunCheckBox.Text = "AutoRun";
            this.autoRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // alarmClearButton
            // 
            this.alarmClearButton.Location = new System.Drawing.Point(463, 390);
            this.alarmClearButton.Name = "alarmClearButton";
            this.alarmClearButton.Size = new System.Drawing.Size(87, 34);
            this.alarmClearButton.TabIndex = 18;
            this.alarmClearButton.Text = "Alarm Clear";
            this.alarmClearButton.UseVisualStyleBackColor = true;
            // 
            // alarmSetButton
            // 
            this.alarmSetButton.Location = new System.Drawing.Point(370, 390);
            this.alarmSetButton.Name = "alarmSetButton";
            this.alarmSetButton.Size = new System.Drawing.Size(87, 34);
            this.alarmSetButton.TabIndex = 16;
            this.alarmSetButton.Text = "Alarm Set";
            this.alarmSetButton.UseVisualStyleBackColor = true;
            // 
            // uwLabel
            // 
            this.uwLabel.AutoSize = true;
            this.uwLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uwLabel.ForeColor = System.Drawing.Color.Red;
            this.uwLabel.Location = new System.Drawing.Point(451, 223);
            this.uwLabel.Name = "uwLabel";
            this.uwLabel.Size = new System.Drawing.Size(112, 12);
            this.uwLabel.TabIndex = 15;
            this.uwLabel.Text = "Unload Waiting..";
            this.uwLabel.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.unloadGlassOrdertextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.unloadGlassCodetextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.unloadGlassIDtextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(209, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 125);
            this.groupBox2.TabIndex = 14;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadGlassOrdertextBox);
            this.groupBox1.Controls.Add(this.loadGlassCodetextBox);
            this.groupBox1.Controls.Add(this.loadGlassIDtextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(209, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 126);
            this.groupBox1.TabIndex = 13;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "GlassID : ";
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
            // eqpControlPanel
            // 
            this.eqpControlPanel.Location = new System.Drawing.Point(8, 19);
            this.eqpControlPanel.Name = "eqpControlPanel";
            this.eqpControlPanel.Size = new System.Drawing.Size(129, 439);
            this.eqpControlPanel.TabIndex = 12;
            // 
            // ConsoleSimulForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 516);
            this.Controls.Add(this.tabControl1);
            this.Name = "ConsoleSimulForm";
            this.Text = "ConsoleSimulator";
            this.Load += new System.EventHandler(this.ConsoleSimulator_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recipeListDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanRecipedataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView recipeListDataGridView;
        private System.Windows.Forms.PropertyGrid recipeParamPropertyGrid;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridView scanRecipedataGridView;
        private System.Windows.Forms.BindingSource recipeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn scanNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMCDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label eqpStatusLabel;
        private System.Windows.Forms.CheckBox glassInCheckBox;
        private System.Windows.Forms.ComboBox alarmComboBox;
        private System.Windows.Forms.CheckBox autoRunCheckBox;
        private System.Windows.Forms.Button alarmClearButton;
        private System.Windows.Forms.Button alarmSetButton;
        private System.Windows.Forms.Label uwLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox unloadGlassOrdertextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox unloadGlassCodetextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox unloadGlassIDtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox loadGlassOrdertextBox;
        private System.Windows.Forms.TextBox loadGlassCodetextBox;
        private System.Windows.Forms.TextBox loadGlassIDtextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel eqpControlPanel;
    }
}