using System;
using System.Collections.Generic;

namespace SlyTheRaccoon.Models
{
    public class GameModel
    {
        public const int CellSize = 80;
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

        /// <summary>
        /// Добавляет координаты мгрока и врагов в модель
        /// </summary>
        private void FindEntitiesPosition()
        {
            Enemies.Clear();
            for (int y = 0; y < CurrentLevel.Height; y++)
            {
                for (int x = 0; x < CurrentLevel.Width; x++)
                {
                    switch (CurrentLevel.Grid[x, y])
                    {
                        case CellType.Player:
                            PlayerX = x;
                            PlayerY = y;
                            break;

                        case CellType.Enemy:
                            Enemies.Add(new Enemy(x, y));
                            break;

                        case CellType.SmartEnemy:
                            Enemies.Add(new Enemy(x, y, isSmart: true));
                            break;

                        case CellType.FastEnemy:
                            Enemies.Add(new Enemy(x, y, isFast: true));
                            break;
                    }

                }
            }
        }
    }
}