using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LuggerWPF.Converter
{
    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((bool)value)
            {
                return Brushes.Black;
            }
            else
            {
                return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isSomething = (string)value;

            return (isSomething == "thing is true.");
        }
    }
}