﻿using System;
using Bindings;
using System.Net.Sockets;
using Myra.Graphics2D.UI;

namespace Client
{
    class ClientTCP
    {
        public TcpClient PlayerSocket;
        private static NetworkStream myStream;
        private ClientHandleData clientDataHandle;
        private byte[] asyncBuff;
        private bool connecting;
        private bool connected;

        public static Types.PlayerStruct[] Player = new Types.PlayerStruct[Constants.MAX_PLAYERS];
        public static Types.MapStruct[] Map = new Types.MapStruct[Constants.MAX_MAPS];
        public void ConnectToServer()
        {
            if(PlayerSocket != null)
            {
                if (PlayerSocket.Connected || connected)
                {
                    return;
                }
                PlayerSocket.Close();
                PlayerSocket = null;
            }

            PlayerSocket = new TcpClient();
            clientDataHandle = new ClientHandleData();
            PlayerSocket.ReceiveBufferSize = 4096;
            PlayerSocket.SendBufferSize = 4096;
            PlayerSocket.NoDelay = false;

            Array.Resize(ref asyncBuff, 8192);

            PlayerSocket.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ConnectCallback), PlayerSocket);
            connecting = true;
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            PlayerSocket.EndConnect(ar);
            if(PlayerSocket.Connected == false)
            {
                connecting = false;
                connected = false;
                return;
            }
            else
            {
                PlayerSocket.NoDelay = true;
                myStream = PlayerSocket.GetStream();
                myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
                connected = true;
                connecting = false;
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            int byteAmt = myStream.EndRead(ar);
            byte[] myBytes = null;
            Array.Resize(ref myBytes, byteAmt);
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteAmt);

            if(byteAmt == 0)
            {
                return;
            }

            clientDataHandle.HandleNetworkMessages(0, myBytes);
            myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
        }

        public static bool IsPlaying(int index)
        {

            if(Types.Player[index].Name != null) 
            {
                return true;
            }

            return false;
        }

        public void SendData(byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            myStream.Write(buffer.ToArray(), 0, buffer.ToArray().Length);
            buffer.Dispose();
        }

        public void SendLogin()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ClientPackets.CLogin);
            buffer.AddString(Globals.loginUsername);
            buffer.AddString(Globals.loginPassword);

            SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public void SendRegister()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ClientPackets.CRegister);
            buffer.AddString(Globals.regUsername);
            buffer.AddString(Globals.regPassword);
            buffer.AddString(Globals.regRepeatPassword);

            SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public void SendPlayerMove()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ClientPackets.CPlayerMove);

            buffer.AddByte(Types.Player[Globals.playerIndex].Dir);
            buffer.AddInteger(Types.Player[Globals.playerIndex].Moving);

            SendData(buffer.ToArray());
            buffer.Dispose();
        }
    }
}
