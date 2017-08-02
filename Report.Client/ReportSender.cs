using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Report.Client
{
    internal class SocketBufferHelper
    {
        internal Socket WorkSocket = null;
        internal const int BufferSize = 5*1024*1024; //5M
        internal byte[] Buffer = new byte[BufferSize];
        internal int CurBufferPos = 0;

        internal void Reset()
        {
            CurBufferPos = 0;
            WorkSocket = null;

            //clear buffer data
        }
    }

    internal class ReportSender
    {
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);

        private static ReportInfo _curReport;

        internal static void SendReport(ReportInfo report)
        { 
            try
            {
                _curReport = report;

                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), ReportClient.ServerPort);
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                 
                clientSocket.BeginConnect(serverEndPoint, new AsyncCallback(ConnectCallback), clientSocket);
                _connectDone.WaitOne();

                Send(clientSocket, report);
                _sendDone.WaitOne();

                Receive(clientSocket);
                _receiveDone.WaitOne();

                Console.WriteLine("Response received");

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            { 
                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());
                 
                _connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket socket, ReportInfo report)
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

                _sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket socket)
        {
            try
            {
                SocketBufferHelper state = new SocketBufferHelper();
                state.WorkSocket = socket;

                socket.BeginReceive(state.Buffer, 0, SocketBufferHelper.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                SocketBufferHelper receiveData = (SocketBufferHelper)ar.AsyncState;
                Socket socket = receiveData.WorkSocket;
 
                int bytesRead = socket.EndReceive(ar);

                if (bytesRead > 0)
                {  
                    socket.BeginReceive(receiveData.Buffer, receiveData.CurBufferPos, SocketBufferHelper.BufferSize, 0, new AsyncCallback(ReceiveCallback), receiveData);
                    receiveData.CurBufferPos += bytesRead;
                }
                else
                {
                    using (var memStream = new MemoryStream())
                    {
                        var binForm = new BinaryFormatter();
                        memStream.Write(receiveData.Buffer, 0, receiveData.CurBufferPos);
                        memStream.Seek(0, SeekOrigin.Begin);

                        ReportInfo report = binForm.Deserialize(memStream) as ReportInfo;
                        _curReport.CopyFrom(report);
                    }

                    // Signal that all bytes have been received.  
                    _receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}