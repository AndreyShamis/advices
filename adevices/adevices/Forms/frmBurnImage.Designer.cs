namespace adevices
{
    partial class frmBurnImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBurnImage));
            this.txtPathToImage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBurn = new System.Windows.Forms.Button();
            this.cmdDevices = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tmrExit = new System.Windows.Forms.Timer(this.components);
            this.chkKillFlashhTollProc = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtPathToImage
            // 
            this.txtPathToImage.Location = new System.Drawing.Point(9, 25);
            this.txtPathToImage.Name = "txtPathToImage";
            this.txtPathToImage.Size = new System.Drawing.Size(697, 20);
            this.txtPathToImage.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path to image";
            // 
            // cmdBurn
            // 
            this.cmdBurn.Location = new System.Drawing.Point(631, 78);
            this.cmdBurn.Name = "cmdBurn";
            this.cmdBurn.Size = new System.Drawing.Size(75, 23);
            this.cmdBurn.TabIndex = 2;
            this.cmdBurn.Text = "Burn";
            this.cmdBurn.UseVisualStyleBackColor = true;
            this.cmdBurn.Click += new System.EventHandler(this.cmdBurn_Click);
            // 
            // cmdDevices
            // 
            this.cmdDevices.FormattingEnabled = true;
            this.cmdDevices.Location = new System.Drawing.Point(125, 51);
            this.cmdDevices.Name = "cmdDevices";
            this.cmdDevices.Size = new System.Drawing.Size(581, 21);
            this.cmdDevices.TabIndex = 5;
            this.cmdDevices.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Only one device";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(269, 107);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(437, 448);
            this.txtLog.TabIndex = 7;
            // 
            // tmrExit
            // 
            this.tmrExit.Interval = 3000;
            // 
            // chkKillFlashhTollProc
            // 
            this.chkKillFlashhTollProc.AutoSize = true;
            this.chkKillFlashhTollProc.Checked = true;
            this.chkKillFlashhTollProc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKillFlashhTollProc.Location = new System.Drawing.Point(269, 78);
            this.chkKillFlashhTollProc.Name = "chkKillFlashhTollProc";
            this.chkKillFlashhTollProc.Size = new System.Drawing.Size(135, 17);
            this.chkKillFlashhTollProc.TabIndex = 8;
            this.chkKillFlashhTollProc.Text = "Kill FlashhTool Process";
            this.chkKillFlashhTollProc.UseVisualStyleBackColor = true;
            // 
            // frmBurnImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 555);
            this.Controls.Add(this.chkKillFlashhTollProc);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmdDevices);
            this.Controls.Add(this.cmdBurn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPathToImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBurnImage";
            this.Text = "Burn Image To Devices";
            this.Load += new System.EventHandler(this.frmBurnImage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathToImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBurn;
        private System.Windows.Forms.ComboBox cmdDevices;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer tmrExit;
        private System.Windows.Forms.CheckBox chkKillFlashhTollProc;
    }
}