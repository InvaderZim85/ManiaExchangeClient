using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ManiaExchangeClient.Ui.Converter
{
    public class ContrastColorConverter : IValueConverter
    {
        private object _defaultValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _defaultValue = value;

            if (!(value is SolidColorBrush color))
                return new SolidColorBrush(Color.FromRgb(0, 0, 0));

            var a = 1 - (0.299 * color.Color.R + 0.587 * color.Color.G + 0.114 * color.Color.B) / 255;

            var d = a < 0.5 ? (byte)0 : (byte)255;

            return new SolidColorBrush(Color.FromRgb(d, d, d));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _defaultValue;
        }
    }
}
