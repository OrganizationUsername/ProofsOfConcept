using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    public class MainVm : INotifyPropertyChanged
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();


        public Item SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public MainVm()
        {
            Items.Add(new Item() { Name = "Big 1", Percent = 0.30 });
            Items.Add(new Item()
            {
                Name = "Big 2",
                Percent = 0.40,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle()
                        {
                            X = 25,
                            Y = 25,
                            Height = 30,
                            Width = 30
                        }, new Circle()
                        {
                            Radius = 20,
                            X = 90,
                            Y=20
                        }
                    })
            });
            Items.Add(new Item() { Name = "Big 3", Percent = 0.50 });
            if (Items.Count != 0)
            {
                SelectedItem = Items.First();
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