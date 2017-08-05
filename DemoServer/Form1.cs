using Report.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoServer
{
    public partial class Form1 : Form
    {
        private bool _reportServerStarted = false;
        private List<ReportStruct> _reportList = new List<ReportStruct>();

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

            ReportServer.LogManager.LogEvent += LogManager_LogEvent;
        }

        private void LogManager_LogEvent(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            Invoke((MethodInvoker)(() =>
            {
                StringBuilder sbLog = new StringBuilder(msg);
                sbLog.Append(Environment.NewLine).Append(tbxLog.Text);

                tbxLog.Text = sbLog.ToString();
            }));
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
            Invoke((MethodInvoker)(() =>
            {
                _reportList.Add(new ReportStruct()
                {
                    PatientId = arg.PatientId,
                    ReportPath = arg.ReportPath,
                    CallingIP = arg.CallingIP,
                    IsOverwrite = arg.IsOverwriteExist
                });

                reportStructBindingSource.DataSource = null;
                reportStructBindingSource.DataSource = _reportList;
                dataGridView1.Refresh();
            }));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    string reportPath = dataGridView1.Rows[e.RowIndex].Cells[2].Value as string;
                    reportPath += "\\Report.pdf";

                    if (File.Exists(reportPath))
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = reportPath;
                        process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                        process.Start();
                        process.Close();
                    }
                }
            }
        }
    }
}
