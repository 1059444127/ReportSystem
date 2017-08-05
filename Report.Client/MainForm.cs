using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
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

            InitializeSettings();
        }

        private void InitializeSettings()
        {
            string reportInbox = ConfigurationManager.AppSettings["ReportInbox"];
            string serverIP = ConfigurationManager.AppSettings["ServerIP"];
            string serverPort = ConfigurationManager.AppSettings["ServerPort"];
            string needConfirmOkReport = ConfigurationManager.AppSettings["NeedConfirmOkReport"];

            int nConfirmOK = 0;
            int.TryParse(needConfirmOkReport, out nConfirmOK);

            int nPort = 11121;
            int.TryParse(serverPort, out nPort);

            //init UI
            tbxReportPath.Text = reportInbox;
            tbxServerIP.Text = serverIP;
            tbxServerPort.Text = serverPort;
            cbxConfirmOKReport.Checked = nConfirmOK != 0;

            //set ReportClient
            ReportClient.ServerIP = serverIP;
            ReportClient.ServerPort = nPort;
            ReportClient.PdfReportFolder = reportInbox;
            ReportClient.LogPath = @"C:\ReportLog\Client";
            ReportClient.NeedConfirmPatientId = nConfirmOK != 0;

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
            else if (report.HasError())
            {
                this.notifyIcon.ShowBalloonTip(2000, report.ErrorMessage, "v1.0.0", ToolTipIcon.Info);
            }
            else if(report.IsServerDone())
            {
                this.notifyIcon.ShowBalloonTip(5000, "报告提交成功", "v1.0.0", ToolTipIcon.Info);
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

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            ReportInfo report = ReportClient.GetReportNeedConfirm();
            if(report != null)
            {
                this.notifyIcon.ShowBalloonTip(10000, "请确认报告信息", "v1.0.0", ToolTipIcon.Warning);
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void tbxServerPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                return;
            }
        }

        private void tbxServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string reportInbox = tbxReportPath.Text;
            if (!Directory.Exists(reportInbox))
            {
                MessageBox.Show("报告路径不存在");
                return;
            }
 
            string serverIP = tbxServerIP.Text;
            string serverPort = tbxServerPort.Text;
            bool bConfirmOKReport = cbxConfirmOKReport.Checked;

            ConfigurationManager.AppSettings["ReportInbox"] = reportInbox;
            ConfigurationManager.AppSettings["ServerIP"] = serverIP;
            ConfigurationManager.AppSettings["ServerPort"] = serverPort;
            ConfigurationManager.AppSettings["NeedConfirmOkReport"] = bConfirmOKReport ? "1" : "0";

            ReportClient.Stop();

            int nPort = 11121;
            int.TryParse(serverPort, out nPort);

            ReportClient.ServerIP = serverIP;
            ReportClient.ServerPort = nPort;
            ReportClient.PdfReportFolder = reportInbox;
            ReportClient.LogPath = @"C:\ReportLog\Client";
            ReportClient.NeedConfirmPatientId = bConfirmOKReport;

            ReportClient.Start();

            HideForm();
        }
    }
}
