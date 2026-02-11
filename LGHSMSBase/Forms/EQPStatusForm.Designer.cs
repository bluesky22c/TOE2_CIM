
namespace LGHSMSBase.Forms
{
    partial class EQPStatusForm
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
            this.ProcessDownButton = new System.Windows.Forms.Button();
            this.DOWNButton = new System.Windows.Forms.Button();
            this.IDLEButton = new System.Windows.Forms.Button();
            this.MonitoringButton = new System.Windows.Forms.Button();
            this.ModelChangeButton = new System.Windows.Forms.Button();
            this.DevelopmentButton = new System.Windows.Forms.Button();
            this.ExperimentButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProcessDownButton
            // 
            this.ProcessDownButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ProcessDownButton.Location = new System.Drawing.Point(1, 276);
            this.ProcessDownButton.Name = "ProcessDownButton";
            this.ProcessDownButton.Size = new System.Drawing.Size(176, 33);
            this.ProcessDownButton.TabIndex = 20;
            this.ProcessDownButton.Text = "Process Down";
            this.ProcessDownButton.UseVisualStyleBackColor = true;
            this.ProcessDownButton.Click += new System.EventHandler(this.ProcessDownButton_Click);
            // 
            // DOWNButton
            // 
            this.DOWNButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DOWNButton.Location = new System.Drawing.Point(1, 237);
            this.DOWNButton.Name = "DOWNButton";
            this.DOWNButton.Size = new System.Drawing.Size(176, 33);
            this.DOWNButton.TabIndex = 19;
            this.DOWNButton.Text = "DOWN";
            this.DOWNButton.UseVisualStyleBackColor = true;
            this.DOWNButton.Click += new System.EventHandler(this.DOWNButton_Click);
            // 
            // IDLEButton
            // 
            this.IDLEButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.IDLEButton.Location = new System.Drawing.Point(1, 198);
            this.IDLEButton.Name = "IDLEButton";
            this.IDLEButton.Size = new System.Drawing.Size(176, 33);
            this.IDLEButton.TabIndex = 18;
            this.IDLEButton.Text = "IDLE";
            this.IDLEButton.UseVisualStyleBackColor = true;
            this.IDLEButton.Click += new System.EventHandler(this.IDLEButton_Click);
            // 
            // MonitoringButton
            // 
            this.MonitoringButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MonitoringButton.Location = new System.Drawing.Point(1, 158);
            this.MonitoringButton.Name = "MonitoringButton";
            this.MonitoringButton.Size = new System.Drawing.Size(176, 33);
            this.MonitoringButton.TabIndex = 17;
            this.MonitoringButton.Text = "Monitoring";
            this.MonitoringButton.UseVisualStyleBackColor = true;
            this.MonitoringButton.Click += new System.EventHandler(this.MonitoringButton_Click);
            // 
            // ModelChangeButton
            // 
            this.ModelChangeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ModelChangeButton.Location = new System.Drawing.Point(1, 119);
            this.ModelChangeButton.Name = "ModelChangeButton";
            this.ModelChangeButton.Size = new System.Drawing.Size(176, 33);
            this.ModelChangeButton.TabIndex = 16;
            this.ModelChangeButton.Text = "Model Change";
            this.ModelChangeButton.UseVisualStyleBackColor = true;
            this.ModelChangeButton.Click += new System.EventHandler(this.ModelChangeButton_Click);
            // 
            // DevelopmentButton
            // 
            this.DevelopmentButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DevelopmentButton.Location = new System.Drawing.Point(1, 80);
            this.DevelopmentButton.Name = "DevelopmentButton";
            this.DevelopmentButton.Size = new System.Drawing.Size(176, 33);
            this.DevelopmentButton.TabIndex = 15;
            this.DevelopmentButton.Text = "Development";
            this.DevelopmentButton.UseVisualStyleBackColor = true;
            this.DevelopmentButton.Click += new System.EventHandler(this.DevelopmentButton_Click);
            // 
            // ExperimentButton
            // 
            this.ExperimentButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ExperimentButton.Location = new System.Drawing.Point(1, 40);
            this.ExperimentButton.Name = "ExperimentButton";
            this.ExperimentButton.Size = new System.Drawing.Size(176, 33);
            this.ExperimentButton.TabIndex = 14;
            this.ExperimentButton.Text = "Experiment";
            this.ExperimentButton.UseVisualStyleBackColor = true;
            this.ExperimentButton.Click += new System.EventHandler(this.ExperimentButton_Click);
            // 
            // runButton
            // 
            this.runButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.runButton.Location = new System.Drawing.Point(1, 1);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(176, 33);
            this.runButton.TabIndex = 13;
            this.runButton.Text = "RUN";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // EQPStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 310);
            this.Controls.Add(this.ProcessDownButton);
            this.Controls.Add(this.DOWNButton);
            this.Controls.Add(this.IDLEButton);
            this.Controls.Add(this.MonitoringButton);
            this.Controls.Add(this.ModelChangeButton);
            this.Controls.Add(this.DevelopmentButton);
            this.Controls.Add(this.ExperimentButton);
            this.Controls.Add(this.runButton);
            this.Name = "EQPStatusForm";
            this.Text = "EQPStatusForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ProcessDownButton;
        private System.Windows.Forms.Button DOWNButton;
        private System.Windows.Forms.Button IDLEButton;
        private System.Windows.Forms.Button MonitoringButton;
        private System.Windows.Forms.Button ModelChangeButton;
        private System.Windows.Forms.Button DevelopmentButton;
        private System.Windows.Forms.Button ExperimentButton;
        private System.Windows.Forms.Button runButton;
    }
}