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
            set => Setter(value, ref _x);
        }

        public double Y
        {
            get => _y;
            set => Setter(value, ref _y);
        }

        public double Diameter
        {
            get => _diameter;
            set => Setter(value, ref _diameter);
        }

        public void UpdateOwner()
        {
            if (Owner is null) return;

            Owner.Ratio = Item.Calculate(Owner);
            Owner.Owner.ThisIsBestPractice();
            Owner.Owner.RecalculateAllRatios();
        }

        public void Setter<T>(T newValue, ref T backingField)
        {
            if (newValue.Equals(backingField)) return;

            backingField = newValue;
            OnPropertyChanged();
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