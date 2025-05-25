using System;
using System.Globalization;
using System.Windows.Data;
using SlyTheRaccoon.Models;

namespace SlyTheRaccoon.Converters
{
    public class PositionToCanvasConverter : IValueConverter
    {
        /// <summary>
        /// Перевод из координат сетки в координаты на экране 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int pos)
            {
                return pos * GameModel.CellSize;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}