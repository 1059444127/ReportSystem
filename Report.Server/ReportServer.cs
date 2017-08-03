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

    public class ReportEventArg : EventArgs
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

    public delegate void ReportEventHandler(ReportEventArg arg);

    public class ReportServer
    {
        //listen port
        private static int _port = 11121;
        private static string _reportPath = @"D:\ReportFolder";
        private static GetPatientIDMethod _parseIdMethod = GetPatientIDMethod.ByText;

        private static bool _isRunning = false;
        private static bool _stopSignal = false;

        public static ManualResetEvent _connectSignal = new ManualResetEvent(false);

        public static event ReportEventHandler OnReportArrive;

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
            if (_isRunning)
                return;

            _isRunning = true;
            _stopSignal = false;

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, ReportServer.Port);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                try
                {
                    if (_stopSignal)
                        break;

                    _connectSignal.Reset();

                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    _connectSignal.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }

            _isRunning = false;
        }

        public static void Stop()
        {
            _stopSignal = true;
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            _connectSignal.Set();
 
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            SocketBufferHelper buffer = new SocketBufferHelper();
            buffer.WorkSocket = handler;
            handler.BeginReceive(buffer.Buffer, 0, SocketBufferHelper.BufferSize, 0, new AsyncCallback(ReceiveCallback), buffer);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                SocketBufferHelper buffer = (SocketBufferHelper)ar.AsyncState;
                Socket socket = buffer.WorkSocket;

                int bytesRead = socket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    socket.BeginReceive(buffer.Buffer, buffer.CurBufferPos, SocketBufferHelper.BufferSize, 0, new AsyncCallback(ReceiveCallback), buffer);
                    buffer.CurBufferPos += bytesRead;
                }
                else
                {
                    using (var memStream = new MemoryStream())
                    {
                        var binForm = new BinaryFormatter();
                        memStream.Write(buffer.Buffer, 0, buffer.CurBufferPos);
                        memStream.Seek(0, SeekOrigin.Begin);

                        ReportInfo report = binForm.Deserialize(memStream) as ReportInfo;
                        report.CallingIP = socket.RemoteEndPoint.ToString();

                        //process report
                        HandleReport(report, socket);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void SendReport(ReportInfo report, Socket socket)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, report);
                byte[] byteData = ms.ToArray();

                socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), socket);
            }   
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;

                int bytesSent = socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void HandleReport(ReportInfo report, Socket socket)
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
                    SendReport(report, socket);
                    return;
                }

                //check exist report
                byte[] existReport = GetExistReport(patientId);
                if(existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    SendReport(report, socket);
                    return;
                }

                //all is OK, let client confirm whether the patientId is correct.
                report.Status = ReportStatus.ConfirmPatientId;
                SendReport(report, socket);
            }
            else if(report.Status == ReportStatus.SubmitNewPatientId)
            {
                byte[] existReport = GetExistReport(report.PatientId);
                if (existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    SendReport(report, socket);
                    return;
                }

                report.Status = ReportStatus.ConfirmOK;
                //TODO: notify main program

                if(OnReportArrive != null)
                {
                    ReportEventArg arg = new ReportEventArg();
                    arg.IsOverwriteExist = report.ExistReportAction == ExistReportAction.Overwrite;
                    arg.PatientId = report.PatientId;
                    arg.CallingIP = report.CallingIP;
                    arg.ReportPath = report.GetReportPath();

                    OnReportArrive(arg);
                }

                SendReport(report, socket);
            }
            else if(report.Status == ReportStatus.SubmitOK)
            {
                //notify main program
                report.Status = ReportStatus.ConfirmOK;

                //TODO: notify main program
                ReportEventArg arg = new ReportEventArg();
                arg.IsOverwriteExist = report.ExistReportAction == ExistReportAction.Overwrite;
                arg.PatientId = report.PatientId;
                arg.CallingIP = report.CallingIP;
                arg.ReportPath = report.GetReportPath();

                OnReportArrive(arg);

                SendReport(report, socket);
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
