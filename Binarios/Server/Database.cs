using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Bindings;

namespace Server
{
    class Database
    {
        public bool FileExist(string path)
        {
            return File.Exists(path);
        }

        //Player Stuff
        public bool PasswordCheck(int index, string username, string password)
        {
            string filename = "Data/Accounts/" + username + ".acc";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open);
            Types.Player[index] = (Types.PlayerStruct)binFormat.Deserialize(fs);
            fs.Close();

            if(Types.Player[index].Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AccountExist(string username)
        {
            string filename = "Data/Accounts/" + username + ".acc";

            if (FileExist(filename))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddAcount(int index, string name, string password)
        {
            ClearPlayer(index);
            Types.Player[index - 1].Login = name;
            Types.Player[index - 1].Password = password;
            Types.Player[index - 1].Name = name;

            SavePlayer(index);
        }
        
        public void ClearPlayer(int index)
        {
            Types.Player[index - 1].Login = "";
            Types.Player[index - 1].Password = "";
            Types.Player[index - 1].Name = "";
        }

        public void SavePlayer(int index)
        {
            string filename = "Data/Accounts/" + Types.Player[index - 1].Login + ".acc";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            binFormat.Serialize(fs, Types.Player[index - 1]);
            fs.Close();
        }

        public void LoadPlayer(int index, string name)
        {
            string filename = "Data/Accounts/" + name + ".acc";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open);
            Types.Player[index - 1] = (Types.PlayerStruct)binFormat.Deserialize(fs);
            fs.Close();
        }


        //Map Stuff
        public void CheckMaps()
        {
            string fileName;

            for(int i = 1; i < Constants.MAX_MAPS; i++)
            {
                fileName = "Data/Maps/" + i + ".map";
                if (!FileExist(fileName))
                {
                    ClearMap(i);
                    SaveMap(i);
                }
            }
        }

        private void ClearMap(int mapnum)
        {
            Types.Map[mapnum].Name = "";
        }

        public void SaveMap(int mapnum)
        {
            string filename = "Data/Maps/" + mapnum + ".map";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            binFormat.Serialize(fs, Types.Map[mapnum]);
            fs.Close();
        }

        public void LoadMaps()
        {
            string fileName;

            CheckMaps();

            for (int i = 1; i < Constants.MAX_MAPS; i++)
            {
                fileName = "Data/Maps/" + i + ".map";
                if (FileExist(fileName))
                {
                    BinaryFormatter binFormat = new BinaryFormatter();
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    Types.Map[i] = (Types.MapStruct)binFormat.Deserialize(fs);
                    fs.Close();
                }
            }

            Console.WriteLine("Maps Loaded");
        }
    }
}
