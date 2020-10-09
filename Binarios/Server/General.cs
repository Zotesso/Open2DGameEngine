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

        public void InitializeServer()
        {
            stcp = new ServerTCP();
            handleServerData = new ServerHandleData();

            handleServerData.InitializeMessages();

            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                ServerTCP.Clients[i] = new Client();
                Types.Player[i] = new Types.PlayerStruct();

            }

            stcp.InitializeNetwork();
            Console.WriteLine("Server has started");
        }
    }
}
