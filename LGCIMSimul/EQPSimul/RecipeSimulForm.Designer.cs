
namespace LGCIMServer.EQPSimul
{
    partial class RecipeSimulForm
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            deleteButton = new Button();
            recipeListDataGridView = new DataGridView();
            RecipeName = new DataGridViewTextBoxColumn();
            Desc = new DataGridViewTextBoxColumn();
            addButton = new Button();
            scanRecipedataGridView = new DataGridView();
            scanNoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pARAMADataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pARAMBDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pARAMCDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            recipeBindingSource = new BindingSource(components);
            cancelButton = new Button();
            saveButton = new Button();
            recipeParamPropertyGrid = new PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)recipeListDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scanRecipedataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)recipeBindingSource).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(deleteButton);
            splitContainer1.Panel1.Controls.Add(recipeListDataGridView);
            splitContainer1.Panel1.Controls.Add(addButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(scanRecipedataGridView);
            splitContainer1.Panel2.Controls.Add(cancelButton);
            splitContainer1.Panel2.Controls.Add(saveButton);
            splitContainer1.Panel2.Controls.Add(recipeParamPropertyGrid);
            splitContainer1.Size = new Size(627, 544);
            splitContainer1.SplitterDistance = 268;
            splitContainer1.TabIndex = 0;
            // 
            // deleteButton
            // 
            deleteButton.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            deleteButton.Location = new Point(122, 505);
            deleteButton.Margin = new Padding(3, 4, 3, 4);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(103, 32);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "DELETE";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // recipeListDataGridView
            // 
            recipeListDataGridView.AllowUserToAddRows = false;
            recipeListDataGridView.AllowUserToDeleteRows = false;
            recipeListDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            recipeListDataGridView.Columns.AddRange(new DataGridViewColumn[] { RecipeName, Desc });
            recipeListDataGridView.Dock = DockStyle.Top;
            recipeListDataGridView.Location = new Point(0, 0);
            recipeListDataGridView.Margin = new Padding(3, 4, 3, 4);
            recipeListDataGridView.Name = "recipeListDataGridView";
            recipeListDataGridView.ReadOnly = true;
            recipeListDataGridView.RowTemplate.Height = 23;
            recipeListDataGridView.Size = new Size(268, 497);
            recipeListDataGridView.TabIndex = 0;
            recipeListDataGridView.CellClick += recipeListDataGridView_CellClick;
            // 
            // RecipeName
            // 
            RecipeName.HeaderText = "RecipeName";
            RecipeName.Name = "RecipeName";
            RecipeName.ReadOnly = true;
            RecipeName.Width = 120;
            // 
            // Desc
            // 
            Desc.HeaderText = "Desc";
            Desc.Name = "Desc";
            Desc.ReadOnly = true;
            // 
            // addButton
            // 
            addButton.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            addButton.Location = new Point(13, 505);
            addButton.Margin = new Padding(3, 4, 3, 4);
            addButton.Name = "addButton";
            addButton.Size = new Size(103, 32);
            addButton.TabIndex = 3;
            addButton.Text = "ADD";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // scanRecipedataGridView
            // 
            scanRecipedataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scanRecipedataGridView.AutoGenerateColumns = false;
            scanRecipedataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            scanRecipedataGridView.Columns.AddRange(new DataGridViewColumn[] { scanNoDataGridViewTextBoxColumn, pARAMADataGridViewTextBoxColumn, pARAMBDataGridViewTextBoxColumn, pARAMCDataGridViewTextBoxColumn });
            scanRecipedataGridView.DataSource = recipeBindingSource;
            scanRecipedataGridView.Location = new Point(4, 253);
            scanRecipedataGridView.Margin = new Padding(3, 4, 3, 4);
            scanRecipedataGridView.Name = "scanRecipedataGridView";
            scanRecipedataGridView.RowTemplate.Height = 23;
            scanRecipedataGridView.Size = new Size(351, 244);
            scanRecipedataGridView.TabIndex = 3;
            // 
            // scanNoDataGridViewTextBoxColumn
            // 
            scanNoDataGridViewTextBoxColumn.DataPropertyName = "ScanNo";
            scanNoDataGridViewTextBoxColumn.HeaderText = "ScanNo";
            scanNoDataGridViewTextBoxColumn.Name = "scanNoDataGridViewTextBoxColumn";
            scanNoDataGridViewTextBoxColumn.Width = 60;
            // 
            // pARAMADataGridViewTextBoxColumn
            // 
            pARAMADataGridViewTextBoxColumn.DataPropertyName = "PARAM_A";
            pARAMADataGridViewTextBoxColumn.HeaderText = "PARAM_A";
            pARAMADataGridViewTextBoxColumn.Name = "pARAMADataGridViewTextBoxColumn";
            // 
            // pARAMBDataGridViewTextBoxColumn
            // 
            pARAMBDataGridViewTextBoxColumn.DataPropertyName = "PARAM_B";
            pARAMBDataGridViewTextBoxColumn.HeaderText = "PARAM_B";
            pARAMBDataGridViewTextBoxColumn.Name = "pARAMBDataGridViewTextBoxColumn";
            // 
            // pARAMCDataGridViewTextBoxColumn
            // 
            pARAMCDataGridViewTextBoxColumn.DataPropertyName = "PARAM_C";
            pARAMCDataGridViewTextBoxColumn.HeaderText = "PARAM_C";
            pARAMCDataGridViewTextBoxColumn.Name = "pARAMCDataGridViewTextBoxColumn";
            // 
            // recipeBindingSource
            // 
            recipeBindingSource.DataSource = typeof(TOECIMLib.CIMRecipe);
            // 
            // cancelButton
            // 
            cancelButton.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            cancelButton.Location = new Point(235, 508);
            cancelButton.Margin = new Padding(3, 4, 3, 4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(103, 35);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // saveButton
            // 
            saveButton.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            saveButton.Location = new Point(126, 508);
            saveButton.Margin = new Padding(3, 4, 3, 4);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(103, 35);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // recipeParamPropertyGrid
            // 
            recipeParamPropertyGrid.Dock = DockStyle.Top;
            recipeParamPropertyGrid.Location = new Point(0, 0);
            recipeParamPropertyGrid.Margin = new Padding(3, 4, 3, 4);
            recipeParamPropertyGrid.Name = "recipeParamPropertyGrid";
            recipeParamPropertyGrid.Size = new Size(355, 247);
            recipeParamPropertyGrid.TabIndex = 0;
            // 
            // RecipeSimulForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(641, 580);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "RecipeSimulForm";
            Text = "RecipeSimulator";
            Load += ConsoleSimulator_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)recipeListDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)scanRecipedataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)recipeBindingSource).EndInit();
            ResumeLayout(false);

        }

        #endregion

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