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
    public partial class RelayProperty : Form
    {
        private string m_serial;
        private Relay m_Relay = null;
        public RelayProperty(string serial)
        {
            InitializeComponent();
            this.m_serial = serial;
            this.Text = this.m_serial;
            
            m_Relay = Relay.getRelayById(this.m_serial);
            m_Relay.UpdatePortsStatus();
            this.UpdateCheckBoxes();
        }

        private void RelayProperty_Load(object sender, EventArgs e)
        {

        }

        private void UpdateCheckBoxes()
        {
            relay1.Checked = this.m_Relay.getPortByIndex(1).PortStatus;
            relay2.Checked = this.m_Relay.getPortByIndex(2).PortStatus;
            relay3.Checked = this.m_Relay.getPortByIndex(3).PortStatus;
            relay4.Checked = this.m_Relay.getPortByIndex(4).PortStatus;
            relay5.Checked = this.m_Relay.getPortByIndex(5).PortStatus;
            relay6.Checked = this.m_Relay.getPortByIndex(6).PortStatus;
            relay7.Checked = this.m_Relay.getPortByIndex(7).PortStatus;
            relay8.Checked = this.m_Relay.getPortByIndex(8).PortStatus;

        }


        private void SetPort(short portNumber, bool val)
        {
            Relay.SetRelayPort(m_Relay.SerialId, portNumber, val);
            //m_Relay.UpdatePortsStatus();
            this.UpdateCheckBoxes();
        }


        private void relay1_Click(object sender, EventArgs e)
        {
            SetPort(1, ((CheckBox)sender).Checked);
        }

        private void relay2_Click(object sender, EventArgs e)
        {
            SetPort(2, ((CheckBox)sender).Checked);
        }

        private void relay3_Click(object sender, EventArgs e)
        {
            SetPort(3, ((CheckBox)sender).Checked);
        }

        private void relay4_Click(object sender, EventArgs e)
        {
            SetPort(4, ((CheckBox)sender).Checked);
        }

        private void relay5_Click(object sender, EventArgs e)
        {
            SetPort(5, ((CheckBox)sender).Checked);
        }

        private void relay6_Click(object sender, EventArgs e)
        {
            SetPort(6, ((CheckBox)sender).Checked);
        }

        private void relay7_Click(object sender, EventArgs e)
        {
            SetPort(7, ((CheckBox)sender).Checked);
        }

        private void relay8_Click(object sender, EventArgs e)
        {
            SetPort(8, ((CheckBox)sender).Checked);
        }

        private void relay6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void relay5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void relay7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void relay8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdAllOn_Click(object sender, EventArgs e)
        {
            for (short i = 1; i <= 8; i++)
            {
                if (!this.m_Relay.getPortByIndex(i).PortStatus)
                    SetPort(i, true);
            }
            //this.UpdateCheckBoxes();
        }

        private void cmdAllOff_Click(object sender, EventArgs e)
        {
            for (short i = 1; i <= 8; i++)
            {
                if (this.m_Relay.getPortByIndex(i).PortStatus)
                    SetPort(i, false);
            }
            //this.UpdateCheckBoxes();
        }
    }
}
