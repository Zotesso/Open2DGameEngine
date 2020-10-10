using System;
using Microsoft.Xna.Framework;
using Bindings;

namespace Client
{
    class Globals
    {
        public static Rectangle Camera;
        public static Types.RECT TileView;
        public static bool InGame;

        public static int playerIndex;

        public static string loginUsername = "";
        public static string loginPassword = "";

        public static string regUsername = "";
        public static string regPassword = "";
        public static string regRepeatPassword = "";

        public static bool DirUp;
        public static bool DirDown;
        public static bool DirLeft;
        public static bool DirRight;

    }
}
