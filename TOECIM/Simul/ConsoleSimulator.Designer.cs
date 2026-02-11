
namespace TOECIM.Forms
{
    partial class ConsoleSimulator
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeListDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanRecipedataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(772, 514);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 488);
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
            this.splitContainer1.Size = new System.Drawing.Size(758, 482);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ConsoleSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 514);
            this.Controls.Add(this.tabControl1);
            this.Name = "ConsoleSimulator";
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
    }
}