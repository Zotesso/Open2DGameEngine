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

            buffer.AddString(Types.Player[index].Name);
            buffer.AddInteger(Types.Player[index].Map);
            buffer.AddInteger(Types.Player[index].Level);
            buffer.AddInteger(Types.Player[index].EXP);
            buffer.AddByte(Types.Player[index].Dir);
            buffer.AddInteger(Types.Player[index].X);
            buffer.AddInteger(Types.Player[index].Y);

            SendDataToMap(Types.Player[index].Map, buffer.ToArray());
            buffer.Dispose();
        }

        public void SendJoinMap(int index)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if(isConnected(i) && i != index && Types.Player[i].Map == Types.Player[index].Map)
                {
                    PacketBuffer buffer = new PacketBuffer();
                    buffer.AddInteger((int)ServerPackets.SPlayerData);
                    buffer.AddInteger(i - 1);

                    buffer.AddString(Types.Player[i].Name);
                    buffer.AddInteger(Types.Player[i].Map);
                    buffer.AddInteger(Types.Player[i].Level);
                    buffer.AddInteger(Types.Player[i].EXP);
                    buffer.AddByte(Types.Player[i].Dir);
                    buffer.AddInteger(Types.Player[i].X);
                    buffer.AddInteger(Types.Player[i].Y);

                    Console.WriteLine(i.ToString());
                    Console.WriteLine(Types.Player[0].X.ToString());

                    SendData(index, buffer.ToArray());
                    buffer.Dispose();
                }
            }

            SendPlayerData(index);
        }
    }
}
