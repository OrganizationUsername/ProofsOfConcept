using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LuggerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVm MainVm { get; set; } = new MainVm();
        public MainWindow()
        {
            InitializeComponent();
            MainVm.AddView(this);
            DataContext = MainVm;
        }




    }


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
