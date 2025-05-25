using SlyTheRaccoon.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SlyTheRaccoon.Models.Services
{
    public static class MovementService
    {
        /// <summary>
        /// Возвращает направление движения по нажатой клавише
        /// </summary>
        public static (int dx, int dy) GetDirection(Key key)
        {
            switch (key)
            {
                case Key.Left: return (-1, 0);
                case Key.Right: return (1, 0);
                case Key.Up: return (0, -1);
                case Key.Down: return (0, 1);
                default: return (0, 0);
            }
        }

        /// <summary>
        /// Возвращает направление к цели (sign-вектор)
        /// </summary>
        public static (int dx, int dy) GetDirectionTowardsTarget(int fromX, int fromY, int targetX, int targetY)
        {
            return (Math.Sign(targetX - fromX), Math.Sign(targetY - fromY));
        }

        /// <summary>
        /// Проверяет, можно ли переместиться в указанные координаты
        /// </summary>
        public static bool CanMoveTo(Level level, int x, int y)
        {
            return IsValidCoordinate(level, x, y) 
                    && level.Grid[x, y] != CellType.Wall;
        }

        /// <summary>
        /// Проверяет валидность координат
        /// </summary>
        public static bool IsValidCoordinate(Level level, int x, int y)
        {
            return x >= 0 && x < level.Width &&
                   y >= 0 && y < level.Height;
        }

        /// <summary>
        /// Проверяет наличие прямой видимости между двумя точками
        /// </summary>
        public static bool HasLineOfSight(Level level, int fromX, int fromY, int targetX, int targetY)
        {
            if (!IsValidCoordinate(level, fromX, fromY) ||
                !IsValidCoordinate(level, targetX, targetY))
                return false;

            if (fromX != targetX && fromY != targetY)
                return false;

            var (dx, dy) = GetDirectionTowardsTarget(fromX, fromY, targetX, targetY);

            int x = fromX + dx;
            int y = fromY + dy;

            while (x != targetX || y != targetY)
            {
                if (!IsValidCoordinate(level, x, y))
                    return false;

                if (level.Grid[x, y] == CellType.Wall)
                    return false;

                x += dx;
                y += dy;
            }

            return true;
        }

        /// <summary>
        /// Находит путь между двумя точками (алгоритм BFS)
        /// </summary>
        public static List<(int x, int y)> FindPath(Level level, int startX, int startY, int targetX, int targetY)
        {
            var grid = level.Grid;
            if (!IsValidCoordinate(level, startX, startY) ||
                !IsValidCoordinate(level, targetX, targetY))
                return new List<(int x, int y)>();

            if (startX == targetX && startY == targetY)
                return new List<(int x, int y)> { (startX, startY) };

            var queue = new Queue<(int x, int y)>();
            var visited = new Dictionary<(int x, int y), (int x, int y)>();
            var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

            queue.Enqueue((startX, startY));
            visited[(startX, startY)] = (-1, -1);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var dir in directions)
                {
                    int nx = current.x + dir.dx;
                    int ny = current.y + dir.dy;

                    if (nx == targetX && ny == targetY)
                    {
                        var path = new List<(int x, int y)> { (targetX, targetY) };
                        var temp = current;

                        while (temp.x != startX || temp.y != startY)
                        {
                            path.Add(temp);
                            temp = visited[temp];
                        }
                        path.Reverse();
                        return path;
                    }

                    if (IsValidCoordinate(level, nx, ny) &&
                        grid[nx, ny] != CellType.Wall &&
                        !visited.ContainsKey((nx, ny)))
                    {
                        visited[(nx, ny)] = current;
                        queue.Enqueue((nx, ny));
                    }
                }
            }

            return new List<(int x, int y)>();
        }

        /// <summary>
        /// Вычисляет манхэттенское расстояние между точками
        /// </summary>
        public static int ManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}