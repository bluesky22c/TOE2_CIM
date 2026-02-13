
namespace LGCIMServer.EQPSimul.Forms
{
    partial class AlarmSimulForm
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
            this.ALIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ALTXTextBox = new System.Windows.Forms.TextBox();
            this.ALCDComboBox = new System.Windows.Forms.ComboBox();
            this.ALTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ALIDTextBox
            // 
            this.ALIDTextBox.Location = new System.Drawing.Point(88, 27);
            this.ALIDTextBox.Name = "ALIDTextBox";
            this.ALIDTextBox.Size = new System.Drawing.Size(143, 21);
            this.ALIDTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ALID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ALTX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "ALCD";
            // 
            // ALTXTextBox
            // 
            this.ALTXTextBox.Location = new System.Drawing.Point(88, 65);
            this.ALTXTextBox.Name = "ALTXTextBox";
            this.ALTXTextBox.Size = new System.Drawing.Size(143, 21);
            this.ALTXTextBox.TabIndex = 4;
            // 
            // ALCDComboBox
            // 
            this.ALCDComboBox.FormattingEnabled = true;
            this.ALCDComboBox.Location = new System.Drawing.Point(88, 99);
            this.ALCDComboBox.Name = "ALCDComboBox";
            this.ALCDComboBox.Size = new System.Drawing.Size(143, 20);
            this.ALCDComboBox.TabIndex = 5;
            // 
            // ALTypeComboBox
            // 
            this.ALTypeComboBox.FormattingEnabled = true;
            this.ALTypeComboBox.Location = new System.Drawing.Point(88, 136);
            this.ALTypeComboBox.Name = "ALTypeComboBox";
            this.ALTypeComboBox.Size = new System.Drawing.Size(143, 20);
            this.ALTypeComboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "AlarmType";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(51, 179);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click+= new System.EventHandler(this.okButton_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(132, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AlarmSimulForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 215);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.ALTypeComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ALCDComboBox);
            this.Controls.Add(this.ALTXTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ALIDTextBox);
            this.Name = "AlarmSimulForm";
            this.Text = "Alarm";
            this.Load += new System.EventHandler(this.AddAlarmForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ALIDTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ALTXTextBox;
        private System.Windows.Forms.ComboBox ALCDComboBox;
        private System.Windows.Forms.ComboBox ALTypeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button button2;
    }
}
