using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class Map
    {
        public const int MAX_MAPS = 100;
        public const string MAP_PATH = "Data/Maps/";
        public const string MAP_EXT = ".map";

        public static MapStruct[] Maps = new MapStruct[MAX_MAPS];

        [Serializable]
        public struct TileDataStruct {
            public byte X;
            public byte Y;
            public byte Tileset;
            public byte AutoTile;
        }

        [Serializable]
        public struct TileStruct{
            public TileDataStruct[] layer;
            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public byte DirBlock;
        }

        [Serializable]
        public struct MapStruct {
            public string Name;
            public byte MaxX;
            public byte MaxY;

            public TileStruct[,] Tile;
        }

        [Serializable]
        public enum LayerType{
            Ground = 1,
            Mask,
            Mask2,
            Fringe,
            Fringe2,
            Count
        }

        public static void CheckMaps()
        {
            for(int i = 0; i < MAX_MAPS; i++)
            {
                if(!File.Exists(MAP_PATH + "map" + i + MAP_EXT))
                {
                    ClearMap(i);
                    SaveMap(i);
                }
            }
        }

        public static void ClearMap(int mapnum)
        {
            int layerX = 0;
            int layerY = 0;

            Maps[mapnum].Name = "None";
            Maps[mapnum].MaxX = 50;
            Maps[mapnum].MaxY = 50;
            Maps[mapnum].Tile = new TileStruct[(Maps[mapnum].MaxX), (Maps[mapnum].MaxY)];

            var mapX = Maps[mapnum].Tile.GetLength(0);
            var mapY = Maps[mapnum].Tile.GetLength(1);

            for (int x = 0; x < Maps[mapnum].MaxX; x++)
            {
                mapX = x;
            }

            for (int y = 0; y < Maps[mapnum].MaxY; y++)
            {
                mapY = y;
            }

            for (layerX = 0; layerX < Maps[mapnum].Tile.GetLength(0); layerX++)
            {
                for (layerY = 0; layerY < Maps[mapnum].Tile.GetLength(1); layerY++)
                {
                    Maps[mapnum].Tile[layerX, layerY].layer = new TileDataStruct[(int)LayerType.Count - 1];
                    Maps[mapnum].Tile[layerX, layerY].layer[1].Tileset = 2;
                }
            }
        }

        public static void SaveMap(int mapnum)
        {
            BinaryFormatter binformat = new BinaryFormatter();

            FileStream fileSt = File.Open(MAP_PATH + "map" + mapnum + MAP_EXT, FileMode.OpenOrCreate);
            binformat.Serialize(fileSt, Maps[mapnum]);
            fileSt.Close();
        }

        public static void LoadMap(int mapnum)
        {
            BinaryFormatter binformat = new BinaryFormatter();

            FileStream fileSt = File.Open(MAP_PATH + "map" + mapnum + MAP_EXT, FileMode.OpenOrCreate);
            Maps[mapnum] = (MapStruct)binformat.Deserialize(fileSt);
            fileSt.Close();
        }

        public static void LoadMaps()
        {
            for(int i = 0; i < MAX_MAPS; i++)
            {
                LoadMap(i);
            }
        }
    }
}
