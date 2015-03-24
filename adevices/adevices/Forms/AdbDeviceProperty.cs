using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using adevices;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;

namespace adbdevices
{
    public partial class AdbDeviceProperty : Form
    {
        private string m_serialNumber;
        private AdbDevice m_device = null;
        public AdbDeviceProperty(string mySerialNumber)
        {
 
            this.m_serialNumber = mySerialNumber;
            InitializeComponent();

            this.Text = this.m_serialNumber;

            m_device = Common.GetDeviceBySerialBumber(mySerialNumber);
            
        }

        private void AdbDeviceProperty_Load(object sender, EventArgs e)
        {
            UpdateGui();
            m_device.CheckUptime();
        }



        private void UpdateGui()
        {

            try
            {
                txtApkTestTime.Text = m_device.ApkTestTime.ToString();
                txtSerialNumber.Text = m_device.SerialNumber;
                txtObjectProperty.Text = "";
                txtRelayUsbPortId.Text = m_device.RelayUsbPort.ToString();
                txtRelayPowerPortId.Text = m_device.RelayPowerPort.ToString();
                txtAdbKeeperTime.Text = m_device.KeeperTime.ToString();
                chkRecoveryEnabled.Checked = m_device.IsRecoveryEnabled;
                chkRecoveryEnabled.Text = m_device.IsRecoveryEnabled ? "Enabled" : "Disabled";
                chkKeeperEnabled.Checked = m_device.IsKeeperEnabled;
                chkKeeperEnabled.Text = m_device.IsKeeperEnabled ? "Enabled" : "Disabled";
                //
                chkUsbNormalyClosed.Checked = m_device.UsbNormalyClosed;
                chkPowerNormalyClosed.Checked = m_device.PowerNormalyClosed;
                chkChekApk.Text = m_device.CheckAPK ? "Enabled" : "Disabled";
                chkChekApk.Checked = m_device.CheckAPK;
                chkChekShell.Text = m_device.CheckShell ? "Enabled" : "Disabled";
                chkChekShell.Checked = m_device.CheckShell;
                txtObjectProperty.Text = m_device.ToString();
                txtObjectProperty.Text = txtObjectProperty.Text + "\r\n";
                txtObjectProperty.Text = txtObjectProperty.Text + "Base band:\t" + m_device.BaseBandVaerion + "\r\n";
                txtObjectProperty.Text = txtObjectProperty.Text + "Battery Level:\t" + m_device.BatteryLevel + "\r\n";
                lblRecoveredIn20Min.Text = m_device.getNumberOfRecoveryIn20Min().ToString();
                lblRecoveredIn60Min.Text = m_device.getNumberOfRecoveryInXMin(60).ToString();
                lblRecoveredIn10Min.Text = m_device.getNumberOfRecoveryInXMin(10).ToString();
                lblRecoveredIn2Days.Text = m_device.getNumberOfRecoveryInXMin(60*24*2).ToString();
                txtObjectProperty.Text = txtObjectProperty.Text + m_device.getRecoveryDeviceHistory();

                txtFlashToolAddress.Text = m_device.FlashToolAddress;
                txtFlashToolHubAddress.Text = m_device.FlashToolHubAddress;
            }
            catch (Exception ex)
            {
                Common.AddToLog("[UpdateGui][" + m_device.SerialNumber + "]" + ex.Message);
            }

            //foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(m_device))
            //{
            //    string name = descriptor.Name;
            //    object value = descriptor.GetValue(m_device);
            //    txtObjectProperty.Text = txtObjectProperty.Text + "\r\n" + string.Format("{0}={1}", name, value);
            //}
            
            if (this.m_device.IfHaveRelay())
            {
                if (m_device.IsPowerOn())
                {
                    btnPowerOn.Enabled = false;
                    btnPowerOff.Enabled = true;
                }
                else
                {
                    btnPowerOn.Enabled = true;
                    btnPowerOff.Enabled = false;
                }
            }
            cmbRelayId.Items.Clear();
            cmbRelayId.Items.Add("");
            foreach(Relay tmp in Relay.Relays)
            {
                cmbRelayId.Items.Add(tmp.SerialId);
            }

            int index = cmbRelayId.FindString(m_device.RelayID);
            cmbRelayId.SelectedIndex = index;
        }
        private void btnSetKeeperTime_Click(object sender, EventArgs e)
        {
            try
            {
                m_device.KeeperTime = Int32.Parse(txtAdbKeeperTime.Text);
            }
            catch (Exception)
            {
            }
            UpdateGui();
        }

        private void chkRecoveryEnabled_CheckedChanged(object sender, EventArgs e)
        {
  
        }

        private void chkRecoveryEnabled_Click(object sender, EventArgs e)
        {
            m_device.IsRecoveryEnabled = chkRecoveryEnabled.Checked;
            UpdateGui();
        }

        private void chkKeeperEnabled_Click(object sender, EventArgs e)
        {
            m_device.IsKeeperEnabled = chkKeeperEnabled.Checked;
            UpdateGui();
        }

        private void cmbRelayId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRelayId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbRelayId.Text != "")
            {
                m_device.RelayID = cmbRelayId.Text;
            }
            else
            {
                m_device.RelayID = "";
                m_device.RelayPowerPort = 0;
                m_device.RelayUsbPort = 0;
            }
            //UpdateGui();
        }

        private void cmdSetPowerPort_Click(object sender, EventArgs e)
        {
            try
            {
                m_device.RelayPowerPort = (short)Int32.Parse(txtRelayPowerPortId.Text);
            }
            catch (Exception)
            {
            }
            UpdateGui();
        }

        private void cmdSetUsbPort_Click(object sender, EventArgs e)
        {
            try
            {
                m_device.RelayUsbPort = (short)Int32.Parse(txtRelayUsbPortId.Text);
            }
            catch (Exception)
            {
            }
            UpdateGui();
        }

        private void btnPowerOn_Click(object sender, EventArgs e)
        {
            if (this.m_device.PowerOn())
            {
                btnPowerOn.Enabled = false;
                btnPowerOff.Enabled = true;
            }

        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            if (this.m_device.PowerOn())
            {
                btnPowerOn.Enabled = true;
                btnPowerOff.Enabled = false;
            }

        }

        private void cmdRemoveDevice_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You sure to remove device from db?.\r\nConfirm?", "Removing device from DB", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AdbDevice tmp = Common.GetDeviceBySerialBumber(this.m_serialNumber);
                
                Common.MDevices.Remove(tmp);
                tmp.DisableClass();
                tmp.Dispose();
                tmp = null;
                this.Close();
                this.Dispose();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSetSerialNumber_Click(object sender, EventArgs e)
        {
            m_device.SerialNumber = txtSerialNumber.Text;
            m_serialNumber = m_device.SerialNumber;
            this.Text = m_device.SerialNumber;
        }

        private void tmrPeriodic_Tick(object sender, EventArgs e)
        {
            if (m_device.RelayID.Length < 1)
            {
                txtRelayPowerPortId.Enabled = false;
                cmdSetPowerPort.Enabled = false;
            }
            else
            {
                txtRelayPowerPortId.Enabled = true;
                
            }

            if (txtRelayPowerPortId.Text == "0" || txtRelayPowerPortId.Text == "")
            {
                cmdSetPowerPort.Enabled = false;
                btnPowerOff.Enabled = false;
                btnPowerOn.Enabled = false;
            }
            else
            {
                cmdSetPowerPort.Enabled = true;
                btnPowerOff.Enabled = true;
                btnPowerOn.Enabled = true;
            }


            if (txtRelayUsbPortId.Text == "0" || txtRelayUsbPortId.Text == "")
            {
                cmdSetUsbPort.Enabled = false;
            }
            else
            {
                cmdSetUsbPort.Enabled = true;
            }

            if (txtRelayPowerPortId.Enabled == false)
            {
                txtRelayUsbPortId.Enabled = false;
            }
            else
            {
                txtRelayUsbPortId.Enabled = true;
            }


        }

        private void cmdApkTestTime_Click(object sender, EventArgs e)
        {
            try
            {
                m_device.ApkTestTime = Int32.Parse(txtApkTestTime.Text);
            }
            catch (Exception)
            {
                m_device.ApkTestTime = 10;
            }
            UpdateGui();
        }


        private void btnResetRecovery_Click(object sender, EventArgs e)
        {
            m_device.ClearRecoveryHistory();
            UpdateGui();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void chkChekShell_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkChekShell_Click(object sender, EventArgs e)
        {
            m_device.CheckShell = chkChekShell.Checked;
            UpdateGui();
        }

        private void chkChekApk_Click(object sender, EventArgs e)
        {
            m_device.CheckAPK = chkChekApk.Checked;
            UpdateGui();
        }

        private void chkChekApk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thread cap = new Thread(new ParameterizedThreadStart(m_device.InstallApk));
            //cap.Start("");
            //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => m_device.InstallApk("")));
    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void tmrPeriodicLong_Tick(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_device.CheckIfApkInstalled();
        }

        private void chkKeeperEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkUsbNormalyClosed_Click(object sender, EventArgs e)
        {
            m_device.UsbNormalyClosed = chkUsbNormalyClosed.Checked;
            UpdateGui();
        }

        private void chkPowerNormalyClosed_Click(object sender, EventArgs e)
        {
            m_device.PowerNormalyClosed = chkPowerNormalyClosed.Checked;
            UpdateGui();
        }

        private void btnReboot_Click_1(object sender, EventArgs e)
        {
            m_device.RemoteRebootDevice();
        }

        private void btnCLearGnss_Click(object sender, EventArgs e)
        {
            m_device.RemoveGNSSLogs();
        }

        private void cmdSetFlashToolAddress_Click(object sender, EventArgs e)
        {
            m_device.FlashToolAddress = txtFlashToolAddress.Text;
        }

        private void cmdSettxtFlashToolHubAddress_Click(object sender, EventArgs e)
        {
            m_device.FlashToolHubAddress = txtFlashToolHubAddress.Text;
        }
        private void PDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Manipulate received data here
            if (e.Data != null &&
                e.Data != "" &&
                !e.Data.Contains("exit") &&
                !e.Data.Contains("Microsoft "))
            {
                try
                {
                    txtBurnLog.Invoke(new MethodInvoker(delegate { txtBurnLog.Text = e.Data + "\r\n" + txtBurnLog.Text; }));
   
                }
                catch (Exception)
                {

                }
                //Console.WriteLine(this.SerialNumber + ":" + e.Data);
            }
        }
        private void cmdBurnImage_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => BurnImage(@"\\10.12.225.240\Share\XMM6321\Build_For_BAT\DEV_XMM6321_XGES2_AGES2_FSY_01.1430.03")));
        }

        private void BurnImage(string path)
        {
            System.Diagnostics.Process p = new Process();
            p.StartInfo = Common.psi;
            p.OutputDataReceived += PDataReceived;
            p.EnableRaisingEvents = true;

            p.Start();
            p.PriorityBoostEnabled = true;
            p.PriorityClass = ProcessPriorityClass.High;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.StandardInput.AutoFlush = true;
            m_device.UsbOff();
            m_device.PowerOff();
            Thread.Sleep(2000);
            var dirName = new DirectoryInfo(path).Name;

            bool isExists = System.IO.Directory.Exists(dirName);

            if (!isExists)
            {
                Common.AddToLog("Creating directory :" + dirName);
                System.IO.Directory.CreateDirectory(dirName);
                Common.AddToLog("Copy files into  :" + dirName);
                Common.CopyFilesRecursively(new DirectoryInfo(path), new DirectoryInfo(dirName));
                Common.AddToLog("Finish Copy into  :" + dirName + " ");
            }



            string[] filePaths = Directory.GetFiles(dirName, "*.fls");

            string files = "";
            foreach (var tmp in filePaths)
            {
                files = files + " " + tmp;
            }
            txtBurnLog.Invoke(new MethodInvoker(delegate { txtBurnLog.Text = "Start burn image :" + files; }));
            string str = "DownloadTool.exe --library DownloadTool.dll -c \"" + m_device.FlashToolHubAddress + @"\" + m_device.FlashToolAddress + "\"" + @"  " + files +  " && exit";


            p.StandardInput.WriteLine(str);
            Thread.Sleep(4000);
            m_device.UsbOn();
            Thread.Sleep(1000);
            m_device.PowerOn();

            //p.StandardInput.WriteLine("exit");
            if (p.WaitForExit(5 * 60 * 1000))
            {
                p.WaitForExit();
            }
            p.Close();
            txtBurnLog.Invoke(new MethodInvoker(delegate { txtBurnLog.Text = "Finish!!!" + "\r\n" + txtBurnLog.Text; }));
        }

        private void cmdGetBaseBand_Click(object sender, EventArgs e)
        {
            m_device.getBaseBandVersion();
        }

        private void cmdInstallApk_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => m_device.InstallApk("")));
        }

        private void cndEnableAllCheckBpx_Click(object sender, EventArgs e)
        {
            chkKeeperEnabled.Checked = true;
            chkRecoveryEnabled.Checked = true;
            chkChekShell.Checked = true;
            chkChekApk.Checked = true;
        }

        private void cndDisableAllCheckBpx_Click(object sender, EventArgs e)
        {
            chkKeeperEnabled.Checked = false;
            chkRecoveryEnabled.Checked = false;
            chkChekShell.Checked = false;
            chkChekApk.Checked = false;
        }

        private void getBattery_Click(object sender, EventArgs e)
        {
            m_device.getBatteryLevel();
        }
    }
}
