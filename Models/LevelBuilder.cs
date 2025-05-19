namespace SlyTheRaccoon.Models
{
    public static class LevelBuilder
    {
        public static Level BuildLevel(int width, int height, params (int x, int y, CellType type)[] objects)
        {
            var level = new Level(width, height);

            foreach (var obj in objects)
            {
                level.PlaceObject(obj.x, obj.y, obj.type);
            }

            return level;
        }
    }
}