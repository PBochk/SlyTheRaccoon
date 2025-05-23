namespace SlyTheRaccoon.Models
{
    public static class GameLevels
    {
        public static Level Level1 => LevelBuilder.BuildLevel(10, 10,
            (0, 0, CellType.Player),
            (0, 1, CellType.Wall),
            (1, 1, CellType.Wall),
            (2, 1, CellType.Wall),

            (3, 3, CellType.Wall),
            (4, 3, CellType.Wall),
            (5, 3, CellType.Wall),

            (3, 4, CellType.Wall),
            (5, 4, CellType.Wall),

            (3, 5, CellType.Wall),
            (4, 5, CellType.Wall),
            (5, 5, CellType.Wall),

            (6, 6, CellType.SmartEnemy),
            (0, 7, CellType.Enemy),

            (5, 2, CellType.Food),
            (8, 8, CellType.Exit)
        );
        public static Level Level2 => LevelBuilder.BuildLevel(10, 10,
            (2, 2, CellType.Player),
            (3, 5, CellType.Food),
            (8, 8, CellType.Exit),
            (4, 1, CellType.Wall),
            (4, 2, CellType.Wall),
            (7, 5, CellType.Enemy)
        );

        public static Level Level3 => LevelBuilder.BuildLevel(10, 10,
            (1, 1, CellType.Player),
            (2, 2, CellType.Food),
            (6, 6, CellType.Exit),
            (3, 3, CellType.Wall),
            (3, 4, CellType.Wall),
            (4, 3, CellType.Wall),
            (1, 5, CellType.Wall),
            (5, 1, CellType.Wall),
            (6, 2, CellType.Wall)
        );

    }
}