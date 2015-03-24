using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using adevices;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace adbdevices
{

    [Serializable()]
    public enum AdbStatus
    {
        /// <summary>
        /// Is Online
        /// </summary>
        device,

        /// <summary>
        /// Is Offline
        /// </summary>
        offline,

        /// <summary>
        /// Device not presented in list
        /// </summary>
        notfound,

        /// <summary>
        /// Unauthorized
        /// </summary>
        unauthorized,

        /// <summary>
        /// Checking
        /// </summary>
        checking,

        /// <summary>
        /// recovery
        /// </summary>
        recovery,

        /// <summary>
        /// sideload
        /// </summary>
        sideload,

        /// <summary>
        /// Unknown
        /// </summary>
        unknown
    }

    [Serializable()]
    public class AdbDevice : IDisposable
    {
        public bool         IsRecoveryEnabled   { set; get; }
        public bool         IsKeeperEnabled     { set; get; }
        public string       SerialNumber        { set; get; }
        public AdbStatus    Adb_Status           { set; get; }
        public bool         AdbKeeperStatus     { set; get; }
        public string       RelayID             { set; get; }
        public short        RelayPowerPort      { set; get; }
        public short        RelayUsbPort        { set; get; }
        public string       Uptime              { set; get; }
        public string       BtMac               { set; get; }
        public string       WlanMac             { set; get; }
        public bool         IsRecoveryProgress  { set; get; }
        public string       AndroidVersion      { set; get; }
        public bool         ApkTestSupport      { set; get; }
        public string       FlashToolAddress    { set; get; }
        public string       FlashToolHubAddress { set; get; } 
        public int          ApkTestTime         { set; get; }
        public int          ShellTestTime       { set; get; }
        public int          RecoveryCounter     { set; get; }
        public bool         CheckAPK            { set; get; }
        public bool         CheckShell          { set; get; }
        public bool         UsbNormalyClosed    { set; get; }
        public bool         PowerNormalyClosed  { set; get; }
        public int          KeeperTime          { set; get; }
        public Queue<long>  m_RecoveryTime       { set; get; }
        public Dictionary<long, string> M_RecoveryKind { set; get; }
        private static readonly Random rnd  = new Random();
        private string      LastKindOfRecovery  = "";
        private int         m_ApkTestFailed     = 0;
        private bool        alreadyDisposed     = false;
        private bool        m_ClassDisabled     = false;
        private bool        searching_app       = false;
        private volatile string m_AndroidDebugLogs  = string.Empty;


        public int BatteryLevel { set; get; }
        public string       BaseBandVaerion     = "";
        public static string APK_FOLDER_PATH= @"C:/rec/";
        //private string      RecoveryWatchLocker = "RecoveryWatchLocker";
        //private bool LastApkAdbResult     = false;
        [field: NonSerialized]
        private System.Diagnostics.Process p;
        [field: NonSerialized]
        private List<string> apkToInstall;
        private string burningImage;
        [field: NonSerialized]
        private Thread rsw;
        [field: NonSerialized]
        private Thread raw;
        [field: NonSerialized]
        private Thread ak;
        //private string checkapk = "";


        /// <summary>
        /// 0 - unknown
        /// 1 - not installed
        /// 2 - installed
        /// </summary>
        public int ADT_APK_STATUS = 0;

        /// <summary>
        /// 0 - unknown
        /// 1 - not installed
        /// 2 - installed
        /// </summary>
        public int REC_APK_STATUS = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void setWLAN_MAC_Addr(string newAddr)
        {
            string tmp = newAddr.Trim();
            if (tmp.Length == 17)
            {
                WlanMac = tmp;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serialNumber">Device Serial Number</param>
        /// <param name="adbStatus">Current ADB Status</param>
        public AdbDevice(string serialNumber, string adbStatus)
        {
            this.ApkTestSupport = true;
            this.IsRecoveryProgress = false;
            this.IsKeeperEnabled = true;
            this.BtMac = "00:00:00:00:00:00";
            this.WlanMac = "00:00:00:00:00:00";
            this.RelayID = "";
            this.IsRecoveryEnabled = false;
            this.SerialNumber = serialNumber;
            this.SetAdbStatusFromString(adbStatus);
            this.KeeperTime = 3600;
            this.AdbKeeperStatus = true;
            this.ApkTestTime = 300;
            this.RecoveryCounter = 0;
            this.Uptime = "unknown";
            if (serialNumber.Contains("0044015299")){
                this.FlashToolHubAddress = "@VID_8087&PID_07F4";
            }
            else{
                this.FlashToolHubAddress = "@VID_8087&PID_07F2";
            }
            
            //this.Shell("getprop ro.build.version.release");
            EnableClass();
        }

        /// <summary>
        /// Enable Class which already exist in DB
        /// </summary>
        public void EnableClass()
        {

            apkToInstall = new List<string>();
            apkToInstall.Add("GPS Test.apk");
            //apkToInstall.Add("youtube-5.9.0.13.apk");
            apkToInstall.Add("gps.test.pro_1.46.apk");
            apkToInstall.Add("Wifi_Analyzer_v3.8.7.apk");
            //apkToInstall.Add("recovery.apk");

            //this.Shell("getprop ro.build.version.release");
            //this.RecoveryWatchLocker = "Starting";
            this.ApkTestSupport = true;
            this.IsRecoveryProgress = false;
            this.Uptime = "";

            if (CheckAPK == null)
                this.CheckAPK = true;

            if (CheckShell == null)
                this.CheckShell = true;

            m_ApkTestFailed = 0;
            m_ClassDisabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => LoadSelfThread()));

            
        }
        

        private void PostBuildName()
        {
            try
            {
                if (this.BaseBandVaerion != null && this.BaseBandVaerion.Length > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_BUILD", this.BaseBandVaerion);
                }
            }
            catch (Exception ex)
            {
                AddToLog("[PostBuildName]Error: " + ex.Message);
            }
        }
        private void PostUsbAddr()
        {
            try
            {
                if (this.FlashToolAddress != null && this.FlashToolAddress.Length > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_USB_ADDR", this.FlashToolAddress);
                }
            }
            catch (Exception ex)
            {
                AddToLog("[PostUsbAddr]Error: " + ex.Message);
            }
        }

        public bool delayedRecoveryEnabled = false;
        public void EnbaleRecoveryInDelay(int delaySecTime)
        {
            delayedRecoveryEnabled = true;
            int tempCounter = 0;
            for (tempCounter = 0; tempCounter < delaySecTime; tempCounter++)
            {
                Thread.Sleep(1000);
                tempCounter++;
                if (delayedRecoveryEnabled == false)
                {
                    return;
                }
            }
            
            this.IsRecoveryEnabled = true;
        }


        private void PostBatteryLevel()
        {
            try
            {
                if (this.BatteryLevel != null && this.BatteryLevel > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_BATTERY_LEVEL", this.BatteryLevel.ToString());
                }
            }
            catch (Exception ex)
            {
                AddToLog("[UPDATE_BATTERY_LEVEL]Error: " + ex.Message);
            }
        }

        private void PostBtMAC_ADDR()
        {
            try
            {
                if (this.BtMac != null && this.BtMac.Length > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_BT_MAC_ADDR", this.BtMac);
                }
            }
            catch (Exception ex)
            {
                AddToLog("[PostBtMAC_ADDR]Error: " + ex.Message);
            }
        }
        private void PostWlanMAC_ADDR()
        {
            try
            {
                if (this.WlanMac != null && this.WlanMac.Length > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_WLAN_MAC_ADDR", this.WlanMac);
                }
            }
            catch (Exception ex)
            {
                AddToLog("[PostWlanMAC_ADDR]Error: " + ex.Message);
            }
        }
        private void PostRelayAddr()
        {
            try
            {
                if (this.RelayID != null && this.RelayID.Length > 0)
                {
                    Common.PostUrlData(this.SerialNumber, "UPDATE_RELAY_ADDR", this.RelayID);
                }
            }
            catch (Exception ex)
            {
                AddToLog("[PostRelayAddr]Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSelfThread()
        {
            //this.Adb_Status = AdbStatus.checking;
            Thread.Sleep(1000 + rnd.Next(1000) + rnd.Next(1000));
            rsw = new Thread(new ThreadStart(RecoveryShellWatcher));
            rsw.Name = this.SerialNumber + ":" + "RecoveryShellWatcher";
            rsw.Start();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => RecoveryShellWatcher()));
            Thread.Sleep(1000 + rnd.Next(1000) + rnd.Next(1000));
            raw = new Thread(new ThreadStart(RecoveryApkWatcher));
            raw.Name = this.SerialNumber + ":" + "RecoveryApkWatcher";
            raw.Start();
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => RecoveryApkWatcher()));
            Thread.Sleep(1000 + rnd.Next(2000) + rnd.Next(2000));

            //if (!CheckIfApkInstalled())
           // {
           //     Common.AddToLog("INSTALLING APK ON START");
           //     ThreadPool.QueueUserWorkItem(new WaitCallback((s) => InstallApk("")));
                //InstallApk("");
            //}
            ak = new Thread(new ThreadStart(AdbKeeper));
            ak.Name = this.SerialNumber + ":" + "AdbKeeper";
            ak.Start();

            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostBuildName()));
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => AdbKeeper()));
        }

        /// <summary>
        /// SetAdbStatusFromString
        /// </summary>
        /// <param name="adbStatus">ADB status Strung</param>
        public void SetAdbStatusFromString(string adbStatus)
        {
            try{
                AdbStatus tmpPrev   = this.Adb_Status;
                this.Adb_Status     = (AdbStatus)System.Enum.Parse(typeof(AdbStatus), adbStatus);

                if (tmpPrev == AdbStatus.checking && this.Adb_Status == AdbStatus.notfound)
                {
                    LastKindOfRecovery = "SHELL:NOREC";
                    AddTime();
                }
                if (tmpPrev != AdbStatus.checking && tmpPrev != this.Adb_Status && this.Adb_Status == AdbStatus.device)
                {
                    Thread gav = new Thread(new ThreadStart(GetAndroidVersion));
                    gav.Name = this.SerialNumber + ":" + "GetAndroidVersion";
                    gav.Start();
                    this.CheckUptime();
                    //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => this.GetAndroidVersion()));
                }
            }
            catch (Exception ex){
                AutoClosingMessageBox.Show(ex.Message, "SetAdbStatusFromString ERROR " + adbStatus, 4000);
            }
        }

        /// <summary>
        /// GetAndroidVersion
        /// </summary>
        private void GetAndroidVersion()
        {
            if (this.Adb_Status == AdbStatus.device)
            {
                this.Shell(@"""getprop | grep version.release""");
                Thread.Sleep(1200);
            }
        }

        /// <summary>
        /// DisableRecoveryOnSettingError
        /// </summary>
        public void DisableRecoveryOnSettingError()
        {
            this.IsRecoveryEnabled = false;
            Common.AddToErrors("[" + this.SerialNumber + "] Please set correct settings for relay!!! Disabling recovery for this device");
        }

        /// <summary>
        /// Enable power port
        /// </summary>
        /// <returns>Return true on success</returns>
        public bool PowerOn()
        {
            if (this.RelayID == null || this.RelayID.Equals("")){
                DisableRecoveryOnSettingError();
                return false;
            }
            AddToLog("\tEnabling Power\t[PORT:" + this.RelayPowerPort + "].");
            if (this.IsPowerOn())
                return true;
            if (Relay.SetRelayPort(this.RelayID, this.RelayPowerPort, true ^ this.PowerNormalyClosed))
                return true;
            return PowerOn();
        }

        /// <summary>
        /// Disable power port
        /// </summary>
        /// <returns>Return true on success</returns>
        public bool PowerOff()
        {
            if (this.RelayID == null || this.RelayID.Equals("")){
                DisableRecoveryOnSettingError();
                return false;
            }
            AddToLog("\tDiabling Power\t[PORT:" + this.RelayPowerPort + "].");
            if (!this.IsPowerOn())
                return true;
            if (Relay.SetRelayPort(this.RelayID, this.RelayPowerPort, false ^ this.PowerNormalyClosed))
                return true;
            else
                return PowerOff();
        }

        /// <summary>
        /// Check if Power is ON
        /// </summary>
        /// <returns>Retrun true if Power port in ON</returns>
        public bool IsPowerOn()
        {
            if (this.RelayPowerPort > 0) {
                try{
                    Relay tmp = PreRelayTest();

                    if (tmp == null)
                    {
                        return false;
                    }
                    if (tmp.getPortByIndex(this.RelayPowerPort).PortStatus ^ this.PowerNormalyClosed)
                        return true;
                }
                catch (Exception ex){
                    AddToLog("\t[isPowerOn]" + ex.Message);
                }
            }
            else if (this.RelayPowerPort == -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if USB is ON
        /// </summary>
        /// <returns>Retrun true if USB port in ON</returns>
        public bool IsUsbOn()
        {
            if (this.RelayUsbPort > 0){
                try
                {
                    Relay tmp = PreRelayTest();
                    if (tmp != null && tmp.getPortByIndex(this.RelayUsbPort).PortStatus ^ this.UsbNormalyClosed)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    AddToLog("\t[isUsbOn]" + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Enable USB port
        /// </summary>
        /// <returns>True if pass</returns>
        public bool UsbOn()
        {
            if (this.RelayID == null || this.RelayID.Equals(""))
            {
                DisableRecoveryOnSettingError();
                return false;
            }
            AddToLog("\tEnabling USB\t[PORT:" + this.RelayUsbPort + "].");
            if (this.IsUsbOn())
                return true;
            if (Relay.SetRelayPort(this.RelayID, this.RelayUsbPort, true ^ this.UsbNormalyClosed))
                return true;

            return UsbOn();
        }

        public void AddToLog(string msg)
        {
            Common.AddToLog("\t[" + this.SerialNumber + "]\t" + msg);
        }

        /// <summary>
        /// Disable USB port
        /// </summary>
        /// <returns>True if pass</returns>
        public bool UsbOff()
        {
            if (this.RelayID == null || this.RelayID.Equals("")){
                DisableRecoveryOnSettingError();
                return false;
            }
            AddToLog("\tDisabling USB\t[PORT:" + this.RelayUsbPort + "].");
            if (!this.IsUsbOn())
                return true;
            if (Relay.SetRelayPort(this.RelayID, this.RelayUsbPort, false ^ this.UsbNormalyClosed))
                return true;
            else
                return UsbOff();
        }

        /// <summary>
        /// Check if have seted relay
        /// </summary>
        /// <returns>True if have</returns>
        public bool IfHaveRelay()
        {
            if (string.IsNullOrEmpty(this.RelayID) || this.RelayID.Equals("NotPresent"))
                return false;
            return true;
        }

        /// <summary>
        /// Preload Relay Test
        /// </summary>
        /// <returns>Return relay pointer</returns>
        private Relay PreRelayTest()
        {
            if (!IfHaveRelay()){
                this.RelayID        = "";
                this.RelayPowerPort = 0;
                this.RelayUsbPort   = 0;
                Common.AddToErrors("[" + this.SerialNumber + "]" + "There is no relay");
                return null;
            }
            if ((this.RelayPowerPort < 1 && this.RelayPowerPort != -1) || this.RelayUsbPort < 1){
                Common.AddToErrors("[" + this.SerialNumber + "]" + "Please set Ports");
                return null;
            }
            int MAX_TRYS = 3;
            bool res = false;
            int count = 0;
            Relay tmp  = null;
            int try_counter = 0;
            int MAX_TRYS_CHECK = 10;
            while (tmp == null && try_counter <= MAX_TRYS_CHECK)
            {
                tmp = Relay.getRelayById(this.RelayID);
                if (tmp == null){
                    Thread.Sleep(10);
                    try_counter++;
                }
                if (try_counter >= MAX_TRYS_CHECK)
                {
                    Common.AddToErrors("[PreRelayTest] Get max number of trys. Exit.");
                    return null;
                }
            }

            while (count++ <= MAX_TRYS && !res){
                res = tmp.UpdatePortsStatus();
                Thread.Sleep(10 + rnd.Next(10,100));
            }

            if (!res){
                Common.AddToErrors("Cannot run pre test");
            }
            return tmp;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~AdbDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// SetScreenStayAwake
        /// </summary>
        /// <param name="newValue"></param>
        public void SetScreenStayAwake(bool newValue = false)
        {
            try
            {
                if (newValue == false)
                {
                    this.Shell("svc power stayon false", true, 1000);
                    this.Shell("am startservice --user 0 -a android.intent.action.run -n com.intel.mwg/com.intel.Dispatcher -e ITE_METHOD DUT.Application.SetDisplayStayOnDelay -e ARG 15", true, 2000);
                }
                else
                {
                    this.Shell("svc power stayon true", true, 1000);
                    this.Shell("am startservice --user 0 -a android.intent.action.run -n com.intel.mwg/com.intel.Dispatcher -e ITE_METHOD DUT.Application.SetDisplayStayOnDelay -e ARG 1800", true, 2000);
                }
                
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// BurnImageDataHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BurnImageDataHandler(object sender, DataReceivedEventArgs e)
        {
            // Manipulate received data here
            if (e.Data != null &&
                e.Data != "" &&
                !e.Data.Contains("exit") &&
                !e.Data.Contains("Microsoft "))
            {
                try{
                    AddToLog(e.Data.ToString().Replace("[BurnImageDataHandler]",""));
                }
                catch (Exception) {

                }
            }
        }


        /// <summary>
        /// BurnImage
        /// </summary>
        /// <param name="path"></param>
        public void BurnImage(object pathInput)
        {
            string path = pathInput as string;
            if (burningImage == null)
            {
                char[] letters = { 'A', 'B', 'C' };
                burningImage = new string(letters);
            }
            AddToLog("Starting BurnImage for " + path);
            IsRecoveryEnabled   = false;
            IsKeeperEnabled     = false;
            CheckShell          = false;
            CheckAPK            = false;


            p = new Process();
            //if (p == null)
            //{
            //    p = new Process();

            //}
            //else
            //{
            //    AddToLog("Starting to Kill Process");
            //    try
            //    {
            //        p.Kill();
            //    }
            //    catch (Exception ex)
            //    {
            //        AddToLog("Cannot kill Process:" + ex.Message);
            //    }
            //}
            p.StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                p.OutputDataReceived -= BurnImageDataHandler;
            }
            catch (Exception) { }
            AddToLog("Start burn image Process Started");
            while (this.IsUsbOn())
            {
                AddToLog("Disabling USB");
                this.UsbOff();
                Thread.Sleep(11);
            }
            Thread.Sleep(17);
            while (this.IsPowerOn())
            {
                AddToLog("Disabling Power");
                this.PowerOff();
                Thread.Sleep(13);
            }

            p.OutputDataReceived += BurnImageDataHandler;
            p.EnableRaisingEvents = true;

            p.Start();
            p.PriorityBoostEnabled = true;
            p.StandardInput.AutoFlush = true;
            p.PriorityClass = ProcessPriorityClass.Normal;
            Thread.Sleep(1001);
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();


            Thread.Sleep(4999);

            string[] filePaths = Directory.GetFiles(path, "*.fls");

            string files = "";
            foreach (var tmp in filePaths)
            {
                files = files +@" """ + tmp + @"""";
            }


            AddToLog("Start burn image :" + files);


            Stopwatch sw = Stopwatch.StartNew();


            string str = "DownloadTool.exe --library DownloadTool.dll   --cond-erase -c \"" + FlashToolHubAddress + @"\" + FlashToolAddress + "\"" + @"  " + files + " && exit";
            AddToLog("FLASH:" + str);
            //lock (burningImage)
            //{
                p.StandardInput.WriteLine(str);
                Thread.Sleep(1000);
                //Thread.Sleep(2000);
                while (!this.IsUsbOn())
                {
                    this.UsbOn();
                    Thread.Sleep(3);
                }
                Thread.Sleep(300);
                while (!this.IsPowerOn())
                {
                    this.PowerOn();
                    Thread.Sleep(3);
                }
            //}
            //p.StandardInput.WriteLine("exit");

            if (p.WaitForExit(50* 60 * 1000))
            {
                p.WaitForExit();
            }
            p.Close();
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("                      {0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            AddToLog(elapsedTime);
            //if (p.ExitCode > 0)
            //{
            //    AddToLog("Finish!!! Exit code is " + p.ExitCode + " Abotring next flow.");
            //    return;
           // }
            //p.Close();
            AddToLog("Finish!!! Waiting 1.5 minutes");

            Thread.Sleep(90000);
            IsRecoveryEnabled   = true;
            IsKeeperEnabled     = true;
            CheckShell          = true;
            CheckAPK            = true;
            int apkTestTimeTemp = this.ApkTestTime;
            this.ApkTestTime = 3;
            getBaseBandVersion();
            Thread.Sleep(30000);
            getBaseBandVersion();
            InstallApk("");
            this.ApkTestTime = apkTestTimeTemp;
            AddToLog("Finish!!!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            string ret = "";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(this);
                ret = ret + "\r\n" + string.Format("{0}\t=\t{1}", name, value);
            }

            Type t = this.GetType();
            PropertyInfo[] pi = t.GetProperties();
            ret = ret + "\r\nOther dump\r\n";
            try
            {
                foreach (PropertyInfo p in pi)
                {
                    if (p != null)
                    {
                        string name = p.Name;
                        object value = p.GetValue(this, null).ToString();
                        ret = ret + "\r\n" + string.Format("{0}\t=\t{1}", name, value);
                    }
                }
            }
            catch (Exception ex) { }
            ret = ret + "\r\nOther dump\r\n";
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Manipulate received data here
            if (e.Data != null && 
                e.Data != "" && 
                !e.Data.Contains("exit") && 
                !e.Data.Contains("adb -s ") && 
                !e.Data.Contains("Microsoft "))
            {
                if (e.Data.Contains("version.release]: [4.4.2]"))
                {
                    this.AndroidVersion = "4.4.2";
                }
                if (e.Data.Contains("version.release]: [4.4.4]"))
                {
                    this.AndroidVersion = "4.4.4";
                }
                if (e.Data.Contains("version.release]: [4.2]"))
                {
                    this.AndroidVersion = "4.2";
                }

                if (e.Data.Contains("package:com.intel.mwg"))
                {
                    ADT_APK_STATUS = 2;
                    Common.AddToLog("[" + this.SerialNumber + "]:" + e.Data);
                }
                else if (e.Data.Contains("package:com.intel.recovery"))
                {
                    REC_APK_STATUS = 2;
                    Common.AddToLog("[" + this.SerialNumber + "]:" + e.Data);
                }
                //
                if(e.Data.Contains("baseband")){
                    Common.AddToLog("Baseband received " + BaseBandVaerion);
                    BaseBandVaerion = e.Data;
                    BaseBandVaerion = BaseBandVaerion.Replace("[gsm.version.baseband]: ","");
                    
                    BaseBandVaerion = BaseBandVaerion.Replace("[DEV_XMM6321_FSY_AGES2.0_512MB_01.", "");
                    BaseBandVaerion = BaseBandVaerion.Replace("_PMB]", "");
                    BaseBandVaerion = BaseBandVaerion.Replace("DEV_", "");
                    BaseBandVaerion = BaseBandVaerion.Replace("_FSY", "");
                    BaseBandVaerion = BaseBandVaerion.Replace("]", "");
                    BaseBandVaerion = BaseBandVaerion.Replace("[", "");

                    if(this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
                    {
                        this.FlashToolHubAddress = "@VID_8087&PID_07F4";
                    }
                }

                if (e.Data.Contains("address=") && !e.Data.Contains("device_address="))
                {
                    string tmp =  e.Data.Replace("address=", "");
                    if(tmp.Length == 17)
                    {
                        WlanMac = tmp;
                        Common.AddToLog("WLAN MAC ADDR " + WlanMac);
                    }
                }

                if (e.Data.Contains("bluetooth_address="))
                {
                    BtMac = e.Data.Replace("bluetooth_address=", "");
                    Common.AddToLog("BT MAC ADDR " + BtMac);
                }


                if (e.Data.Contains("level:"))
                {
                    string tmp = e.Data;
                    tmp = tmp.Replace("level:", "");
                    try
                    {
                        BatteryLevel = Int32.Parse(tmp);
                    }
                    catch (Exception ex) { }
                }

                string r_data = e.Data.ToString();
                if (!r_data.Contains("android.intent.action.run cmp=com.intel.recovery") 
                    && !r_data.Contains("temperature:")
                    && !r_data.Contains("voltage:")
                    && !r_data.Contains("scale:")
                    && !r_data.Contains("present: true")
                    && !r_data.Contains("health: 2")
                    && !r_data.Contains("status: 2")
                    && !r_data.Contains("Wireless powered: false")
                    && !r_data.Contains("USB powered: true")
                    && !r_data.Contains("AC powered: false")
                    && !r_data.Contains("Current Battery Service state:")
                    && !r_data.Contains("technology: Li-ion")
                    && !r_data.Contains("charge counter:")
                    && !r_data.Contains(" current now:")
                    && !r_data.Contains("current now:")
                    && !r_data.Contains("LinkAddresses:")
                    && !r_data.Contains("mLinkProperties:")
                    && !r_data.Contains("p2p_device_address")
                    && !r_data.Contains("ip_address=")
                    )
                {
                    Common.AddToLog("[" + this.SerialNumber + "]:" + e.Data);
                }
                try
                {
                    if (e.Data.Contains("up time: "))
                    {
                        int startindex = e.Data.IndexOf(":") + 1;
                        int endindex = e.Data.IndexOf("idle") + 1;
                        this.Uptime = e.Data.Substring(startindex + 1, endindex - startindex - 4);
                    }
                }
                catch (Exception)
                {

                }
                //Console.WriteLine(this.SerialNumber + ":" + e.Data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDataReceivedAdb(object sender, DataReceivedEventArgs e)
        {
            // Manipulate received data here
            if (e.Data != null &&
                e.Data != "" &&
                !e.Data.Contains("exit") &&
                !e.Data.Contains("adb -s ") &&
                !e.Data.Contains("Microsoft "))
            {
                Common.AddToLog("[" + this.SerialNumber + "]:" + e.Data);
                Console.WriteLine(this.SerialNumber + ":" + e.Data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearLogcat()
        {
            var processCleaner = Run("adb.exe", " -s " + this.SerialNumber + " logcat -c", null, null);
            processCleaner.WaitForExit(3000);
            FreeProcess(processCleaner);
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnableAllChecks()
        {
            IsRecoveryEnabled   = true;
            IsKeeperEnabled     = true;
            CheckAPK            = true;
            CheckShell          = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckApplicationStatus()
        {
            if (REC_APK_STATUS == 0 || ADT_APK_STATUS == 0)
            {
                CheckIfApkInstalled();
            }
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => this.ClearLogcat()));
            
            searching_app = true;
            //Thread.Sleep(20);
            //bool ret = this.ApkAdb(" am startservice --user 0 -a android.intent.action.run -n com.intel.mwg/com.intel.Dispatcher -e ITE_METHOD DUT.System.IsAlive");
            bool ret = this.ApkAdb(" am startservice --user 0 -a android.intent.action.run -n com.intel.recovery/com.intel.recovery.Dispatcher -e ITE_METHOD DUT.System.IsAlive");
            //if (!this.ApkTestSupport && ret == false && !this.IsRecoveryProgress)
            //{
            //    LastKindOfRecovery = "APK install";
            //    //if (REC_APK_STATUS == 1 || ADT_APK_STATUS == 1)
            //    //    InstallApk("");
            //    //searching_app = false;
            //    return true;
            //}
            //searching_app = false;
            return ret;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckIfApkInstalled()
        {
            if (this.Adb_Status.Equals(AdbStatus.device)) //|| this.Adb_Status.Equals(AdbStatus.checking)
            {
                REC_APK_STATUS = 1;
                ADT_APK_STATUS = 1;
                this.Shell(@"""pm list packages | find intel.""");

                //Thread.Sleep(1300);
                if (REC_APK_STATUS == 2 || ADT_APK_STATUS == 2)
                    return true;

                return false;
            }
            return true;
        }

        static public void PutAllDevicesBrightnessToMinimum()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.SetMinBrightness();
            }
        }
        static public void PutAllDevicesBrightnessToMaximum()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.SetMaxBrightness();
            }
        }
        static public void SetScreenAwakeTrueThread()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.SetScreenStayAwake(true);
            }
        }
        static public void SetScreenAwakeFalseThread()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                tmp.SetScreenStayAwake(false);
            }
        }

        static public void RebootAllThread()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {

                Thread RebootAll = new Thread(tmp.RebootDeviceThred);
                RebootAll.Name = "RebootAll_"+ tmp.SerialNumber;
                RebootAll.Start();
            }
        }
        private void RebootDeviceThred()
        {
            if (this.IsRecoveryEnabled)
            {
                this.IsRecoveryEnabled = false;
                this.RemoteRebootDevice();
                Thread.Sleep(80);
                this.IsRecoveryEnabled = true;
            }
            else
            {
                this.RemoteRebootDevice();
            }
        }
        public void getBaseBandVersion()
        {
            this.Shell(@"""getprop | grep version.baseband""", true, 3000, false);
        }

        public void getBatteryLevel()
        {
            this.Shell(@"""dumpsys battery """, true, 3000, false);
        }

        public void getBlueToothAddress()
        {
            this.Shell(@"""settings get secure bluetooth_address""", true, 3000, false);
        }
        public void TryToFoundWlanMacAddr()
        {
            this.Shell(@"""dumpsys wifi | grep -i ""address"" """, true, 3000, false);
        }
        public void TryToFoundBtMacAddr()
        {
            this.Shell(@"""echo bluetooth_address=$(settings get secure bluetooth_address) """, true,3000,false);
        }

        public void SetMaxBrightness()
        {
            this.Shell(@"""settings put system screen_brightness 255""", true, 3000, false);
        }
        public void SetMinBrightness()
        {
            this.Shell(@"""settings put system screen_brightness 1""", true, 3000, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationPath"></param>
        public void InstallApk(string applicationPath)
        {
            //return;
            this.GetAndroidVersion();
            Common.AddToLog("INSTALLING APK " + this.SerialNumber);
            Common.AddToLog("[" + this.SerialNumber + "]: Installing APK");
            //this.Shell(@"push """ + AdbDevice.APK_FOLDER_PATH + @"mwgActivity-release.apk"" /sdcard/mwgActivity-release.apk",false);
            //this.Shell(@"push """ + AdbDevice.APK_FOLDER_PATH + @"recovery.apk"" /sdcard/recovery.apk", false);
            //this.Shell(@"push """ + AdbDevice.APK_FOLDER_PATH + @"moorfield_signed.apk"" /sdcard/moorfield_signed.apk", false);
            this.Shell(@"push """ + AdbDevice.APK_FOLDER_PATH + @"apk"" /sdcard", false,20000);
            this.Shell(@"su -c mount -o remount,rw /system",true,10000);
            string apkName = "";
            if (this.AndroidVersion == "4.4.2" || this.AndroidVersion == "4.4.4")
            {
                /*
                 * 
                 * Moorfield appk
                 */
                if (this.SerialNumber.Contains("INV142"))
                {
                    apkName = "moorfield_signed.apk";
                }
                else if (this.SerialNumber.Contains("0044015299") || this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
                {
                    apkName = "mwgActivity-release-sofia.apk";

                    this.Shell(@"su -c rm /system/priv-app/Google_Play_Store_v5.0.38.apk",true,10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c cp /sdcard/Google_Play_Store_v5.0.38.apk /system/priv-app/", true, 10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c chmod 777 /system/priv-app/Google_Play_Store_v5.0.38.apk", true, 10000);

                    Thread.Sleep(20);
                    this.Shell(@"su -c rm /system/priv-app/google_play_services_4.0.34.apk", true, 10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c cp /sdcard/google_play_services_4.0.34.apk /system/priv-app/", true, 10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c chmod 777 /system/priv-app/google_play_services_4.0.34.apk", true, 10000);

                    Thread.Sleep(20);
                    this.Shell(@"su -c rm /system/priv-app/youtube-5.9.0.13.apk", true, 10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c cp /sdcard/youtube-5.9.0.13.apk /system/priv-app/", true, 10000);
                    Thread.Sleep(10);
                    this.Shell(@"su -c chmod 777 /system/priv-app/youtube-5.9.0.13.apk", true, 10000);
                }
                else
                {
                    apkName = "mwgActivity-release.apk"; 
                }
                this.Shell(@"su -c rm /system/priv-app/" + apkName, true, 10000);
                Thread.Sleep(10);
                this.Shell(@"su -c cp /sdcard/" + apkName + " /system/priv-app/", true, 10000);
                Thread.Sleep(10);
                this.Shell(@"su -c chmod 777 /system/priv-app/" + apkName, true, 10000);


                this.Shell(@"su -c rm /system/app/recovery.apk", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c cp /sdcard/recovery.apk /system/priv-app/", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c chmod 777 /system/priv-app/recovery.apk", true, 10000);
            }
            else
            {
                this.Shell(@"su -c rm /system/app/mwgActivity-release.apk", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c cp /sdcard/mwgActivity-release.apk /system/app/", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c chmod 777 /system/app/mwgActivity-release.apk", true, 10000);

                this.Shell(@"su -c rm /system/app/recovery.apk", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c cp /sdcard/recovery.apk /system/app/", true, 10000);
                Thread.Sleep(20);
                this.Shell(@"su -c chmod 777 /system/app/recovery.apk", true, 10000);
            }

            bool firstApk = true;
            foreach (string tmpApk in apkToInstall)
            {
                
                this.Shell(@"su -c pm install ""/sdcard/" + tmpApk + @"""", true, 10000);
                Thread.Sleep(1000);
                if (firstApk == true)
                {
                    Thread.Sleep(5000);
                    this.Shell(@"input keyevent 66", true, 10000);
                    Thread.Sleep(3000);
                    this.Shell(@"input keyevent 22", true, 10000);
                    Thread.Sleep(3000);
                    this.Shell(@"input keyevent 66", true, 10000);
                    firstApk = false;
                    Thread.Sleep(3000);
                }
                

            }
            foreach (string tmpApk in apkToInstall)
            {
                this.Shell(@"install ""apk/" + tmpApk + "", false, 10000);
                Thread.Sleep(1000);
            }
            //press ok on XMM
            this.Shell(@"input touchscreen tap 390 790", false, 10000);
            Thread.Sleep(4000);
            this.RemoteRebootDevice();
            Thread.Sleep(80000);
             
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoteRebootDevice()
        {
            this.Shell("su -c reboot");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <param name="stdoutHandler"></param>
        /// <param name="stderrHandler"></param>
        /// <returns></returns>
        private Process Run(string command, string args, DataReceivedEventHandler stdoutHandler, DataReceivedEventHandler stderrHandler)
        {
            ProcessStartInfo psi = new ProcessStartInfo(command);
            psi.RedirectStandardOutput = stdoutHandler != null;
            psi.RedirectStandardError = stderrHandler != null;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.Arguments = args;
            
            Process result = Process.Start(psi);
            
            if (result == null)
                throw new Exception("Failed to run " + command + " " + args);
            if (stdoutHandler != null)
            {
                result.OutputDataReceived += stdoutHandler;
                result.BeginOutputReadLine();
            }
            if (stderrHandler != null)
            {
                result.ErrorDataReceived += stderrHandler;
                result.BeginErrorReadLine();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void PreUsbRecovery()
        {
            int counter     = 0;
            int MAX_COUNTER = 3;
            Common.AddToLog("[" + this.SerialNumber + "] # # # Starting PreUsbRecovery");
            this.IsRecoveryProgress = true;
            if (this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
            {
                MAX_COUNTER = 5;
            }
            while (!this.Adb_Status.Equals(AdbStatus.device) && counter++ <= MAX_COUNTER && this.IsRecoveryEnabled)
            {
                Thread.Sleep(10);
                this.UsbOff();
                Thread.Sleep(600);
                this.UsbOn();
                Thread.Sleep(3000 + counter * 1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PostUsbRecovery()
        {
            int counter     = 0;
            int MAX_COUNTER = 4;
            Common.AddToLog("[" + this.SerialNumber + "] # # # Starting PostUsbRecovery");
            this.IsRecoveryProgress = true;
            if (this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
            {
                MAX_COUNTER = 6;
            }
            while (!this.Adb_Status.Equals(AdbStatus.device) && counter++ <= MAX_COUNTER &&  this.IsRecoveryEnabled)
            {
                Thread.Sleep(10);
                this.UsbOff();
                Thread.Sleep(600);
                this.UsbOn();
                Thread.Sleep(4000 + counter * 1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool isRecoveryInProgress()
        {
            foreach (AdbDevice tmp in Common.MDevices)
            {
                if (tmp.IsRecoveryProgress)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int RecoveryInProgressCount()
        {
            int count = 0;
            foreach (AdbDevice tmp in Common.MDevices)
            {
                if (tmp.IsRecoveryProgress)
                {
                    count++;
                }

            }
            return count;
        }
        private void ExitFromRecoveryMode()
        {
            if (this.Adb_Status.Equals(AdbStatus.recovery))
            {
                AddToLog("Strating recovery from recovery mode by reboot");
                Shell("reboot");
                Thread.Sleep(60 * 1000);
            }
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hard"></param>
        private void StartingRecovery(bool hard = false)
        {
            int LOAD_TIME = 15;
            int counter = 0;
            //int MAX_COUNTER = 3;
            try
            {
                this.IsRecoveryProgress = true;
                if (hard)
                {
                    InstallApk("");
                    Common.AddToLog("[" + this.SerialNumber + "] # # # Starting HARD Recovery FRAMEWORK ISSUE");
                }
                else
                {
                    ExitFromRecoveryMode();
                    Common.AddToLog("[" + this.SerialNumber + "] # # # Starting Recovery");
                    if (this.IsPowerOn())
                    {
                        PreUsbRecovery();
                        if (this.Adb_Status.Equals(AdbStatus.device))
                        {
                            this.IsRecoveryProgress = false;
                            return;
                        }
                    }
                }
                if (!this.IsRecoveryEnabled)
                {
                    return;
                }
                bool all_disabled = false;
                try
                {
                    while (this.IsUsbOn() && (this.IsPowerOn() || this.RelayPowerPort == -1) && !all_disabled && this.IsRecoveryEnabled)
                    {
                        while (this.IsUsbOn())
                        {
                            this.UsbOff();
                            Thread.Sleep(700);
                        }
                        if (this.RelayPowerPort != -1)
                        {
                            while (this.IsPowerOn())
                            {
                                this.PowerOff();
                                Thread.Sleep(1000);
                            }
                        }

                        if (this.IsUsbOn() && (this.IsPowerOn() || this.RelayPowerPort == -1))
                        {
                            all_disabled = true;
                        }
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                    all_disabled = false;
                }

                if (!this.IsRecoveryEnabled)
                {
                    return;
                }

                Thread.Sleep(3000);
                if (this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
                {
                    Thread.Sleep(8000);
                }
                this.PowerOn();
                Thread.Sleep(11000);
                this.UsbOn();
                Thread.Sleep(11000);

                if (this.BaseBandVaerion.ToLower().Contains("SOFIA_3G".ToLower()))
                {
                    LOAD_TIME = 45;
                }
                counter = 0;
                while (!this.Adb_Status.Equals(AdbStatus.device) && counter++ <= LOAD_TIME) {
                    Thread.Sleep(1000);
                }
                this.RecoveryCounter++;
                Thread.Sleep(3000);

                ExitFromRecoveryMode();

                PostUsbRecovery();
                AddTime();
                this.Shell("uptime");
                getBaseBandVersion();
            }
            catch (Exception){ }
            finally
            {
                Common.AddToLog("[" + this.SerialNumber + "]" + "  # # # Recovery finished");
                //lock (this.RecoveryWatchLocker)
                //{
                    this.IsRecoveryProgress = false;
                //}
            }
        }

        /// <summary>
        /// Insert recovery time
        /// </summary>
        private void AddTime()
        {
            long lastTimeStamp = TimeNow();

            try
            {
                if (m_RecoveryTime == null)
                {
                    m_RecoveryTime = new Queue<long>();
                }
                if (M_RecoveryKind == null)
                {
                    M_RecoveryKind = new Dictionary<long, string>();
                }
                m_RecoveryTime.Enqueue(lastTimeStamp);
                M_RecoveryKind.Add(lastTimeStamp, LastKindOfRecovery);
                Common.PostUrlData(this.SerialNumber, "UPDATE_RECOVERY", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "__" + string.Format("{0:HH:mm:ss}", DateTime.Now) + "__" + LastKindOfRecovery);
                ClearTimeOfRecovery();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getNumberOfRecoveryIn20Min()
        {
            return getNumberOfRecoveryInXMin(20);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public int getNumberOfRecoveryInXMin(int min)
        {
            int count = 0;
            try
            {
                if (m_RecoveryTime != null)
                {
                    Queue<long> tmp_r = new Queue<long>(m_RecoveryTime.Reverse());
                    long tmp_date = TimeNow() - min * 60;

                    foreach (long tmp in tmp_r)
                    {
                        if (tmp >= tmp_date)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception) { }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long TimeNow()
        {
            return (DateTime.UtcNow.Ticks / 10000000);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearTimeOfRecovery()
        {
            long  lastTimeStamp = AdbDevice.TimeNow() - 3600*24*5;
            Queue<long> tmpa    = new Queue<long>();
            if (m_RecoveryTime != null && m_RecoveryTime.Count > 0){
                while (m_RecoveryTime.Count > 0) {
                    long tmp = m_RecoveryTime.Dequeue();
                    if (tmp >= lastTimeStamp){
                        tmpa.Enqueue(tmp);
                    }
                }
                m_RecoveryTime = tmpa;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RecoveryApkWatcher()
        {
            Thread.Sleep(20000 + rnd.Next(20000));
            int ApkCheckTimeVariable = this.ApkTestTime;
            while (true && !m_ClassDisabled)
            {
                if (this.ApkTestTime < 1){
                    this.ApkTestTime = 10;
                }
                else if (this.IsRecoveryEnabled && !this.IsRecoveryProgress && this.CheckAPK) {
                    while (this.Adb_Status.Equals(AdbStatus.checking)){
                        Thread.Sleep(10);
                    }
                    if (this.Adb_Status.Equals(AdbStatus.device) && delayedRecoveryEnabled == false)
                    {
                        getBatteryLevel();
                        if (CheckApplicationStatus()){
                            this.m_ApkTestFailed = 0;
                            ApkCheckTimeVariable = this.ApkTestTime;
                            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostBatteryLevel()));
                        }
                        else
                        {
                            this.m_ApkTestFailed++;
                            
                            if (this.m_ApkTestFailed >= 3)
                            {
                                Common.AddToLog("[" + this.SerialNumber + "] # # # Device  NO APK.");
                                if (!this.IsRecoveryProgress)
                                {
                                    LastKindOfRecovery = "APK";
                                    StartingRecovery(true);
                                    LastKindOfRecovery = "";
                                }
                                this.m_ApkTestFailed = 0;
                            }
                            else
                            {
                                ApkCheckTimeVariable = ApkCheckTimeVariable / 2;
                                Common.AddToLog("[" + this.SerialNumber + "]" + " APK test failed " + this.m_ApkTestFailed + " times.");
                            }
                        }
                    }
                }
                int timeout = 0;
                while (timeout <= ApkCheckTimeVariable)
                {
                    timeout++;
                    Thread.Sleep(1000);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void RecoveryShellWatcher()
        {
            //Thread.Sleep(40000);
            Thread.Sleep(3000);
            while (true && !m_ClassDisabled)
            {
                if (this.ShellTestTime < 3 || this.ShellTestTime > 10){
                    this.ShellTestTime = 4;
                }
                if (this.Adb_Status.Equals(AdbStatus.checking)){
                    Thread.Sleep(2000);
                }
                
                if (this.IsRecoveryEnabled && !this.IsRecoveryProgress && this.CheckShell)
                {
                    if (!this.Adb_Status.Equals(AdbStatus.device))
                    {
                        this.m_ApkTestFailed = 0;
                        Common.AddToLog("[" + this.SerialNumber + "] # # # Device  NO shell. Status is " + Adb_Status);
                        if (!this.IsRecoveryProgress && !this.Adb_Status.Equals(AdbStatus.device)){
                            LastKindOfRecovery = "SHELL";
                            StartingRecovery();
                            LastKindOfRecovery = "";
                        }
                    }
                }
                Thread.Sleep(this.ShellTestTime * 1000 + rnd.Next(3000));
            }
        }

        /// <summary>
        /// Clear GNSS Logs on devices
        /// </summary>
        public void RemoveGNSSLogs()
        {
            this.Shell("rm /data/gnss/LBS_*");
        }


        /// <summary>
        /// ParseLogLine
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string ParseLogLine(string line)
        {
            string ret = "";
            try{
                int index = line.IndexOf(":", StringComparison.Ordinal);
                if (line.Length > index + 2)
                    index += 2;

                ret = line.Substring(index);
            }catch (Exception ex){
                Common.AddToLog("[UpdatePortsParseLogLineStatus][" + line + "]" + ex.Message);
            }
            return ret;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ParseException(string value)
        {
            return value.Replace("\t", Environment.NewLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ParseLogcatLine(string line)
        {
            line = line.Trim();

            string timestampFormat = @"00-00 00:00:00.000 ";    // Squeltch the timestamp
            if (line.Length > timestampFormat.Length)
                line = line.Substring(timestampFormat.Length);

            if (line.EndsWith("END", StringComparison.Ordinal))
                return string.Empty;
            else if (line.StartsWith("D", StringComparison.Ordinal))
                m_AndroidDebugLogs += (ParseLogLine(line) + "\n");
            else if (line.StartsWith("I", StringComparison.Ordinal))
                return ParseLogLine(line);
            else if (line.StartsWith("W", StringComparison.Ordinal))
                Common.AddToLog(ParseLogLine(line));
            else if (line.StartsWith("E", StringComparison.Ordinal))
            {
                if (line.Contains("Unknown argument:") || line.Contains("Not found; no service started.")){
                    this.ApkTestSupport = false;
                    Common.AddToLog("Aplication exist but not support this command:" + line);
                }
                return "ERROR" + ParseLogLine(ParseException(line));
            }
            return null;
        }

        public void CheckUptime()
        {
            Thread RebootAll = new Thread(this.CheckUptimeThread);
            RebootAll.Name = "CheckUptime_" + this.SerialNumber;
            RebootAll.Start(); 
        }

        private void CheckUptimeThread()
        {
            this.Shell("uptime");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="shell"></param>
        private void Shell(string cmd, bool shell = true,int timeout = 3000, bool PrintInput = true)
        {
            try
            {
                //Common.GetDevices();
                if (this.Adb_Status.Equals(AdbStatus.device) || this.Adb_Status.Equals(AdbStatus.recovery))
                {
                    System.Diagnostics.Process p = new Process();
                    p.StartInfo = Common.psi;
                    p.OutputDataReceived += PDataReceived;
                    p.EnableRaisingEvents = true;

                    string str;
                    if (shell)
                        str = "-s " + this.SerialNumber + " shell " + cmd;
                    else
                        str = "-s " + this.SerialNumber + "  " + cmd;

                    p.StartInfo.Arguments = str;
                    p.Start();
                    p.PriorityBoostEnabled = true;
                    p.PriorityClass = ProcessPriorityClass.High;
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    p.StandardInput.AutoFlush = true;
                    if (PrintInput)
                    {
                        AddToLog(str);
                    }
                    //p.StandardInput.WriteLine(str);
                    //p.StandardInput.WriteLine("exit");
                    if (p.WaitForExit(timeout))
                    {
                        p.WaitForExit();
                    }
                    p.Close();
                }
                else
                {
                    Console.WriteLine("Device " + this.SerialNumber + " in status " + this.Adb_Status.ToString());
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DataReceivedADB(object sender, DataReceivedEventArgs e)
        {
            // Manipulate received data here
            if (e.Data != null &&
                e.Data != "" &&
                !e.Data.Contains("exit") &&
                !e.Data.Contains("adb -s ") &&
                !e.Data.Contains("Microsoft "))
            {

                try
                {
                    Common.AddToLog(e.Data.ToString());
                }
                catch (Exception)
                {

                }
                //Console.WriteLine(this.SerialNumber + ":" + e.Data);
            }
        }
        public static void ShellADB(string cmd, int timeout = 3000)
        {

            System.Diagnostics.Process p = new Process();
            p.StartInfo = Common.psi;
            p.OutputDataReceived += DataReceivedADB;
            p.EnableRaisingEvents = true;
            p.StartInfo.Arguments = cmd;
            p.Start();
            p.PriorityBoostEnabled = true;
            p.PriorityClass = ProcessPriorityClass.High;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.StandardInput.AutoFlush = true;
            Common.AddToLog(cmd);
            //p.StandardInput.WriteLine(str);
            //p.StandardInput.WriteLine("exit");
            if (p.WaitForExit(timeout))
            {
                p.WaitForExit();
            }
            p.Close();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getRecoveryDeviceHistory()
        {
            int counter = 0;
            string rec_kind = "";
            long tim = AdbDevice.TimeNow();
            long prev = tim;
            string temp_output = "";
            if (m_RecoveryTime != null)
            {
                foreach (long tmp in m_RecoveryTime.ToArray())
                {
                    counter++;
                    try
                    {
                        rec_kind = M_RecoveryKind[tmp];
                    }
                    catch (Exception) { }
                    var date = new DateTime(tmp * 10000000);
                    string formatted = date.ToString("yyyy-MM-dd HH:mm:ss");

                    temp_output += "\r\n" + counter + "\t" + formatted + " [" + rec_kind + "]" + (tim - tmp).ToString() + "\t" + tmp.ToString() + "\t- " + (tmp - prev);
                    prev = tmp;
                }
            }
            return temp_output;
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteRecoveryDeviceHistoryToFile()
        {
            string currentContent = "";
            string file_name = this.SerialNumber + ".txt";
            if (File.Exists(file_name)){
                currentContent = File.ReadAllText(file_name);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(file_name, false)) {
                file.Write(getRecoveryDeviceHistory() + currentContent);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void DeleteGnssLogsOnAllDevices()
        {
            try{
                Common.AddToLog(" @ Starting delete all devices GNSS logs.");
                foreach (AdbDevice tmp in Common.MDevices){
                    try{
                        tmp.RemoveGNSSLogs();
                    }
                    catch (Exception) { }
                }
                Common.AddToLog(" @ Finish delete all devices GNSS logs.");
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearAllDevicesRecoveryHistory()
        {
            try
            {
                Common.AddToLog(" @ Starting delete all devices recovery history.");
                foreach (AdbDevice tmp in Common.MDevices){
                    try{
                        tmp.ClearRecoveryHistory();
                    }
                    catch (Exception) { }
                }
                Common.AddToLog(" @ Finish delete all devices recovery history.");
            }
            catch (Exception ex) { }
        }

        public void ClearRecoveryHistory()
        {
            WriteRecoveryDeviceHistoryToFile();
            RecoveryCounter = 0;
            if (m_RecoveryTime != null)
                m_RecoveryTime.Clear();
            ClearTimeOfRecovery();
        }

        private void AdbKeeper()
        {

            Thread.Sleep(rnd.Next(20000));
            while (this.AdbKeeperStatus && !m_ClassDisabled)
            {
                try
                {
                    Thread.Sleep(1000 + rnd.Next(3000));
                    if (this.IsKeeperEnabled && !this.IsRecoveryProgress)
                    {
                        this.CheckUptime();
                        this.TryToFoundWlanMacAddr();
                        this.TryToFoundBtMacAddr();
                        getBaseBandVersion();
                        getBatteryLevel();
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostBuildName()));
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostUsbAddr()));
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostRelayAddr()));
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostWlanMAC_ADDR()));
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostBtMAC_ADDR()));
                        ThreadPool.QueueUserWorkItem(new WaitCallback((s) => PostBatteryLevel()));
                        int i = 0;
                        while (i <= this.KeeperTime)
                        {
                            i = i + 5;
                            Thread.Sleep(5000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.AddToLog("[AdbKeeper][" + SerialNumber + "]" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisableClass()
        {
            m_ClassDisabled = true;

            if(rsw != null)
                rsw.Abort();

            if(raw != null)
                raw.Abort();

            if (ak != null)
                ak.Abort();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="explicitCall"></param>
        public void Dispose(bool explicitCall)
        {
            if (!this.alreadyDisposed)
            {
                if (explicitCall)
                {
                    //System.Console.WriteLine("Not in the destructor, " +"so cleaning up other objects.");
                    // Not in the destructor, so we can reference other objects.
                    //OtherObject1.Dispose();
                    //OtherObject2.Dispose();
                }
                // Perform standard cleanup here...
                //System.Console.WriteLine("Cleaning up.");
            }
            alreadyDisposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sort device in DB by Serual Number
        /// </summary>
        public static void Sort()
        {
            try
            {
                Common.MDevices.Sort(new DeviceComparer());
            }
            catch (Exception ex)
            {
                Common.AddToLog("Cannot sort Common.MDevices:" + ex.Message );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        private static void FreeProcess(Process process)
        {
            if (!process.HasExited)
                process.Kill();
            process.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool ApkAdb(string cmd)
        {
            //Common.GetDevices();
            if (this.Adb_Status.Equals(AdbStatus.device))
            {

                Process logcat = null;
                string result = null;
                string summ = "";
                DataReceivedEventHandler onNewLine = (object sender, DataReceivedEventArgs line) =>
                {
                    try
                    {
                        if (result == null && line != null && line.Data != null && line.Data.Trim().Length != 0) // Ignore pending callbacks
                        {
                            result = ParseLogcatLine(line.Data);
                            summ += "\r\n" + line.Data;
                            if (result != null && logcat != null && !logcat.HasExited)
                            {
                                logcat.CancelOutputRead();
                                logcat.Kill();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Common.AddToLog("[ApkAdb][DataReceivedEventHandler]" + e.Message);
                        Common.AddToErrors("[ApkAdb][DataReceivedEventHandler]" + e.Message);
                        //Console.Error.Write(e);
                    }
                };
                var process = Run("adb.exe", " -s " + this.SerialNumber + " " + "logcat  -v time | grep IntelRecovery", onNewLine, onNewLine);
                this.Shell(cmd);
                try
                {
                    if (process.WaitForExit(2 * 1600))
                    {
                        process.WaitForExit();
                    }
                    if (result != null)
                    {
                        Common.AddToLog("[" + this.SerialNumber + "] " + result);
                        if (result == "True" || result == "alive")
                        {
                            //LastApkAdbResult = true;
                            return true;
                        }
                        else
                        {
                            //LastApkAdbResult = false;
                        }
                    }
                    else
                    {
                        if (searching_app)
                            this.ApkTestSupport = false;
                    }
                }
                finally
                {
                    FreeProcess(process);
                }
            }
            else
            {
                Common.AddToLog("[ApkAdb][DataReceivedEventHandler]Device " + this.SerialNumber + " in status " + this.Adb_Status.ToString());
                Common.AddToErrors("[ApkAdb][DataReceivedEventHandler]Device " + this.SerialNumber + " in status " + this.Adb_Status.ToString());
                //Console.WriteLine();
            }
            return false;
        }
    }
}
