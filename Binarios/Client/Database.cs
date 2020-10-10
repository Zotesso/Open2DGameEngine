using Bindings;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Client
{
    class Database
    {
        public void SaveLocalMap(int mapnum)
        {
            string filename = "Data/Maps/" + (mapnum + 1) + ".map";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            binFormat.Serialize(fs, ClientTCP.Map[mapnum]);
            fs.Close();
        }
    }
}
