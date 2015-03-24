using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using adbdevices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Web;

namespace adevices
{
    public static class Common
    {

        public static ArrayList ApplicationErrors = new ArrayList();
        public static bool RelaysFirstLoad = true;
        public static bool DebugMode = false;
        public static string DbSync = "DataBaseSync";
        public static string LogSync = "LogSync";
        static string receiveddata = "";
        public static ArrayList Log = new ArrayList();
        static bool startreceive = false;
        public static ArrayList MDevices = new ArrayList();
        public static bool LoadDevices = true;

        public static int MAX_AdbCanRunAtSameTime = 30;
        public static int timeBetweenStartAndAdbDeviceWatcher = 5;
        public static int adbDeviceCheckTime = 400;  // mili secs
        //public static TcpListener serverSocket = new TcpListener(7004);
        //public static TcpClient clientSocket = default(TcpClient);
        public static bool AutoKillAdb = true;
        public static bool ExitApplication = false;




        public static void ExternalAPIHandler()
        {
            try
            {
                //serverSocket.Start(2);

                //Common.AddToLog("[ExternalAPIHandler] >> " + "Server Started");
                //long counter = 0;
                //while (!ExitApplication)
                //{
                //    counter += 1;
                //    clientSocket = serverSocket.AcceptTcpClient();
                //    //handleClinet client = new handleClinet();
                handleClinet.StartListening();
                //    //client.startClient(clientSocket, Convert.ToString(counter));
                //}
            }
            catch (SocketException) // or whatever the exception is that you're getting
            {
                //try
                //{
                //    serverSocket.Stop();
                //}
                //catch (Exception)
                //{
                //}
            }
            finally
            {
                //try
                //{
                //    serverSocket.Stop();
                //}
                //catch (Exception)
                //{
                //}
            }
        }

        /// <summary>
        /// Add message to error log
        /// </summary>
        /// <param name="msg">new error message</param>
        public static void AddToErrors(string msg = "")
        {
            try
            {
                StackFrame calledFunc = new StackFrame(1, true);
                string funcName = "";// calledFunc.GetMethod().Name;

                StackTrace stackTrace = new StackTrace();           // get call stack
                StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)
                var tmp = stackFrames.ToList();
                tmp.RemoveAt(0);
                stackFrames = tmp.ToArray();
                // write call stack method names
                foreach (StackFrame stackFrame in stackFrames)
                {
                    if (stackFrame.GetMethod().Name.Contains("WaitCallback_Context"))
                        break;
                    funcName += "[" + stackFrame.GetMethod().Name + "] ";
                }

                Common.AddToLog(msg, funcName);
                lock (LogSync)
                {
                    ApplicationErrors.Add(" [" + funcName + "] [" + calledFunc.GetFileLineNumber() + "] " + msg + " .\t" + " File Name:[" + calledFunc.GetFileName() + "] ");
                }
            }
            catch (Exception) { }
        }

        private static string PreviousLog = "";
        public static void AddToLog(string newLog, string calledFunction = "")
        {
            try
            {
                StackFrame calledFunc = new StackFrame(1, true);
                string funcName = "";

                if (!calledFunction.Equals(""))
                {
                    funcName = calledFunction;
                }
                else
                {
                    funcName = calledFunc.GetMethod().Name;
                }
                if (PreviousLog != newLog)
                {
                    lock (LogSync)
                    {
                        Log.Add("[" + funcName + "]" + newLog);
                        PreviousLog = newLog;
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Clear logs buffer
        /// </summary>
        public static void ClearErrors()
        {
            try
            {
                lock (LogSync)
                {
                    ApplicationErrors.Clear();
                }
            }
            catch (Exception) { }
        }

        public static void LoadRelays()
        {
            if (!Relay.LoadingRelays)
            {
                Relay.LoadingRelays = true;
                Thread lrt = new Thread(new ThreadStart(Relay.LoadRelaysThread));
                lrt.Name = "LoadRelaysThread";
                lrt.Start();
                //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => Relay.LoadRelaysThread()));
            }
        }
        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        private static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
        public static string getBurnImagePathFromNetwork(string path)
        {
            var dirName = new DirectoryInfo(path).Name + "__________" + Guid.NewGuid().ToString();

           // try
           // {

                bool isExists = System.IO.Directory.Exists(dirName);
                if (isExists)
                {
                    Common.AddToLog("                         ");
                    Common.AddToLog("                         Deletying directory :" + dirName);
                    DeleteDirectory(dirName);
                    //System.IO.Directory.Delete(dirName,true);

                }
                Common.AddToLog("                         ");
                Common.AddToLog("                         Creating directory :" + dirName);
                System.IO.Directory.CreateDirectory(dirName);
                Common.AddToLog("                         Copy files into  :" + dirName);
                CopyFilesRecursively(new DirectoryInfo(path), new DirectoryInfo(dirName));
                Common.AddToLog("                         Finish Copy into  :" + dirName + " ");
                Common.AddToLog("                         ");
                Common.AddToLog("                         ");
           // }
           // catch (Exception ex)
           // {
           //     throw ex;
           // }
            return dirName;
        }

        public static System.Diagnostics.ProcessStartInfo psi_kill = new System.Diagnostics.ProcessStartInfo()
        {
            FileName = @"taskkill",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        public static System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo()
        {
            FileName = @"adb",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        public static System.Diagnostics.ProcessStartInfo psi_cmd = new System.Diagnostics.ProcessStartInfo()
        {
            FileName = @"cmd",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        //public static System.Diagnostics.ProcessStartInfo psiAdb = new System.Diagnostics.ProcessStartInfo()
        //{
        //    FileName = @"C:\adt-bundle-windows-x86_64-20131030\sdk\platform-tools\adb.exe",
        //    RedirectStandardOutput = true,
        //    RedirectStandardError = true,
        //    RedirectStandardInput = true,
        //    UseShellExecute = false,
        //    CreateNoWindow = true
        //};

        /// <summary>
        /// Check if ADB device exist in DB
        /// </summary>
        /// <param name="serialNumber">ADB device Serial Number</param>
        /// <returns>True if exist</returns>
        public static bool ifDeviceExistInDb(string serialNumber)
        {
            try
            {
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.SerialNumber == serialNumber)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Common.AddToErrors(ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "ifDeviceExistInDb ERROR", 4000);
            }
            return false;
        }

        public static void PostUrlData(string identity, string act, string value)
        {

       
            string url = @"http://testbook.intel.com/framework/API_UpdateDevice.php?identity=" + identity + "&act=" + act + "&value=" + System.Uri.EscapeDataString(value);
            //AddToLog("Sending:" + url);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader resStream = new StreamReader(response.GetResponseStream());
                string ret = resStream.ReadToEnd();
                AddToLog(ret);
            }
            catch (Exception) { }
            
            //Stream resStream = response.GetResponseStream();


        }

        /// <summary>
        /// Disable all devices after check if they status still "checking"
        /// </summary>
        public static void DisableDevices()
        {
            try
            {
                int countOffline = 0;
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.Adb_Status != AdbStatus.device)
                    {
                        countOffline++;
                    }
                }

                if (countOffline == Common.MDevices.Count && Common.MDevices.Count > 1 && AdbDevice.RecoveryInProgressCount() < Common.MDevices.Count)
                {
                    if (Common.AutoKillAdb)
                    {
                        Common.AddToLog("              !!!!!!!!!!!!!!!!!!!!!!! ALL DEVICES IS OFFLINE. Killing ADB!!!!!!!!!!!!!!!!!!!!!!!                    ");
                        Common.KillAdbPrecess();
                        Thread.Sleep(3000);
                    }
                    Thread.Sleep(3000);
                    foreach (AdbDevice tmp in Common.MDevices)
                    {
                        if (tmp.Adb_Status == AdbStatus.checking)
                        {
                            Common.AddToLog("Device not found :" + tmp.SerialNumber);
                            tmp.Adb_Status = AdbStatus.notfound;
                        }
                    }
                }
                else
                {

                    foreach (AdbDevice tmp in Common.MDevices)
                    {
                        if (tmp.Adb_Status == AdbStatus.checking)
                        {
                            Common.AddToLog("Device not found :" + tmp.SerialNumber);
                            tmp.Adb_Status = AdbStatus.notfound;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddToErrors(ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "DisableDevices ERROR", 4000);
            }
        }

        /// <summary>
        /// Set all devices to cheking state
        /// </summary>
        public static void SetCheckingDevices()
        {
            try
            {
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.Adb_Status == AdbStatus.device || tmp.Adb_Status == AdbStatus.offline || tmp.Adb_Status == AdbStatus.unauthorized)
                    {
                        tmp.Adb_Status = AdbStatus.checking;
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "SetCheckingDevices ERROR", 4000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AdbDeviceList()
        {
            Thread.Sleep(1000 * Common.timeBetweenStartAndAdbDeviceWatcher);
            while (true)
            {
                try
                {
                    if (AdbDevice.isRecoveryInProgress())
                    {
                        Thread.Sleep(Common.adbDeviceCheckTime);
                    }
                    else
                    {
                        Thread.Sleep(2500);
                    }
                    if (Common.LoadDevices)
                    {
                        Common.GetDevices();
                    }
                }
                catch (Exception ex)
                {
                    Common.AddToErrors("[AdbDeviceList]" + ex.Message);
                }
            }
        }
        public static void KillFlashToolProcess()
        {
            try
            {
                Process[] adbProcesses = Process.GetProcessesByName("DownloadTool");
                foreach (Process name in adbProcesses)
                {
                    try
                    {
                        AddToLog("Killing :" + name.Id + " name:" + name.ProcessName);
                        name.Kill();

                    }
                    catch (Exception exr2)
                    {
                        AddToLog("[KillFlashToolProcess]:" + exr2.Message);
                    }
                }
                Process[] adbProcesses2 = Process.GetProcessesByName("DownloadTool.exe");
                foreach (Process name in adbProcesses2)
                {
                    try
                    {
                        name.Kill();
                    }
                    catch (Exception exr2)
                    {
                        AddToLog("[KillFlashToolProcess]:" + exr2.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.AddToErrors("[KillFlashToolProcess]" + ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "KillFlashToolProcess ERROR", 4000);
            }
        }

        public static void KillAdbPrecess()
        {
            try
            {
                AdbDevice.ShellADB("kill-server", 5000);
                Process[] adbProcesses = Process.GetProcessesByName("adb");
                foreach (Process name in adbProcesses)
                {
                    try
                    {
                        AddToLog("Killing :" + name.Id + " name:" + name.ProcessName);
                        name.Kill();

                    }
                    catch (Exception exr2)
                    {
                        AddToLog("[KillAdbPrecess]:" + exr2.Message);
                    }
                }
                Process[] adbProcesses2 = Process.GetProcessesByName("adb.exe");
                foreach (Process name in adbProcesses2)
                {
                    try
                    {
                        name.Kill();
                    }
                    catch (Exception exr2)
                    {
                        AddToLog("[KillAdbPrecess]:" + exr2.Message);
                    }
                }
                AdbDevice.ShellADB("kill-server", 5000);
                /*
                System.Diagnostics.Process p = new Process();
                p.StartInfo = Common.psi_kill;
                p.OutputDataReceived += p_DataReceived;
                p.EnableRaisingEvents = true;
                p.StartInfo.Arguments = "/F /IM adb";
                Common.AddToLog("Killing ADB");
                p.Start();
                //p.BeginOutputReadLine();
                //p.BeginErrorReadLine();
                p.WaitForExit();
                if (p.ExitCode != 0)
                {
                    AddToLog("[KillAdbPrecess]: Exit code is : " + p.ExitCode);
                }
                p.Close();
                 * */
            }
            catch (Exception ex)
            {
                Common.AddToErrors("[KillAdbPrecess]" + ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "KillAdbPrecess ERROR", 4000);
            }
        }


        public static void WriteLogToFile(string input)
        {
            //string [] tmp = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string currentContent = "";
            if (File.Exists(@"log.txt"))
            {
                currentContent = File.ReadAllText(@"log.txt");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"log.txt", false))
            {
                file.Write(input + currentContent);
            }
        }

        public static void AddNewAdbDevice(string serialNumber, string adbStatus)
        {
            AdbDevice newDev = new AdbDevice(serialNumber, adbStatus);
            newDev.getBaseBandVersion();
            Common.MDevices.Add(newDev);
        }

        public static string AdbParserStatus = "off";
        /// <summary>
        /// Prase device list from adb devices
        /// </summary>
        /// <param name="input">adb devices output</param>
        private static void ParseDeviceList(string input)
        {
            try
            {
                //Common.AddToLog("Parsing device list");
                string[] tmpLines = input.Split(new char[] { '\n' });
                AdbParserStatus = "Set to checking";
                // Put all device status to cheking state
                Common.SetCheckingDevices();
                AdbParserStatus = "Parsing input";
                //lock (Common.MDevices.SyncRoot)
                //{
                foreach (string tmpLine in tmpLines)
                {
                    string[] tmp = tmpLine.Split(new char[] { '\t' }); ;
                    if (tmp.Length == 2)
                    {
                        string tmpSerialNumber = tmp[0];
                        if (!ifDeviceExistInDb(tmpSerialNumber))
                        {
                            Common.AddNewAdbDevice(tmpSerialNumber, tmp[1]);
                            Thread.Sleep(10);

                        }
                        else
                        {
                            AdbDevice tmpDevise = GetDeviceBySerialBumber(tmpSerialNumber);
                            tmpDevise.SetAdbStatusFromString(tmp[1]);
                        }
                    }
                }
                //}
                AdbParserStatus = "Disabling not founded devices";
                Common.DisableDevices();
                AdbParserStatus = "Finish " + Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                Common.AddToErrors("[ParseDeviceList]" + ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "ParseDeviceList ERROR", 4000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sreialNumber"></param>
        /// <returns></returns>
        public static AdbDevice GetDeviceBySerialBumber(string sreialNumber)
        {
            try
            {
                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.SerialNumber == sreialNumber)
                    {
                        return tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddToErrors("[getDeviceBySerialBumber]" + ex.Message);
                AutoClosingMessageBox.Show(ex.Message, "getDeviceBySerialBumber ERROR", 4000);
            }
            return null;
        }

        public static void KillGetDevicesProcess()
        {

            /*
             * try
            {

                if (p!= null && !p.HasExited)
                {
                    p.CancelOutputRead();
                    p.CancelErrorRead();
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[KillGetDevicesProcess 1]" + ex.Message);
            }

            try
            {
                p.Close();
            }
            catch (Exception ex)
            {
                Common.AddToLog("[KillGetDevicesProcess 2]" + ex.Message);
            }
             * */
        }
        private static bool m_GetDevicesStatus = false;
        private static System.Diagnostics.Process p;

        public static bool GetDevicesIssue = false;
        public static bool GetDevicesStuck = false;

        public static int CannotStartGetDevicesCounter = 0;
        /// <summary>
        /// 
        /// </summary>
        public static void GetDevices()
        {

            if (m_GetDevicesStatus == false)
            {
                m_GetDevicesStatus  = true;
                GetDevicesStuck     = false;
                try
                {
                    receiveddata = "";

                    if (p == null)
                        p = new Process();
                    System.Diagnostics.ProcessStartInfo psi_self = new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = @"adb",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = "devices"
                    };
                    
                    p.StartInfo = psi_self;
                    p.OutputDataReceived -= p_DataReceived;
                    p.OutputDataReceived += p_DataReceived;
                    p.ErrorDataReceived -= p_ErrorReceived;
                    p.ErrorDataReceived += p_ErrorReceived;

                    p.EnableRaisingEvents = true;
                }
                catch (Exception ex)
                {
                    Common.AddToLog("[!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! GetDevices] ERROR Before start:" + ex.Message);
                }
                try
                {
                    p.Start();
                    p.PriorityClass = ProcessPriorityClass.BelowNormal;
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    p.StandardInput.AutoFlush = true;
                    //p.StandardInput.WriteLine("adb devices");// && exit
                    //p.StandardInput.Flush();

                    //p.StandardInput.WriteLine("exit");// && 
                    //p.StandardInput.Flush();
                    //p.StandardInput.Close();
                    //Common.AddToLog("[GetDevices]: Starting get devices");
                    //p.WaitForExit();
                    //Common.AddToLog("[GetDevices] Waiting" );
                }
                catch (Exception ex)
                {
                    Common.AddToLog("[!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! GetDevices] ERROR Before Wait:" + ex.Message);
                }
                try
                {
                    if (p.WaitForExit(5000))
                    {
                        p.WaitForExit();
                    }

                    p.CancelOutputRead();
                    p.CancelErrorRead();
                    if (!p.HasExited)
                    {
                        Common.AddToLog("[!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! GetDevices] Killing");

                        p.Kill();
                    }

                    //Common.AddToLog("[GetDevices]: Finish get devices");
                    //Common.AddToLog("[GetDevices] Closing ");


                    p.Close();
                }
                catch (Exception ex)
                {
                    Common.AddToLog("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! [GetDevices] ERROR 1:" + ex.Message);
                }
                try
                {
                    //Thread.Sleep(100);
                    if (p_DataReceived_STR.Length > 5)
                    {
                        if (AdbDevice.isRecoveryInProgress())
                        {
                            string forAddStr = p_DataReceived_STR.Replace("List of devices attached", "");
                            p_DataReceived_STR = "";
                            string[] devicesInNotDeviceMode = forAddStr.Split(new string[] { "\r\n","\n" }, StringSplitOptions.None);
                            foreach (string tmp in devicesInNotDeviceMode)
                            {
                                if (!tmp.Contains("device"))
                                {
                                    Common.AddToLog("[DEVICES]" + forAddStr);
                                }

                            }
                            //Common.AddToLog("[DEVICES]" + forAddStr);
                        }
                        GetDevicesIssue = false;
                        ParseDeviceList(receiveddata);
                        Common.CannotStartGetDevicesCounter = 0;
                    }
                    else
                    {
                        GetDevicesIssue = true;
                    }
                    p_DataReceived_STR = "";
                }
                catch (Exception ex)
                {
                    Common.AddToLog("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! [GetDevices] ERROR " + ex.Message);
                }
                //textBox1.Text = receiveddata.Replace("\n", "\r\n");
                //AddToLog(receiveddata.Replace("\n", "\r\n"));

                m_GetDevicesStatus = false;
            }
            else
            {
                Common.CannotStartGetDevicesCounter++;
                GetDevicesStuck = true;
                Common.AddToLog("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! [GetDevices]: Cannot get devices m_GetDevicesStatus=" + m_GetDevicesStatus.ToString());
            }
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => ParseDeviceList(receiveddata)));
        }

        private static string p_DataReceived_STR = "";


        private static void p_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string tmpStr;
            if (e.Data != null && !e.Data.Contains("adb devices")
                && !e.Data.Contains("exit") && !e.Data.Contains("All rights ")
                && !e.Data.Contains("Version 6"))
            {
                tmpStr = e.Data.ToString();
                //Console.WriteLine(tmpStr);

                if (startreceive && tmpStr != "")
                {
                    receiveddata += tmpStr + "\n";
                }
                if (tmpStr.Contains("List of devices attached"))
                {
                    startreceive = true;
                }
                else if (tmpStr == "")
                {
                    startreceive = false;
                }
                if (tmpStr.Contains("ADB server didn't ACK"))
                {
                    Common.AddToErrors("!!!!!!!!!!!!!!!!!!!" + tmpStr);
                    //receiveddata +=  + "\n";
                }
                p_DataReceived_STR += tmpStr;
            }
        }

        private static string p_ErrorReceived_STR = "";
        private static void p_ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            string tmpStr;
            if (e.Data != null && !e.Data.Contains("adb devices")
                && !e.Data.Contains("exit") && !e.Data.Contains("All rights ")
                && !e.Data.Contains("Version 6"))
            {
                tmpStr = e.Data.ToString();
                //Console.WriteLine(tmpStr);
                Common.AddToErrors(tmpStr);
                p_ErrorReceived_STR += tmpStr;
            }
        }
    }
}
