using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LuggerWPF.Annotations;

namespace LuggerWPF
{

    public class AddShapeCommand : ICommand
    {
        private readonly MainVm mainVm;

        public AddShapeCommand(MainVm MainVM)
        {
            mainVm = MainVM;
        }
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {

            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }


    public class MainVm : INotifyPropertyChanged
    {
        //Still need to add shapes
        //Excel
        //show shortest distance
        //show shortest distance lines?
        private Item _selectedItem;
        public IShape SelectedShape { get; set; }
        public Rectangle UnsavedRectangle { get; set; } = new Rectangle() { Height = 120, Width = 120, X = 0, Y = 0 };
        public Circle UnsavedCircle { get; set; } = new Circle() { Diameter = 50, X = 35, Y = 120 };
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public MainWindow MainWindow { get; set; } = null;
        public AddShapeCommand AddShapeCommand { get; set; }



        public Item SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); DrawSomethingInVmBecauseIDontKnowBetter(); }
        }

        public MainVm()
        {
            Items.Add(new Item()
            {
                Owner = this,
                Name = "Big 4",
                Ratio = 0.50,
                Thickness = 1.0,
                Demand = 500,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 0, Y = 0, Height = 90, Width = 150},
                        new Circle() {Diameter = 30, X = 20, Y = 20},

                    }),
            });
            Items.Add(new Item()
            {
                Owner = this,
                Name = "Big 2",
                Ratio = 0.50,
                Thickness = 1.0,
                Demand = 500,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 0, Y = 0, Height = 90, Width = 150},
                        new Circle() {Diameter = 25, X = 60, Y = 45},
                        new Circle() {Diameter = 25, X = 90, Y = 45},
                    }),
            });
            Items.Add(new Item()
            {
                Owner = this,
                Name = "Big 1",
                Thickness = 1.0,
                Demand = 500,
                Shapes = new ObservableCollection<IShape>(
                new List<IShape>()
                {
                        new Rectangle() {X = 0, Y = 0, Height = 90, Width = 150},
                        new Circle() {Diameter = 30, X = 50, Y = 50},

                }),
            });
            if (Items.Count > 0)
            {
                SelectedItem = Items.First();
            }

            foreach (var item in Items)
            {
                item.Ratio = Item.Calculate(item);
            }
        }

        public void AddView(MainWindow mw)
        {
            MainWindow = mw;
            DrawSomethingInVmBecauseIDontKnowBetter();
        }

        public void DrawSomethingInVmBecauseIDontKnowBetter()
        {
            if (MainWindow is null || SelectedItem is null) { return; }

            MainWindow.DirtyCanvas.Children.Clear();

            foreach (IShape shape in SelectedItem.Shapes)
            {
                if (shape is Circle circle)
                {
                    Ellipse cir = new Ellipse();
                    cir.Height = circle.Diameter;
                    cir.Width = circle.Diameter;
                    cir.StrokeThickness = 3;
                    cir.Stroke = Brushes.Blue;

                    MainWindow.DirtyCanvas.Children.Add(cir);
                    Canvas.SetBottom(cir, circle.Y - (circle.Diameter / 2.0));
                    Canvas.SetLeft(cir, circle.X - (circle.Diameter / 2.0));
                }
                else if (shape is Rectangle rectangle)
                {
                    var rec = new System.Windows.Shapes.Rectangle();
                    rec.Height = rectangle.Height;
                    rec.Width = rectangle.Width;
                    rec.StrokeThickness = 1;
                    rec.Stroke = Brushes.Red;

                    MainWindow.DirtyCanvas.Children.Add(rec);
                    Canvas.SetBottom(rec, rectangle.Y);
                    Canvas.SetLeft(rec, rectangle.X);
                }
                else
                {
                    throw new ArgumentException("Bad shape.");
                }
                //arcs
                //http://csharphelper.com/blog/2019/05/draw-an-elliptical-arc-in-wpf-and-xaml/
                //dimension line?
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