using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class ReportSendReceiver
    {
        public static bool SendReport(ReportInfo report, NetworkStream socketStream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, report);
                ms.Seek(0, SeekOrigin.Begin);

                // Send Length (Int64)
                socketStream.Write(BitConverter.GetBytes(ms.Length), 0, 8);

                var buffer = new byte[1024 * 100];
                int count;
                while ((count = ms.Read(buffer, 0, buffer.Length)) > 0)
                {
                    socketStream.Write(buffer, 0, count);
                }
            }

            return true;
        }

        public static ReportInfo ReceiveReport(NetworkStream socketStream)
        {
            Int64 bytesReceived = 0;
            int count;
            var buffer = new byte[1024 * 100];

            // Read length - Int64
            socketStream.Read(buffer, 0, 8);
            Int64 numberOfBytes = BitConverter.ToInt64(buffer, 0);

            using (var ms = new MemoryStream())
            {
                while (bytesReceived < numberOfBytes && (count = socketStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, count);
                    bytesReceived += count;
                }

                var binFormat = new BinaryFormatter();
                ms.Seek(0, SeekOrigin.Begin);

                ReportInfo report = binFormat.Deserialize(ms) as ReportInfo;

                return report;
            }

        }

    }
}
