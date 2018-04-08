using System;
using System.Globalization;
using Xamarin.Forms;

namespace TheMovie.Common.Converters
{
    public class StringDateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                return date.ToString("D", new CultureInfo("en-US"));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
