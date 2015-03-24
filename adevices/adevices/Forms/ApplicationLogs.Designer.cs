namespace adevices
{
    partial class ApplicationLogs
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
            this.txtApplicationLog = new System.Windows.Forms.TextBox();
            this.btnErrors = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtApplicationLog
            // 
            this.txtApplicationLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtApplicationLog.Location = new System.Drawing.Point(0, 42);
            this.txtApplicationLog.Multiline = true;
            this.txtApplicationLog.Name = "txtApplicationLog";
            this.txtApplicationLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtApplicationLog.Size = new System.Drawing.Size(755, 416);
            this.txtApplicationLog.TabIndex = 0;
            // 
            // btnErrors
            // 
            this.btnErrors.Location = new System.Drawing.Point(58, 13);
            this.btnErrors.Name = "btnErrors";
            this.btnErrors.Size = new System.Drawing.Size(75, 23);
            this.btnErrors.TabIndex = 1;
            this.btnErrors.Text = "Errors";
            this.btnErrors.UseVisualStyleBackColor = true;
            this.btnErrors.Click += new System.EventHandler(this.btnErrors_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(139, 13);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 2;
            this.btnLog.Text = "All log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // ApplicationLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 458);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnErrors);
            this.Controls.Add(this.txtApplicationLog);
            this.Name = "ApplicationLogs";
            this.Text = "ApplicationLogs";
            this.Load += new System.EventHandler(this.ApplicationLogs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtApplicationLog;
        private System.Windows.Forms.Button btnErrors;
        private System.Windows.Forms.Button btnLog;
    }
}