using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Client
{
    class Program
    {
        public static RenderWindow gameWindow;
        static void Main(string[] args)
        {
            gameWindow = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Game Engine");
            Game();
        }

        static void Game()
        {
            while (gameWindow.IsOpen)
            {
                gameWindow.DispatchEvents();
                gameWindow.Clear(Color.Red);

                gameWindow.Display();
            }
        }
    }
}
