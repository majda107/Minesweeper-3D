using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test123;
using Minesweeper3D;

namespace Test123
{
    public enum GameState { InMenu, Helpscreen, InGame, Win, Lose, Credits };
    class GameData
    {
        public Map map { get; private set; }
        public Vector2D size { get; private set; }
        public Random rand;
        public int cellsLeft { get; private set; }
        public int minesSet { get; private set; }
        public int flags { get; private set; }
        public int seed { get; private set; }
        public Vector2D selected { get; set; }
        public System.Diagnostics.Stopwatch stopwatch { get; set; }
       
        private Stack<Vector2D> explosionStack;

        public GameData(int xMax, int yMax, int mines)
        {
            var seedRand = new Random(); this.seed = seedRand.Next(0, 10000);
            stopwatch = new System.Diagnostics.Stopwatch();

            this.size = new Vector2D(xMax, yMax);
            this.rand = new Random(this.seed);
            this.selected = new Vector2D(0, 0);

            this.minesSet = mines;
            this.cellsLeft = xMax * yMax;
            this.flags = 0;

            map = new Map(xMax, yMax);
            LoadData(mines);
            this.explosionStack = new Stack<Vector2D>();

            stopwatch.Start();
        }

        public GameData(int xMax, int yMax, int mines, int seed)
        {
            stopwatch = new System.Diagnostics.Stopwatch();
            this.size = new Vector2D(xMax, yMax);
            this.rand = new Random(seed); this.seed = seed;
            this.selected = new Vector2D(0, 0);

            this.minesSet = mines;
            this.cellsLeft = xMax * yMax;
            this.flags = 0;

            map = new Map(xMax, yMax);
            LoadData(mines);

            this.explosionStack = new Stack<Vector2D>();

            stopwatch.Start();
        }


        private void LoadData(int mines)
        {
            for(int i = 0; i < size.x; i++)
            {
                for(int j = 0; j < size.y; j++)
                {
                    map[i, j] = new Cell();
                }
            }

            for(int i = 0; i < mines; i++)
            {
                Vector2D minePos = new Vector2D(rand.Next(0, size.x), rand.Next(0, size.y));
                while(map[minePos].isMine)
                {
                    minePos = new Vector2D(rand.Next(0, size.x), rand.Next(0, size.y));
                }
                map[minePos].isMine = true;
            }
        }

        public void StackExplode(int x, int y)
        {
            explosionStack.Push(new Vector2D(x, y));
            while(explosionStack.Count > 0)
            {
                ProcessStack();
            }
        }

       

        public void ProcessStack()
        {
            Vector2D point = explosionStack.Pop();
            if (this.map[point.x, point.y].state == CellState.Hidden || this.map[point.x, point.y].state == CellState.QuestionMark)
            {
                cellsLeft--;
                int count = 0;

                int left = (point.x > 0) ? point.x - 1 : 0;
                int right = (point.x < this.size.x - 1) ? point.x + 1 : this.size.x - 1;
                int bottom = (point.y > 0) ? point.y - 1 : 0;
                int top = (point.y < this.size.y - 1) ? point.y + 1 : this.size.y - 1;

                for (int i = left; i <= right; i++)
                {
                    for (int j = bottom; j <= top; j++)
                    {
                        if (this.map[i, j].isMine) count++;
                    }
                }

                this.map[point.x, point.y].state = CellState.Shown;
                this.map[point.x, point.y].minesAround = count;

                if (count == 0)
                {

                    for (int i = left; i <= right; i++)
                    {
                        for (int j = bottom; j <= top; j++)
                        {
                            explosionStack.Push(new Vector2D(i, j));
                        }
                    }
                }
            }
        }

        public void Flag(int x, int y)
        {
            if (this.map[x, y].state == CellState.Hidden) { this.flags++; this.map[x, y].state = CellState.Flagged; }
            else if (this.map[x, y].state == CellState.Flagged) { this.map[x, y].state = CellState.QuestionMark; }
            else if (this.map[x, y].state == CellState.QuestionMark) { this.flags--; this.map[x, y].state = CellState.Hidden; }
        }
    }
}
