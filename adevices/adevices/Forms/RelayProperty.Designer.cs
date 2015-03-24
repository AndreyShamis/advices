namespace adevices
{
    partial class RelayProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelayProperty));
            this.relay1 = new System.Windows.Forms.CheckBox();
            this.relay8 = new System.Windows.Forms.CheckBox();
            this.relay3 = new System.Windows.Forms.CheckBox();
            this.relay2 = new System.Windows.Forms.CheckBox();
            this.relay4 = new System.Windows.Forms.CheckBox();
            this.relay5 = new System.Windows.Forms.CheckBox();
            this.relay7 = new System.Windows.Forms.CheckBox();
            this.relay6 = new System.Windows.Forms.CheckBox();
            this.cmdAllOn = new System.Windows.Forms.Button();
            this.cmdAllOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // relay1
            // 
            this.relay1.AutoSize = true;
            this.relay1.Location = new System.Drawing.Point(12, 12);
            this.relay1.Name = "relay1";
            this.relay1.Size = new System.Drawing.Size(62, 17);
            this.relay1.TabIndex = 9;
            this.relay1.Text = "Relay 1";
            this.relay1.UseVisualStyleBackColor = true;
            this.relay1.Click += new System.EventHandler(this.relay1_Click);
            // 
            // relay8
            // 
            this.relay8.AutoSize = true;
            this.relay8.Location = new System.Drawing.Point(98, 12);
            this.relay8.Name = "relay8";
            this.relay8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.relay8.Size = new System.Drawing.Size(62, 17);
            this.relay8.TabIndex = 10;
            this.relay8.Text = "Relay 8";
            this.relay8.UseVisualStyleBackColor = true;
            this.relay8.CheckedChanged += new System.EventHandler(this.relay8_CheckedChanged);
            this.relay8.Click += new System.EventHandler(this.relay8_Click);
            // 
            // relay3
            // 
            this.relay3.AutoSize = true;
            this.relay3.Location = new System.Drawing.Point(12, 58);
            this.relay3.Name = "relay3";
            this.relay3.Size = new System.Drawing.Size(62, 17);
            this.relay3.TabIndex = 11;
            this.relay3.Text = "Relay 3";
            this.relay3.UseVisualStyleBackColor = true;
            this.relay3.Click += new System.EventHandler(this.relay3_Click);
            // 
            // relay2
            // 
            this.relay2.AutoSize = true;
            this.relay2.Location = new System.Drawing.Point(12, 35);
            this.relay2.Name = "relay2";
            this.relay2.Size = new System.Drawing.Size(62, 17);
            this.relay2.TabIndex = 12;
            this.relay2.Text = "Relay 2";
            this.relay2.UseVisualStyleBackColor = true;
            this.relay2.Click += new System.EventHandler(this.relay2_Click);
            // 
            // relay4
            // 
            this.relay4.AutoSize = true;
            this.relay4.Location = new System.Drawing.Point(12, 81);
            this.relay4.Name = "relay4";
            this.relay4.Size = new System.Drawing.Size(62, 17);
            this.relay4.TabIndex = 13;
            this.relay4.Text = "Relay 4";
            this.relay4.UseVisualStyleBackColor = true;
            this.relay4.Click += new System.EventHandler(this.relay4_Click);
            // 
            // relay5
            // 
            this.relay5.AutoSize = true;
            this.relay5.Location = new System.Drawing.Point(98, 81);
            this.relay5.Name = "relay5";
            this.relay5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.relay5.Size = new System.Drawing.Size(62, 17);
            this.relay5.TabIndex = 14;
            this.relay5.Text = "Relay 5";
            this.relay5.UseVisualStyleBackColor = true;
            this.relay5.CheckedChanged += new System.EventHandler(this.relay5_CheckedChanged);
            this.relay5.Click += new System.EventHandler(this.relay5_Click);
            // 
            // relay7
            // 
            this.relay7.AutoSize = true;
            this.relay7.Location = new System.Drawing.Point(98, 35);
            this.relay7.Name = "relay7";
            this.relay7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.relay7.Size = new System.Drawing.Size(62, 17);
            this.relay7.TabIndex = 16;
            this.relay7.Text = "Relay 7";
            this.relay7.UseVisualStyleBackColor = true;
            this.relay7.CheckedChanged += new System.EventHandler(this.relay7_CheckedChanged);
            this.relay7.Click += new System.EventHandler(this.relay7_Click);
            // 
            // relay6
            // 
            this.relay6.AutoSize = true;
            this.relay6.Location = new System.Drawing.Point(98, 58);
            this.relay6.Name = "relay6";
            this.relay6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.relay6.Size = new System.Drawing.Size(62, 17);
            this.relay6.TabIndex = 15;
            this.relay6.Text = "Relay 6";
            this.relay6.UseVisualStyleBackColor = true;
            this.relay6.CheckedChanged += new System.EventHandler(this.relay6_CheckedChanged);
            this.relay6.Click += new System.EventHandler(this.relay6_Click);
            // 
            // cmdAllOn
            // 
            this.cmdAllOn.Location = new System.Drawing.Point(197, 23);
            this.cmdAllOn.Name = "cmdAllOn";
            this.cmdAllOn.Size = new System.Drawing.Size(75, 23);
            this.cmdAllOn.TabIndex = 17;
            this.cmdAllOn.Text = "All On";
            this.cmdAllOn.UseVisualStyleBackColor = true;
            this.cmdAllOn.Click += new System.EventHandler(this.cmdAllOn_Click);
            // 
            // cmdAllOff
            // 
            this.cmdAllOff.Location = new System.Drawing.Point(197, 52);
            this.cmdAllOff.Name = "cmdAllOff";
            this.cmdAllOff.Size = new System.Drawing.Size(75, 23);
            this.cmdAllOff.TabIndex = 18;
            this.cmdAllOff.Text = "All Off";
            this.cmdAllOff.UseVisualStyleBackColor = true;
            this.cmdAllOff.Click += new System.EventHandler(this.cmdAllOff_Click);
            // 
            // RelayProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 109);
            this.Controls.Add(this.cmdAllOff);
            this.Controls.Add(this.cmdAllOn);
            this.Controls.Add(this.relay1);
            this.Controls.Add(this.relay8);
            this.Controls.Add(this.relay3);
            this.Controls.Add(this.relay2);
            this.Controls.Add(this.relay4);
            this.Controls.Add(this.relay5);
            this.Controls.Add(this.relay7);
            this.Controls.Add(this.relay6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RelayProperty";
            this.Text = "RelayProperty";
            this.Load += new System.EventHandler(this.RelayProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox relay1;
        private System.Windows.Forms.CheckBox relay8;
        private System.Windows.Forms.CheckBox relay3;
        private System.Windows.Forms.CheckBox relay2;
        private System.Windows.Forms.CheckBox relay4;
        private System.Windows.Forms.CheckBox relay5;
        private System.Windows.Forms.CheckBox relay7;
        private System.Windows.Forms.CheckBox relay6;
        private System.Windows.Forms.Button cmdAllOn;
        private System.Windows.Forms.Button cmdAllOff;
    }
}