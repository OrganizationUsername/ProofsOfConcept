using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

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
}
