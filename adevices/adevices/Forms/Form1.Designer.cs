namespace adbdevices
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tmrStartUp = new System.Windows.Forms.Timer(this.components);
            this.tmrPeriodic = new System.Windows.Forms.Timer(this.components);
            this.tmrPeriodicShort = new System.Windows.Forms.Timer(this.components);
            this.lblSerialCount = new System.Windows.Forms.Label();
            this.lstRelays = new System.Windows.Forms.ListBox();
            this.tmrLoadRelays = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblOpenedRelays = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAdbParserStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cmdClearLog = new System.Windows.Forms.Button();
            this.chkUpdateLog = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.chkKillAdb = new System.Windows.Forms.CheckBox();
            this.lblInfoCannotStartGetDevicesCounter = new System.Windows.Forms.Label();
            this.lblAmountOfAdb = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.chkUsbWatcher = new System.Windows.Forms.CheckBox();
            this.tmrPeriodicFast = new System.Windows.Forms.Timer(this.components);
            this.lblGetDevicesIssue = new System.Windows.Forms.Label();
            this.lblGetDevicesStuck = new System.Windows.Forms.Label();
            this.lblLoadingRelays = new System.Windows.Forms.Label();
            this.lblCannotStartGetDevicesCounter = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRelaysOpened = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.clearRecoveryHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rebootAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.burnImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startAllRecoveryWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllRecoveryWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setMinBrightnessOnAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.setScreenStayAwakeTRUEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setScreenStayAwakeFALSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killADBProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killDownloadToolProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFlashing = new System.Windows.Forms.Label();
            this.tmrCheckAdbProcess = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.ForeColor = System.Drawing.Color.Navy;
            this.button1.Location = new System.Drawing.Point(4, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1364, 86);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(465, 155);
            this.textBox2.TabIndex = 3;
            // 
            // tmrStartUp
            // 
            this.tmrStartUp.Enabled = true;
            this.tmrStartUp.Interval = 140;
            this.tmrStartUp.Tick += new System.EventHandler(this.tmrStartUp_Tick);
            // 
            // tmrPeriodic
            // 
            this.tmrPeriodic.Enabled = true;
            this.tmrPeriodic.Interval = 60000;
            this.tmrPeriodic.Tick += new System.EventHandler(this.tmrPeriodic_Tick);
            // 
            // tmrPeriodicShort
            // 
            this.tmrPeriodicShort.Interval = 50;
            this.tmrPeriodicShort.Tick += new System.EventHandler(this.tmrPeriodicShort_Tick);
            // 
            // lblSerialCount
            // 
            this.lblSerialCount.AutoSize = true;
            this.lblSerialCount.Location = new System.Drawing.Point(2, 433);
            this.lblSerialCount.Name = "lblSerialCount";
            this.lblSerialCount.Size = new System.Drawing.Size(63, 13);
            this.lblSerialCount.TabIndex = 9;
            this.lblSerialCount.Text = "Serial count";
            // 
            // lstRelays
            // 
            this.lstRelays.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstRelays.FormattingEnabled = true;
            this.lstRelays.ItemHeight = 12;
            this.lstRelays.Location = new System.Drawing.Point(3, 448);
            this.lstRelays.Name = "lstRelays";
            this.lstRelays.Size = new System.Drawing.Size(130, 160);
            this.lstRelays.TabIndex = 10;
            this.lstRelays.DoubleClick += new System.EventHandler(this.lstRelays_DoubleClick);
            // 
            // tmrLoadRelays
            // 
            this.tmrLoadRelays.Interval = 1000;
            this.tmrLoadRelays.Tick += new System.EventHandler(this.tmrLoadRelays_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 389);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Opened relays";
            // 
            // lblOpenedRelays
            // 
            this.lblOpenedRelays.AutoSize = true;
            this.lblOpenedRelays.Location = new System.Drawing.Point(108, 389);
            this.lblOpenedRelays.Name = "lblOpenedRelays";
            this.lblOpenedRelays.Size = new System.Drawing.Size(13, 13);
            this.lblOpenedRelays.TabIndex = 14;
            this.lblOpenedRelays.Text = "0";
            this.lblOpenedRelays.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAdbParserStatus);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.cmdClearLog);
            this.panel1.Controls.Add(this.chkUpdateLog);
            this.panel1.Controls.Add(this.txtLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(139, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 589);
            this.panel1.TabIndex = 16;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblAdbParserStatus
            // 
            this.lblAdbParserStatus.AutoSize = true;
            this.lblAdbParserStatus.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblAdbParserStatus.Location = new System.Drawing.Point(325, 234);
            this.lblAdbParserStatus.Name = "lblAdbParserStatus";
            this.lblAdbParserStatus.Size = new System.Drawing.Size(13, 10);
            this.lblAdbParserStatus.TabIndex = 21;
            this.lblAdbParserStatus.Text = "off";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "ADB parser status";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listView1.GridLines = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1133, 226);
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick_1);
            // 
            // cmdClearLog
            // 
            this.cmdClearLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdClearLog.ForeColor = System.Drawing.Color.Green;
            this.cmdClearLog.Location = new System.Drawing.Point(12, 230);
            this.cmdClearLog.Name = "cmdClearLog";
            this.cmdClearLog.Size = new System.Drawing.Size(75, 23);
            this.cmdClearLog.TabIndex = 18;
            this.cmdClearLog.Text = "Clear";
            this.cmdClearLog.UseVisualStyleBackColor = true;
            this.cmdClearLog.Click += new System.EventHandler(this.cmdClearLog_Click_1);
            // 
            // chkUpdateLog
            // 
            this.chkUpdateLog.AutoSize = true;
            this.chkUpdateLog.Checked = true;
            this.chkUpdateLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateLog.Location = new System.Drawing.Point(93, 234);
            this.chkUpdateLog.Name = "chkUpdateLog";
            this.chkUpdateLog.Size = new System.Drawing.Size(94, 17);
            this.chkUpdateLog.TabIndex = 17;
            this.chkUpdateLog.Text = "RT log update";
            this.chkUpdateLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtLog.Location = new System.Drawing.Point(0, 258);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1133, 331);
            this.txtLog.TabIndex = 16;
            // 
            // chkKillAdb
            // 
            this.chkKillAdb.AutoSize = true;
            this.chkKillAdb.Checked = true;
            this.chkKillAdb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKillAdb.Location = new System.Drawing.Point(29, 82);
            this.chkKillAdb.Name = "chkKillAdb";
            this.chkKillAdb.Size = new System.Drawing.Size(64, 17);
            this.chkKillAdb.TabIndex = 34;
            this.chkKillAdb.Text = "Kill ADB";
            this.chkKillAdb.UseVisualStyleBackColor = true;
            this.chkKillAdb.CheckedChanged += new System.EventHandler(this.chkKillAdb_CheckedChanged);
            this.chkKillAdb.Click += new System.EventHandler(this.chkKillAdb_Click);
            // 
            // lblInfoCannotStartGetDevicesCounter
            // 
            this.lblInfoCannotStartGetDevicesCounter.AutoSize = true;
            this.lblInfoCannotStartGetDevicesCounter.Location = new System.Drawing.Point(2, 102);
            this.lblInfoCannotStartGetDevicesCounter.Name = "lblInfoCannotStartGetDevicesCounter";
            this.lblInfoCannotStartGetDevicesCounter.Size = new System.Drawing.Size(99, 13);
            this.lblInfoCannotStartGetDevicesCounter.TabIndex = 18;
            this.lblInfoCannotStartGetDevicesCounter.Text = "GetDevices Stucks";
            // 
            // lblAmountOfAdb
            // 
            this.lblAmountOfAdb.AutoSize = true;
            this.lblAmountOfAdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountOfAdb.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblAmountOfAdb.Location = new System.Drawing.Point(110, 124);
            this.lblAmountOfAdb.Name = "lblAmountOfAdb";
            this.lblAmountOfAdb.Size = new System.Drawing.Size(14, 13);
            this.lblAmountOfAdb.TabIndex = 19;
            this.lblAmountOfAdb.Text = "0";
            this.lblAmountOfAdb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Green;
            this.button3.Location = new System.Drawing.Point(4, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Update Table";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // chkUsbWatcher
            // 
            this.chkUsbWatcher.AutoSize = true;
            this.chkUsbWatcher.Location = new System.Drawing.Point(12, 210);
            this.chkUsbWatcher.Name = "chkUsbWatcher";
            this.chkUsbWatcher.Size = new System.Drawing.Size(89, 17);
            this.chkUsbWatcher.TabIndex = 22;
            this.chkUsbWatcher.Text = "USB watcher";
            this.chkUsbWatcher.UseVisualStyleBackColor = true;
            this.chkUsbWatcher.CheckedChanged += new System.EventHandler(this.chkUsbWatcher_CheckedChanged);
            // 
            // tmrPeriodicFast
            // 
            this.tmrPeriodicFast.Enabled = true;
            this.tmrPeriodicFast.Interval = 3;
            this.tmrPeriodicFast.Tick += new System.EventHandler(this.tmrPeriodicFast_Tick);
            // 
            // lblGetDevicesIssue
            // 
            this.lblGetDevicesIssue.BackColor = System.Drawing.Color.Lime;
            this.lblGetDevicesIssue.Location = new System.Drawing.Point(12, 150);
            this.lblGetDevicesIssue.Name = "lblGetDevicesIssue";
            this.lblGetDevicesIssue.Size = new System.Drawing.Size(18, 18);
            this.lblGetDevicesIssue.TabIndex = 23;
            this.lblGetDevicesIssue.Text = "A";
            this.lblGetDevicesIssue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGetDevicesStuck
            // 
            this.lblGetDevicesStuck.BackColor = System.Drawing.Color.Lime;
            this.lblGetDevicesStuck.Location = new System.Drawing.Point(48, 150);
            this.lblGetDevicesStuck.Name = "lblGetDevicesStuck";
            this.lblGetDevicesStuck.Size = new System.Drawing.Size(18, 18);
            this.lblGetDevicesStuck.TabIndex = 24;
            this.lblGetDevicesStuck.Text = "A";
            this.lblGetDevicesStuck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadingRelays
            // 
            this.lblLoadingRelays.BackColor = System.Drawing.Color.Lime;
            this.lblLoadingRelays.Location = new System.Drawing.Point(83, 150);
            this.lblLoadingRelays.Name = "lblLoadingRelays";
            this.lblLoadingRelays.Size = new System.Drawing.Size(18, 18);
            this.lblLoadingRelays.TabIndex = 25;
            this.lblLoadingRelays.Text = "R";
            this.lblLoadingRelays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCannotStartGetDevicesCounter
            // 
            this.lblCannotStartGetDevicesCounter.AutoSize = true;
            this.lblCannotStartGetDevicesCounter.Location = new System.Drawing.Point(110, 102);
            this.lblCannotStartGetDevicesCounter.Name = "lblCannotStartGetDevicesCounter";
            this.lblCannotStartGetDevicesCounter.Size = new System.Drawing.Size(13, 13);
            this.lblCannotStartGetDevicesCounter.TabIndex = 30;
            this.lblCannotStartGetDevicesCounter.Text = "0";
            this.lblCannotStartGetDevicesCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "ADBs count";
            // 
            // lblRelaysOpened
            // 
            this.lblRelaysOpened.AutoSize = true;
            this.lblRelaysOpened.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblRelaysOpened.ForeColor = System.Drawing.Color.Red;
            this.lblRelaysOpened.Location = new System.Drawing.Point(108, 411);
            this.lblRelaysOpened.Name = "lblRelaysOpened";
            this.lblRelaysOpened.Size = new System.Drawing.Size(14, 13);
            this.lblRelaysOpened.TabIndex = 22;
            this.lblRelaysOpened.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Relays busy";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1272, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showErrorLogsToolStripMenuItem,
            this.toolStripSeparator6,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator7,
            this.saveSettingsToolStripMenuItem,
            this.saveSettingsAsToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem,
            this.toolStripSeparator8});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // showErrorLogsToolStripMenuItem
            // 
            this.showErrorLogsToolStripMenuItem.Name = "showErrorLogsToolStripMenuItem";
            this.showErrorLogsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.showErrorLogsToolStripMenuItem.Text = "Show Error Logs";
            this.showErrorLogsToolStripMenuItem.Click += new System.EventHandler(this.showErrorLogsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(156, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(156, 6);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsAsToolStripMenuItem
            // 
            this.saveSettingsAsToolStripMenuItem.Name = "saveSettingsAsToolStripMenuItem";
            this.saveSettingsAsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveSettingsAsToolStripMenuItem.Text = "Save Settings As";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(156, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(156, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewDeviceToolStripMenuItem,
            this.toolStripSeparator9,
            this.clearRecoveryHistoryToolStripMenuItem,
            this.toolStripSeparator3,
            this.rebootAllDevicesToolStripMenuItem,
            this.toolStripSeparator4,
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem,
            this.burnImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.startAllRecoveryWatcherToolStripMenuItem,
            this.stopAllRecoveryWatcherToolStripMenuItem,
            this.toolStripSeparator1,
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem,
            this.setMinBrightnessOnAllDevicesToolStripMenuItem,
            this.toolStripSeparator10,
            this.setScreenStayAwakeTRUEToolStripMenuItem,
            this.setScreenStayAwakeFALSEToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.optionsToolStripMenuItem.Text = "Devices";
            // 
            // addNewDeviceToolStripMenuItem
            // 
            this.addNewDeviceToolStripMenuItem.Name = "addNewDeviceToolStripMenuItem";
            this.addNewDeviceToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.addNewDeviceToolStripMenuItem.Text = "Add new Device";
            this.addNewDeviceToolStripMenuItem.Click += new System.EventHandler(this.addNewDeviceToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(253, 6);
            // 
            // clearRecoveryHistoryToolStripMenuItem
            // 
            this.clearRecoveryHistoryToolStripMenuItem.Name = "clearRecoveryHistoryToolStripMenuItem";
            this.clearRecoveryHistoryToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.clearRecoveryHistoryToolStripMenuItem.Text = "Clear Recovery History";
            this.clearRecoveryHistoryToolStripMenuItem.Click += new System.EventHandler(this.clearRecoveryHistoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(253, 6);
            // 
            // rebootAllDevicesToolStripMenuItem
            // 
            this.rebootAllDevicesToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.rebootAllDevicesToolStripMenuItem.Name = "rebootAllDevicesToolStripMenuItem";
            this.rebootAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.rebootAllDevicesToolStripMenuItem.Text = "Reboot all devices";
            this.rebootAllDevicesToolStripMenuItem.Click += new System.EventHandler(this.rebootAllDevicesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(253, 6);
            // 
            // clearGNSSLogsOnAllDevicesToolStripMenuItem
            // 
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem.Name = "clearGNSSLogsOnAllDevicesToolStripMenuItem";
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem.Text = "Clear GNSS logs on all devices";
            this.clearGNSSLogsOnAllDevicesToolStripMenuItem.Click += new System.EventHandler(this.clearGNSSLogsOnAllDevicesToolStripMenuItem_Click);
            // 
            // burnImageToolStripMenuItem
            // 
            this.burnImageToolStripMenuItem.BackColor = System.Drawing.Color.Yellow;
            this.burnImageToolStripMenuItem.Name = "burnImageToolStripMenuItem";
            this.burnImageToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.burnImageToolStripMenuItem.Text = "Burn image";
            this.burnImageToolStripMenuItem.Click += new System.EventHandler(this.burnImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(253, 6);
            // 
            // startAllRecoveryWatcherToolStripMenuItem
            // 
            this.startAllRecoveryWatcherToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.startAllRecoveryWatcherToolStripMenuItem.Name = "startAllRecoveryWatcherToolStripMenuItem";
            this.startAllRecoveryWatcherToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.startAllRecoveryWatcherToolStripMenuItem.Text = "start All recovery watcher";
            this.startAllRecoveryWatcherToolStripMenuItem.Click += new System.EventHandler(this.startAllRecoveryWatcherToolStripMenuItem_Click);
            // 
            // stopAllRecoveryWatcherToolStripMenuItem
            // 
            this.stopAllRecoveryWatcherToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.stopAllRecoveryWatcherToolStripMenuItem.Name = "stopAllRecoveryWatcherToolStripMenuItem";
            this.stopAllRecoveryWatcherToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.stopAllRecoveryWatcherToolStripMenuItem.Text = "STOP All recovery watcher";
            this.stopAllRecoveryWatcherToolStripMenuItem.Click += new System.EventHandler(this.stopAllRecoveryWatcherToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(253, 6);
            // 
            // setMaxBrightnessOnAllDevicesToolStripMenuItem
            // 
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem.Name = "setMaxBrightnessOnAllDevicesToolStripMenuItem";
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem.Text = "Set MAX Brightness On All Devices";
            this.setMaxBrightnessOnAllDevicesToolStripMenuItem.Click += new System.EventHandler(this.setMaxBrightnessOnAllDevicesToolStripMenuItem_Click);
            // 
            // setMinBrightnessOnAllDevicesToolStripMenuItem
            // 
            this.setMinBrightnessOnAllDevicesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.setMinBrightnessOnAllDevicesToolStripMenuItem.Name = "setMinBrightnessOnAllDevicesToolStripMenuItem";
            this.setMinBrightnessOnAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.setMinBrightnessOnAllDevicesToolStripMenuItem.Text = "Set min brightness on all Devices";
            this.setMinBrightnessOnAllDevicesToolStripMenuItem.Click += new System.EventHandler(this.setMinBrightnessOnAllDevicesToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(253, 6);
            // 
            // setScreenStayAwakeTRUEToolStripMenuItem
            // 
            this.setScreenStayAwakeTRUEToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.setScreenStayAwakeTRUEToolStripMenuItem.Name = "setScreenStayAwakeTRUEToolStripMenuItem";
            this.setScreenStayAwakeTRUEToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.setScreenStayAwakeTRUEToolStripMenuItem.Text = "Set Screen Stay Awake TRUE";
            this.setScreenStayAwakeTRUEToolStripMenuItem.Click += new System.EventHandler(this.setScreenStayAwakeTRUEToolStripMenuItem_Click);
            // 
            // setScreenStayAwakeFALSEToolStripMenuItem
            // 
            this.setScreenStayAwakeFALSEToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.setScreenStayAwakeFALSEToolStripMenuItem.Name = "setScreenStayAwakeFALSEToolStripMenuItem";
            this.setScreenStayAwakeFALSEToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.setScreenStayAwakeFALSEToolStripMenuItem.Text = "Set Screen Stay Awake FALSE";
            this.setScreenStayAwakeFALSEToolStripMenuItem.Click += new System.EventHandler(this.setScreenStayAwakeFALSEToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killADBProcessToolStripMenuItem,
            this.killDownloadToolProcessToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.logsToolStripMenuItem.Text = "Process";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.logsToolStripMenuItem_Click);
            // 
            // killADBProcessToolStripMenuItem
            // 
            this.killADBProcessToolStripMenuItem.Name = "killADBProcessToolStripMenuItem";
            this.killADBProcessToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.killADBProcessToolStripMenuItem.Text = "Kill ADB process";
            this.killADBProcessToolStripMenuItem.Click += new System.EventHandler(this.killADBProcessToolStripMenuItem_Click);
            // 
            // killDownloadToolProcessToolStripMenuItem
            // 
            this.killDownloadToolProcessToolStripMenuItem.Name = "killDownloadToolProcessToolStripMenuItem";
            this.killDownloadToolProcessToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.killDownloadToolProcessToolStripMenuItem.Text = "Kill Download Tool Process";
            this.killDownloadToolProcessToolStripMenuItem.Click += new System.EventHandler(this.killDownloadToolProcessToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblFlashing
            // 
            this.lblFlashing.BackColor = System.Drawing.Color.Lime;
            this.lblFlashing.Location = new System.Drawing.Point(12, 180);
            this.lblFlashing.Name = "lblFlashing";
            this.lblFlashing.Size = new System.Drawing.Size(109, 18);
            this.lblFlashing.TabIndex = 35;
            this.lblFlashing.Text = "0";
            this.lblFlashing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrCheckAdbProcess
            // 
            this.tmrCheckAdbProcess.Enabled = true;
            this.tmrCheckAdbProcess.Interval = 5000;
            this.tmrCheckAdbProcess.Tick += new System.EventHandler(this.tmrCheckAdbProcess_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(9, 230);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(49, 13);
            this.lblTime.TabIndex = 36;
            this.lblTime.Text = "00:00:00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 613);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblFlashing);
            this.Controls.Add(this.chkKillAdb);
            this.Controls.Add(this.lblRelaysOpened);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCannotStartGetDevicesCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblLoadingRelays);
            this.Controls.Add(this.lblGetDevicesStuck);
            this.Controls.Add(this.lblGetDevicesIssue);
            this.Controls.Add(this.chkUsbWatcher);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblAmountOfAdb);
            this.Controls.Add(this.lblInfoCannotStartGetDevicesCounter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblOpenedRelays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstRelays);
            this.Controls.Add(this.lblSerialCount);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 640);
            this.Name = "MainForm";
            this.Text = "ADVICES";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer tmrStartUp;
        private System.Windows.Forms.Timer tmrPeriodic;
        private System.Windows.Forms.Timer tmrPeriodicShort;
        private System.Windows.Forms.Label lblSerialCount;
        private System.Windows.Forms.ListBox lstRelays;
        private System.Windows.Forms.Timer tmrLoadRelays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOpenedRelays;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button cmdClearLog;
        private System.Windows.Forms.CheckBox chkUpdateLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblInfoCannotStartGetDevicesCounter;
        private System.Windows.Forms.Label lblAmountOfAdb;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chkUsbWatcher;
        private System.Windows.Forms.Label lblAdbParserStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer tmrPeriodicFast;
        private System.Windows.Forms.Label lblGetDevicesIssue;
        private System.Windows.Forms.Label lblGetDevicesStuck;
        private System.Windows.Forms.Label lblLoadingRelays;
        private System.Windows.Forms.Label lblCannotStartGetDevicesCounter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRelaysOpened;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkKillAdb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecoveryHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebootAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGNSSLogsOnAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem burnImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllRecoveryWatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllRecoveryWatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killADBProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setMaxBrightnessOnAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setMinBrightnessOnAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showErrorLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addNewDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.Label lblFlashing;
        private System.Windows.Forms.ToolStripMenuItem setScreenStayAwakeTRUEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setScreenStayAwakeFALSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.Timer tmrCheckAdbProcess;
        private System.Windows.Forms.ToolStripMenuItem killDownloadToolProcessToolStripMenuItem;
        private System.Windows.Forms.Label lblTime;
    }
}

