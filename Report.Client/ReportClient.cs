using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Client
{
    internal delegate void ReportStatusUpdateHandler(ReportInfo report);

    internal class ReportClient
    {
        #region Fileds/Properties

        private static string _serverIP = "127.0.0.1";
        private static int _serverPort = 11121;
        private static bool _confirmPatientId = false;
        private static string _pdfReportFolder = "";

        private static string _logPath = @"C:\ReportClientLog";
        private static string _usbUID;
        private static string _doctorSignImage;

        //e.g. user plugin usb, start, and if plugoff the usb, then need to stop
        private static bool _stopSignal = false;
        private static ManualResetEvent _reportConfirmSignal = new ManualResetEvent(false);
        private static ManualResetEvent _reportDoneSignal = new ManualResetEvent(false);
        private static bool _isRunning = false;

        public static event ReportStatusUpdateHandler OnReportStatusUpdate;

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

        public static bool ConfirmPatientId
        {
            get { return _confirmPatientId; }
            set { _confirmPatientId = value; }
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

        public static void Run()
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

        private static void ThreadFunc(object ctx)
        {
            while(true)
            {
                try
                {
                    if (_stopSignal)
                        break;

                    string pdfFile = QueryPdfFile();
                    if (string.IsNullOrEmpty(pdfFile))
                    {
                        Thread.Sleep(2000);
                        continue;
                    }

                    ReportInfo report = new ReportInfo();
                    report.PdfReport = File.ReadAllBytes(pdfFile);
                    report.Status = ReportStatus.SubmitInitial;

                    SubmitReport(report);

                    //wait for the last report finish signal
                    _reportDoneSignal.WaitOne();
                }
                catch(Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }

            _isRunning = false;
        }

        private static string QueryPdfFile()
        {
            return @"C:\ReportPDF\report.pdf";
        }

        internal static void SubmitReport(ReportInfo report)
        {
            ReportSender.SendReport(report);

            //check response
            if (report.Status == ReportStatus.ConfirmOK || report.Status == ReportStatus.ErrorOther)
            {
                //tell main thread this report is done, can handle next report.
                _reportDoneSignal.Set();
            }

            if(OnReportStatusUpdate != null)
            {
                OnReportStatusUpdate(report);
            }
        }
    }
}
