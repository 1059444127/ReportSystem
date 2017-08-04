using Report.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoServer
{
    public partial class Form1 : Form
    {
        private bool _reportServerStarted = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReportServer.Port = 11121;
            ReportServer.ReportPath = "D:\\Report";
            ReportServer.ReportReceiveEvent += ReportServer_OnReportReceive;
            ReportServer.LogPath = @"C:\ReportLog\Server";

            ReportServer.Start();
            _reportServerStarted = true;
        }

        private void btnStopReportServer_Click(object sender, EventArgs e)
        {
            if(_reportServerStarted)
            {
                ReportServer.Stop();
                btnStopReportServer.Text = "Start Report Server";

                _reportServerStarted = false;
            }
            else
            {
                ReportServer.Start();
                btnStopReportServer.Text = "Stop Report Server";
                _reportServerStarted = true;
            }
        }

        private void ReportServer_OnReportReceive(ReportReceiveEventArg arg)
        {
            //MessageBox.Show("New report comes: " + arg.CallingIP);
        }
    }
}
