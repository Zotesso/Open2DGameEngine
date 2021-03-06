﻿using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Bindings;
using System.IO;
using Myra.Graphics2D.UI;

namespace Client
{
    class Graphics
    {
        public static Texture2D[] Characters = new Texture2D[3];
        public static Texture2D[] Tilesets = new Texture2D[3];
        private static SpriteFont font;

        public static void InitializeGraphics(ContentManager manager)
        {
            LoadFonts(manager);
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
                        DrawPlayerName(i);
                        DrawPlayer(i);
                    }
                }
            }

            for (int npcNum = 0; npcNum < Constants.MAX_MAP_NPCS; npcNum++)
            {
                if (Types.MapNpc[npcNum].Map == Types.Player[Globals.playerIndex].Map)
                {
                    DrawNpc(npcNum);
                }
            }
            Game1._spriteBatch.End();
        }

        private static void LoadFonts(ContentManager manager)
        {
            font = manager.Load<SpriteFont>("Font");
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
            int cameraLeft = 0;
            int tileViewLeft = 0;

           cameraLeft = Types.Player[Globals.playerIndex].X  + Types.Player[Globals.playerIndex].XOffset;
           tileViewLeft = Types.Player[Globals.playerIndex].X - 5;

            return x - (tileViewLeft * 32) - cameraLeft;
        }

        public static int ConvertMapY(int y)
        {
            int cameraTop = 0;
            int tileViewTop = 0;

            cameraTop = Types.Player[Globals.playerIndex].Y + Types.Player[Globals.playerIndex].YOffset;
            tileViewTop = Types.Player[Globals.playerIndex].Y - 5;
            return y - (tileViewTop * 32) - cameraTop;
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

            srcrec = new Rectangle((anim) * (Characters[SpriteNum].Width / 4), spriteLeft * (Characters[SpriteNum].Height / 4), Characters[SpriteNum].Width / 4, Characters[SpriteNum].Height / 4);
            X = Types.Player[index].X * 32 + Types.Player[index].XOffset - ((Characters[SpriteNum].Width / 4 - 32) /2);
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

        private static void DrawPlayerName(int index)
        { 

            int xoffset = Types.Player[index].X * 32 + Types.Player[index].XOffset;
            int yoffset = Types.Player[index].Y * 32 + Types.Player[index].YOffset;

            int x = ConvertMapX(xoffset) - 6;
            int y = ConvertMapY(yoffset) - 20;

            Game1._spriteBatch.DrawString(font, Types.Player[index].Name, new Vector2(x, y), Color.Yellow);
        }

        private static void DrawNpc(int num)
        {
            byte anim = 1;
            int X, Y;
            Rectangle srcrec;
            int SpriteNum = Types.Npc[Types.MapNpc[num].num].Sprite + 1;
            int spriteLeft = 0;

            srcrec = new Rectangle((anim) * (Characters[SpriteNum].Width / 4), spriteLeft * (Characters[SpriteNum].Height / 4), Characters[SpriteNum].Width / 4, Characters[SpriteNum].Height / 4);
            X = Types.MapNpc[num].x * 32 + Types.MapNpc[num].xOffset - ((Characters[SpriteNum].Width / 4 - 32) / 2);
            Y = Types.MapNpc[num].y * 32 + Types.MapNpc[num].yOffset;

            DrawSprite(SpriteNum, X, Y, srcrec);
        }
    }
}
