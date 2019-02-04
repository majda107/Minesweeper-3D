using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test123
{
    class Map
    {
        private Cell[,] map;
        public Map(int xMax, int yMax)
        {
            map = new Cell[xMax, yMax];
        }

        public Cell this[int x, int y]
        {
            get { return map[x, y]; }
            set { map[x, y] = value; }
        }

        public Cell this[Vector2D vc]
        {
            get { return map[vc.x, vc.y]; }
            set { map[vc.x, vc.y] = value; }
        }
    }
}
