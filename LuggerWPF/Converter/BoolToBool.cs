using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LuggerWPF.Converter
{
    public class BoolToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isSomething = (string)value;

            return (isSomething == "thing is true.");
        }
    }
}