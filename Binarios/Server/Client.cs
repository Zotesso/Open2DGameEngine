using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server
{
    class Client
    {
        public int Index;
        public string IP;
        public TcpClient Socket;
        public NetworkStream myStream;
        private ServerHandleData handleServerData;
        public bool Closing;
        public byte[] readBuff;

        public void Start()
        {
            handleServerData = new ServerHandleData();

            Socket.SendBufferSize = 4096;
            Socket.ReceiveBufferSize = 4096;
            myStream = Socket.GetStream();

            Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
            myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
        }

        private void OnReceiveData( IAsyncResult ar)
        {
            try
            {
                int readBytes = myStream.EndRead(ar);
                if(readBytes <= 0)
                {
                    CloseSocket(Index);
                    return;
                }

                byte[] newBytes = null;
                Array.Resize(ref newBytes, readBytes);
                Buffer.BlockCopy(readBuff, 0, newBytes, 0, readBytes);

                handleServerData.HandleNetworkMessages(Index, newBytes);
                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
            }
            catch
            {
                CloseSocket(Index);
            }
        }

        private void CloseSocket(int index)
        {
            Console.WriteLine("Connection from " + IP + "has been ended.");
            Socket.Close();
            Socket = null;
        }


    }
}
