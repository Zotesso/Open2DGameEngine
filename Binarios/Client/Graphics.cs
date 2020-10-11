using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Bindings;
using System.IO;

namespace Client
{
    class Graphics
    {
        public static Texture2D[] Characters = new Texture2D[3];
        public static Texture2D[] Tilesets = new Texture2D[3];

        public static void InitializeGraphics(ContentManager manager)
        {
            LoadCharacters(manager);
            LoadTilesets(manager);
        }

        public static void RenderGraphics()
        {

            
            Game1._spriteBatch.Begin();
            DrawMapGrid();
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if (ClientTCP.IsPlaying(i))
                {
                    if (Types.Player[i].Map == Types.Player[Globals.playerIndex].Map)
                    {
                        DrawPlayer(i);
                    }
                }
            }
            Game1._spriteBatch.End();
        }

        private static void LoadCharacters(ContentManager manager)
        {
            for(int i = 1; i < Characters.Length; i++)
            {
                Characters[i] = manager.Load<Texture2D>("Characters/" + i.ToString());
            }
        }

        private static void LoadTilesets(ContentManager manager)
        {
            for(int i = 1; i < Tilesets.Length; i++)
            {
                Tilesets[i] = manager.Load<Texture2D>("Tilesets/" + i.ToString());
            }
        }

        public static int ConvertMapX(int x)
        {
            return x - (Globals.TileView.left * 32) - Globals.Camera.Left;
        }

        public static int ConvertMapY(int y)
        {
            return y - (Globals.TileView.top * 32) - Globals.Camera.Top;
        }

        private static void DrawSprite(int sprite, int x2, int y2, Rectangle srcrec)
        {
            int X, Y;
            X = ConvertMapX(x2);
            Y = ConvertMapY(y2);

            Game1._spriteBatch.Draw(Characters[sprite], new Vector2(X, Y), srcrec, Color.White);
        }

        private static void DrawTile(int tilesetNum, int x2, int y2, Rectangle srcrec)
        {
            int X, Y;
            X = ConvertMapX(x2);
            Y = ConvertMapY(y2);

            Game1._spriteBatch.Draw(Tilesets[tilesetNum], new Vector2(X, Y), srcrec, Color.White);
        }

        private static void DrawPlayer(int index)
        {
            byte anim;
            int X, Y;
            Rectangle srcrec;
            int SpriteNum;
            int spriteLeft;

            SpriteNum = 1;
            spriteLeft = 0;

            anim = 1;

            switch (Types.Player[index].Dir) {
                case Constants.DIR_UP:
                    spriteLeft = 3;
                    if (Types.Player[index].YOffset > 8)
                        anim = Types.Player[index].Steps;
                    break;
               case Constants.DIR_DOWN:
                    spriteLeft = 0;
                    if (Types.Player[index].YOffset < -8)
                        anim = Types.Player[index].Steps;
                    break;
               case Constants.DIR_LEFT:
                    spriteLeft = 1;
                    if (Types.Player[index].XOffset > 8)
                        anim = Types.Player[index].Steps;
                    break;
               case Constants.DIR_RIGHT:
                    spriteLeft = 2;
                    if (Types.Player[index].XOffset < -8)
                        anim = Types.Player[index].Steps;
                    break;
            }

            srcrec = new Rectangle((anim) * (Characters[SpriteNum].Width / 12), spriteLeft * (Characters[SpriteNum].Height / 8), Characters[SpriteNum].Width / 12, Characters[SpriteNum].Height / 8);
            X = Types.Player[index].X * 32 + Types.Player[index].XOffset - ((Characters[SpriteNum].Width / 12 - 32) /2);
            Y = Types.Player[index].Y * 32 + Types.Player[index].YOffset;

            DrawSprite(SpriteNum, X, Y, srcrec);
        }

        private static void DrawMapGrid()
        {
            int tileNum;
            Rectangle srcrec;

            tileNum = 1;

            for(int x = 0; x < Constants.MAX_MAP_X; x++)
            {
                for(int y = 0; y < Constants.MAX_MAP_Y; y++)
                {
                    srcrec = new Rectangle(0, 0, 32, 32);
                    int xPos = x * 32;
                    int yPos = y * 32;
                    DrawTile(tileNum, xPos, yPos, srcrec);
                }
            }
        }
    }
}
