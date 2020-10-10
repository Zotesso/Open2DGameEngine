using System;
using Bindings;

namespace Bindings
{
    class Types
    {
        public static PlayerStruct[] Player = new PlayerStruct[Constants.MAX_PLAYERS];
        public static MapStruct[] Map = new MapStruct[Constants.MAX_MAPS];

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
    }
}
