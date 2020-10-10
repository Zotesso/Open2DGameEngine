using System;
using Bindings;
using Myra.Graphics2D.UI;

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
            if(Types.Player[Globals.playerIndex].Moving != 0)
            {
                return false;
            }

            int dir = Types.Player[Globals.playerIndex].Dir;

            if (Globals.DirUp)
            {
                Types.Player[Globals.playerIndex].Dir = Constants.DIR_UP;
            }
            if (Globals.DirDown)
            {
                Types.Player[Globals.playerIndex].Dir = Constants.DIR_DOWN;
            }
            if (Globals.DirRight)
            {
                Types.Player[Globals.playerIndex].Dir = Constants.DIR_RIGHT;
            }
            if (Globals.DirLeft)
            {
                Types.Player[Globals.playerIndex].Dir = Constants.DIR_LEFT;
            }

            return true;
        }

        public static bool CheckDirections(byte direction)
        {
            int X, Y;

            switch (direction)
            {
                case Constants.DIR_UP:
                    X = Types.Player[Globals.playerIndex].X;
                    Y = Types.Player[Globals.playerIndex].Y - 1;
                    break;
                case Constants.DIR_DOWN:
                    X = Types.Player[Globals.playerIndex].X;
                    Y = Types.Player[Globals.playerIndex].Y + 1;
                    break;
                case Constants.DIR_LEFT:
                    X = Types.Player[Globals.playerIndex].X - 1;
                    Y = Types.Player[Globals.playerIndex].Y;
                    break;
                case Constants.DIR_RIGHT:
                    X = Types.Player[Globals.playerIndex].X + 1;
                    Y = Types.Player[Globals.playerIndex].Y;
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
                    Types.Player[Globals.playerIndex].Moving = 1;
                    switch (Types.Player[Globals.playerIndex].Dir)
                    {
                        case Constants.DIR_UP:
                            Types.Player[Globals.playerIndex].YOffset = 32;
                            Types.Player[Globals.playerIndex].Y -= 1;
                            break;
                        case Constants.DIR_DOWN:
                            Types.Player[Globals.playerIndex].YOffset = (32 * -1);
                            Types.Player[Globals.playerIndex].Y += 1;
                            break;
                        case Constants.DIR_LEFT:
                            Types.Player[Globals.playerIndex].XOffset = 32;
                            Types.Player[Globals.playerIndex].X -= 1;
                            break;
                        case Constants.DIR_RIGHT:
                            Types.Player[Globals.playerIndex].XOffset = (32 * -1);
                            Types.Player[Globals.playerIndex].X += 1;
                            break;
                    }
                }
            }
        }

        public static void ProcessMovement()
        {
            int movementSpeed = (Types.Player[Globals.playerIndex].Moving * 6);

            switch (Types.Player[Globals.playerIndex].Dir) {
                case Constants.DIR_UP:
                    Types.Player[Globals.playerIndex].YOffset -= movementSpeed;
                    if(Types.Player[Globals.playerIndex].YOffset < 0){
                        Types.Player[Globals.playerIndex].YOffset = 0;
                    }
                    break;
                case Constants.DIR_DOWN:
                    Types.Player[Globals.playerIndex].YOffset += movementSpeed;
                    if (Types.Player[Globals.playerIndex].YOffset > 0)
                    { 
                        Types.Player[Globals.playerIndex].YOffset = 0;
                    }
                    break;
                case Constants.DIR_LEFT:
                    Types.Player[Globals.playerIndex].XOffset -= movementSpeed;
                    if (Types.Player[Globals.playerIndex].XOffset < 0)
                    {
                        Types.Player[Globals.playerIndex].XOffset = 0;
                    }
                    break;
                case Constants.DIR_RIGHT:
                    Types.Player[Globals.playerIndex].XOffset += movementSpeed;
                    if (Types.Player[Globals.playerIndex].XOffset > 0)
                    {
                        Types.Player[Globals.playerIndex].XOffset = 0;
                    }
                    break;
            }

            if(Types.Player[Globals.playerIndex].Moving > 0)
            {
                if(Types.Player[Globals.playerIndex].Dir == Constants.DIR_RIGHT || Types.Player[Globals.playerIndex].Dir == Constants.DIR_DOWN)
                {
                    if(Types.Player[Globals.playerIndex].XOffset >= 0 && Types.Player[Globals.playerIndex].YOffset >= 0)
                    {
                        Types.Player[Globals.playerIndex].Moving = 0;
                        if(Types.Player[Globals.playerIndex].Steps == 0)
                        {
                            Types.Player[Globals.playerIndex].Steps = 2;
                        }
                        else
                        {
                            Types.Player[Globals.playerIndex].Steps = 0;
                        }
                    }
                }
                else
                {
                    if (Types.Player[Globals.playerIndex].XOffset <= 0 && Types.Player[Globals.playerIndex].YOffset <= 0)
                    {
                        Types.Player[Globals.playerIndex].Moving = 0;
                        if (Types.Player[Globals.playerIndex].Steps == 0)
                        {
                            Types.Player[Globals.playerIndex].Steps = 2;
                        }
                        else
                        {
                            Types.Player[Globals.playerIndex].Steps = 0;
                        }
                    }
                }
            }
        }

        public static void InGame()
        {
            var messageBox = Dialog.CreateMessageBox("Digitou errado :(", Globals.playerIndex.ToString());
            messageBox.ShowModal(Game1._desktop);
            Globals.InGame = true;
        }
    }
}
