using System;
using System.Collections.Generic;
using System.Text;
using Bindings;

namespace Server
{
    class General
    {
        private ServerTCP stcp;
        private ServerHandleData handleServerData;
        private Database db;
        public void InitializeServer()
        {
            stcp = new ServerTCP();
            handleServerData = new ServerHandleData();
            db = new Database();

            handleServerData.InitializeMessages();

            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                ServerTCP.Clients[i] = new Client();
                Types.Player[i] = new Types.PlayerStruct();
            }

            for (int i = 1; i <Constants.MAX_MAPS; i++)
            {
                Types.Map[i] = new Types.MapStruct();
            }

            db.LoadMaps();
            db.LoadNpcs();

            stcp.InitializeNetwork();
            Console.WriteLine("Server has started");
        }
    }
}
