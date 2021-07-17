using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    /// <summary>
    /// This actually represents "assembly"
    /// </summary>
    public class Item : INotifyPropertyChanged
    {
        //Let's have one rectangle which contains multiple circles
        private double _percent;
        private string _name;
        private double _thickness;
        public ObservableCollection<IShape> Shapes { get; set; } = new ObservableCollection<IShape>();

        public bool Passes
        {
            get { return this.Percent <= 1.0; }
        }


        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public double Thickness
        {
            get => _thickness;
            set => _thickness = value;
        }


        public double Percent
        {
            get => _percent;
            set { _percent = value; OnPropertyChanged(); OnPropertyChanged(nameof(Passes)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IShape
    {
        double Width { get; set; }
        double Height { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Radius { get; set; }
    }

    public class Rectangle : INotifyPropertyChanged, IShape
    {
        private double _y;
        private double _x;
        private double _height;
        private double _width;


        public double Width
        {
            get => _width;
            set { _width = value; OnPropertyChanged(); }
        }

        public double Height
        {
            get => _height;
            set { _height = value; OnPropertyChanged(); }

        }
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

        public double Radius { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

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