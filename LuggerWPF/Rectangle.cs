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
        private int _id = -1;
        public int Id
        {
            get => _id;
            set => Setter(value, ref _id);
        }

        public double Width
        {
            get => _width;
            set => Setter(value, ref _width);
        }

        public double Height
        {
            get => _height;
            set => Setter(value, ref _height);

        }
        public double X
        {
            get => _x;
            set => Setter(value, ref _x);
        }

        /// <summary>
        /// Bottom of the rectangle
        /// </summary>
        public double Y
        {
            get => _y;
            set => Setter(value, ref _y);
        }

        public double Diameter { get; set; }

        /// <summary>
        /// Propogate update to Item
        /// </summary>
        public void UpdateOwner()
        {
            if (Owner is null) return;

            Owner.Ratio = Item.Calculate(Owner);
            Owner.Owner.ThisIsBestPractice();
            Owner.Owner.RecalculateAllRatios();

        }

        public void Setter<T>(T newValue, ref T backingField)
        {
            if (!newValue.Equals(backingField))
            {
                backingField = newValue;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateOwner();
        }
    }
}