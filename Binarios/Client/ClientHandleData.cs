using System;
using System.Collections.Generic;
using Bindings;

namespace Client
{
    class ClientHandleData
    {
        private static Database db;

        public PacketBuffer Buffer = new PacketBuffer();
        private delegate void Packet(int index, byte[] data);
        private static Dictionary<int, Packet> Packets;

        public void InitializeMessages()
        {
            Packets = new Dictionary<int, Packet>();

            Packets.Add((int)ServerPackets.SJoinGame, HandleJoinGame);
        }

        public void HandleNetworkMessages(int index, byte[] data)
        {
            int packetNum;
            PacketBuffer buffer;
            buffer = new PacketBuffer();

            buffer.AddByteArray(data);
            packetNum = buffer.GetInteger();
            buffer.Dispose();

            if(Packets.TryGetValue(packetNum, out Packet Packet))
            {
                Packet.Invoke(index, data);
            }
        }

        private void HandleJoinGame(int index, byte[] data)
        {
            db = new Database();

            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            buffer.GetInteger();

            Globals.playerIndex = buffer.GetInteger();
            string playerName = buffer.GetString();
            int playerMap = buffer.GetInteger();

            db.SaveLocalMap(playerMap);

            MenuManager.ChangeMenu(MenuManager.Menu.InGame, Game1._desktop);
            GameLogic.InGame();
        }
    }
}
