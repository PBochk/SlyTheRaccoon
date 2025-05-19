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

        public void PlaceObject(int x, int y, CellType type)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException("Invalid coordinates");

            Grid[x, y] = type;
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsWalkable(int x, int y)
        {
            return IsValidPosition(x, y) && Grid[x, y] != CellType.Wall;
        }

        public (int x, int y)? FindFirst(CellType type)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (Grid[x, y] == type)
                        return (x, y);
            return null;
        }
    }
}