using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Server
{
    public enum GetPatientIDMethod
    {
        ByText = 1,
        ByImage = 2
    }

    public class ReportReceiveEventArg : EventArgs
    {
        public string PatientId
        {
            get;
            set;
        }

        public string ReportPath
        {
            get;
            set;
        }

        public string CallingIP
        {
            get;
            set;
        }

        public bool IsOverwriteExist
        {
            get;
            set;
        }
    }

    public delegate void ReportReceiveEventHandler(ReportReceiveEventArg arg);

    public class ReportServer
    {
        //listen port
        private static int _port = 11121;
        private static string _reportPath = @"D:\ReportFolder";
        private static GetPatientIDMethod _parseIdMethod = GetPatientIDMethod.ByText;

        private static bool _isRunning = false;
        private static bool _stopSignal = false;

        public static ManualResetEvent _connectSignal = new ManualResetEvent(false);

        public static event ReportReceiveEventHandler ReportReceiveEvent;

        public static int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public static string ReportPath
        {
            get { return _reportPath; }
            set { _reportPath = value; }
        }

        public static GetPatientIDMethod GetPatientIdMethod
        {
            get { return _parseIdMethod; }
            set { _parseIdMethod = value; }
        }

        public static void Start()
        {
            //start listen
            if (!_isRunning)
            {
                _isRunning = true;
                _stopSignal = false;

                ThreadPool.QueueUserWorkItem(ThreadFunc);
            }
        }

        public static void Stop()
        {
            _stopSignal = true;
            _connectSignal.Set();
        }

        private static void ThreadFunc(object ctx)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ipAddress, Port);
            listener.Start();

            while (true)
            {
                try
                {
                    if (_stopSignal)
                        break;

                    _connectSignal.Reset();

                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAcceptTcpClient(new AsyncCallback(ConnectCallback), listener);

                    _connectSignal.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }

            listener.Stop();
            _isRunning = false;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            _connectSignal.Set();

            try
            {
                TcpListener listener = (TcpListener)ar.AsyncState;
                TcpClient client = listener.EndAcceptTcpClient(ar);
                //client.SendTimeout = 5000;
                //client.ReceiveTimeout = 5000;

                using (var socketStream = client.GetStream())
                {
                    ReportInfo report = ReportSendReceiver.ReceiveReport(socketStream);

                    HandleReport(report, socketStream);
                }

                client.Close();
            }
            catch(Exception ex)
            {

            }
        }

        private static void HandleReport(ReportInfo report, NetworkStream socket)
        {
            if(report.Status == ReportStatus.SubmitInitial)
            {
                //save report
                SaveReport(report);

                string patientId = ParsePatientId(report);
                report.PatientId = patientId;
                if(string.IsNullOrEmpty(patientId))
                {
                    report.Status = ReportStatus.ErrorGetIdFail;
                    ReportSendReceiver.SendReport(report, socket);

                    return;
                }

                //check exist report
                byte[] existReport = GetExistReport(patientId);
                if(existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    ReportSendReceiver.SendReport(report, socket);
                    return;
                }

                //all is OK, let client confirm whether the patientId is correct.
                report.Status = ReportStatus.ConfirmPatientId;
                ReportSendReceiver.SendReport(report, socket);
            }
            else if(report.Status == ReportStatus.SubmitNewPatientId)
            {
                byte[] existReport = GetExistReport(report.PatientId);
                if (existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    ReportSendReceiver.SendReport(report, socket);
                    return;
                }

                report.Status = ReportStatus.ConfirmOK;
                //TODO: notify main program

                if(ReportReceiveEvent != null)
                {
                    ReportReceiveEventArg arg = new ReportReceiveEventArg();
                    arg.IsOverwriteExist = report.ExistReportAction == ExistReportAction.Overwrite;
                    arg.PatientId = report.PatientId;
                    arg.CallingIP = report.CallingIP;
                    arg.ReportPath = report.GetReportPath();

                    ReportReceiveEvent(arg);
                }

                ReportSendReceiver.SendReport(report, socket);
            }
            else if(report.Status == ReportStatus.SubmitOK)
            {
                //notify main program
                report.Status = ReportStatus.ConfirmOK;

                //TODO: notify main program
                if (ReportReceiveEvent != null)
                {
                    ReportReceiveEventArg arg = new ReportReceiveEventArg();
                    arg.IsOverwriteExist = report.ExistReportAction == ExistReportAction.Overwrite;
                    arg.PatientId = report.PatientId;
                    arg.CallingIP = report.CallingIP;
                    arg.ReportPath = report.GetReportPath();

                    ReportReceiveEvent(arg);
                }

                ReportSendReceiver.SendReport(report, socket);
            }
        }

        private static void SaveReport(ReportInfo report)
        {
            //save pdf to report folder
            string path = report.GetReportPath();
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = Path.Combine(path, "Report.pdf");
            //if(report.ExistReportAction == ExistReportAction.Overwrite)
            {
                using (FileStream fs = new FileStream(file, FileMode.CreateNew))
                {
                    fs.Write(report.PdfReport, 0, report.PdfReport.Length);
                    fs.Flush();
                }
            }
            //else
            {

            }
            //save txt and jpg file.

        }

        private static string ParsePatientId(ReportInfo report)
        {
            return "ServerParsedPatientId";
        }

        private static byte[] GetExistReport(string patientId)
        {
            return null;
        }
    }
}
