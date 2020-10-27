using System;
using Bindings;

namespace Bindings
{
    class Types
    {
        public static PlayerStruct[] Player = new PlayerStruct[Constants.MAX_PLAYERS];
        public static MapStruct[] Map = new MapStruct[Constants.MAX_MAPS];
        public static NpcStruct[] Npc = new NpcStruct[Constants.MAX_NPCS];
        public static MapNpcStruct[] MapNpc = new MapNpcStruct[Constants.MAX_MAP_NPCS];

        [Serializable]
        public struct PlayerStruct
        {
            public string Login;
            public string Password;

            public string Name;
            public int Sprite;
            public int Level;
            public int EXP;

            public int Map;
            public int X;
            public int Y;
            public byte Dir;

            public int XOffset;
            public int YOffset;
            public int Moving;
            public byte Steps;
        }

        public struct RECT {
            public int top;
            public int left;
            public int right;
            public int bottom;
        }

        [Serializable]
        public struct MapStruct
        {
            public string Name;
            public byte MaxMapX;
            public byte MaxMapY;

            public byte Moral;

            public int tpUp;
            public int tpDown;
            public int tpLeft;
            public int tpRight;

            public TileStruct[,] Tile;
            public NpcStruct[] Npc;
        }

        [Serializable]
        public struct TileStruct{
            public int Ground;
            public int Mask;
            public int Mask2;
            public int Fringe;

            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
        }

        [Serializable]
        public struct NpcStruct {
            public string Name;

            public int Sprite;
        }

        public struct MapNpcStruct
        {
            public int num;

            public int target;
            public int HP;
            public int MAXHP;

            public int Map;
            public byte x;
            public byte y;
            public byte dir;

            public int xOffset;
            public int yOffset;
            public byte moving;
            public byte attacking;
        }
    }
}
