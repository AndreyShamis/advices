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
    public partial class ApplicationLogs : Form
    {
        public ApplicationLogs()
        {
            InitializeComponent();
            PrintErrors();
        }

        private void ApplicationLogs_Load(object sender, EventArgs e)
        {

        }

        private void btnErrors_Click(object sender, EventArgs e)
        {
            PrintErrors();
        }

        private void PrintErrors()
        {
            try
            {
                txtApplicationLog.Clear();
                string tmpT = "";
                foreach (string tmp in Common.ApplicationErrors)
                {
                    tmpT = tmpT + DateTime.Now.ToString("d HH:mm:ss") + ":" +  tmp + "\r\n";
                }
                txtApplicationLog.Text = tmpT;
            }
            catch (Exception)
            {

            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                txtApplicationLog.Clear();
                string tmpT = "";
                foreach (string tmp in Common.Log)
                {
                    tmpT = tmpT + tmp + "\r\n";
                }
                txtApplicationLog.Text = tmpT;
            }
            catch (Exception)
            {

            }
        }
    }
}
