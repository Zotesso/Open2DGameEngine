using System;
using Bindings;
using System.Collections.Generic;

namespace Server
{
    class ServerHandleData
    {
        private delegate void Packet(int index, byte[] data);
        private static Dictionary<int, Packet> Packets;
        private Database db = new Database();

        public void InitializeMessages()
        {
            Packets = new Dictionary<int, Packet>();

            Console.WriteLine("Initializing Packets");

            //Packets
            Packets.Add((int)ClientPackets.CLogin, HandleLogin);
            Packets.Add((int)ClientPackets.CRegister, HandleRegister);
        }

        public void HandleNetworkMessages(int index, byte[] data)
        {
            int packetNum;
            PacketBuffer buffer;
            buffer = new PacketBuffer();

            buffer.AddByteArray(data);
            packetNum = buffer.GetInteger();
            buffer.Dispose();

            if (Packets.TryGetValue(packetNum, out Packet Packet))
            {
                Packet.Invoke(index, data);
            }
        }

        private void HandleLogin(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            buffer.GetInteger();

            string username = buffer.GetString();
            string password = buffer.GetString();

            if (!db.AccountExist(username))
            {
                return;
            }

            if(!db.PasswordCheck(index, username, password))
            {
                return;
            }

            db.LoadPlayer(index, username);
            GameLogic.JoinGame(index);
            Console.WriteLine("Player " + username + " Has logged in");
        }

        private void HandleRegister(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            buffer.GetInteger();

            string username = buffer.GetString();
            string password = buffer.GetString();

            if (!db.AccountExist(username))
            {
                db.AddAcount(index, username, password);
            }
            else
            {
                Console.WriteLine("user already exist");
            }
        }

    }
}
