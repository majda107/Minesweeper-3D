using System;
namespace Test123
{
    public enum CellState { Hidden, Shown, Flagged, QuestionMark };
    public class Cell
    {
        public CellState state { get; set; }
        public bool isMine { get; set; }
        public int minesAround { get; set; }
        public bool renderForFirstTime { get; set; }

        public Cell()
        {
            this.renderForFirstTime = false;
            this.state = CellState.Hidden;
            this.isMine = false;
            this.minesAround = 0;
        }
    }
}
