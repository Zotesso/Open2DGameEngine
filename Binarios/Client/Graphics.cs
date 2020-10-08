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

        private static void DrawSprite(int sprite, int x2, int y2, Rectangle srcrec)
        {
            Game1._spriteBatch.Draw(Characters[sprite], new Vector2(x2, y2), srcrec, Color.White);
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

            srcrec = new Rectangle((anim) * (Characters[SpriteNum].Width / 12), spriteLeft * (Characters[SpriteNum].Height / 12), Characters[SpriteNum].Width / 12, Characters[SpriteNum].Height / 12);
            X = 0 * 32 + 0 - ((Characters[SpriteNum].Width / 12 - 32) /2);
            Y = 0 * 32 + 0;

            DrawSprite(SpriteNum, X, Y, srcrec);
        }
    }
}
