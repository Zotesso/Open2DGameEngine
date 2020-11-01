using System;
using System.Threading;

namespace Server
{
    class Program
    {
        private static Thread consoleThread;
        private static General general;

        static void Main(string[] args)
        {
            general = new General();
            consoleThread = new Thread(new ThreadStart(ConsoleThread));
            consoleThread.Start();
            general.InitializeServer();
        }

        static void ConsoleThread()
        {
          var cmnd =   Console.ReadLine();

            if (cmnd == "SetAccess")
            {
                Console.WriteLine("--------Nome do jogador-------");
                string playerToGetAccess = Console.ReadLine();
                Console.WriteLine("--------Tier do Acesso 0 = sem acesso, 10 = acesso maximo-------");
                byte accessToBeSeted = Convert.ToByte(Console.ReadLine());

                int playerIndex = GameLogic.GetPlayerIndexByName(playerToGetAccess);
                if (playerIndex > -1)
                {
                    GameLogic.SetPlayerAccess(playerIndex, accessToBeSeted);
                }
            }
            
            ConsoleThread();
        }
    }
}
