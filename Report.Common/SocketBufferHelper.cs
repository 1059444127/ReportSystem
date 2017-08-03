using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class SocketBufferHelper
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 5 * 1024 * 1024; //5M
        public byte[] Buffer = new byte[BufferSize];
        public int CurBufferPos = 0;

        public void Reset()
        {
            CurBufferPos = 0;
            WorkSocket = null;

            //clear buffer data
        }
    }
}
