using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SlyTheRaccoon.Models;

namespace SlyTheRaccoon.ViewModels
{
    public class CellViewModel : ObservableObject
    {
        private CellType _type;
        public CellType Type
        {
            get => _type;
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Sprite { get; }

        public int X { get; }
        public int Y { get; }

        public CellViewModel(CellType type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;

            switch (type)
            {
                case CellType.Player:
                    Sprite = LoadSprite("player.png");
                    break;
                case CellType.Wall:
                    Sprite = LoadSprite("wall.png");
                    break;
                case CellType.Exit:
                    Sprite = LoadSprite("exit.png");
                    break;
                case CellType.Enemy:
                    Sprite = LoadSprite("enemy.png");
                    break;
                case CellType.SmartEnemy:
                    Sprite = LoadSprite("smartEnemy.png");
                    break;
                                    
                case CellType.FastEnemy:
                    Sprite = LoadSprite("fastEnemy.png");
                    break;

                case CellType.Food:
                    Sprite = LoadSprite("food.png");
                    break;
                default:
                    Sprite = LoadSprite("empty.png");
                    break;
            }
        }

        private static ImageSource LoadSprite(string path)
        {
            var uri = new Uri($"pack://application:,,,/Assets/{path}");
            var image = new BitmapImage(uri);
            return image;
        }
    }
}