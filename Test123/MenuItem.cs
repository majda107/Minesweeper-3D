using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test123;
using Minesweeper3D;

namespace Test123
{
    class MenuItem
    {
        public string value { get; private set; }
        public int textureId { get; private set; }
        public bool editable { get; private set; }
        public Menu enterMenu { get; set; }
        public MenuItem(string value)
        {
            this.value = value;
            this.textureId = 0;
            this.editable = false;
            enterMenu = null;
        }
        public MenuItem(string value, int textureId)
        {
            enterMenu = null;
            this.value = value;
            this.textureId = textureId;
            this.editable = false;
        }

        public MenuItem(string value, int textureId, bool editable)
        {
            enterMenu = null;
            this.value = value;
            this.textureId = textureId;
            this.editable = editable;
        }
      
        public MenuItem(string value, int textureId, bool editable, Menu enterMenu)
        {
            enterMenu = this.enterMenu;
            this.value = value;
            this.textureId = textureId;
            this.editable = editable;
        }

        public void ChangeTexture(int newID)
        {
            textureId = newID;
        }
    }
}
