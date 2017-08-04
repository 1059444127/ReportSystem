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

            ReportClient.ServerIP = "127.0.0.1";
            ReportClient.ServerPort = 11121;
            ReportClient.PdfReportFolder = @"C:\ReportPDF";
            ReportClient.LogPath = @"C:\ReportLog\Client";
            ReportClient.NeedConfirmPatientId = true;

            ReportClient.ReportSendEvent += ReportClient_ReportSendEventHandler;
        }

        private void ShowForm()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void HideForm()
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

        void ReportClient_ReportSendEventHandler(ReportSendEventArg arg)
        {
            if (arg.HasError)
            {
                this.notifyIcon.ShowBalloonTip(2000, arg.ErrorMessage, "v1.0.0", ToolTipIcon.Info);
                return;
            }

            ReportInfo report = arg.Report;
            if (report.NeedConfirm())
            {
                //should notify UI and let doctor to decide, now just test without UI
                if(report.Status == ReportStatus.FailedGetPatientId)
                {
                    report.PatientId = "newPatientID";
                    report.Status = ReportStatus.SubmitNewPatientId;

                    this.notifyIcon.ShowBalloonTip(10000, "请手动输入病人Id", "v1.0.0", ToolTipIcon.Error);
                }
                else if(report.Status == ReportStatus.ConfirmPatientId)
                {
                    report.Status = ReportStatus.SubmitOK;
                    this.notifyIcon.ShowBalloonTip(10000, "请确认报告信息", "v1.0.0", ToolTipIcon.Warning);
                }
                else if(report.Status == ReportStatus.ConfirmExistReport)
                {
                    report.ExistReportAction = ExistReportAction.Append;
                    report.Status = ReportStatus.SubmitExistReportAction;

                    this.notifyIcon.ShowBalloonTip(10000, "请确认报告信息", "v1.0.0", ToolTipIcon.Warning);
                }

                ReportClient.SetReportConfirmed(report);
            }
            else if (report.Status == ReportStatus.Error)
            {
                this.notifyIcon.ShowBalloonTip(2000, report.ErrorMessage, "v1.0.0", ToolTipIcon.Info);
            }
            else if(report.Status == ReportStatus.ConfirmOK)
            {
                this.notifyIcon.ShowBalloonTip(5000, "报告处理成功", "v1.0.0", ToolTipIcon.Info);
            }
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void iconMenuShow_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void iconMenuHide_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void iconMenuProperties_Click(object sender, EventArgs e)
        {
            ShowForm();
        }
        private void iconMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
