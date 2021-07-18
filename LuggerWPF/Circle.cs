using System.ComponentModel;
using System.Runtime.CompilerServices;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    public class Circle : INotifyPropertyChanged, IShape
    {
        public Item Owner { get; set; }
        private double _y;
        private double _x;
        private double _diameter;
        public double Width { get; set; }
        public double Height { get; set; }

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); UpdateOwner(); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); UpdateOwner(); }
        }

        public double Diameter
        {
            get => _diameter;
            set { _diameter = value; OnPropertyChanged(); UpdateOwner(); }
        }

        /// <summary>
        /// Propogate update to Item
        /// </summary>
        public void UpdateOwner()
        {
            if (Owner != null)
            {
                Owner.Ratio = Item.Calculate(Owner);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}