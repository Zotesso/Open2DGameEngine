using Bindings;
using System;


namespace Server
{
    class GameLogic
    {
        private static ServerTCP stcp = new ServerTCP();
        private static Database db = new Database();

        public static void JoinGame(int index)
        {
            Console.WriteLine(Types.Player[1].X.ToString());
            PlayerWarp(index, Types.Player[index].Map, Types.Player[index].X, Types.Player[index].Y);

            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ServerPackets.SJoinGame);
            buffer.AddInteger(index - 1);
            buffer.AddString(Types.Player[index].Name);
            buffer.AddInteger(Types.Player[index].Map);

            stcp.SendData(index, buffer.ToArray());

            buffer.Dispose();
            stcp.SendJoinMap(index);
        }

        public static void PlayerMove(int index, byte dir, int movement)
        {
            SetPlayerDir(index, dir);

            int x = DirToX(Types.Player[index].X, dir);
            int y = DirToY(Types.Player[index].Y, dir);

            if(IsValidPosition(x, y)){
                Types.Player[index].X = x;
                Types.Player[index].Y = y;

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

        public static void PlayerWarp(int index, int mapNum, int x, int y)
        {
            Types.Player[index].X = x;
            Types.Player[index].Y = y;
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

        public static void SetPlayerAccess(int index, byte access)
        {
            Types.Player[index].Access = access;
            stcp.SendPlayerData(index);
        }

        public static int GetPlayerIndexByName(string name)
        {
            for(int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if(Types.Player[i].Name == name)
                {
                    return i;
                }

                if (Types.Player[i].Name == String.Empty)
                {
                    return -1;
                }
            }

            return -1;
        }

        public static void LeftGame(int index)
        {
            db.SavePlayer(index);
            ClearPlayer(index);
            stcp.SendPlayerData(index);
        }

        public static void ClearPlayer(int index)
        {
            Types.Player[index].Access = 0;
            Types.Player[index].Dir = 0;
            Types.Player[index].EXP = 0;
            Types.Player[index].Level = 0;
            Types.Player[index].Login = "";
            Types.Player[index].Map = 0;
            Types.Player[index].Moving = 0;
            Types.Player[index].Name = "";
            Types.Player[index].Password = "";
            Types.Player[index].Sprite = 0;
            Types.Player[index].Steps = 0;
            Types.Player[index].X = 0;
            Types.Player[index].XOffset = 0;
            Types.Player[index].Y = 0;
            Types.Player[index].YOffset = 0;
        }
    }
}
