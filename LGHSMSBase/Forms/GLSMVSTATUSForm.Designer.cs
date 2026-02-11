
namespace LGHSMSBase.Forms
{
    partial class GLSMVSTATUSForm
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
            this.LWButton = new System.Windows.Forms.Button();
            this.UWButton = new System.Windows.Forms.Button();
            this.LRButton = new System.Windows.Forms.Button();
            this.URButton = new System.Windows.Forms.Button();
            this.ERButton = new System.Windows.Forms.Button();
            this.CRButton = new System.Windows.Forms.Button();
            this.forcedTransferCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LWButton
            // 
            this.LWButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LWButton.Location = new System.Drawing.Point(0, 34);
            this.LWButton.Name = "LWButton";
            this.LWButton.Size = new System.Drawing.Size(216, 41);
            this.LWButton.TabIndex = 0;
            this.LWButton.Text = "LW (Load Wait)";
            this.LWButton.UseVisualStyleBackColor = true;
            this.LWButton.Click += new System.EventHandler(this.LWButton_Click);
            // 
            // UWButton
            // 
            this.UWButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UWButton.Location = new System.Drawing.Point(0, 81);
            this.UWButton.Name = "UWButton";
            this.UWButton.Size = new System.Drawing.Size(216, 41);
            this.UWButton.TabIndex = 1;
            this.UWButton.Text = "UW (Unload Wait)";
            this.UWButton.UseVisualStyleBackColor = true;
            this.UWButton.Click += new System.EventHandler(this.UWButton_Click);
            // 
            // LRButton
            // 
            this.LRButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LRButton.Location = new System.Drawing.Point(0, 128);
            this.LRButton.Name = "LRButton";
            this.LRButton.Size = new System.Drawing.Size(216, 41);
            this.LRButton.TabIndex = 2;
            this.LRButton.Text = "LR (Load Request)";
            this.LRButton.UseVisualStyleBackColor = true;
            this.LRButton.Click += new System.EventHandler(this.LRButton_Click);
            // 
            // URButton
            // 
            this.URButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.URButton.Location = new System.Drawing.Point(0, 175);
            this.URButton.Name = "URButton";
            this.URButton.Size = new System.Drawing.Size(216, 41);
            this.URButton.TabIndex = 3;
            this.URButton.Text = "UR (Unload Request)";
            this.URButton.UseVisualStyleBackColor = true;
            this.URButton.Click += new System.EventHandler(this.URButton_Click);
            // 
            // ERButton
            // 
            this.ERButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ERButton.Location = new System.Drawing.Point(0, 222);
            this.ERButton.Name = "ERButton";
            this.ERButton.Size = new System.Drawing.Size(216, 41);
            this.ERButton.TabIndex = 4;
            this.ERButton.Text = "ER (Exchange Request)";
            this.ERButton.UseVisualStyleBackColor = true;
            this.ERButton.Click += new System.EventHandler(this.ERButton_Click);
            // 
            // CRButton
            // 
            this.CRButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CRButton.Location = new System.Drawing.Point(0, 269);
            this.CRButton.Name = "CRButton";
            this.CRButton.Size = new System.Drawing.Size(216, 41);
            this.CRButton.TabIndex = 5;
            this.CRButton.Text = "CR (Cancel Request)";
            this.CRButton.UseVisualStyleBackColor = true;
            this.CRButton.Click += new System.EventHandler(this.CRButton_Click);
            // 
            // forcedTransferCheckBox
            // 
            this.forcedTransferCheckBox.AutoSize = true;
            this.forcedTransferCheckBox.Location = new System.Drawing.Point(136, 7);
            this.forcedTransferCheckBox.Name = "forcedTransferCheckBox";
            this.forcedTransferCheckBox.Size = new System.Drawing.Size(76, 16);
            this.forcedTransferCheckBox.TabIndex = 6;
            this.forcedTransferCheckBox.Text = "강제 변경";
            this.forcedTransferCheckBox.UseVisualStyleBackColor = true;
            // 
            // GLSMVSTATUSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 313);
            this.Controls.Add(this.forcedTransferCheckBox);
            this.Controls.Add(this.CRButton);
            this.Controls.Add(this.ERButton);
            this.Controls.Add(this.URButton);
            this.Controls.Add(this.LRButton);
            this.Controls.Add(this.UWButton);
            this.Controls.Add(this.LWButton);
            this.Name = "GLSMVSTATUSForm";
            this.Text = "GLSMVSTATUS";
            this.Load += new System.EventHandler(this.GLSMVSTATUSForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LWButton;
        private System.Windows.Forms.Button UWButton;
        private System.Windows.Forms.Button LRButton;
        private System.Windows.Forms.Button URButton;
        private System.Windows.Forms.Button ERButton;
        private System.Windows.Forms.Button CRButton;
        public System.Windows.Forms.CheckBox forcedTransferCheckBox;
    }
}