namespace adbdevices
{
    partial class AdbDeviceProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdbDeviceProperty));
            this.txtObjectProperty = new System.Windows.Forms.TextBox();
            this.txtAdbKeeperTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetKeeperTime = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRecoveryEnabled = new System.Windows.Forms.CheckBox();
            this.chkKeeperEnabled = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRelayId = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdSetPowerPort = new System.Windows.Forms.Button();
            this.txtRelayPowerPortId = new System.Windows.Forms.TextBox();
            this.cmdSetUsbPort = new System.Windows.Forms.Button();
            this.txtRelayUsbPortId = new System.Windows.Forms.TextBox();
            this.btnPowerOff = new System.Windows.Forms.Button();
            this.btnPowerOn = new System.Windows.Forms.Button();
            this.cmdRemoveDevice = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSetSerialNumber = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.tmrPeriodic = new System.Windows.Forms.Timer(this.components);
            this.cmdApkTestTime = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtApkTestTime = new System.Windows.Forms.TextBox();
            this.btnResetRecovery = new System.Windows.Forms.Button();
            this.lblRecoveredIn20Min = new System.Windows.Forms.Label();
            this.lblRecoveredIn60Min = new System.Windows.Forms.Label();
            this.lblRecoveredIn10Min = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkChekShell = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkChekApk = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblRecoveredIn2Days = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tmrPeriodicLong = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.chkUsbNormalyClosed = new System.Windows.Forms.CheckBox();
            this.chkPowerNormalyClosed = new System.Windows.Forms.CheckBox();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnCLearGnss = new System.Windows.Forms.Button();
            this.txtFlashToolAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdSetFlashToolAddress = new System.Windows.Forms.Button();
            this.cmdSettxtFlashToolHubAddress = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFlashToolHubAddress = new System.Windows.Forms.TextBox();
            this.cmdBurnImage = new System.Windows.Forms.Button();
            this.txtBurnLog = new System.Windows.Forms.TextBox();
            this.cmdGetBaseBand = new System.Windows.Forms.Button();
            this.cmdInstallApk = new System.Windows.Forms.Button();
            this.cndEnableAllCheckBpx = new System.Windows.Forms.Button();
            this.cndDisableAllCheckBpx = new System.Windows.Forms.Button();
            this.getBattery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtObjectProperty
            // 
            this.txtObjectProperty.Location = new System.Drawing.Point(261, 7);
            this.txtObjectProperty.Multiline = true;
            this.txtObjectProperty.Name = "txtObjectProperty";
            this.txtObjectProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtObjectProperty.Size = new System.Drawing.Size(400, 395);
            this.txtObjectProperty.TabIndex = 0;
            // 
            // txtAdbKeeperTime
            // 
            this.txtAdbKeeperTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdbKeeperTime.Location = new System.Drawing.Point(112, 31);
            this.txtAdbKeeperTime.Name = "txtAdbKeeperTime";
            this.txtAdbKeeperTime.Size = new System.Drawing.Size(55, 20);
            this.txtAdbKeeperTime.TabIndex = 1;
            this.txtAdbKeeperTime.Text = "0";
            this.txtAdbKeeperTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Keeper Time(sec)";
            // 
            // btnSetKeeperTime
            // 
            this.btnSetKeeperTime.Location = new System.Drawing.Point(173, 32);
            this.btnSetKeeperTime.Name = "btnSetKeeperTime";
            this.btnSetKeeperTime.Size = new System.Drawing.Size(38, 21);
            this.btnSetKeeperTime.TabIndex = 3;
            this.btnSetKeeperTime.Text = "Set";
            this.btnSetKeeperTime.UseVisualStyleBackColor = true;
            this.btnSetKeeperTime.Click += new System.EventHandler(this.btnSetKeeperTime_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(14, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Is recovery enabled";
            // 
            // chkRecoveryEnabled
            // 
            this.chkRecoveryEnabled.AutoSize = true;
            this.chkRecoveryEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chkRecoveryEnabled.Location = new System.Drawing.Point(131, 342);
            this.chkRecoveryEnabled.Name = "chkRecoveryEnabled";
            this.chkRecoveryEnabled.Size = new System.Drawing.Size(89, 17);
            this.chkRecoveryEnabled.TabIndex = 5;
            this.chkRecoveryEnabled.Text = "checkBox1";
            this.chkRecoveryEnabled.UseVisualStyleBackColor = true;
            this.chkRecoveryEnabled.CheckedChanged += new System.EventHandler(this.chkRecoveryEnabled_CheckedChanged);
            this.chkRecoveryEnabled.Click += new System.EventHandler(this.chkRecoveryEnabled_Click);
            // 
            // chkKeeperEnabled
            // 
            this.chkKeeperEnabled.AutoSize = true;
            this.chkKeeperEnabled.Location = new System.Drawing.Point(131, 319);
            this.chkKeeperEnabled.Name = "chkKeeperEnabled";
            this.chkKeeperEnabled.Size = new System.Drawing.Size(80, 17);
            this.chkKeeperEnabled.TabIndex = 8;
            this.chkKeeperEnabled.Text = "checkBox1";
            this.chkKeeperEnabled.UseVisualStyleBackColor = true;
            this.chkKeeperEnabled.CheckedChanged += new System.EventHandler(this.chkKeeperEnabled_CheckedChanged);
            this.chkKeeperEnabled.Click += new System.EventHandler(this.chkKeeperEnabled_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Is keeper enabled";
            // 
            // cmbRelayId
            // 
            this.cmbRelayId.BackColor = System.Drawing.Color.LightCyan;
            this.cmbRelayId.FormattingEnabled = true;
            this.cmbRelayId.Location = new System.Drawing.Point(78, 124);
            this.cmbRelayId.Name = "cmbRelayId";
            this.cmbRelayId.Size = new System.Drawing.Size(158, 21);
            this.cmbRelayId.TabIndex = 9;
            this.cmbRelayId.SelectedIndexChanged += new System.EventHandler(this.cmbRelayId_SelectedIndexChanged);
            this.cmbRelayId.SelectedValueChanged += new System.EventHandler(this.cmbRelayId_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(11, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Relay ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(11, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Relay Power";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(11, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Relay USB";
            // 
            // cmdSetPowerPort
            // 
            this.cmdSetPowerPort.Location = new System.Drawing.Point(117, 149);
            this.cmdSetPowerPort.Name = "cmdSetPowerPort";
            this.cmdSetPowerPort.Size = new System.Drawing.Size(38, 20);
            this.cmdSetPowerPort.TabIndex = 14;
            this.cmdSetPowerPort.Text = "Set";
            this.cmdSetPowerPort.UseVisualStyleBackColor = true;
            this.cmdSetPowerPort.Click += new System.EventHandler(this.cmdSetPowerPort_Click);
            // 
            // txtRelayPowerPortId
            // 
            this.txtRelayPowerPortId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRelayPowerPortId.Location = new System.Drawing.Point(80, 151);
            this.txtRelayPowerPortId.Name = "txtRelayPowerPortId";
            this.txtRelayPowerPortId.Size = new System.Drawing.Size(31, 20);
            this.txtRelayPowerPortId.TabIndex = 13;
            // 
            // cmdSetUsbPort
            // 
            this.cmdSetUsbPort.Location = new System.Drawing.Point(117, 172);
            this.cmdSetUsbPort.Name = "cmdSetUsbPort";
            this.cmdSetUsbPort.Size = new System.Drawing.Size(38, 20);
            this.cmdSetUsbPort.TabIndex = 16;
            this.cmdSetUsbPort.Text = "Set";
            this.cmdSetUsbPort.UseVisualStyleBackColor = true;
            this.cmdSetUsbPort.Click += new System.EventHandler(this.cmdSetUsbPort_Click);
            // 
            // txtRelayUsbPortId
            // 
            this.txtRelayUsbPortId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRelayUsbPortId.Location = new System.Drawing.Point(80, 172);
            this.txtRelayUsbPortId.Name = "txtRelayUsbPortId";
            this.txtRelayUsbPortId.Size = new System.Drawing.Size(31, 20);
            this.txtRelayUsbPortId.TabIndex = 15;
            // 
            // btnPowerOff
            // 
            this.btnPowerOff.Location = new System.Drawing.Point(161, 172);
            this.btnPowerOff.Name = "btnPowerOff";
            this.btnPowerOff.Size = new System.Drawing.Size(75, 20);
            this.btnPowerOff.TabIndex = 17;
            this.btnPowerOff.Text = "Power Off";
            this.btnPowerOff.UseVisualStyleBackColor = true;
            this.btnPowerOff.Click += new System.EventHandler(this.btnPowerOff_Click);
            // 
            // btnPowerOn
            // 
            this.btnPowerOn.Location = new System.Drawing.Point(161, 149);
            this.btnPowerOn.Name = "btnPowerOn";
            this.btnPowerOn.Size = new System.Drawing.Size(75, 20);
            this.btnPowerOn.TabIndex = 18;
            this.btnPowerOn.Text = "Power On";
            this.btnPowerOn.UseVisualStyleBackColor = true;
            this.btnPowerOn.Click += new System.EventHandler(this.btnPowerOn_Click);
            // 
            // cmdRemoveDevice
            // 
            this.cmdRemoveDevice.Location = new System.Drawing.Point(14, 242);
            this.cmdRemoveDevice.Name = "cmdRemoveDevice";
            this.cmdRemoveDevice.Size = new System.Drawing.Size(102, 23);
            this.cmdRemoveDevice.TabIndex = 19;
            this.cmdRemoveDevice.Text = "Remove Device";
            this.cmdRemoveDevice.UseVisualStyleBackColor = true;
            this.cmdRemoveDevice.Click += new System.EventHandler(this.cmdRemoveDevice_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdClose.Location = new System.Drawing.Point(667, 379);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 20;
            this.cmdClose.Text = "Exit";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSetSerialNumber
            // 
            this.cmdSetSerialNumber.Location = new System.Drawing.Point(217, 5);
            this.cmdSetSerialNumber.Name = "cmdSetSerialNumber";
            this.cmdSetSerialNumber.Size = new System.Drawing.Size(38, 21);
            this.cmdSetSerialNumber.TabIndex = 23;
            this.cmdSetSerialNumber.Text = "Set";
            this.cmdSetSerialNumber.UseVisualStyleBackColor = true;
            this.cmdSetSerialNumber.Click += new System.EventHandler(this.cmdSetSerialNumber_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(9, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Serial";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.Color.LightBlue;
            this.txtSerialNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerialNumber.Location = new System.Drawing.Point(54, 5);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(157, 20);
            this.txtSerialNumber.TabIndex = 21;
            this.txtSerialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tmrPeriodic
            // 
            this.tmrPeriodic.Enabled = true;
            this.tmrPeriodic.Interval = 200;
            this.tmrPeriodic.Tick += new System.EventHandler(this.tmrPeriodic_Tick);
            // 
            // cmdApkTestTime
            // 
            this.cmdApkTestTime.Location = new System.Drawing.Point(173, 56);
            this.cmdApkTestTime.Name = "cmdApkTestTime";
            this.cmdApkTestTime.Size = new System.Drawing.Size(38, 21);
            this.cmdApkTestTime.TabIndex = 26;
            this.cmdApkTestTime.Text = "Set";
            this.cmdApkTestTime.UseVisualStyleBackColor = true;
            this.cmdApkTestTime.Click += new System.EventHandler(this.cmdApkTestTime_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "APK test Time(sec)";
            // 
            // txtApkTestTime
            // 
            this.txtApkTestTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApkTestTime.Location = new System.Drawing.Point(112, 56);
            this.txtApkTestTime.Name = "txtApkTestTime";
            this.txtApkTestTime.Size = new System.Drawing.Size(55, 20);
            this.txtApkTestTime.TabIndex = 24;
            this.txtApkTestTime.Text = "0";
            this.txtApkTestTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnResetRecovery
            // 
            this.btnResetRecovery.Location = new System.Drawing.Point(12, 213);
            this.btnResetRecovery.Name = "btnResetRecovery";
            this.btnResetRecovery.Size = new System.Drawing.Size(224, 23);
            this.btnResetRecovery.TabIndex = 27;
            this.btnResetRecovery.Text = "Reset Recovery History";
            this.btnResetRecovery.UseVisualStyleBackColor = true;
            this.btnResetRecovery.Click += new System.EventHandler(this.btnResetRecovery_Click);
            // 
            // lblRecoveredIn20Min
            // 
            this.lblRecoveredIn20Min.AutoSize = true;
            this.lblRecoveredIn20Min.Location = new System.Drawing.Point(184, 283);
            this.lblRecoveredIn20Min.Name = "lblRecoveredIn20Min";
            this.lblRecoveredIn20Min.Size = new System.Drawing.Size(13, 13);
            this.lblRecoveredIn20Min.TabIndex = 29;
            this.lblRecoveredIn20Min.Text = "0";
            this.lblRecoveredIn20Min.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRecoveredIn60Min
            // 
            this.lblRecoveredIn60Min.AutoSize = true;
            this.lblRecoveredIn60Min.Location = new System.Drawing.Point(212, 283);
            this.lblRecoveredIn60Min.Name = "lblRecoveredIn60Min";
            this.lblRecoveredIn60Min.Size = new System.Drawing.Size(13, 13);
            this.lblRecoveredIn60Min.TabIndex = 31;
            this.lblRecoveredIn60Min.Text = "0";
            this.lblRecoveredIn60Min.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRecoveredIn10Min
            // 
            this.lblRecoveredIn10Min.AutoSize = true;
            this.lblRecoveredIn10Min.Location = new System.Drawing.Point(154, 283);
            this.lblRecoveredIn10Min.Name = "lblRecoveredIn10Min";
            this.lblRecoveredIn10Min.Size = new System.Drawing.Size(13, 13);
            this.lblRecoveredIn10Min.TabIndex = 33;
            this.lblRecoveredIn10Min.Text = "0";
            this.lblRecoveredIn10Min.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 283);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Recovered in 10/20/60 min";
            // 
            // chkChekShell
            // 
            this.chkChekShell.AutoSize = true;
            this.chkChekShell.Location = new System.Drawing.Point(131, 362);
            this.chkChekShell.Name = "chkChekShell";
            this.chkChekShell.Size = new System.Drawing.Size(80, 17);
            this.chkChekShell.TabIndex = 35;
            this.chkChekShell.Text = "checkBox1";
            this.chkChekShell.UseVisualStyleBackColor = true;
            this.chkChekShell.CheckedChanged += new System.EventHandler(this.chkChekShell_CheckedChanged);
            this.chkChekShell.Click += new System.EventHandler(this.chkChekShell_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 363);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Check Shell";
            // 
            // chkChekApk
            // 
            this.chkChekApk.AutoSize = true;
            this.chkChekApk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chkChekApk.Location = new System.Drawing.Point(131, 385);
            this.chkChekApk.Name = "chkChekApk";
            this.chkChekApk.Size = new System.Drawing.Size(89, 17);
            this.chkChekApk.TabIndex = 37;
            this.chkChekApk.Text = "checkBox1";
            this.chkChekApk.UseVisualStyleBackColor = true;
            this.chkChekApk.CheckedChanged += new System.EventHandler(this.chkChekApk_CheckedChanged);
            this.chkChekApk.Click += new System.EventHandler(this.chkChekApk_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label13.Location = new System.Drawing.Point(14, 385);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Check APK";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(122, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Install APK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblRecoveredIn2Days
            // 
            this.lblRecoveredIn2Days.AutoSize = true;
            this.lblRecoveredIn2Days.Location = new System.Drawing.Point(223, 296);
            this.lblRecoveredIn2Days.Name = "lblRecoveredIn2Days";
            this.lblRecoveredIn2Days.Size = new System.Drawing.Size(13, 13);
            this.lblRecoveredIn2Days.TabIndex = 40;
            this.lblRecoveredIn2Days.Text = "0";
            this.lblRecoveredIn2Days.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 296);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 13);
            this.label15.TabIndex = 39;
            this.label15.Text = "Recovered in 2 days";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button2.ForeColor = System.Drawing.Color.DarkGreen;
            this.button2.Location = new System.Drawing.Point(667, 101);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 41;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tmrPeriodicLong
            // 
            this.tmrPeriodicLong.Enabled = true;
            this.tmrPeriodicLong.Interval = 20000;
            this.tmrPeriodicLong.Tick += new System.EventHandler(this.tmrPeriodicLong_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(7, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 11);
            this.panel1.TabIndex = 42;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(667, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 43;
            this.button3.Text = "Check APKs";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chkUsbNormalyClosed
            // 
            this.chkUsbNormalyClosed.AutoSize = true;
            this.chkUsbNormalyClosed.Location = new System.Drawing.Point(667, 9);
            this.chkUsbNormalyClosed.Name = "chkUsbNormalyClosed";
            this.chkUsbNormalyClosed.Size = new System.Drawing.Size(66, 17);
            this.chkUsbNormalyClosed.TabIndex = 44;
            this.chkUsbNormalyClosed.Text = "USB NC";
            this.chkUsbNormalyClosed.UseVisualStyleBackColor = true;
            this.chkUsbNormalyClosed.Click += new System.EventHandler(this.chkUsbNormalyClosed_Click);
            // 
            // chkPowerNormalyClosed
            // 
            this.chkPowerNormalyClosed.AutoSize = true;
            this.chkPowerNormalyClosed.Location = new System.Drawing.Point(667, 32);
            this.chkPowerNormalyClosed.Name = "chkPowerNormalyClosed";
            this.chkPowerNormalyClosed.Size = new System.Drawing.Size(74, 17);
            this.chkPowerNormalyClosed.TabIndex = 45;
            this.chkPowerNormalyClosed.Text = "Power NC";
            this.chkPowerNormalyClosed.UseVisualStyleBackColor = true;
            this.chkPowerNormalyClosed.Click += new System.EventHandler(this.chkPowerNormalyClosed_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnReboot.ForeColor = System.Drawing.Color.Maroon;
            this.btnReboot.Location = new System.Drawing.Point(667, 198);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(75, 23);
            this.btnReboot.TabIndex = 46;
            this.btnReboot.Text = "Reboot";
            this.btnReboot.UseVisualStyleBackColor = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click_1);
            // 
            // btnCLearGnss
            // 
            this.btnCLearGnss.Location = new System.Drawing.Point(667, 169);
            this.btnCLearGnss.Name = "btnCLearGnss";
            this.btnCLearGnss.Size = new System.Drawing.Size(75, 23);
            this.btnCLearGnss.TabIndex = 47;
            this.btnCLearGnss.Text = "Clear GNSS";
            this.btnCLearGnss.UseVisualStyleBackColor = true;
            this.btnCLearGnss.Click += new System.EventHandler(this.btnCLearGnss_Click);
            // 
            // txtFlashToolAddress
            // 
            this.txtFlashToolAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFlashToolAddress.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtFlashToolAddress.Location = new System.Drawing.Point(99, 82);
            this.txtFlashToolAddress.Name = "txtFlashToolAddress";
            this.txtFlashToolAddress.Size = new System.Drawing.Size(112, 18);
            this.txtFlashToolAddress.TabIndex = 48;
            this.txtFlashToolAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(9, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 11);
            this.label9.TabIndex = 49;
            this.label9.Text = "Flash Tool Address";
            // 
            // cmdSetFlashToolAddress
            // 
            this.cmdSetFlashToolAddress.Location = new System.Drawing.Point(217, 80);
            this.cmdSetFlashToolAddress.Name = "cmdSetFlashToolAddress";
            this.cmdSetFlashToolAddress.Size = new System.Drawing.Size(38, 21);
            this.cmdSetFlashToolAddress.TabIndex = 50;
            this.cmdSetFlashToolAddress.Text = "Set";
            this.cmdSetFlashToolAddress.UseVisualStyleBackColor = true;
            this.cmdSetFlashToolAddress.Click += new System.EventHandler(this.cmdSetFlashToolAddress_Click);
            // 
            // cmdSettxtFlashToolHubAddress
            // 
            this.cmdSettxtFlashToolHubAddress.Location = new System.Drawing.Point(217, 102);
            this.cmdSettxtFlashToolHubAddress.Name = "cmdSettxtFlashToolHubAddress";
            this.cmdSettxtFlashToolHubAddress.Size = new System.Drawing.Size(38, 21);
            this.cmdSettxtFlashToolHubAddress.TabIndex = 53;
            this.cmdSettxtFlashToolHubAddress.Text = "Set";
            this.cmdSettxtFlashToolHubAddress.UseVisualStyleBackColor = true;
            this.cmdSettxtFlashToolHubAddress.Click += new System.EventHandler(this.cmdSettxtFlashToolHubAddress_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label11.Location = new System.Drawing.Point(9, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 11);
            this.label11.TabIndex = 52;
            this.label11.Text = "Flash Hub Address";
            // 
            // txtFlashToolHubAddress
            // 
            this.txtFlashToolHubAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFlashToolHubAddress.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtFlashToolHubAddress.Location = new System.Drawing.Point(98, 104);
            this.txtFlashToolHubAddress.Name = "txtFlashToolHubAddress";
            this.txtFlashToolHubAddress.Size = new System.Drawing.Size(113, 18);
            this.txtFlashToolHubAddress.TabIndex = 51;
            this.txtFlashToolHubAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdBurnImage
            // 
            this.cmdBurnImage.Location = new System.Drawing.Point(666, 242);
            this.cmdBurnImage.Name = "cmdBurnImage";
            this.cmdBurnImage.Size = new System.Drawing.Size(75, 23);
            this.cmdBurnImage.TabIndex = 54;
            this.cmdBurnImage.Text = "Burn IMG";
            this.cmdBurnImage.UseVisualStyleBackColor = true;
            this.cmdBurnImage.Click += new System.EventHandler(this.cmdBurnImage_Click);
            // 
            // txtBurnLog
            // 
            this.txtBurnLog.Location = new System.Drawing.Point(11, 433);
            this.txtBurnLog.Multiline = true;
            this.txtBurnLog.Name = "txtBurnLog";
            this.txtBurnLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBurnLog.Size = new System.Drawing.Size(724, 98);
            this.txtBurnLog.TabIndex = 55;
            // 
            // cmdGetBaseBand
            // 
            this.cmdGetBaseBand.Location = new System.Drawing.Point(668, 272);
            this.cmdGetBaseBand.Name = "cmdGetBaseBand";
            this.cmdGetBaseBand.Size = new System.Drawing.Size(75, 23);
            this.cmdGetBaseBand.TabIndex = 56;
            this.cmdGetBaseBand.Text = "BaseBand";
            this.cmdGetBaseBand.UseVisualStyleBackColor = true;
            this.cmdGetBaseBand.Click += new System.EventHandler(this.cmdGetBaseBand_Click);
            // 
            // cmdInstallApk
            // 
            this.cmdInstallApk.Location = new System.Drawing.Point(668, 72);
            this.cmdInstallApk.Name = "cmdInstallApk";
            this.cmdInstallApk.Size = new System.Drawing.Size(75, 23);
            this.cmdInstallApk.TabIndex = 57;
            this.cmdInstallApk.Text = "Install APK";
            this.cmdInstallApk.UseVisualStyleBackColor = true;
            this.cmdInstallApk.Click += new System.EventHandler(this.cmdInstallApk_Click);
            // 
            // cndEnableAllCheckBpx
            // 
            this.cndEnableAllCheckBpx.ForeColor = System.Drawing.Color.Green;
            this.cndEnableAllCheckBpx.Location = new System.Drawing.Point(131, 406);
            this.cndEnableAllCheckBpx.Name = "cndEnableAllCheckBpx";
            this.cndEnableAllCheckBpx.Size = new System.Drawing.Size(54, 21);
            this.cndEnableAllCheckBpx.TabIndex = 58;
            this.cndEnableAllCheckBpx.Text = "All ON";
            this.cndEnableAllCheckBpx.UseVisualStyleBackColor = true;
            this.cndEnableAllCheckBpx.Click += new System.EventHandler(this.cndEnableAllCheckBpx_Click);
            // 
            // cndDisableAllCheckBpx
            // 
            this.cndDisableAllCheckBpx.ForeColor = System.Drawing.Color.Maroon;
            this.cndDisableAllCheckBpx.Location = new System.Drawing.Point(191, 406);
            this.cndDisableAllCheckBpx.Name = "cndDisableAllCheckBpx";
            this.cndDisableAllCheckBpx.Size = new System.Drawing.Size(50, 21);
            this.cndDisableAllCheckBpx.TabIndex = 59;
            this.cndDisableAllCheckBpx.Text = "All OFF";
            this.cndDisableAllCheckBpx.UseVisualStyleBackColor = true;
            this.cndDisableAllCheckBpx.Click += new System.EventHandler(this.cndDisableAllCheckBpx_Click);
            // 
            // getBattery
            // 
            this.getBattery.Location = new System.Drawing.Point(668, 301);
            this.getBattery.Name = "getBattery";
            this.getBattery.Size = new System.Drawing.Size(75, 23);
            this.getBattery.TabIndex = 60;
            this.getBattery.Text = "Battery";
            this.getBattery.UseVisualStyleBackColor = true;
            this.getBattery.Click += new System.EventHandler(this.getBattery_Click);
            // 
            // AdbDeviceProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 537);
            this.Controls.Add(this.getBattery);
            this.Controls.Add(this.cndDisableAllCheckBpx);
            this.Controls.Add(this.cndEnableAllCheckBpx);
            this.Controls.Add(this.cmdInstallApk);
            this.Controls.Add(this.cmdGetBaseBand);
            this.Controls.Add(this.txtBurnLog);
            this.Controls.Add(this.cmdBurnImage);
            this.Controls.Add(this.cmdSettxtFlashToolHubAddress);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtFlashToolHubAddress);
            this.Controls.Add(this.cmdSetFlashToolAddress);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFlashToolAddress);
            this.Controls.Add(this.btnCLearGnss);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.chkPowerNormalyClosed);
            this.Controls.Add(this.chkUsbNormalyClosed);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblRecoveredIn2Days);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkChekApk);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.chkChekShell);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblRecoveredIn10Min);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblRecoveredIn60Min);
            this.Controls.Add(this.lblRecoveredIn20Min);
            this.Controls.Add(this.btnResetRecovery);
            this.Controls.Add(this.cmdApkTestTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtApkTestTime);
            this.Controls.Add(this.cmdSetSerialNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdRemoveDevice);
            this.Controls.Add(this.btnPowerOn);
            this.Controls.Add(this.btnPowerOff);
            this.Controls.Add(this.cmdSetUsbPort);
            this.Controls.Add(this.txtRelayUsbPortId);
            this.Controls.Add(this.cmdSetPowerPort);
            this.Controls.Add(this.txtRelayPowerPortId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbRelayId);
            this.Controls.Add(this.chkKeeperEnabled);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkRecoveryEnabled);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSetKeeperTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAdbKeeperTime);
            this.Controls.Add(this.txtObjectProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdbDeviceProperty";
            this.Text = "AdbDeviceProperty";
            this.Load += new System.EventHandler(this.AdbDeviceProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtObjectProperty;
        private System.Windows.Forms.TextBox txtAdbKeeperTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetKeeperTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRecoveryEnabled;
        private System.Windows.Forms.CheckBox chkKeeperEnabled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRelayId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdSetPowerPort;
        private System.Windows.Forms.TextBox txtRelayPowerPortId;
        private System.Windows.Forms.Button cmdSetUsbPort;
        private System.Windows.Forms.TextBox txtRelayUsbPortId;
        private System.Windows.Forms.Button btnPowerOff;
        private System.Windows.Forms.Button btnPowerOn;
        private System.Windows.Forms.Button cmdRemoveDevice;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSetSerialNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Timer tmrPeriodic;
        private System.Windows.Forms.Button cmdApkTestTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtApkTestTime;
        private System.Windows.Forms.Button btnResetRecovery;
        private System.Windows.Forms.Label lblRecoveredIn20Min;
        private System.Windows.Forms.Label lblRecoveredIn60Min;
        private System.Windows.Forms.Label lblRecoveredIn10Min;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkChekShell;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkChekApk;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblRecoveredIn2Days;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer tmrPeriodicLong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chkUsbNormalyClosed;
        private System.Windows.Forms.CheckBox chkPowerNormalyClosed;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnCLearGnss;
        private System.Windows.Forms.TextBox txtFlashToolAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdSetFlashToolAddress;
        private System.Windows.Forms.Button cmdSettxtFlashToolHubAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFlashToolHubAddress;
        private System.Windows.Forms.Button cmdBurnImage;
        private System.Windows.Forms.TextBox txtBurnLog;
        private System.Windows.Forms.Button cmdGetBaseBand;
        private System.Windows.Forms.Button cmdInstallApk;
        private System.Windows.Forms.Button cndEnableAllCheckBpx;
        private System.Windows.Forms.Button cndDisableAllCheckBpx;
        private System.Windows.Forms.Button getBattery;
    }
}