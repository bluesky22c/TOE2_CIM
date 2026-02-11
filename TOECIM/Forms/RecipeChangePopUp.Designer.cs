
namespace TOECIM.Forms
{
    partial class RecipeChangePopUp
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.recipeIDLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.scanNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pARAMCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timerLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(135, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(346, 32);
            this.titleLabel.TabIndex = 63;
            this.titleLabel.Text = "Recipe Changed (From Host)";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(469, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 50);
            this.button1.TabIndex = 64;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // recipeIDLabel
            // 
            this.recipeIDLabel.AutoSize = true;
            this.recipeIDLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.recipeIDLabel.Location = new System.Drawing.Point(4, 69);
            this.recipeIDLabel.Name = "recipeIDLabel";
            this.recipeIDLabel.Size = new System.Drawing.Size(99, 21);
            this.recipeIDLabel.TabIndex = 66;
            this.recipeIDLabel.Text = "Recipe ID : ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.scanNoDataGridViewTextBoxColumn,
            this.pARAMADataGridViewTextBoxColumn,
            this.pARAMBDataGridViewTextBoxColumn,
            this.pARAMCDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.recipeBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(600, 219);
            this.dataGridView1.TabIndex = 67;
            // 
            // scanNoDataGridViewTextBoxColumn
            // 
            this.scanNoDataGridViewTextBoxColumn.DataPropertyName = "ScanNo";
            this.scanNoDataGridViewTextBoxColumn.HeaderText = "ScanNo";
            this.scanNoDataGridViewTextBoxColumn.Name = "scanNoDataGridViewTextBoxColumn";
            this.scanNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pARAMADataGridViewTextBoxColumn
            // 
            this.pARAMADataGridViewTextBoxColumn.DataPropertyName = "PARAM_A";
            this.pARAMADataGridViewTextBoxColumn.HeaderText = "PARAM_A";
            this.pARAMADataGridViewTextBoxColumn.Name = "pARAMADataGridViewTextBoxColumn";
            this.pARAMADataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pARAMBDataGridViewTextBoxColumn
            // 
            this.pARAMBDataGridViewTextBoxColumn.DataPropertyName = "PARAM_B";
            this.pARAMBDataGridViewTextBoxColumn.HeaderText = "PARAM_B";
            this.pARAMBDataGridViewTextBoxColumn.Name = "pARAMBDataGridViewTextBoxColumn";
            this.pARAMBDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pARAMCDataGridViewTextBoxColumn
            // 
            this.pARAMCDataGridViewTextBoxColumn.DataPropertyName = "PARAM_C";
            this.pARAMCDataGridViewTextBoxColumn.HeaderText = "PARAM_C";
            this.pARAMCDataGridViewTextBoxColumn.Name = "pARAMCDataGridViewTextBoxColumn";
            this.pARAMCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // recipeBindingSource
            // 
            this.recipeBindingSource.DataSource = typeof(TOECIMLib.CIMRecipe);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.timerLabel.ForeColor = System.Drawing.Color.Red;
            this.timerLabel.Location = new System.Drawing.Point(9, 379);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(177, 17);
            this.timerLabel.TabIndex = 68;
            this.timerLabel.Text = "5초 뒤에 자동으로 닫힙니다.";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.descriptionLabel.Location = new System.Drawing.Point(328, 69);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(62, 21);
            this.descriptionLabel.TabIndex = 69;
            this.descriptionLabel.Text = "Desc : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(5, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 239);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // RecipeChangePopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 403);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.recipeIDLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.titleLabel);
            this.Name = "RecipeChangePopUp";
            this.Text = "RecipeChangePopUp";
            this.Load += new System.EventHandler(this.RecipeChangePopUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recipeBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn scanNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARAMCDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource recipeBindingSource;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Label recipeIDLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}