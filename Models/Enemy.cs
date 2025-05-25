namespace SlyTheRaccoon.Models
{
    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int StartX { get; }
        public int StartY { get; }
        public bool IsSmart { get; }
        public bool IsFast { get; }
        public bool HasDetectedPlayer { get; set; }

        public Enemy(int x, int y, bool isSmart = false, bool isFast = false)
        {
            X = x;
            Y = y;
            StartX = x;
            StartY = y;
            IsSmart = isSmart;
            IsFast = isFast;
            HasDetectedPlayer = false;
        }
    }
}