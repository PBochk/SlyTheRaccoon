namespace SlyTheRaccoon.Models
{
    public static class GameLevels
    {
        // Старт
        public static Level Level1 => LevelBuilder.BuildLevel(10, 10,
            (0, 5, CellType.Player),
            (5, 5, CellType.Food),
            (9, 5, CellType.Exit),

            (1, 2, CellType.Enemy),
            (8, 2, CellType.Enemy),
            (1, 8, CellType.Enemy),
            (8, 8, CellType.Enemy),

            (0, 4, CellType.Wall),
            (1, 4, CellType.Wall),
            (2, 4, CellType.Wall),
            (3, 4, CellType.Wall),
            (4, 4, CellType.Wall),
            (5, 4, CellType.Wall),
            (6, 4, CellType.Wall),
            (7, 4, CellType.Wall),
            (8, 4, CellType.Wall),
            (9, 4, CellType.Wall),

            (0, 6, CellType.Wall),
            (1, 6, CellType.Wall),
            (2, 6, CellType.Wall),
            (3, 6, CellType.Wall),
            (4, 6, CellType.Wall),
            (5, 6, CellType.Wall),
            (6, 6, CellType.Wall),
            (7, 6, CellType.Wall),
            (8, 6, CellType.Wall),
            (9, 6, CellType.Wall)
        );

        // Маленькие враги
        public static Level Level2 => LevelBuilder.BuildLevel(10, 10,
            (1, 3, CellType.Player),
            (9, 6, CellType.Food),
            (6, 5, CellType.Exit),

            (4, 0, CellType.Enemy),
            (8, 1, CellType.Enemy),
            (0, 6, CellType.Enemy),
            (8, 9, CellType.Enemy),

            (3, 0, CellType.Wall),
            (3, 1, CellType.Wall),
            (3, 2, CellType.Wall),
            (3, 3, CellType.Wall),

            (5, 0, CellType.Wall),
            (5, 1, CellType.Wall),
            (5, 2, CellType.Wall),
            (5, 3, CellType.Wall),
            (5, 4, CellType.Wall),
            (5, 5, CellType.Wall),
            (5, 7, CellType.Wall),
            (5, 8, CellType.Wall),
            (5, 9, CellType.Wall),

            (0, 5, CellType.Wall),
            (1, 5, CellType.Wall),
            (2, 5, CellType.Wall),
            (3, 5, CellType.Wall),

            (0, 7, CellType.Wall),
            (1, 7, CellType.Wall),
            (2, 7, CellType.Wall),
            (3, 7, CellType.Wall),
            (4, 7, CellType.Wall),
            (5, 7, CellType.Wall)
        );

        // Появление быстрых врагов
        public static Level Level3 => LevelBuilder.BuildLevel(10, 10,
            (4, 7, CellType.Player),
            (8, 3, CellType.Food),
            (5, 5, CellType.Exit),

            (1, 0, CellType.FastEnemy),
            (9, 8, CellType.FastEnemy),
            (9, 1, CellType.Enemy),
            (7, 6, CellType.Enemy),


            (2, 0, CellType.Wall),
            (2, 1, CellType.Wall),
            (2, 2, CellType.Wall),
            (2, 3, CellType.Wall),
            (2, 8, CellType.Wall),

            (3, 6, CellType.Wall),
            (3, 7, CellType.Wall),

            (4, 2, CellType.Wall),
            (4, 3, CellType.Wall),
            (4, 4, CellType.Wall),
            (4, 5, CellType.Wall),
            (4, 6, CellType.Wall),

            (5, 6, CellType.Wall),
            (5, 7, CellType.Wall),
            (5, 9, CellType.Wall),
            (6, 2, CellType.Wall),

            (6, 7, CellType.Wall),
            (7, 7, CellType.Wall),
            (8, 7, CellType.Wall),
            (9, 7, CellType.Wall),

            (6, 9, CellType.Wall),
            (7, 9, CellType.Wall),
            (8, 9, CellType.Wall),
            (9, 9, CellType.Wall),

            (0, 6, CellType.Wall)
        );

        //Ступеньки - быстрые и маленькие
        public static Level Level4 => LevelBuilder.BuildLevel(10, 10,
            (9, 0, CellType.Player),

            (8, 4, CellType.Food),
            (8, 3, CellType.Exit),

            (5, 9, CellType.Enemy),
            (8, 9, CellType.FastEnemy),
            (1, 1, CellType.FastEnemy),
            (9, 6, CellType.Enemy),

            (1, 0, CellType.Wall),
            (2, 0, CellType.Wall),
            (3, 0, CellType.Wall),
            (4, 0, CellType.Wall),
            (8, 0, CellType.Wall),

            (0, 2, CellType.Wall),
            (2, 2, CellType.Wall),
            (3, 2, CellType.Wall),
            (6, 2, CellType.Wall),

            (0, 3, CellType.Wall),
            (5, 3, CellType.Wall),
            (6, 3, CellType.Wall),
            (7, 3, CellType.Wall),
            (9, 3, CellType.Wall),

            (0, 4, CellType.Wall),
            (4, 4, CellType.Wall),
            (3, 5, CellType.Wall),
            (6, 5, CellType.Wall),
            (2, 6, CellType.Wall),
            (4, 7, CellType.Wall),

            (0, 8, CellType.Wall),
            (2, 8, CellType.Wall),
            (3, 8, CellType.Wall),

            (0, 0, CellType.Wall)
        );


        // Большая спираль - появление умного врага
        public static Level Level5 => LevelBuilder.BuildLevel(10, 10,
            (8, 8, CellType.Player),

            (7, 2, CellType.Food),

            (2, 7, CellType.Exit),

            (6, 7, CellType.SmartEnemy),

            (1, 0, CellType.Wall),
            (1, 1, CellType.Wall),
            (1, 2, CellType.Wall),
            (1, 3, CellType.Wall),
            (1, 4, CellType.Wall),
            (1, 5, CellType.Wall),
            (1, 6, CellType.Wall),
            (1, 7, CellType.Wall),
            (1, 8, CellType.Wall),

            (3, 4, CellType.Wall),

            (4, 1, CellType.Wall),
            (4, 2, CellType.Wall),
            (4, 3, CellType.Wall),
            (4, 4, CellType.Wall),

            (6, 3, CellType.Wall),
            (6, 4, CellType.Wall),

            (8, 1, CellType.Wall),
            (8, 2, CellType.Wall),
            (8, 3, CellType.Wall),
            (8, 4, CellType.Wall),
            (8, 5, CellType.Wall),
            (8, 6, CellType.Wall),

            (5, 1, CellType.Wall),
            (6, 1, CellType.Wall),
            (7, 1, CellType.Wall),

            (3, 6, CellType.Wall),
            (4, 6, CellType.Wall),
            (5, 6, CellType.Wall),
            (6, 6, CellType.Wall),
            (7, 6, CellType.Wall),

            (2, 8, CellType.Wall),
            (3, 8, CellType.Wall),
            (4, 8, CellType.Wall),
            (5, 8, CellType.Wall),
            (6, 8, CellType.Wall),
            (7, 8, CellType.Wall),


            (0, 0, CellType.Wall)
        );

        // Roundabout
        public static Level Level6 => LevelBuilder.BuildLevel(10, 10,
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
            (0, 7, CellType.FastEnemy),

            (5, 2, CellType.Food),
            (8, 8, CellType.Exit)
        );

        //Fast and Furious
        public static Level Level7 => LevelBuilder.BuildLevel(10, 10,
            (1, 7, CellType.Player),

            (6, 3, CellType.Food),

            (8, 8, CellType.Exit),

            (0, 0, CellType.FastEnemy),
            (2, 2, CellType.FastEnemy),
            (3, 0, CellType.FastEnemy),
            (5, 5, CellType.FastEnemy),
            (5, 8, CellType.FastEnemy),
            (7, 9, CellType.FastEnemy),
            (8, 0, CellType.FastEnemy),

            (1, 2, CellType.Wall),
            (1, 3, CellType.Wall),
            (1, 4, CellType.Wall),
            (1, 5, CellType.Wall),
            (1, 6, CellType.Wall),
            (1, 8, CellType.Wall),

            (2, 3, CellType.Wall),
            (2, 7, CellType.Wall),

            (4, 5, CellType.Wall),
            (4, 8, CellType.Wall),
            (4, 9, CellType.Wall),

            (5, 4, CellType.Wall),
            (5, 6, CellType.Wall),
            (5, 7, CellType.Wall),

            (5, 0, CellType.Wall)
        );

        //Погоня
        public static Level Level8 => LevelBuilder.BuildLevel(10, 10,
            (2, 2, CellType.Player),

            (8, 6, CellType.Food),

            (8, 9, CellType.Exit),

            (0, 1, CellType.FastEnemy),
            (0, 5, CellType.Enemy),
            (0, 7, CellType.SmartEnemy),
            (0, 9, CellType.FastEnemy),

            (0, 2, CellType.SmartEnemy),

            (5, 0, CellType.Wall),
            (7, 2, CellType.Wall),

            (0, 3, CellType.Wall),
            (1, 3, CellType.Wall),
            (2, 3, CellType.Wall),
            (4, 3, CellType.Wall),
            (6, 3, CellType.Wall),
            (7, 3, CellType.Wall),

            (2, 4, CellType.Wall),
            (6, 4, CellType.Wall),

            (5, 5, CellType.Wall),
            (6, 5, CellType.Wall),
            (8, 5, CellType.Wall),

            (2, 6, CellType.Wall),
            (4, 6, CellType.Wall),
            (5, 6, CellType.Wall),
            (6, 6, CellType.Wall),

            (5, 7, CellType.Wall),
            (6, 7, CellType.Wall),
            (7, 7, CellType.Wall),
            (8, 7, CellType.Wall),
            (9, 7, CellType.Wall),
            (2, 8, CellType.Wall),
            (3, 8, CellType.Wall),
            (7, 8, CellType.Wall)
        );

        //Последний уровень
        public static Level Level9 => LevelBuilder.BuildLevel(10, 10,
            (3, 6, CellType.Player),

            (1, 1, CellType.Food),

            (9, 9, CellType.Exit),

            (0, 4, CellType.Enemy),
            (0, 9, CellType.Enemy),
            (9, 7, CellType.Enemy),
            (0, 8, CellType.FastEnemy),
            (1, 7, CellType.FastEnemy),
            (9, 1, CellType.FastEnemy),
            (7, 4, CellType.FastEnemy),
            (9, 0, CellType.SmartEnemy),

            (0, 2, CellType.SmartEnemy),

            (0, 2, CellType.Wall),

            (2, 1, CellType.Wall),
            (2, 3, CellType.Wall),
            (2, 5, CellType.Wall),
            (2, 6, CellType.Wall),
            (2, 7, CellType.Wall),

            (4, 2, CellType.Wall),
            (4, 3, CellType.Wall),
            (4, 5, CellType.Wall),
            (4, 6, CellType.Wall),

            (6, 3, CellType.Wall),
            (7, 3, CellType.Wall),
            (8, 3, CellType.Wall),
            (9, 3, CellType.Wall),

            (5, 5, CellType.Wall),
            (6, 5, CellType.Wall),
            (7, 5, CellType.Wall),
            (8, 5, CellType.Wall),
            (9, 5, CellType.Wall),

            (8, 8, CellType.Wall),
            (9, 8, CellType.Wall)
        );
    }
}