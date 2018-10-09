using System;
using System.Globalization;
using System.Windows.Data;

namespace ClassListWPF
{
    internal class DateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "White";
            }

            var date = (DateTime)value;
            return date.AddYears(18) < DateTime.Today ? "lightgreen" : "red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}