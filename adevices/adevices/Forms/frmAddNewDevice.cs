using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adbdevices;

namespace adevices
{
    public partial class frmAddNewDevice : Form
    {
        public frmAddNewDevice()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtIdentity.Text.Length > 0)
            {
                Common.AddNewAdbDevice(txtIdentity.Text, AdbStatus.unknown.ToString());
                this.Close();
            }
        }
    }
}
