using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CSharpCom
{
    public class Connection
    {
        private static Socket Connect(string server, int port)
        {
            Socket socket = null;
            IPHostEntry hostEntry = null;

            hostEntry = Dns.GetHostEntry(server);

            foreach(IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);

                Socket TemporarySocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                
                TemporarySocket.Connect(ipe);

                if (TemporarySocket.Connected)
                {
                    socket = TemporarySocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return socket;

        }

        private static string GetInfo(string ip, int port)
        {
            Byte[] DataSent = Encoding.UTF8.GetBytes(ip);
            Byte[] DataReceived = new Byte[256];

            string Data = "";


            using (Socket socket = Connect(ip, port))
            {
                if (socket == null)
                {
                    return ("ERR");
                }

                socket.Send(DataSent, DataSent.Length, 0);

                int ByteNum = 0;

                while (ByteNum > 0)
                {
                    ByteNum = socket.Receive(DataReceived, DataReceived.Length, 0);
                    Data += Encoding.UTF8.GetString(DataReceived, 0, ByteNum);
                }

            }
            return Data;

        }


        public static string Recv()
        {
            string ip;
            int port = 22;

            ip = Dns.GetHostName();
            return GetInfo(ip, port);
        }


    }
}