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

            //only one program is allowed
            ThreadPool.RegisterWaitForSingleObject(Program.ProgramStarted, OnProgramStarted, null, -1, false);

            this.notifyIcon.ShowBalloonTip(2000, "报告客户端", "v1.0.0", ToolTipIcon.Info);
            this.notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;

            ReportClient.OnReportStatusUpdate += ReportClient_OnReportStatusUpdate;
        }

        private void Show()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void Hide()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        void OnProgramStarted(object state, bool timeout)
        {
            Invoke((MethodInvoker)(() =>
            {
                this.notifyIcon.ShowBalloonTip(2000, "报告客户端已经启动", "v1.0.0", ToolTipIcon.Info);
            }));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //check usb at specified folder
            if (IsUSBAlive())
            {
                ReportClient.Start();   
            }
            else
            {
                ReportClient.Stop();
            }
        }

        private bool IsUSBAlive()
        {
            return true;
        }

        void ReportClient_OnReportStatusUpdate(ReportInfo report)
        {
            if (report.NeedConfirm())
            {

            }
            else if (report.Status == ReportStatus.ErrorOther)
            {
                this.notifyIcon.ShowBalloonTip(2000, report.ErrorMessage, "v1.0.0", ToolTipIcon.Info);
            }
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Show();
        }

        private void iconMenuShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void iconMenuHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void iconMenuProperties_Click(object sender, EventArgs e)
        {
            Show();
        }
        private void iconMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
