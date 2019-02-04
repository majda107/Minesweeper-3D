using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test123;
using Minesweeper3D;

namespace Test123
{
    static class StaticMenuDatabase
    {
        static public MenuItem[] mainMenu = new MenuItem[5]
        {
            new MenuItem("Play", 21),
            new MenuItem("Help", 22),
            new MenuItem("Credits", 23),
            new MenuItem("Sound", 45),
            new MenuItem("Exit", 24),
        };

        static public MenuItem[] playMenu = new MenuItem[6]
        {
            new MenuItem("Easy", 25),
            new MenuItem("Medium", 26),
            new MenuItem("Hard", 27),
            new MenuItem("Insane", 28),
            new MenuItem("Custom", 29),
            new MenuItem("Random", 50),
        };

        static public MenuItem[] pauseMenu = new MenuItem[3]
        {
            new MenuItem("Resume", 30),
            new MenuItem("Main menu", 31),
            new MenuItem("Exit", 24)
        };

        static public MenuItem[] customMapMenu = new MenuItem[5]
        {
            new MenuItem("Width", 35, true),
            new MenuItem("Height", 36, true),
            new MenuItem("Mines", 37, true),
            new MenuItem("Seed", 38, true),
            new MenuItem("Done", 21),
        };
    }
}
