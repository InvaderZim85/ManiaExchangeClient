using System;
using System.Globalization;
using System.Windows.Data;

namespace ManiaExchangeClient.Ui.Converter
{
    public class ReplayTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int replayTime)
            {
                var timeSpan = TimeSpan.FromMilliseconds(replayTime);

                return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
            }

            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
