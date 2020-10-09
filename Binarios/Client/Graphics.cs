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
        
        public static void InitializeGraphics(ContentManager manager)
        {
            LoadCharacters(manager);
        }

        public static void RenderGraphics()
        {
            Game1._spriteBatch.Begin();
            DrawPlayer();
            Game1._spriteBatch.End();
        }

        private static void LoadCharacters(ContentManager manager)
        {
            for(int i = 1; i < Characters.Length; i++)
            {
                Characters[i] = manager.Load<Texture2D>("Characters/" + i.ToString());
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

        private static void DrawPlayer()
        {
            byte anim;
            int X, Y;
            Rectangle srcrec;
            int SpriteNum;
            int spriteLeft;

            SpriteNum = 1;
            spriteLeft = 0;

            anim = 1;

            switch (Types.Player[0].Dir) {
                case Constants.DIR_UP:
                    spriteLeft = 3;
                    if (Types.Player[0].YOffset > 8)
                        anim = Types.Player[0].Steps;
                    break;
               case Constants.DIR_DOWN:
                    spriteLeft = 0;
                    if (Types.Player[0].YOffset < -8)
                        anim = Types.Player[0].Steps;
                    break;
               case Constants.DIR_LEFT:
                    spriteLeft = 1;
                    if (Types.Player[0].XOffset > 8)
                        anim = Types.Player[0].Steps;
                    break;
               case Constants.DIR_RIGHT:
                    spriteLeft = 2;
                    if (Types.Player[0].XOffset < -8)
                        anim = Types.Player[0].Steps;
                    break;
            }

            srcrec = new Rectangle((anim) * (Characters[SpriteNum].Width / 12), spriteLeft * (Characters[SpriteNum].Height / 8), Characters[SpriteNum].Width / 12, Characters[SpriteNum].Height / 8);
            X = Types.Player[0].X * 32 + Types.Player[0].XOffset - ((Characters[SpriteNum].Width / 12 - 32) /2);
            Y = Types.Player[0].Y * 32 + Types.Player[0].YOffset;

            DrawSprite(SpriteNum, X, Y, srcrec);
        }
    }
}
