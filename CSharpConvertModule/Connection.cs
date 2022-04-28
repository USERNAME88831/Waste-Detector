using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace CSharpConvertModule
{
    public class Connection
    {
        private static Socket Connect(string ip, int port)
        {
            Socket socket = null;
            IPHostEntry hostEntry = null;


            foreach(IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket = new Socket (ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect (ip, port);

                if (tempSocket.Connected) { socket = tempSocket; break; }
                else { continue; }
            }
            return socket;
        }

        private static string GetInfo(string ip, int port)
        {
            Byte[] DataSent = Encoding.UTF8.GetBytes(ip);
            Byte[] DataRecv = new byte[1024];
            string Data = "";

            using (Socket socket = Connect(ip, port))
            {
                if(socket == null)
                {
                    return ("FAILED");
                }





                socket.Send(DataSent, DataSent.Length, 0);

                int ByteNumber = 0;

                while (ByteNumber > 0)
                {
                    ByteNumber = socket.Receive(DataRecv, DataRecv.Length, 0);
                    Data += Encoding.UTF8.GetString(DataRecv, 0, ByteNumber);
                }
            }
            return Data;
        }


        public static string GetData(string[] args)
        {
            string ip;
            int port = 80;

            if (args.Length > 0)
            {
                ip = Dns.GetHostName();
            }
            else { ip = args[0]; }
            
            string Data = GetInfo(ip, port);

            return Data;
            
        }
    }
}