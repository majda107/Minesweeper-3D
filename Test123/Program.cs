using System;
using OpenTK;

namespace Test123
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            GameData gameData = new GameData(10, 5, 5);
            GameWindow gameWindow = new GameWindow(700, 500, OpenTK.Graphics.GraphicsMode.Default, "Minesweeper 3D");

            Game game = new Game(gameWindow, gameData);
        }
    }
}
