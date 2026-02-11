
namespace LGHSMSBase.Forms
{
    partial class RCSForm
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
            this.remoteButton = new System.Windows.Forms.Button();
            this.localButton = new System.Windows.Forms.Button();
            this.offlineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // remoteButton
            // 
            this.remoteButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.remoteButton.Location = new System.Drawing.Point(0, 0);
            this.remoteButton.Name = "remoteButton";
            this.remoteButton.Size = new System.Drawing.Size(187, 46);
            this.remoteButton.TabIndex = 0;
            this.remoteButton.Text = "Online Remote";
            this.remoteButton.UseVisualStyleBackColor = true;
            this.remoteButton.Click += new System.EventHandler(this.remoteButton_Click);
            // 
            // localButton
            // 
            this.localButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.localButton.Location = new System.Drawing.Point(0, 52);
            this.localButton.Name = "localButton";
            this.localButton.Size = new System.Drawing.Size(187, 46);
            this.localButton.TabIndex = 1;
            this.localButton.Text = "Online Local";
            this.localButton.UseVisualStyleBackColor = true;
            this.localButton.Click += new System.EventHandler(this.localButton_Click);
            // 
            // offlineButton
            // 
            this.offlineButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.offlineButton.Location = new System.Drawing.Point(0, 104);
            this.offlineButton.Name = "offlineButton";
            this.offlineButton.Size = new System.Drawing.Size(187, 46);
            this.offlineButton.TabIndex = 2;
            this.offlineButton.Text = "Offline";
            this.offlineButton.UseVisualStyleBackColor = true;
            this.offlineButton.Click += new System.EventHandler(this.offlineButton_Click);
            // 
            // RCSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 153);
            this.Controls.Add(this.offlineButton);
            this.Controls.Add(this.localButton);
            this.Controls.Add(this.remoteButton);
            this.Name = "RCSForm";
            this.Text = "RCSForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button remoteButton;
        private System.Windows.Forms.Button localButton;
        private System.Windows.Forms.Button offlineButton;
    }
}