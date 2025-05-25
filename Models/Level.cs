using System;

namespace SlyTheRaccoon.Models
{
    public class Level
    {
        public CellType[,] Grid { get; }
        public int Width { get; }
        public int Height { get; }

        public Level(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new CellType[width, height];
        }

        /// <summary>
        /// Задаёт тип клетки
        /// </summary>
        public void PlaceObject(int x, int y, CellType type)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException("Invalid coordinates");

            Grid[x, y] = type;
        }

        /// <summary>
        /// Возвращает нахождение клетки в пределах сетки
        /// </summary>
        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}