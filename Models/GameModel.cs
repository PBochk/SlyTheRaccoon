using System;
using System.Collections.Generic;

namespace SlyTheRaccoon.Models
{
    public class GameModel
    {
        public const int CellSize = 100;
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public Level CurrentLevel { get; private set; }
        public List<Enemy> Enemies { get; } = new List<Enemy>();
        public bool HasFood { get; set; } = false;

        public void LoadLevel(Level level)
        {
            CurrentLevel = level;
            HasFood = false;
            FindEntitiesPosition();
        }

        private void FindEntitiesPosition()
        {
            Enemies.Clear();
            for (int y = 0; y < CurrentLevel.Height; y++)
            {
                for (int x = 0; x < CurrentLevel.Width; x++)
                {
                    if (CurrentLevel.Grid[x, y] == CellType.Player)
                    {
                        PlayerX = x;
                        PlayerY = y;
                    }
                    else if (CurrentLevel.Grid[x, y] == CellType.Enemy)
                    {
                        Enemies.Add(new Enemy(x, y));
                    }
                    else if (CurrentLevel.Grid[x, y] == CellType.SmartEnemy)
                    {
                        Enemies.Add(new Enemy(x, y, isSmart: true));
                    }
                    else if (CurrentLevel.Grid[x, y] == CellType.FastEnemy)
                    {
                        Enemies.Add(new Enemy(x, y, isFast: true));
                    }

                }
            }
        }

        public bool CanMoveTo(int x, int y)
        {
            return x >= 0 && x < CurrentLevel.Width &&
                   y >= 0 && y < CurrentLevel.Height;
        }
    }
}