using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    public class MainVm : INotifyPropertyChanged
    {
        private Item _selectedItem;
        public IShape SelectedShape { get; set; }
        public Rectangle UnsavedRectangle { get; set; } = new Rectangle() { Height = 120, Width = 120, X = 0, Y = 0 };
        public Circle UnsavedCircle { get; set; } = new Circle() { Diameter = 50, X = 35, Y = 120 };
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public MainWindow MainWindow { get; set; } = null;

        public Item SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); DrawSomethingInVmBecauseIDontKnowBetter(); }
        }

        public MainVm()
        {
            Items.Add(new Item()
            {
                Name = "Big 1",
                Percent = 1.30,
                Thickness = 1.0,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 50, Y = 50, Height = 120, Width = 90},
                        new Circle() {Diameter = 20, X = 090, Y=100},
                        new Circle() {Diameter = 20, X = 090, Y=100},
                        new Circle() {Diameter = 30, X = 110, Y=100},
                        new Circle() {Diameter = 60, X = 130, Y=100},
                        new Circle() {Diameter = 70, X = 85, Y=100},
                    }),
            });
            Items.Add(new Item()
            {
                Name = "Big 2",
                Percent = 0.40,
                Thickness = 1.0,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 25, Y = 25, Height = 150, Width = 100},
                        new Circle() {Diameter = 20, X = 90, Y=20},
                        new Circle() {Diameter = 30, X = 110, Y=100},
                        new Circle() {Diameter = 60, X = 130, Y=100},
                        new Circle() {Diameter = 70, X = 85, Y=100},
                    }),
            });
            Items.Add(new Item()
            {
                Name = "Big 3",
                Percent = 0.50,
                Thickness = 1.0,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 10, Y = 10, Height = 90, Width = 150},
                        new Circle() {Diameter = 30, X = 90, Y=50},

                    }),
            });
            if (Items.Count > 0)
            {
                SelectedItem = Items.First();
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
                    Canvas.SetBottom(cir, circle.Y);
                    Canvas.SetLeft(cir, circle.X);
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