using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report.Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();

            ThreadPool.RegisterWaitForSingleObject(Program.ProgramStarted, OnProgramStarted, null, -1, false);

            this.notifyIcon.ShowBalloonTip(2000, "Report Client", "v1.0.0", ToolTipIcon.Info);
            this.notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        void OnProgramStarted(object state, bool timeout)
        {
            Invoke((MethodInvoker)(() =>
            {
                this.notifyIcon.ShowBalloonTip(2000, "Report Client", "v1.0.0", ToolTipIcon.Info);
            }));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //check pdf at specified folder
            try
            {

            }
            catch
            {

            }
        }

        private void iconMenuShow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void iconMenuHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void iconMenuProperties_Click(object sender, EventArgs e)
        {

        }
        private void iconMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
