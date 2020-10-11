using System;
using System.Collections.Generic;
using Bindings;
using Myra.Graphics2D.UI;

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
            Packets.Add((int)ServerPackets.SPlayerMove, HandlePlayerMove);
            Packets.Add((int)ServerPackets.SPlayerData, HandlePlayerData);
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
            Types.Player[Globals.playerIndex].Name = buffer.GetString();
            int playerMap = buffer.GetInteger();

            db.SaveLocalMap(playerMap);

            MenuManager.ChangeMenu(MenuManager.Menu.InGame, Game1._desktop);
            GameLogic.InGame();
        }

        private void HandlePlayerMove(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            buffer.GetInteger();

            int targetIndex = buffer.GetInteger();
            int targetX = buffer.GetInteger();
            int targetY = buffer.GetInteger();
            byte targetDirection = buffer.GetByte();
            int targetMoving = buffer.GetInteger();

            Types.Player[targetIndex].X = targetX;
            Types.Player[targetIndex].Y = targetY;
            Types.Player[targetIndex].Dir = targetDirection;

            Types.Player[targetIndex].XOffset = 0;
            Types.Player[targetIndex].YOffset = 0;
            Types.Player[targetIndex].Moving = targetMoving;

            switch (Types.Player[targetIndex].Dir)
            {
                case Constants.DIR_UP:
                    Types.Player[targetIndex].YOffset = 32;
                    break;
                case Constants.DIR_DOWN:
                    Types.Player[targetIndex].YOffset = 32 * -1;
                    break;
                case Constants.DIR_LEFT:
                    Types.Player[targetIndex].XOffset = 32;
                    break;
                case Constants.DIR_RIGHT:
                    Types.Player[targetIndex].XOffset = 32 * -1;
                    break;
            }
        }

        private void HandlePlayerData(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddByteArray(data);
            buffer.GetInteger();

            int targetIndex = buffer.GetInteger();
            string targetName = buffer.GetString();
            int targetMap = buffer.GetInteger();
            int targetLevel = buffer.GetInteger();
            int targetEXP = buffer.GetInteger();
            byte targetDirection = buffer.GetByte();
            int targetX = buffer.GetInteger();
            int targetY = buffer.GetInteger();



            Types.Player[targetIndex].X = targetX;
            Types.Player[targetIndex].Y = targetY;
            Types.Player[targetIndex].Dir = targetDirection;

            Types.Player[targetIndex].Name = targetName;
            Types.Player[targetIndex].Map = targetMap;
            Types.Player[targetIndex].Level = targetLevel;
            Types.Player[targetIndex].EXP = targetEXP;

            var messageBox = Dialog.CreateMessageBox("Digitou errado :(", targetIndex.ToString());
            messageBox.ShowModal(Game1._desktop);

        }
    }
}
