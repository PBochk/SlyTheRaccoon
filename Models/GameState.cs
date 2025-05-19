using System.Collections.Generic;

namespace SlyTheRaccoon.Models
{
    public class GameState
    {
        public Level CurrentLevel { get; set; }
        public Player Player { get; } = new Player();
        public List<Enemy> Enemies { get; } = new List<Enemy>();
        public bool HasFood { get; set; }
        public bool IsGameOver { get; set; }
        public int CurrentLevelIndex { get; set; }
    }

    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Lives { get; set; } = 3;
    }
}