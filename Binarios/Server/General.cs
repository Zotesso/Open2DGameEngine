using System;
using System.Collections.Generic;
using System.Text;
using Bindings;

namespace Server
{
    class General
    {
        private ServerTCP stcp;

        public void InitializeServer()
        {
            stcp = new ServerTCP();
            for(int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                ServerTCP.Clients[i] = new Client();

            }

            stcp.InitializeNetwork();
            Console.WriteLine("Server has started");
        }
    }
}
