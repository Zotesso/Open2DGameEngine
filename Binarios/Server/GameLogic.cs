using Bindings;
using System;


namespace Server
{
    class GameLogic
    {
        private static ServerTCP stcp;

        public static void JoinGame(int index)
        {
            stcp = new ServerTCP();
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ServerPackets.SJoinGame);
            buffer.AddInteger(index - 1);
            buffer.AddString(Types.Player[index - 1].Name);
            buffer.AddInteger(Types.Player[index - 1].Map);

            stcp.SendData(index, buffer.ToArray());
            stcp.SendPlayerData(index);

            buffer.Dispose();
        }

        public static void PlayerMove(int index, byte dir, int movement)
        {
            SetPlayerDir(index, dir);

            int x = DirToX(Types.Player[index].X, dir);
            int y = DirToY(Types.Player[index].Y, dir);

            if(IsValidPosition(x, y)){
                Types.Player[index].X = x;
                Types.Player[index].Y = y;

                stcp = new ServerTCP();
                PacketBuffer buffer = new PacketBuffer();
                buffer.AddInteger((int)ServerPackets.SPlayerMove);
                buffer.AddInteger(index - 1);
                buffer.AddInteger(Types.Player[index].X);
                buffer.AddInteger(Types.Player[index].Y);
                buffer.AddByte(dir);
                buffer.AddInteger(movement);

                stcp.SendDataToMapBut(index, Types.Player[index].Map ,buffer.ToArray());
                buffer.Dispose();
            }
        }
        
        public static void SetPlayerDir(int index, byte dir)
        {
            Types.Player[index].Dir = dir;
        }

        public static bool IsValidPosition(int x, int y)
        {
            if(x < 0 || x > Constants.MAX_MAP_X || y < 0 || y > Constants.MAX_MAP_Y)
            {
                return false;
            }
            return true;
        }
        
        public static int DirToX(int x, byte dir)
        {
            if(dir == Constants.DIR_UP || dir == Constants.DIR_DOWN)
            {
                return x;
            }

            return x + ((dir * 2) - 5);
        }

        public static int DirToY(int y, byte dir)
        {
            if (dir == Constants.DIR_LEFT || dir == Constants.DIR_RIGHT)
            {
                return y;
            }

            return y + ((dir * 2) - 1);
        }
    }
}
