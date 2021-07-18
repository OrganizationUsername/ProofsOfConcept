using System.ComponentModel;
using System.Runtime.CompilerServices;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    public class Rectangle : INotifyPropertyChanged, IShape
    {
        public Item Owner { get; set; }
        private double _y;
        private double _x;
        private double _height;
        private double _width;


        public double Width
        {
            get => _width;
            set { _width = value; OnPropertyChanged(); UpdateOwner(); }
        }

        public double Height
        {
            get => _height;
            set { _height = value; OnPropertyChanged(); UpdateOwner(); }

        }
        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); UpdateOwner(); }
        }

        /// <summary>
        /// Bottom of the rectangle
        /// </summary>
        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); UpdateOwner(); }
        }

        public double Diameter { get; set; }

        /// <summary>
        /// Propogate update to Item
        /// </summary>
        public void UpdateOwner()
        {
            if (Owner != null)
            {
                Owner.Ratio = Item.Calculate(Owner);
                PropertyChanged?.Invoke(Owner, new PropertyChangedEventArgs(nameof(Owner.Ratio)));
                Owner.Owner.DrawSomethingInVmBecauseIDontKnowBetter();
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