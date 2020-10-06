using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.IO;

namespace Client
{
    class Graphic
    {
        public const string DATA_PATH = "Data/";
        public const string GRAPHIC_PATH = "Data/GFX/";
        public const string FILE_EXT = ".jpg";

        public const string TILESET_PATH = GRAPHIC_PATH + "Tilesets/" + "Tiles";

        public struct Rect {
            public int Top;
            public int Left;
            public int Right;
            public int Bottom;
        }

        public static Rect TileView;

        //TILESET
        static Sprite[] TilesetSprite;
        static int numTilesets;

        public static void LoadGameAssets()
        {
            CheckTilesets();
        }

        public static void RenderGraphics()
        {
            for(int x = TileView.Left; x <= TileView.Right; x++)
            {
                for(int y = TileView.Top; y <= TileView.Bottom; y++)
                {
                    DrawMapTile(1, x, y);
                }
            }

            DrawMapGrid();
        }

        public static void RenderSprite(Sprite spriteToDraw, RenderWindow target, int destX, int destY, int sourceX, int SourceY, int sourceWidth, int sourceHeight)
        {
            spriteToDraw.TextureRect = new IntRect(sourceX, SourceY, sourceWidth, sourceHeight);
            spriteToDraw.Position = new SFML.System.Vector2f(destX, destY);
            target.Draw(spriteToDraw);
        }

        static void DrawMapTile(int mapnum, int x, int y)
        {
            int i = 0;
            RectangleShape srcRect = new RectangleShape(new SFML.System.Vector2f(0, 0));
            srcRect.Size = new SFML.System.Vector2f(0, 0);

            TileView.Top = 0;
            TileView.Bottom = Map.Maps[1].MaxY - 1;
            TileView.Left = 0;
            TileView.Right = Map.Maps[1].MaxX - 1;

            for(i = (int)Map.LayerType.Ground; i <= (int)Map.LayerType.Mask2; i++)
            {
                if (Map.Maps[mapnum].Tile[x, y].layer[i].Tileset > 0 & Map.Maps[mapnum].Tile[x, y].layer[i].Tileset <= numTilesets)
                {
                    RenderSprite(TilesetSprite[2], Program.gameWindow, (x * 32), (y * 32), 0, 0, 32, (128 / 4));
                }
            }
        }

        static void DrawMapGrid()
        {
            RectangleShape rec = new RectangleShape();
            for(int x = TileView.Left; x <= TileView.Right + 1; x++)
            {
                for (int y = TileView.Top; y <= TileView.Bottom + 1; y++)
                {
                    rec.OutlineColor = new Color(Color.Red);
                    rec.OutlineThickness = 0.8f;
                    rec.FillColor = new Color(Color.Transparent);
                    rec.Size = new SFML.System.Vector2f((x * 32), (y * 32));
                    rec.Position = new SFML.System.Vector2f((TileView.Left * 32), (TileView.Top * 32));

                    Program.gameWindow.Draw(rec);
                }
            }
        }
        static void CheckTilesets()
        {
            numTilesets = 1;
            while(File.Exists(TILESET_PATH + numTilesets + FILE_EXT))
            {
                numTilesets += 1;
            }

            Array.Resize(ref TilesetSprite, numTilesets);

            for(int i = 1; i < numTilesets; i++)
            {
                TilesetSprite[i] = new Sprite(new Texture(TILESET_PATH + i + FILE_EXT));
            }
        }
    }
}
