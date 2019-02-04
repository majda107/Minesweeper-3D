using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test123;

namespace Minesweeper3D
{
    class Menu
    {
        public MenuItem[] menuItems { get; private set; }
        public Menu(MenuItem[] menuItems)
        {
            this.menuItems = menuItems;
            this.superiorMenu = null;
            this.Length = menuItems.Length;
        }
        public int Length;
        public Menu superiorMenu { get; set; }
        public MenuItem this[int index]
        {
            get { return menuItems[index]; }
            set { menuItems[index] = value; }
        }
    }
}
