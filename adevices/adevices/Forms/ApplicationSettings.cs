using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace adevices
{
    public partial class ApplicationSettings : Form
    {
        public ApplicationSettings()
        {
            InitializeComponent();
        }

        private void ApplicationSettings_Load(object sender, EventArgs e)
        {
            numMaxAdbProcesses.Value = Common.MAX_AdbCanRunAtSameTime;
            timeBetweenStartAndAdbDeviceWatcher.Value = Common.timeBetweenStartAndAdbDeviceWatcher;
            numAdbDeviceCheckTime.Value = Common.adbDeviceCheckTime;
        }

        private void timeBetweenStartAndAdbDeviceWatcher_ValueChanged(object sender, EventArgs e)
        {
            Common.timeBetweenStartAndAdbDeviceWatcher = (int)timeBetweenStartAndAdbDeviceWatcher.Value;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numAdbDeviceCheckTime_ValueChanged(object sender, EventArgs e)
        {
            Common.adbDeviceCheckTime = (int)numAdbDeviceCheckTime.Value;
        }
    }
}
