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
            Types.Player[index].Login = name;
            Types.Player[index].Password = password;

            SavePlayer(index);
        }
        
        public void ClearPlayer(int index)
        {
            Types.Player[index].Login = "";
            Types.Player[index].Password = "";
            Types.Player[index].Name = "";
        }

        public void SavePlayer(int index)
        {
            string filename = "Data/Accounts/" + Types.Player[index].Login + ".acc";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            binFormat.Serialize(fs, Types.Player[index]);
            fs.Close();
        }

        public void LoadPlayer(int index, string name)
        {
            string filename = "Data/Accounts/" + name + ".acc";
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open);
            Types.Player[index] = (Types.PlayerStruct)binFormat.Deserialize(fs);
            fs.Close();
        }
    }
}
