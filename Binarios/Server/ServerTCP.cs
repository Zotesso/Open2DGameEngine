using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Bindings;

namespace Server
{
    class ServerTCP
    {
        public static Client[] Clients = new Client[Constants.MAX_PLAYERS];
        public TcpListener ServerSocket;
        public static NetworkStream clientStream;
        public void InitializeNetwork()
        {
            ServerSocket = new TcpListener(IPAddress.Any, 5555);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
        }

        private void OnClientConnect(IAsyncResult ar)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(ar);
            client.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            for(int i = 1; i <= Constants.MAX_PLAYERS; i++)
            {
                if(Clients[i].Socket == null)
                {
                    Clients[i].Socket = client;
                    Clients[i].Index = i;
                    Clients[i].IP = client.Client.RemoteEndPoint.ToString();
                    Clients[i].Start();

                    Console.WriteLine("Connection received from " + Clients[i].IP);
                    return;
                }
            }
        }

        public bool isConnected(int index)
        {
            if(Clients[index].Socket != null )
            {
                return Clients[index].Socket.Connected;
            }
            return false;
        }
        public void SendData(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            clientStream = Clients[index].Socket.GetStream();
            clientStream.Write(buffer.ToArray(), 0, buffer.ToArray().Length);
            buffer.Dispose();
        }

        public void SendDataToMapBut(int index, int mapNum, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (isConnected(i))
                {
                    if (Types.Player[i].Map == mapNum && i != index)
                    {
                        SendData(i, data);
                    }
                }
            }
        }

        public void SendDataToMap(int mapNum, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (isConnected(i))
                {
                    if (Types.Player[i].Map == mapNum)
                    {
                        SendData(i, data);
                    }
                }
            }
        }

        public void SendPlayerData(int index)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ServerPackets.SPlayerData);
            buffer.AddInteger(index - 1);

            buffer.AddString(Types.Player[index - 1].Name);
            buffer.AddInteger(Types.Player[index - 1].Map);
            buffer.AddInteger(Types.Player[index - 1].Level);
            buffer.AddInteger(Types.Player[index - 1].EXP);
            buffer.AddByte(Types.Player[index - 1].Dir);
            buffer.AddInteger(Types.Player[index - 1].X);
            buffer.AddInteger(Types.Player[index - 1].Y);

            SendDataToMap(Types.Player[index - 1].Map, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
