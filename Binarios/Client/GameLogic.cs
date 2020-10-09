using System;
using Bindings;

namespace Client
{
    class GameLogic
    {

        public static bool IsTryingToMove()
        {
            if(Globals.DirUp || Globals.DirRight || Globals.DirLeft || Globals.DirDown)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanMove()
        {
            if(Types.Player[0].Moving != 0)
            {
                return false;
            }

            int dir = Types.Player[0].Dir;

            if (Globals.DirUp)
            {
                Types.Player[0].Dir = Constants.DIR_UP;
            }
            if (Globals.DirDown)
            {
                Types.Player[0].Dir = Constants.DIR_DOWN;
            }
            if (Globals.DirRight)
            {
                Types.Player[0].Dir = Constants.DIR_RIGHT;
            }
            if (Globals.DirLeft)
            {
                Types.Player[0].Dir = Constants.DIR_LEFT;
            }

            return true;
        }

        public static bool CheckDirections(byte direction)
        {
            int X, Y;

            switch (direction)
            {
                case Constants.DIR_UP:
                    X = Types.Player[0].X;
                    Y = Types.Player[0].Y - 1;
                    break;
                case Constants.DIR_DOWN:
                    X = Types.Player[0].X;
                    Y = Types.Player[0].Y + 1;
                    break;
                case Constants.DIR_LEFT:
                    X = Types.Player[0].X - 1;
                    Y = Types.Player[0].Y;
                    break;
                case Constants.DIR_RIGHT:
                    X = Types.Player[0].X + 1;
                    Y = Types.Player[0].Y;
                    break;
            }

            return false;
        }

        public static void CheckMovement()
        {
            if(IsTryingToMove())
            {
                if (CanMove())
                {
                    Types.Player[0].Moving = 1;
                    switch (Types.Player[0].Dir)
                    {
                        case Constants.DIR_UP:
                            Types.Player[0].YOffset = 32;
                            Types.Player[0].Y -= 1;
                            break;
                        case Constants.DIR_DOWN:
                            Types.Player[0].YOffset = (32 * -1);
                            Types.Player[0].Y += 1;
                            break;
                        case Constants.DIR_LEFT:
                            Types.Player[0].XOffset = 32;
                            Types.Player[0].X -= 1;
                            break;
                        case Constants.DIR_RIGHT:
                            Types.Player[0].XOffset = (32 * -1);
                            Types.Player[0].X += 1;
                            break;
                    }
                }
            }
        }

        public static void ProcessMovement()
        {
            int movementSpeed = (Types.Player[0].Moving * 6);

            switch (Types.Player[0].Dir) {
                case Constants.DIR_UP:
                    Types.Player[0].YOffset -= movementSpeed;
                    if(Types.Player[0].YOffset < 0){
                        Types.Player[0].YOffset = 0;
                    }
                    break;
                case Constants.DIR_DOWN:
                    Types.Player[0].YOffset += movementSpeed;
                    if (Types.Player[0].YOffset > 0)
                    { 
                        Types.Player[0].YOffset = 0;
                    }
                    break;
                case Constants.DIR_LEFT:
                    Types.Player[0].XOffset -= movementSpeed;
                    if (Types.Player[0].XOffset < 0)
                    {
                        Types.Player[0].XOffset = 0;
                    }
                    break;
                case Constants.DIR_RIGHT:
                    Types.Player[0].XOffset += movementSpeed;
                    if (Types.Player[0].XOffset > 0)
                    {
                        Types.Player[0].XOffset = 0;
                    }
                    break;
            }

            if(Types.Player[0].Moving > 0)
            {
                if(Types.Player[0].Dir == Constants.DIR_RIGHT || Types.Player[0].Dir == Constants.DIR_DOWN)
                {
                    if(Types.Player[0].XOffset >= 0 && Types.Player[0].YOffset >= 0)
                    {
                        Types.Player[0].Moving = 0;
                        if(Types.Player[0].Steps == 0)
                        {
                            Types.Player[0].Steps = 2;
                        }
                        else
                        {
                            Types.Player[0].Steps = 0;
                        }
                    }
                }
                else
                {
                    if (Types.Player[0].XOffset <= 0 && Types.Player[0].YOffset <= 0)
                    {
                        Types.Player[0].Moving = 0;
                        if (Types.Player[0].Steps == 0)
                        {
                            Types.Player[0].Steps = 2;
                        }
                        else
                        {
                            Types.Player[0].Steps = 0;
                        }
                    }
                }
            }
        }
    }
}
