
namespace LGCIM.Forms
{
    partial class CassetteMAPForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.materialCardCassette = new MaterialSkin.Controls.MaterialCard();
            this.materialLabelCassette = new MaterialSkin.Controls.MaterialLabel();
            this.cstDataGridView = new System.Windows.Forms.DataGridView();
            this.materialCardPort = new MaterialSkin.Controls.MaterialCard();
            this.materialLabelPort = new MaterialSkin.Controls.MaterialLabel();
            this.portDataGridView = new System.Windows.Forms.DataGridView();
            this.closeButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.reqButton = new System.Windows.Forms.Button();
            this.selectDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sLOTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pORTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slotItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cSTREADYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pORTIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSTIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOTIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mAPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.materialCardCassette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cstDataGridView)).BeginInit();
            this.materialCardPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slotItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portItemBindingSource)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.materialCardCassette);
            this.splitContainer1.Panel1.Controls.Add(this.materialCardPort);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.closeButton);
            this.splitContainer1.Panel2.Controls.Add(this.refreshButton);
            this.splitContainer1.Panel2.Controls.Add(this.reqButton);
            this.splitContainer1.Size = new System.Drawing.Size(864, 636);
            this.splitContainer1.SplitterDistance = 589;
            this.splitContainer1.TabIndex = 5;
            // 
            // 
            // materialCardCassette
            // 
            this.materialCardCassette.Controls.Add(this.materialLabelCassette);
            this.materialCardCassette.Controls.Add(this.cstDataGridView);
            this.materialCardCassette.Depth = 0;
            this.materialCardCassette.Location = new System.Drawing.Point(359, 3);
            this.materialCardCassette.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardCassette.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardCassette.Name = "materialCardCassette";
            this.materialCardCassette.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardCassette.Size = new System.Drawing.Size(502, 583);
            this.materialCardCassette.TabIndex = 1;
            // 
            // materialLabelCassette
            // 
            this.materialLabelCassette.AutoSize = true;
            this.materialLabelCassette.Depth = 0;
            this.materialLabelCassette.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabelCassette.Location = new System.Drawing.Point(10, 5);
            this.materialLabelCassette.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelCassette.Name = "materialLabelCassette";
            this.materialLabelCassette.Size = new System.Drawing.Size(78, 19);
            this.materialLabelCassette.TabIndex = 1;
            this.materialLabelCassette.Text = "CASSETTE";
            // 
            // cstDataGridView
            // 
            this.cstDataGridView.AllowUserToAddRows = false;
            this.cstDataGridView.AllowUserToDeleteRows = false;
            this.cstDataGridView.AutoGenerateColumns = false;
            this.cstDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cstDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectDataGridViewCheckBoxColumn,
            this.sLOTIDDataGridViewTextBoxColumn,
            this.pORTIDDataGridViewTextBoxColumn,
            this.cSTIDDataGridViewTextBoxColumn,
            this.lOTIDDataGridViewTextBoxColumn});
            this.cstDataGridView.DataSource = this.slotItemBindingSource;
            this.cstDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cstDataGridView.Location = new System.Drawing.Point(3, 17);
            this.cstDataGridView.Name = "cstDataGridView";
            this.cstDataGridView.RowTemplate.Height = 23;
            this.cstDataGridView.Size = new System.Drawing.Size(496, 563);
            this.cstDataGridView.TabIndex = 0;
            // 
            // 
            // materialCardPort
            // 
            this.materialCardPort.Controls.Add(this.materialLabelPort);
            this.materialCardPort.Controls.Add(this.portDataGridView);
            this.materialCardPort.Depth = 0;
            this.materialCardPort.Location = new System.Drawing.Point(3, 3);
            this.materialCardPort.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardPort.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardPort.Name = "materialCardPort";
            this.materialCardPort.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardPort.Size = new System.Drawing.Size(350, 583);
            this.materialCardPort.TabIndex = 0;
            // 
            // materialLabelPort
            // 
            this.materialLabelPort.AutoSize = true;
            this.materialLabelPort.Depth = 0;
            this.materialLabelPort.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabelPort.Location = new System.Drawing.Point(10, 5);
            this.materialLabelPort.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelPort.Name = "materialLabelPort";
            this.materialLabelPort.Size = new System.Drawing.Size(42, 19);
            this.materialLabelPort.TabIndex = 1;
            this.materialLabelPort.Text = "PORT";
            // 
            // portDataGridView
            // 
            this.portDataGridView.AllowUserToAddRows = false;
            this.portDataGridView.AllowUserToDeleteRows = false;
            this.portDataGridView.AutoGenerateColumns = false;
            this.portDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cSTREADYDataGridViewTextBoxColumn,
            this.pORTIDDataGridViewTextBoxColumn1,
            this.cSTIDDataGridViewTextBoxColumn1,
            this.lOTIDDataGridViewTextBoxColumn1,
            this.mAPDataGridViewTextBoxColumn});
            this.portDataGridView.DataSource = this.portItemBindingSource;
            this.portDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portDataGridView.Location = new System.Drawing.Point(3, 17);
            this.portDataGridView.Name = "portDataGridView";
            this.portDataGridView.ReadOnly = true;
            this.portDataGridView.RowTemplate.Height = 23;
            this.portDataGridView.Size = new System.Drawing.Size(344, 563);
            this.portDataGridView.TabIndex = 0;
            this.portDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.portDataGridView_CellClick);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(774, 5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(84, 32);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(5, 5);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(106, 32);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "Refresh MAP";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // reqButton
            // 
            this.reqButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.reqButton.Location = new System.Drawing.Point(684, 5);
            this.reqButton.Name = "reqButton";
            this.reqButton.Size = new System.Drawing.Size(84, 32);
            this.reqButton.TabIndex = 2;
            this.reqButton.Text = "Request";
            this.reqButton.UseVisualStyleBackColor = true;
            this.reqButton.Click += new System.EventHandler(this.reqButton_Click);
            // 
            // selectDataGridViewCheckBoxColumn
            // 
            this.selectDataGridViewCheckBoxColumn.DataPropertyName = "Select";
            this.selectDataGridViewCheckBoxColumn.HeaderText = "Select";
            this.selectDataGridViewCheckBoxColumn.Name = "selectDataGridViewCheckBoxColumn";
            this.selectDataGridViewCheckBoxColumn.Width = 60;
            // 
            // sLOTIDDataGridViewTextBoxColumn
            // 
            this.sLOTIDDataGridViewTextBoxColumn.DataPropertyName = "SLOTID";
            this.sLOTIDDataGridViewTextBoxColumn.HeaderText = "SLOTID";
            this.sLOTIDDataGridViewTextBoxColumn.Name = "sLOTIDDataGridViewTextBoxColumn";
            this.sLOTIDDataGridViewTextBoxColumn.Width = 60;
            // 
            // pORTIDDataGridViewTextBoxColumn
            // 
            this.pORTIDDataGridViewTextBoxColumn.DataPropertyName = "PORTID";
            this.pORTIDDataGridViewTextBoxColumn.HeaderText = "PORTID";
            this.pORTIDDataGridViewTextBoxColumn.Name = "pORTIDDataGridViewTextBoxColumn";
            // 
            // cSTIDDataGridViewTextBoxColumn
            // 
            this.cSTIDDataGridViewTextBoxColumn.DataPropertyName = "CSTID";
            this.cSTIDDataGridViewTextBoxColumn.HeaderText = "CSTID";
            this.cSTIDDataGridViewTextBoxColumn.Name = "cSTIDDataGridViewTextBoxColumn";
            // 
            // lOTIDDataGridViewTextBoxColumn
            // 
            this.lOTIDDataGridViewTextBoxColumn.DataPropertyName = "LOTID";
            this.lOTIDDataGridViewTextBoxColumn.HeaderText = "LOTID";
            this.lOTIDDataGridViewTextBoxColumn.Name = "lOTIDDataGridViewTextBoxColumn";
            // 
            // slotItemBindingSource
            // 
            this.slotItemBindingSource.DataSource = typeof(LGCIM.Forms.SlotItem);
            // 
            // cSTREADYDataGridViewTextBoxColumn
            // 
            this.cSTREADYDataGridViewTextBoxColumn.DataPropertyName = "CSTREADY";
            this.cSTREADYDataGridViewTextBoxColumn.HeaderText = "CSTREADY";
            this.cSTREADYDataGridViewTextBoxColumn.Name = "cSTREADYDataGridViewTextBoxColumn";
            this.cSTREADYDataGridViewTextBoxColumn.ReadOnly = true;
            this.cSTREADYDataGridViewTextBoxColumn.Width = 80;
            // 
            // pORTIDDataGridViewTextBoxColumn1
            // 
            this.pORTIDDataGridViewTextBoxColumn1.DataPropertyName = "PORTID";
            this.pORTIDDataGridViewTextBoxColumn1.HeaderText = "PORTID";
            this.pORTIDDataGridViewTextBoxColumn1.Name = "pORTIDDataGridViewTextBoxColumn1";
            this.pORTIDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.pORTIDDataGridViewTextBoxColumn1.Width = 80;
            // 
            // cSTIDDataGridViewTextBoxColumn1
            // 
            this.cSTIDDataGridViewTextBoxColumn1.DataPropertyName = "CSTID";
            this.cSTIDDataGridViewTextBoxColumn1.HeaderText = "CSTID";
            this.cSTIDDataGridViewTextBoxColumn1.Name = "cSTIDDataGridViewTextBoxColumn1";
            this.cSTIDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.cSTIDDataGridViewTextBoxColumn1.Width = 80;
            // 
            // lOTIDDataGridViewTextBoxColumn1
            // 
            this.lOTIDDataGridViewTextBoxColumn1.DataPropertyName = "LOTID";
            this.lOTIDDataGridViewTextBoxColumn1.HeaderText = "LOTID";
            this.lOTIDDataGridViewTextBoxColumn1.Name = "lOTIDDataGridViewTextBoxColumn1";
            this.lOTIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // mAPDataGridViewTextBoxColumn
            // 
            this.mAPDataGridViewTextBoxColumn.DataPropertyName = "MAP";
            this.mAPDataGridViewTextBoxColumn.HeaderText = "MAP";
            this.mAPDataGridViewTextBoxColumn.Name = "mAPDataGridViewTextBoxColumn";
            this.mAPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // portItemBindingSource
            // 
            this.portItemBindingSource.DataSource = typeof(LGCIM.Forms.PortItem);
            // 
            // CassetteMAPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 636);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CassetteMAPForm";
            this.Text = "CassetteMAPForm";
            this.Load += new System.EventHandler(this.CassetteMAPForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.materialCardCassette.ResumeLayout(false);
            this.materialCardCassette.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cstDataGridView)).EndInit();
            this.materialCardPort.ResumeLayout(false);
            this.materialCardPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slotItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button reqButton;
        private MaterialSkin.Controls.MaterialCard materialCardCassette;
        private MaterialSkin.Controls.MaterialLabel materialLabelCassette;
        private MaterialSkin.Controls.MaterialCard materialCardPort;
        private MaterialSkin.Controls.MaterialLabel materialLabelPort;
        private System.Windows.Forms.DataGridView cstDataGridView;
        private System.Windows.Forms.DataGridView portDataGridView;
        private System.Windows.Forms.BindingSource slotItemBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLOTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pORTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource portItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSTREADYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pORTIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSTIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOTIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mAPDataGridViewTextBoxColumn;
    }
}