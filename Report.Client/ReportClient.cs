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

namespace Report.Client
{
    internal class ReportSendEventArg : EventArgs
    {
        internal bool HasError
        {
            get;
            set;
        }

        internal string ErrorMessage
        {
            get;
            set;
        }

        internal bool NeedConfirm
        {
            get;
            set;
        }

        internal ReportInfo Report
        {
            get;
            set;
        }
    }

    internal delegate void ReportSendEventHandler(ReportSendEventArg arg);

    internal class ReportClient
    {
        #region Fileds/Properties

        private static string _serverIP = "127.0.0.1";
        private static int _serverPort = 11121;
        private static bool _needConfirmPatientId = false;
        private static string _pdfReportFolder = "";

        private static string _logPath = @"C:\ReportClientLog";
        private static string _usbUID;
        private static string _doctorSignImage;

        //e.g. user plugin usb, start, and if plugoff the usb, then need to stop
        private static bool _stopSignal = false;
        private static bool _isRunning = false;
        private static ManualResetEvent _reportConfirmedSignal = new ManualResetEvent(false);

        public static event ReportSendEventHandler ReportSendEvent;

        public static string ServerIP
        {
            get { return _serverIP; }
            set { _serverIP = value; }
        }

        public static int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }

        public static bool NeedConfirmPatientId
        {
            get { return _needConfirmPatientId; }
            set { _needConfirmPatientId = value; }
        }

        public static string PdfReportFolder
        {
            get { return _pdfReportFolder; }
            set { _pdfReportFolder = value; }
        }

        public static string LogPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }

        public static string UsbUID
        {
            get { return _usbUID; }
            set { _usbUID = value; }
        }

        public static string DoctorSignImage
        {
          get { return _doctorSignImage; }
          set { _doctorSignImage = value; }
        }

        #endregion

        public static void Start()
        {
            if (!_isRunning)
            {
                _stopSignal = false;
                _isRunning = true;

                ThreadPool.QueueUserWorkItem(ThreadFunc);
            }
        }

        /// <summary>
        /// Inform ReportClient to stop send reports. Note if there is one report under processing, ReprotClient will finish that report and stop.
        /// </summary>
        public static void Stop()
        {
            _stopSignal = true;
        }

        /// <summary>
        /// called after client side confirmed the report
        /// </summary>
        /// <param name="report"></param>
        public static void SetReportConfirmed(ReportInfo report)
        {
            _reportConfirmedSignal.Set();
        }

        private static void ThreadFunc(object ctx)
        {
            while(true)
            {
                try
                {
                    if (_stopSignal)
                        break;

                    //1. get new pdf file
                    string pdfFile = QueryPdfFile();
                    if (string.IsNullOrEmpty(pdfFile))
                    {
                        Thread.Sleep(2000);
                        continue;
                    }

                    //2. start handle report
                    ReportInfo report = new ReportInfo();
                    report.PdfReport = File.ReadAllBytes(pdfFile);
                    report.Status = ReportStatus.SubmitInitial;

                    bool reportDone = false;
                    do
                    {
                        TcpClient client = new TcpClient();
                        //client.SendTimeout = 5000;//5s
                        //client.ReceiveTimeout = 5000;//5s
                        client.Connect(IPAddress.Parse(ServerIP), ServerPort);//it will throw an exception if failed to connect.

                        using(NetworkStream socketStream = client.GetStream())
                        {
                            ReportSendReceiver.SendReport(report, socketStream);

                            report = ReportSendReceiver.ReceiveReport(socketStream);
                            if (report.Status == ReportStatus.ConfirmOK || report.Status == ReportStatus.ErrorOther)
                            {
                                reportDone = true;
                            }
                            else
                            {
                                _reportConfirmedSignal.Reset();

                                if (ReportSendEvent != null)
                                {
                                    ReportSendEvent(new ReportSendEventArg() { HasError = false, Report = report, NeedConfirm = report.NeedConfirm() });
                                }

                                _reportConfirmedSignal.WaitOne();
                            }
                        }

                        //client.Close();
                    }
                    while (!reportDone);

                    //4. clear work
                    if (ReportSendEvent != null)
                    {
                        ReportSendEvent(new ReportSendEventArg() { HasError = report.Status == ReportStatus.ErrorOther, Report = report, NeedConfirm = report.NeedConfirm() });
                    }
                }
                catch(Exception ex)
                {
                    if (ReportSendEvent != null)
                    {
                        ReportSendEvent(new ReportSendEventArg() { HasError = true, ErrorMessage = ex.Message });
                    }

                    Thread.Sleep(1000);
                }
            }

            _isRunning = false;
        }

        private static string QueryPdfFile()
        {
            return @"C:\ReportPDF\report.pdf";
        }
    }
}
