
namespace LGCIMServer.Forms
{
    partial class RecipeSearchForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.recipSetDataGridView = new System.Windows.Forms.DataGridView();
            this.rECIPEIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scanRecipePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.bulitButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.curReipceLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.s7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ePPD조회S7F19ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ePPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupInRecipeID조회S7F69ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeBody조회S7F77ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.현재RecipeIDList조회S15F65ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.레시피조회S15F71ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.하위레시피조회S15F73ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.레시피파라미터조회S15F75ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanRecipeDataGridView = new System.Windows.Forms.DataGridView();
            this.scanNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recipeConfirmButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.streamTextBox = new System.Windows.Forms.TextBox();
            this.functionTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.recipSetDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeSetBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scanRecipeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(646, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // recipSetDataGridView
            // 
            this.recipSetDataGridView.AutoGenerateColumns = false;
            this.recipSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipSetDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rECIPEIDDataGridViewTextBoxColumn,
            this.dESCDataGridViewTextBoxColumn});
            this.recipSetDataGridView.DataSource = this.recipeSetBindingSource;
            this.recipSetDataGridView.Location = new System.Drawing.Point(12, 80);
            this.recipSetDataGridView.Name = "recipSetDataGridView";
            this.recipSetDataGridView.RowTemplate.Height = 23;
            this.recipSetDataGridView.Size = new System.Drawing.Size(306, 407);
            this.recipSetDataGridView.TabIndex = 9;
            this.recipSetDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.recipSetDataGridView_CellClick);
            // 
            // rECIPEIDDataGridViewTextBoxColumn
            // 
            this.rECIPEIDDataGridViewTextBoxColumn.DataPropertyName = "RECIPEID";
            this.rECIPEIDDataGridViewTextBoxColumn.HeaderText = "RECIPEID";
            this.rECIPEIDDataGridViewTextBoxColumn.Name = "rECIPEIDDataGridViewTextBoxColumn";
            this.rECIPEIDDataGridViewTextBoxColumn.Width = 110;
            // 
            // dESCDataGridViewTextBoxColumn
            // 
            this.dESCDataGridViewTextBoxColumn.DataPropertyName = "DESC";
            this.dESCDataGridViewTextBoxColumn.HeaderText = "DESC";
            this.dESCDataGridViewTextBoxColumn.Name = "dESCDataGridViewTextBoxColumn";
            // 
            // recipeSetBindingSource
            // 
            this.recipeSetBindingSource.DataSource = typeof(TOECIMLib.RecipeSet);
            // 
            // scanRecipePropertyGrid
            // 
            this.scanRecipePropertyGrid.Location = new System.Drawing.Point(324, 80);
            this.scanRecipePropertyGrid.Name = "scanRecipePropertyGrid";
            this.scanRecipePropertyGrid.Size = new System.Drawing.Size(405, 226);
            this.scanRecipePropertyGrid.TabIndex = 10;
            // 
            // bulitButton
            // 
            this.bulitButton.Location = new System.Drawing.Point(11, 493);
            this.bulitButton.Name = "bulitButton";
            this.bulitButton.Size = new System.Drawing.Size(72, 44);
            this.bulitButton.TabIndex = 11;
            this.bulitButton.Text = "Built";
            this.bulitButton.UseVisualStyleBackColor = true;
            this.bulitButton.Click += new System.EventHandler(this.bulitButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(90, 493);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(72, 44);
            this.changeButton.TabIndex = 12;
            this.changeButton.Text = "Change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(168, 493);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(72, 44);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(246, 493);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(72, 44);
            this.renameButton.TabIndex = 14;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // curReipceLabel
            // 
            this.curReipceLabel.AutoSize = true;
            this.curReipceLabel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.curReipceLabel.ForeColor = System.Drawing.Color.Red;
            this.curReipceLabel.Location = new System.Drawing.Point(12, 42);
            this.curReipceLabel.Name = "curReipceLabel";
            this.curReipceLabel.Size = new System.Drawing.Size(77, 15);
            this.curReipceLabel.TabIndex = 15;
            this.curReipceLabel.Text = "CurRecipe : ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.s7ToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1074, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // s7ToolStripMenuItem
            // 
            this.s7ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ePPD조회S7F19ToolStripMenuItem,
            this.ePPGToolStripMenuItem,
            this.groupInRecipeID조회S7F69ToolStripMenuItem,
            this.recipeBody조회S7F77ToolStripMenuItem});
            this.s7ToolStripMenuItem.Name = "s7ToolStripMenuItem";
            this.s7ToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.s7ToolStripMenuItem.Text = "S7";
            // 
            // ePPD조회S7F19ToolStripMenuItem
            // 
            this.ePPD조회S7F19ToolStripMenuItem.Name = "ePPD조회S7F19ToolStripMenuItem";
            this.ePPD조회S7F19ToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.ePPD조회S7F19ToolStripMenuItem.Text = "EPPD 조회(S7F19)";
            this.ePPD조회S7F19ToolStripMenuItem.Click += new System.EventHandler(this.ePPD조회S7F19ToolStripMenuItem_Click);
            // 
            // ePPGToolStripMenuItem
            // 
            this.ePPGToolStripMenuItem.Name = "ePPGToolStripMenuItem";
            this.ePPGToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.ePPGToolStripMenuItem.Text = "GroupID 조회(S7F67)";
            this.ePPGToolStripMenuItem.Click += new System.EventHandler(this.ePPGToolStripMenuItem_Click);
            // 
            // groupInRecipeID조회S7F69ToolStripMenuItem
            // 
            this.groupInRecipeID조회S7F69ToolStripMenuItem.Name = "groupInRecipeID조회S7F69ToolStripMenuItem";
            this.groupInRecipeID조회S7F69ToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.groupInRecipeID조회S7F69ToolStripMenuItem.Text = "Group in RecipeID 조회(S7F69)";
            this.groupInRecipeID조회S7F69ToolStripMenuItem.Click += new System.EventHandler(this.groupInRecipeID조회S7F69ToolStripMenuItem_Click);
            // 
            // recipeBody조회S7F77ToolStripMenuItem
            // 
            this.recipeBody조회S7F77ToolStripMenuItem.Name = "recipeBody조회S7F77ToolStripMenuItem";
            this.recipeBody조회S7F77ToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.recipeBody조회S7F77ToolStripMenuItem.Text = "Recipe Body 조회(S7F77)";
            this.recipeBody조회S7F77ToolStripMenuItem.Click += new System.EventHandler(this.recipeBody조회S7F77ToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.현재RecipeIDList조회S15F65ToolStripMenuItem,
            this.레시피조회S15F71ToolStripMenuItem,
            this.하위레시피조회S15F73ToolStripMenuItem,
            this.레시피파라미터조회S15F75ToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.reportToolStripMenuItem.Text = "S15";
            // 
            // 현재RecipeIDList조회S15F65ToolStripMenuItem
            // 
            this.현재RecipeIDList조회S15F65ToolStripMenuItem.Name = "현재RecipeIDList조회S15F65ToolStripMenuItem";
            this.현재RecipeIDList조회S15F65ToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.현재RecipeIDList조회S15F65ToolStripMenuItem.Text = "현재 RecipeID List 조회(S15F65)";
            this.현재RecipeIDList조회S15F65ToolStripMenuItem.Click += new System.EventHandler(this.현재RecipeIDList조회S15F65ToolStripMenuItem_Click);
            // 
            // 레시피조회S15F71ToolStripMenuItem
            // 
            this.레시피조회S15F71ToolStripMenuItem.Name = "레시피조회S15F71ToolStripMenuItem";
            this.레시피조회S15F71ToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.레시피조회S15F71ToolStripMenuItem.Text = "레시피 조회(S15F71)";
            this.레시피조회S15F71ToolStripMenuItem.Click += new System.EventHandler(this.레시피조회S15F71ToolStripMenuItem_Click);
            // 
            // 하위레시피조회S15F73ToolStripMenuItem
            // 
            this.하위레시피조회S15F73ToolStripMenuItem.Name = "하위레시피조회S15F73ToolStripMenuItem";
            this.하위레시피조회S15F73ToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.하위레시피조회S15F73ToolStripMenuItem.Text = "하위 레시피 조회(S15F73)";
            this.하위레시피조회S15F73ToolStripMenuItem.Click += new System.EventHandler(this.하위레시피조회S15F73ToolStripMenuItem_Click);
            // 
            // 레시피파라미터조회S15F75ToolStripMenuItem
            // 
            this.레시피파라미터조회S15F75ToolStripMenuItem.Name = "레시피파라미터조회S15F75ToolStripMenuItem";
            this.레시피파라미터조회S15F75ToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.레시피파라미터조회S15F75ToolStripMenuItem.Text = "레시피 파라미터 조회(S15F75)";
            this.레시피파라미터조회S15F75ToolStripMenuItem.Click += new System.EventHandler(this.레시피파라미터조회S15F75ToolStripMenuItem_Click);
            // 
            // scanRecipeDataGridView
            // 
            this.scanRecipeDataGridView.AutoGenerateColumns = false;
            this.scanRecipeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scanRecipeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.scanNoDataGridViewTextBoxColumn,
            this.pARAMADataGridViewTextBoxColumn,
            this.pARAMBDataGridViewTextBoxColumn,
            this.pARAMCDataGridViewTextBoxColumn});
            this.scanRecipeDataGridView.DataSource = this.recipeBindingSource;
            this.scanRecipeDataGridView.Location = new System.Drawing.Point(324, 312);
            this.scanRecipeDataGridView.Name = "scanRecipeDataGridView";
            this.scanRecipeDataGridView.RowTemplate.Height = 23;
            this.scanRecipeDataGridView.Size = new System.Drawing.Size(405, 174);
            this.scanRecipeDataGridView.TabIndex = 17;
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
            // recipeConfirmButton
            // 
            this.recipeConfirmButton.Location = new System.Drawing.Point(333, 495);
            this.recipeConfirmButton.Name = "recipeConfirmButton";
            this.recipeConfirmButton.Size = new System.Drawing.Size(75, 43);
            this.recipeConfirmButton.TabIndex = 18;
            this.recipeConfirmButton.Text = "Confirm";
            this.recipeConfirmButton.UseVisualStyleBackColor = true;
            this.recipeConfirmButton.Click += new System.EventHandler(this.recipeConfirmButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(511, 495);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(94, 41);
            this.sendButton.TabIndex = 19;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(738, 60);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(336, 478);
            this.textBox1.TabIndex = 20;
            this.textBox1.WordWrap = false;
            // 
            // streamTextBox
            // 
            this.streamTextBox.Location = new System.Drawing.Point(738, 36);
            this.streamTextBox.Name = "streamTextBox";
            this.streamTextBox.Size = new System.Drawing.Size(84, 21);
            this.streamTextBox.TabIndex = 21;
            // 
            // functionTextBox
            // 
            this.functionTextBox.Location = new System.Drawing.Point(844, 36);
            this.functionTextBox.Name = "functionTextBox";
            this.functionTextBox.Size = new System.Drawing.Size(86, 21);
            this.functionTextBox.TabIndex = 21;
            // 
            // RecipeSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 548);
            this.Controls.Add(this.functionTextBox);
            this.Controls.Add(this.streamTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.recipeConfirmButton);
            this.Controls.Add(this.scanRecipeDataGridView);
            this.Controls.Add(this.curReipceLabel);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.bulitButton);
            this.Controls.Add(this.scanRecipePropertyGrid);
            this.Controls.Add(this.recipSetDataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RecipeSearchForm";
            this.Text = "RecipeSearchForm";
            this.Load += new System.EventHandler(this.RecipeSearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.recipSetDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeSetBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scanRecipeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView recipSetDataGridView;
        private System.Windows.Forms.PropertyGrid scanRecipePropertyGrid;
        private System.Windows.Forms.Button bulitButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn rECIPEIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource recipeSetBindingSource;
        private System.Windows.Forms.Label curReipceLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 레시피조회S15F71ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 하위레시피조회S15F73ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 레시피파라미터조회S15F75ToolStripMenuItem;
        private System.Windows.Forms.DataGridView scanRecipeDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn scanNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMCDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource recipeBindingSource;
        private System.Windows.Forms.ToolStripMenuItem 현재RecipeIDList조회S15F65ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ePPD조회S7F19ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ePPGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupInRecipeID조회S7F69ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeBody조회S7F77ToolStripMenuItem;
        private System.Windows.Forms.Button recipeConfirmButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox streamTextBox;
        private System.Windows.Forms.TextBox functionTextBox;
    }
}