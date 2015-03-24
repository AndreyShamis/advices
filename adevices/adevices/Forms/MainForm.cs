using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using adevices;
using System.Drawing;
using System.Diagnostics;
using System.Management;
using System.IO.Pipes;
using System.Reflection;

namespace adbdevices
{
    public partial class MainForm : Form
    {
        //private System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
        private string m_set = @".\settings.ads";
        
        public MainForm()
        {
            if (AnotherInstanceExists())
            {
                MessageBox.Show("You cannot run more than one instance of this application.", "Only one instance allowed to run", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.ExitThread();
                Environment.Exit(0);
                Application.Exit();
                return;
            }
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Common.GetDevices();
            PrintAllDeviceInfo();

            AddToLog(Common.MDevices.Count + " devices count.");
        }

        private static int prev_table_hash = 0;

        private void PrintAllDeviceInfo()
        {
            
            //int hash = Common.MDevices.GetHashCode();
            //// || prev_table_hash != hash
            //if (lstDevices.Items.Count != Common.MDevices.Count)
            //{
            //    prev_table_hash = hash;
            //    //lstDevices.BeginInvoke(new MethodInvoker(() => UpdateDeviceList()));
            //    try
            //    {
            //        lstDevices.Items.Clear();
            //        foreach (AdbDevice tmp in Common.MDevices)
            //        {
            //            if (!lstDevices.Items.Contains(tmp.SerialNumber))
            //                lstDevices.Items.Add(tmp.SerialNumber);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Common.AddToLog("[]" + ex.Message);
            //    }
            //}

            string tmpStr = "";

            try
            {
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    tmpStr = tmpStr + tmp.SerialNumber + "-" + tmp.Adb_Status
                        + " [" + tmp.IsKeeperEnabled
                        + " [" + tmp.IsRecoveryEnabled
                        + " [" + tmp.ApkTestSupport
                        + " [" + tmp.IsRecoveryEnabled
                        + " [" + tmp.IsRecoveryProgress
                        + " [" + tmp.RelayID
                        + " [" + tmp.RelayPowerPort
                        + " [" + tmp.RelayUsbPort
                        + " [" + tmp.RecoveryCounter
                        + " [" + tmp.BatteryLevel.ToString()
                        + " [" + tmp.BaseBandVaerion.ToString()
                        + tmp.getNumberOfRecoveryInXMin(10).ToString()
                        + tmp.getNumberOfRecoveryInXMin(1).ToString()
                        + tmp.ShellTestTime.ToString()
                        + tmp.RelayPowerPort.ToString()
                        + tmp.RelayUsbPort.ToString()
                        + tmp.RelayPowerPort.ToString()
                        + tmp.RelayPowerPort.ToString() 
                        + tmp.CheckAPK + tmp.CheckShell + tmp.KeeperTime + tmp.ShellTestTime + tmp.ApkTestTime
                        + " [" + tmp.Uptime + "]" + "\r\n";
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[PrintAllDeviceInfo]" + ex.Message);
            }

            try
            {
                if (tmpStr.Length != textBox2.Text.Length)
                {
                    textBox2.BeginInvoke(new MethodInvoker(() => textBox2.Text = tmpStr));
                    //listView1.BeginInvoke(new MethodInvoker(() => UpdateDeviceTable()));
                    UpdateDeviceTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PrintAllDeviceInfo" + ex.Message);
            }
        }

        private void SetDeviceTableHeaderSettings()
        {
            try
            {
                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;

                ColumnHeader fch = new ColumnHeader();
                fch.Text = "Serial";
                fch.Width = 110;
                fch.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                listView1.Columns.Add(fch);
                listView1.Columns.Add("Status", 60);
                listView1.Columns.Add("Recovered", 60);
                listView1.Columns.Add("Bat", 60);
                listView1.Columns.Add("Recovery Status", 60);
                listView1.Columns.Add("Shell Check", 30);
                listView1.Columns.Add("APK Check", 30);
                listView1.Columns.Add("APK Check Time", 30);
                listView1.Columns.Add("Baseband", 220);
                listView1.Columns.Add("REC PROC", 60);
                listView1.Columns.Add("Relay ID", 70);
                listView1.Columns.Add("Power Port", 50);
                listView1.Columns.Add("USB Port", 50);
                listView1.Columns.Add("Keeper Status", 60);
                listView1.Columns.Add("Rec 10 Min", 60);
                listView1.Columns.Add("Rec 60 Min", 60);
                listView1.Columns.Add("Uptime", 140);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Update ADB Device Table
        /// </summary>
        private void UpdateDeviceTable()
        {
            try
            {
                try
                {
                    listView1.BeginInvoke(new MethodInvoker(() => listView1.Items.Clear()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("[Error][UpdateDeviceTable]" + ex.Message);
                }
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    try
                    {
                        string tmp_data = tmp.SerialNumber;
                        //lstDevices.BeginInvoke(new MethodInvoker(() => lstDevices.Items.Add(tmp_data)));

                        string[] arr = new string[20];
                        ListViewItem itm;

                        //Add first item
                        arr[0] = tmp.SerialNumber;
                        arr[1] = tmp.Adb_Status.ToString();

                        arr[2] = tmp.RecoveryCounter.ToString();
                        arr[3] = tmp.BatteryLevel.ToString();
                        arr[4] = tmp.IsRecoveryEnabled.ToString();

                        arr[5] = tmp.CheckShell.ToString();
                        arr[6] = tmp.CheckAPK.ToString();
                        arr[7] = tmp.ApkTestTime.ToString();
                        arr[8] = tmp.BaseBandVaerion;// tmp.ApkTestSupport.ToString();
                        arr[9] = tmp.IsRecoveryProgress.ToString();

                        arr[10] = tmp.RelayID.ToString();
                        arr[11] = tmp.RelayPowerPort.ToString();
                        arr[12] = tmp.RelayUsbPort.ToString();
                        arr[13] = tmp.IsKeeperEnabled.ToString();
                        arr[14] = tmp.getNumberOfRecoveryInXMin(10).ToString();
                        arr[15] = tmp.getNumberOfRecoveryInXMin(60).ToString();
                        arr[16] = tmp.Uptime.ToString();
                        itm = new ListViewItem(arr);
                        
                        if (tmp.Adb_Status.Equals(AdbStatus.notfound))
                        {
                            if (tmp.IsRecoveryProgress && tmp.IsRecoveryEnabled)
                            {
                                itm.BackColor = Color.Red;
                                itm.ForeColor = Color.White;
                            }
                            else if (!tmp.IsRecoveryProgress && tmp.IsRecoveryEnabled)
                            {
                                itm.ForeColor = Color.Red;
                            }
                            else if (!tmp.IsRecoveryEnabled)
                            {
                                itm.BackColor = Color.Gray;
                                itm.ForeColor = Color.Red;
                            }
                        }
                        else if (tmp.Adb_Status.Equals(AdbStatus.device))
                        {
                            if (tmp.IsRecoveryEnabled)
                            {
                                itm.BackColor = Color.Green;
                                itm.ForeColor = Color.White;
                            }
                            else
                                itm.ForeColor = Color.Green;
                        }
                        else if (tmp.Adb_Status.Equals(AdbStatus.unauthorized))
                        {
                            itm.BackColor = Color.Yellow;
                        }
                        else if (tmp.Adb_Status.Equals(AdbStatus.offline))
                        {
                            itm.BackColor = Color.SeaShell;
                        }

                        ListViewItem temp_itm = itm;
                        listView1.BeginInvoke(new MethodInvoker(() => listView1.Items.Add(temp_itm)));
                    }
                    catch (Exception ex)
                    {
                        Common.AddToLog("[UpdateDeviceTable]" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Common.AddToLog("[UpdateDeviceTable 2]" + ex.Message);
            }
        }
                
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListView tmp = ((ListView)sender);
            //ListViewItem tmp1 = tmp.FocusedItem;
            
            //if (!string.IsNullOrWhiteSpace(tmp.FocusedItem.Text))
            //{
            //   // tmp1.
            //    tmp1.BeginEdit();
            //}
        }
        /// <summary>
        /// Update device list
        /// </summary>
        //private void UpdateDeviceList()
        //{
        //    try{
        //        lstDevices.Items.Clear();
        //        foreach (AdbDevice tmp in Common.MDevices){         
        //            if (!lstDevices.Items.Contains(tmp.SerialNumber))
        //                lstDevices.Items.Add(tmp.SerialNumber);
        //        }
        //     } catch (Exception ex){
        //         Common.AddToLog("[UpdateDeviceList]" + ex.Message);              
        //     }
        //}

        /// <summary>
        /// Add To Log new entry
        /// </summary>
        /// <param name="newLog">new log entry</param>
        private void AddToLog(string newLog,bool putnewLine = true)
        {
            if (putnewLine)
                txtLog.Text = newLog + "\r\n" + txtLog.Text;
            else
                txtLog.Text = newLog  + txtLog.Text;
        }

        ///// <summary>
        ///// Open ADB device window property
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void lstDevices_DoubleClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(lstDevices.Text))
        //    {
        //        AdbDeviceProperty frm = new AdbDeviceProperty(lstDevices.Text);
        //        frm.Show();
        //    }
        //}

        /// <summary>
        /// Load aplication settings
        /// </summary>
        private void LoadSettings()
        {
            //Common.AddToLog("   *   Loading application settings...");
            string path = "";
            try{
                var serializer = new BinaryFormatter();
                path  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_set);
                path = string.Format("{0}", path);

                using (var stream = File.OpenRead(path))
                {
                    Common.MDevices = (ArrayList)serializer.Deserialize(stream);
                }
                AdbDevice.Sort();
                Common.AddToLog("   *   Settings loaded.");
            }catch (Exception ex){
                AutoClosingMessageBox.Show(ex.Message, "LoadSettingsFromFile ERROR", 1000);
            }
            finally{
                try{
                    string hour = DateTime.Now.ToString("HH_mm_ss");
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string filename = string.Format("settings_backup_{0}_{1}.ads",date,hour);
                    string pathBackup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                    System.IO.File.Copy(path, pathBackup, true);
                }
                catch(Exception ex){}
            }

            
        }



        private static bool m_USB_WatcherEnabled = false;
        ManagementEventWatcher watcher = null;
        static void m_mewWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (m_USB_WatcherEnabled)
            {
                Thread.Sleep(200);
                ThreadPool.QueueUserWorkItem(new WaitCallback((s) => Common.GetDevices()));
            }
            //Common.GetDevices();
        }

        public static bool AnotherInstanceExists()
        {
            Process currentRunningProcess = Process.GetCurrentProcess();
            Process[] listOfProcs = Process.GetProcessesByName(currentRunningProcess.ProcessName);

            foreach (Process proc in listOfProcs)
            {
                if ((proc.MainModule.FileName == currentRunningProcess.MainModule.FileName) && (proc.Id != currentRunningProcess.Id))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ApllicationExit);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text += "[" + version + "]";

            SetDeviceTableHeaderSettings();
            LoadSettings();
            //RPC rpc = new RPC();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => rpc.StartReceiver()));
            Thread cgd = new Thread(new ThreadStart(Common.GetDevices));
            cgd.Name = "GetDevicesOnStart";
            cgd.Start();
            
            
            
            //Common.AddToLog("Starting USB watcher");
            ////////var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent  where EventType = 1 or EventType = 2");
            //////////query.WithinInterval = new TimeSpan(0, 0, 3);
            ////////watcher = new ManagementEventWatcher(query);
            ////////watcher.EventArrived += m_mewWatcher_EventArrived;

            //////////watcher.Stopped += watcher_Stopped;
            ////////watcher.Query = query;
            ////////watcher.Start();
            Common.AddToLog("Application started");
            chkUsbWatcher.Checked = false;
            m_USB_WatcherEnabled = false;
            //Common.AddToErrors("APP STARTED");
            chkKillAdb.Checked = Common.AutoKillAdb;

        }

        private void ApllicationExit(object sender, EventArgs e)
        {
            try
            {
                if (watcher != null)
                {
                    watcher.Dispose();
                    watcher.Stop();
                }
            }
            catch (Exception){}
            SaveSettings();
        }

        private void SaveSettings()
        {
            var serializer = new BinaryFormatter();
            using (var stream = File.OpenWrite(m_set)){
                serializer.Serialize(stream, Common.MDevices);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //.KillGetDevicesProcess();
        }

        private void EnableApplication()
        {
            try
            {
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    tmp.EnableClass();
                    Thread.Sleep(3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error][EnableApplication] " + ex.Message);
            }
            Thread.Sleep(100);
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => UpdateDeviceTable()));
            UpdateDeviceTable();
            Thread.Sleep(100);
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    // You are debugging
            //    Common.DebugMode = true;
            //    Common.AddToLog(" ###  ###  ### Loaded in debug mode ###  ###  ### ");
            //    Common.AddToLog(" ###  ###  ### FTD2XX_NET will be loaded in delay ###  ###  ### ");
            //    tmrLoadRelays.Interval = 100;
            //}
            

            //PrintAllDeviceInfo();
            Thread ad = new Thread(new ThreadStart(Common.AdbDeviceList));
            ad.Name = "AdbDeviceList";
            ad.Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => Common.ExternalAPIHandler()));
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => Common.AdbDeviceList()));
        }

        private void tmrStartUp_Tick(object sender, EventArgs e)
        {

            tmrStartUp.Enabled = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => EnableApplication()));
            Common.AddToLog("tmrStartUp_Tick");
            tmrLoadRelays.Enabled = true;
            tmrPeriodicShort.Enabled = true;
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitFromApplication();

        }

        public void ExitFromApplication()
        {
            if (MessageBox.Show("Close application ??? - ???.\r\nConfirm ???", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                foreach (AdbDevice tmp in Common.MDevices)
                {
                    tmp.DisableClass();
                    tmp.Dispose();
                }

                ClearLogTextBox();
                SaveSettings();
                Common.ExitApplication = true;
                Thread.Sleep(1000);
                //if (Common.clientSocket != null )
                //     Common.clientSocket.Close();
                //Common.serverSocket.Stop();
                Application.ExitThread();
                Environment.Exit(0);
                Application.Exit();
            }
        }
        private void tmrPeriodicShort_Tick(object sender, EventArgs e)
        {

            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            //AddToLog(Common.MDevices.Count + " devices count.");
            try
            {
                if (Relay.serialsArray != null)
                {
                    lblSerialCount.Text = "Relays count:" + Relay.Relays.Count.ToString();
                    if (lstRelays.Items.Count != Relay.Relays.Count)
                    {
                        lstRelays.Items.Clear();

                        foreach (Relay tmp in Relay.Relays)
                        {
                            lstRelays.Items.Add(tmp.SerialId + ":" + tmp.RelayDescription);
                        }
                    }
                }
                else
                {
                    lblSerialCount.Text = "Relays count:" + "0";
                }

                //if (!checking_adb_process)
                //{
                //    Thread cadp = new Thread(new ThreadStart(CheckAdbProcess));
                //    cadp.Name = "CheckAdbProcess";
                //    cadp.Start();
                //    //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => CheckAdbProcess()));
                //}
                lblRelaysOpened.Text = Relay.openedRelays.Count.ToString();

                if (AdbDevice.RecoveryInProgressCount() > 0)
                {
                    lblFlashing.BackColor = Color.Linen;

                }
                else
                {
                    lblFlashing.BackColor = Color.Lime;
                }
                lblFlashing.Text = AdbDevice.RecoveryInProgressCount().ToString();

            }
            catch (Exception ex)
            {
                Common.AddToLog("[tmrPeriodicShort_Tick]" + ex.Message);
            }
            PrintLogToTextBox();
            PrintAllDeviceInfo();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PrintAllDeviceInfo()));
        }

        private bool checking_adb_process = false;
        private void CheckAdbProcess()
        {
            checking_adb_process = true;
            Thread.Sleep(500);
            try
            {
                int adb_f = AdbFound();
                int adb_f2 = 0;
                if (adb_f > Common.MAX_AdbCanRunAtSameTime && Common.AutoKillAdb)
                {
                    Thread.Sleep(2000);
                    adb_f2 = AdbFound();
                    if (adb_f2 > Common.MAX_AdbCanRunAtSameTime && adb_f2 == adb_f)
                    {
                        Common.AddToErrors("AMOUNT OF ADB IS TOO BIG:" + adb_f2 + ". STUCK");
                        
                        Common.KillAdbPrecess();
                        Common.KillGetDevicesProcess();
                        Thread.Sleep(2000);
                    }
                    else if (adb_f2 > 30)
                    {
                        Common.AddToErrors("AMOUNT OF ADB IS TOO BIG:" + adb_f2 + ". BIG");
                        Common.KillAdbPrecess();
                        Common.KillGetDevicesProcess();
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception)
            {

            }
            checking_adb_process = false;
        }
        private int AdbCounter = 0;
        private int AdbFound()
        {
            var dupl = (Process.GetProcessesByName("adb"));
            //lblAmountOfAdb.Text = dupl.Length.ToString();
            AdbCounter = dupl.Length;
            //if (lblAmountOfAdb.InvokeRequired)
            //{
            //    lblAmountOfAdb.Invoke(new MethodInvoker(delegate { lblAmountOfAdb.Text = dupl.Length.ToString(); }));
            //}
            return dupl.Length;
        }
        private void PrintLogToTextBox()
        {
            if (chkUpdateLog.Checked)
            {
                if (txtLog.Text.Length >= 150000)
                {
                    ClearLogTextBox();
                }
                ArrayList tmp = null;
                lock (Common.LogSync)
                {
                    tmp = new ArrayList(Common.Log);
                    Common.Log.Clear();
                }
                string tmp_all = "";
                foreach (string tmpLog in tmp)
                {
                    tmp_all = DateTime.Now.ToString("d HH:mm:ss") + ":" + tmpLog + "\r\n" + tmp_all;
                    //AddToLog(DateTime.Now.ToString("d HH:mm:ss") + ":" + tmpLog);
                }
                if (tmp_all.Length > 0)
                    AddToLog(tmp_all,false);
            }
        }
        private void ClearLogTextBox()
        {
            lock (Common.LogSync)
            {
                Common.WriteLogToFile(txtLog.Text);
                txtLog.Clear();
            }
        }

        private void tmrPeriodic_Tick(object sender, EventArgs e)
        {
            
            SaveSettings();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var DLL = Assembly.LoadFile(@"C:\Users\itl\Documents\Visual Studio 2010\Projects\adevices\adevices\bin\Debug\FTD2XX_NET.dll");

            //foreach (Type type in DLL.GetExportedTypes())
            //{
            //    var c = Activator.CreateInstance(type);
            //    type.InvokeMember("Output", BindingFlags.InvokeMethod, null, c, new object[] { @"Hello" });
            //}
            
           // ;
            //return serialsArray;

            //Console.ReadLine();
        }

        private void lstRelays_DoubleClick(object sender, EventArgs e)
        {
            if (lstRelays != null &&  !string.IsNullOrWhiteSpace(lstRelays.Text))
            {
                string[] serials = lstRelays.Text.Split(new char[] { ':' });
                string serial = "";
                if (serials.Length >= 1)
                {
                    serial = serials[0];
                    RelayProperty frm = new RelayProperty(serial);
                    frm.Show();
                }

            }
        }

        private void tmrLoadRelays_Tick(object sender, EventArgs e)
        {
            //Common.AddToLog("tmrLoadRelays_Tick");
            tmrLoadRelays.Interval = 60000;
            try
            {
                if (Common.RelaysFirstLoad)
                {
                    Common.RelaysFirstLoad = false;
                    Common.AddToLog("Starting load relays.");
                    Thread.Sleep(50);
                }
                Thread lr = new Thread(new ThreadStart(Common.LoadRelays));
                lr.Name = "Common.LoadRelays";
                lr.Start();
                //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => Common.LoadRelays()));
                
                tmrLoadRelays.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.AddToLog("[tmrLoadRelays_Tick]" + ex.Message);
            }
        }


        private void btnShowApkLogs_Click(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListView tmp = ((ListView)sender);

            if (!string.IsNullOrWhiteSpace(tmp.FocusedItem.Text))
            {
                AdbDeviceProperty frm = new AdbDeviceProperty(tmp.FocusedItem.Text);
                frm.Show();
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            SvaeFormSize();
        }

        private void SvaeFormSize()
        {
            this.panel1.Width = this.Width - 150;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            SvaeFormSize();
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            SvaeFormSize();
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            ListView tmp = ((ListView)sender);

            if (tmp != null && tmp.FocusedItem != null && !string.IsNullOrWhiteSpace(tmp.FocusedItem.Text))
            {
                AdbDeviceProperty frm = new AdbDeviceProperty(tmp.FocusedItem.Text);
                frm.Show();
            }
        }

        private void cmdClearLog_Click_1(object sender, EventArgs e)
        {

            ClearLogTextBox();
 
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            UpdateDeviceTable();
        }

        private void cmdStopAll_Click(object sender, EventArgs e)
        {

        }

        private void chkUsbWatcher_CheckedChanged(object sender, EventArgs e)
        {
            m_USB_WatcherEnabled = chkUsbWatcher.Checked;
        }

        private void SetColor(bool val, Label target)
        {
            try
            {
                if (val)
                {
                    target.BackColor = Color.Red;
                }
                else
                {
                    target.BackColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[SetColor]" + ex.Message);
            }
        }

        private void SetText(Label target,int value)
        {
            SetText(target,value.ToString());
        }

        private void SetText(Label target, string value)
        {
            try
            {
                target.Text = value.ToString();
            }
            catch (Exception ex)
            {
                Common.AddToLog("[SetText]" + ex.Message);
            }
        }

        private void tmrPeriodicFast_Tick(object sender, EventArgs e)
        {

            try
            {
                SetText(lblOpenedRelays, Relay.RelaysOpened);
                SetText(lblCannotStartGetDevicesCounter, Common.CannotStartGetDevicesCounter);
                SetText(lblAdbParserStatus, Common.AdbParserStatus);
                SetColor(Common.GetDevicesIssue, lblGetDevicesIssue);
                SetColor(Common.GetDevicesStuck, lblGetDevicesStuck);
                SetColor(Relay.LoadingRelays, lblLoadingRelays);
                lblAmountOfAdb.Text = AdbCounter.ToString();
            }
            catch (Exception) { }
        }


        private void cmdStartAll_Click(object sender, EventArgs e)
        {

        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cmdClearReacoveryHostory_Click(object sender, EventArgs e)
        {

        }

        public void ClearReacoveryHostory()
        {
            Thread cadrh = new Thread(new ThreadStart(AdbDevice.ClearAllDevicesRecoveryHistory));
            cadrh.Name = "AdbDevice.ClearAllDevicesRecoveryHistory";
            cadrh.Start();
            Thread.Sleep(200);
            UpdateDeviceTable();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => AdbDevice.ClearAllDevicesRecoveryHistory()));
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.DisableClass();
                tmp.Dispose();
            }

            ClearLogTextBox();
            SaveSettings();
            Common.ExitApplication = true;
            //if (Common.clientSocket != null)
            //    Common.clientSocket.Close();
            //Common.serverSocket.Stop();
            Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }

        
        private void cmdMinBrightness_Click(object sender, EventArgs e)
        {

        }

        private void cmdMaxBrightNess_Click(object sender, EventArgs e)
        {

        }

        private void cmdRebootAll_Click(object sender, EventArgs e)
        {


        }
        public void RebootAllDevices()
        {
            if (MessageBox.Show("Reboot All devices ??? - ???.\r\nConfirm ???", "Rebooting all devices", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Thread RebootAll = new Thread(AdbDevice.RebootAllThread);
                RebootAll.Name = "RebootAll";
                RebootAll.Start();
            }
        }
        private void chkKillAdb_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkKillAdb_Click(object sender, EventArgs e)
        {
            Common.AutoKillAdb = chkKillAdb.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitFromApplication();
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearRecoveryHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearReacoveryHostory();
        }

        private void rebootAllDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RebootAllDevices();
        }

        private void burnImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBurnImage frm = new frmBurnImage();
            frm.Show();
        }

        private void clearGNSSLogsOnAllDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread dgload = new Thread(new ThreadStart(AdbDevice.DeleteGnssLogsOnAllDevices));
            dgload.Name = "AdbDevice.DeleteGnssLogsOnAllDevices";
            dgload.Start();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => AdbDevice.DeleteGnssLogsOnAllDevices()));
        }

        private void killADBProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.KillAdbPrecess();
        }

        private void stopAllRecoveryWatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.IsRecoveryEnabled = false;
            }
        }

        private void startAllRecoveryWatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                if (tmp.RelayID.Length > 0 && tmp.RelayPowerPort > 0 && tmp.RelayUsbPort > 0 && tmp.SerialNumber.Contains("00440"))
                {
                    tmp.EnableAllChecks();
                }
            }
        }

        private void setMaxBrightnessOnAllDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread SetMaxBrightness = new Thread(AdbDevice.PutAllDevicesBrightnessToMaximum);
            SetMaxBrightness.Name = "SetMaxBrightness";
            SetMaxBrightness.Start();
        }

        private void setMinBrightnessOnAllDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread SetMinBrightness = new Thread(AdbDevice.PutAllDevicesBrightnessToMinimum);
            SetMinBrightness.Name = "SetMinBrightness";
            SetMinBrightness.Start();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationSettings frm = new ApplicationSettings();
            frm.Show();
        }

        private void showErrorLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationLogs frm = new ApplicationLogs();
            frm.Show();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void addNewDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewDevice frm = new frmAddNewDevice();
            frm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Show();
        }

        private void setScreenStayAwakeTRUEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread SetScreenAwakeTrueThread = new Thread(AdbDevice.SetScreenAwakeTrueThread);
            SetScreenAwakeTrueThread.Name = "SetScreenAwakeTrueThread";
            SetScreenAwakeTrueThread.Start();
        }

        private void setScreenStayAwakeFALSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread SetScreenAwakeFalseThread = new Thread(AdbDevice.SetScreenAwakeFalseThread);
            SetScreenAwakeFalseThread.Name = "SetScreenAwakeFalseThread";
            SetScreenAwakeFalseThread.Start();
        }

        private void tmrCheckAdbProcess_Tick(object sender, EventArgs e)
        {
            //AddToLog(Common.MDevices.Count + " devices count.");
            try
            {
                if (!checking_adb_process)
                {
                    Thread cadp = new Thread(new ThreadStart(CheckAdbProcess));
                    cadp.Name = "CheckAdbProcess";
                    cadp.Start();
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[tmrCheckAdbProcess_Tick]" + ex.Message);
            }
        }

        private void killDownloadToolProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.KillFlashToolProcess();
        }


        //Process process = Process.GetCurrentProcess();
        //var dupl = (Process.GetProcessesByName("adb"));
        ////if (dupl.Length > 1 && MessageBox.Show("Kill?", "Kill duplicates?", MessageBoxButtons.YesNo) == DialogResult.Yes)
        ////{
        ////    foreach (var p in dupl)
        ////    {
        ////        if (p.Id != process.Id)
        ////            p.Kill();
        ////    }
        ////}

        ////string cmd = @"tasklist /FI ""IMAGENAME eq adb.exe"" 2>NUL | find /I /C ""adb.exe""";
        ////MessageBox.Show(cmd);
    }
}
