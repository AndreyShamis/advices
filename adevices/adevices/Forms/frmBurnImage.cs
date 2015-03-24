using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adbdevices;
using System.Threading;

namespace adevices
{
    public partial class frmBurnImage : Form
    {
        public frmBurnImage()
        {
            InitializeComponent();
        }

        private void StartBurning()
        {

            string deviceID = "";
            if (cmdDevices.InvokeRequired)
            {
                cmdDevices.Invoke(new MethodInvoker(delegate { deviceID = cmdDevices.Text; }));
            }

            string pathLocal ="";

            try
            {
                pathLocal = Common.getBurnImagePathFromNetwork(txtPathToImage.Text);
            }
            catch (Exception ex)
            {
                Common.AddToErrors("Cannot copy file file for burn image " + ex.Message);
                return;
            }

            if (!checkBox1.Checked)
            {

                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.RelayID.Length > 0 && tmp.RelayPowerPort > 0 && tmp.RelayUsbPort > 0
                         && tmp.FlashToolAddress != null && tmp.FlashToolAddress.Length > 1 && tmp.FlashToolHubAddress != null && tmp.FlashToolHubAddress.Length > 1
                        && (tmp.SerialNumber.Contains("00440") || tmp.SerialNumber.Contains("INV14") || tmp.SerialNumber.Contains("123456"))
                        )
                    {

                        //CheckBox ttt = this.Controls.Find("dev" + tmp.SerialNumber,true) as CheckBox;
                        try
                        {
                            tmp.PowerOff();
                            tmp.UsbOff();

                        }
                        catch (Exception ex) { }
                    }

                }

                foreach (AdbDevice tmp in Common.MDevices)
                {
                    if (tmp.RelayID.Length > 0 && tmp.RelayPowerPort > 0 && tmp.RelayUsbPort > 0
                         && tmp.FlashToolAddress != null && tmp.FlashToolAddress.Length > 1 && tmp.FlashToolHubAddress != null && tmp.FlashToolHubAddress.Length > 1
                        && (tmp.SerialNumber.Contains("00440") || tmp.SerialNumber.Contains("INV14") || tmp.SerialNumber.Contains("123456"))
                        )
                    {

                        //CheckBox ttt = this.Controls.Find("dev" + tmp.SerialNumber,true) as CheckBox;
                        try
                        {
                            txtLog.BeginInvoke(new MethodInvoker(() => txtLog.Text = "Starting write image to " + tmp.SerialNumber + "\r\n" + txtLog.Text));
                        }
                        catch (Exception ex) { }
                        Thread t = new Thread(new ParameterizedThreadStart(tmp.BurnImage));
                        t.Name = "tmp.BurnImage";
                        t.Start(pathLocal);
                        //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => tmp.BurnImage(pathLocal)));
                        Common.AddToLog("Write image to " + tmp.SerialNumber + " started");
                        Thread.Sleep(1123);
                    }
                    else
                    {
                        try
                        {
                            txtLog.BeginInvoke(new MethodInvoker(() => txtLog.Text = "Skeeping device: " + tmp.SerialNumber + "\r\n" + txtLog.Text));
                        }
                        catch (Exception ex) { }
                    }
                }
                tmrExit.Enabled = true;
            }
            else
            {

                AdbDevice tmp = Common.GetDeviceBySerialBumber(deviceID);

                if (tmp.FlashToolAddress == null)
                {
                    MessageBox.Show("FlashToolAddress addr is NULL!!!");
                    return;
                }
                if (tmp != null && tmp.RelayID.Length > 0 && tmp.RelayPowerPort > 0 && tmp.RelayUsbPort > 0
                       && tmp.FlashToolAddress != null && tmp.FlashToolAddress.Length > 1 && tmp.FlashToolHubAddress.Length > 1)
                {
                    try
                    {
                    txtLog.BeginInvoke(new MethodInvoker(() => txtLog.Text = "Starting write image to " + tmp.SerialNumber + "\r\n" + txtLog.Text));

                                            }
                        catch (Exception ex) { }
                    Thread t = new Thread(new ParameterizedThreadStart(tmp.BurnImage));
                    t.Name = "tmp.BurnImage";
                    t.Start(pathLocal);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback((s) => tmp.BurnImage(pathLocal)));
                    Common.AddToLog("Write image to " + tmp.SerialNumber + " started");
                    Thread.Sleep(10);
                    tmrExit.Enabled = true;
                }
                else
                {
                                            try
                        {
                    txtLog.BeginInvoke(new MethodInvoker(() => txtLog.Text = "Skeeping device: " + tmp.SerialNumber + "\r\n" + txtLog.Text));
                        }
                                            catch (Exception ex) { }
                }
            }
        }
        private void cmdBurn_Click(object sender, EventArgs e)
        {
            if (chkKillFlashhTollProc.Checked)
            {
                Common.KillFlashToolProcess();
            }
            Thread lrt = new Thread(new ThreadStart(StartBurning));
            lrt.Name = "StartBurning";
            lrt.Start();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cmdDevices.Visible = true;
                chkKillFlashhTollProc.Checked = false;
            }
            else
            {
                cmdDevices.Visible = false;

            }
        }

        private void frmBurnImage_Load(object sender, EventArgs e)
        {
            int i = 0;
            CheckBox box;
            foreach (AdbDevice tmp in Common.MDevices)
            {
                cmdDevices.Items.Add(tmp.SerialNumber);
                box = new CheckBox();
                box.Tag = i.ToString();
                box.Text = tmp.SerialNumber;
                box.AutoSize = true;
                box.Name = "dev_" + tmp.SerialNumber;
                if (tmp.RelayID.Length > 0 && tmp.RelayPowerPort > 0 && tmp.RelayUsbPort > 0
                     && tmp.FlashToolAddress != null && tmp.FlashToolAddress.Length > 1 && tmp.FlashToolHubAddress != null && tmp.FlashToolHubAddress.Length > 1
                    && (tmp.SerialNumber.Contains("00440") || tmp.SerialNumber.Contains("INV14") || tmp.SerialNumber.Contains("123456"))
                    )
                {
                    box.Checked = true;
                }
                box.Location = new Point(10, 90 + (i * 20)); //vertical
                //box.Location = new Point(i * 50, 10); //horizontal
                this.Controls.Add(box);

                i++;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
