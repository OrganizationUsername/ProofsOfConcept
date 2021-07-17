using System.ComponentModel;
using System.Runtime.CompilerServices;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    public class Circle : INotifyPropertyChanged, IShape
    {
        private double _y;
        private double _x;
        private double _radius;
        public double Width { get; set; }
        public double Height { get; set; }


        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); }
        }

        public double Radius
        {
            get => _radius;
            set { _radius = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}