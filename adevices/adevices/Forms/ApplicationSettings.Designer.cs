namespace adevices
{
    partial class ApplicationSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.numMaxAdbProcesses = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeBetweenStartAndAdbDeviceWatcher = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numAdbDeviceCheckTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAdbProcesses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBetweenStartAndAdbDeviceWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdbDeviceCheckTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(156, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(265, 171);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // numMaxAdbProcesses
            // 
            this.numMaxAdbProcesses.Location = new System.Drawing.Point(258, 29);
            this.numMaxAdbProcesses.Name = "numMaxAdbProcesses";
            this.numMaxAdbProcesses.Size = new System.Drawing.Size(85, 20);
            this.numMaxAdbProcesses.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Max ADB processes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Time between start and adb Devices watcher(sec)";
            // 
            // timeBetweenStartAndAdbDeviceWatcher
            // 
            this.timeBetweenStartAndAdbDeviceWatcher.Location = new System.Drawing.Point(258, 62);
            this.timeBetweenStartAndAdbDeviceWatcher.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.timeBetweenStartAndAdbDeviceWatcher.Name = "timeBetweenStartAndAdbDeviceWatcher";
            this.timeBetweenStartAndAdbDeviceWatcher.Size = new System.Drawing.Size(85, 20);
            this.timeBetweenStartAndAdbDeviceWatcher.TabIndex = 4;
            this.timeBetweenStartAndAdbDeviceWatcher.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.timeBetweenStartAndAdbDeviceWatcher.ValueChanged += new System.EventHandler(this.timeBetweenStartAndAdbDeviceWatcher_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Min 5 sec, Max 100 sec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(349, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Min 50 mSec Max 5000 mSec";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Adb device check time (msec)";
            // 
            // numAdbDeviceCheckTime
            // 
            this.numAdbDeviceCheckTime.Location = new System.Drawing.Point(258, 93);
            this.numAdbDeviceCheckTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numAdbDeviceCheckTime.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numAdbDeviceCheckTime.Name = "numAdbDeviceCheckTime";
            this.numAdbDeviceCheckTime.Size = new System.Drawing.Size(85, 20);
            this.numAdbDeviceCheckTime.TabIndex = 7;
            this.numAdbDeviceCheckTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numAdbDeviceCheckTime.ValueChanged += new System.EventHandler(this.numAdbDeviceCheckTime_ValueChanged);
            // 
            // ApplicationSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 208);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numAdbDeviceCheckTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeBetweenStartAndAdbDeviceWatcher);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMaxAdbProcesses);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApplicationSettings";
            this.Text = "ApplicationSettings";
            this.Load += new System.EventHandler(this.ApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAdbProcesses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBetweenStartAndAdbDeviceWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdbDeviceCheckTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.NumericUpDown numMaxAdbProcesses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown timeBetweenStartAndAdbDeviceWatcher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numAdbDeviceCheckTime;
    }
}