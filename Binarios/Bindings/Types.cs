using System;
using Bindings;

namespace Bindings
{
    class Types
    {
        public static PlayerStruct[] Player = new PlayerStruct[Constants.MAX_PLAYERS];

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
    }
}
