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
        private double _percent;
        private string _name;
        private double _thickness;
        public ObservableCollection<IShape> Shapes { get; set; } = new ObservableCollection<IShape>();

        public bool Passes => this.Percent <= 1.0;


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
}