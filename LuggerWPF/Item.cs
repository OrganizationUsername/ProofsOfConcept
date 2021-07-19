using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using LuggerWPF.Annotations;

namespace LuggerWPF
{
    /// <summary>
    /// This actually represents "assembly", but that's not an appropriate name in programming.
    /// </summary>
    public class Item : INotifyPropertyChanged
    {
        public MainVm Owner { get; set; }
        private double _ratio;
        private string _name;
        private double _thickness;
        private double _demand;
        public ObservableCollection<IShape> Shapes { get; set; } = new ObservableCollection<IShape>();

        public bool Passes => Ratio <= 1.0 && Ratio >= 0;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public double Demand
        {
            get => _demand;
            set { _demand = value; OnPropertyChanged(); Ratio = Item.Calculate(this); }
        }

        public double Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value; OnPropertyChanged(); Ratio = Item.Calculate(this);
            }
        }

        public double Ratio
        {
            get => Math.Round(_ratio, 2, MidpointRounding.AwayFromZero);
            set { _ratio = value; OnPropertyChanged(); OnPropertyChanged(nameof(Passes)); }
        }

        /// <summary>
        /// This is the function that will set Ratio. It really should be invoked in another way.
        /// </summary>
        public static double Calculate(Item item)
        {
            if (item.Shapes.Count == 0) { return -1; }
            List<Rectangle> rectangles = new List<Rectangle>();
            List<Circle> circles = new List<Circle>();

            foreach (var shape in item.Shapes)
            {
                shape.Owner = item;
            }

            foreach (var shape in item.Shapes) //If this were .net 5+ and lang=latest, I could use a cool switch statement.
            {
                if (shape is Rectangle rectangle)
                {
                    rectangles.Add(rectangle);
                }
                else if (shape is Circle circle)
                {
                    circles.Add(circle);
                }
                else { throw new NotSupportedException("Fix this."); }
            }

            if (rectangles.Count != 1)
            {
                throw new NotSupportedException("Only one rectangle allowed atm.");
            }
            double minDistance = double.MaxValue;

            foreach (Rectangle rect in rectangles)
            {
                foreach (Circle cir in circles)
                {
                    double tempVar = GetDistance(cir, rect);
                    minDistance = Math.Min(minDistance, tempVar);
                }
            }

            for (var i = 0; i < circles.Count - 1; i++)
            {
                var circle = circles[i];

                for (var j = i + 1; j < circles.Count; j++)
                {
                    var secondaryCircle = circles[j];
                    double tempVar = Math.Min(minDistance, GetDistance(circle, secondaryCircle));
                    minDistance = Math.Min(minDistance, tempVar);
                }
                minDistance = Math.Min(minDistance, GetDistance(circle, rectangles.First()));
            }
            double arbitraryConstant = 60;
            double capacity = arbitraryConstant * item.Thickness * minDistance;
            return item.Demand / capacity;
        }

        public static double GetDistance(Circle circle1, IShape anyShape)
        {
            if (circle1 == null || anyShape == null) { throw new NullReferenceException("Only one rectangle allowed atm."); }
            switch (anyShape)
            {
                case Circle circle2:
                    return Math.Sqrt((circle2.X - circle1.X) * (circle2.X - circle1.X) +
                                     (circle2.Y - circle1.Y) * (circle2.Y - circle1.Y)) -
                           circle1.Diameter / 2.0 - circle2.Diameter / 2.0;
                case Rectangle rectangle when Item.CircleCenterOutsideRectangle(circle1, rectangle):
                    return -1;
                case Rectangle rectangle:
                    {
                        List<Point> points = new List<Point>
                    {
                        new Point(circle1.X - circle1.Diameter / 2.0, circle1.Y),
                        new Point(circle1.X + circle1.Diameter / 2.0, circle1.Y),
                        new Point(circle1.X, circle1.Y - circle1.Diameter / 2.0),
                        new Point(circle1.X, circle1.Y + circle1.Diameter / 2.0)
                    };
                        double minDistance = double.MaxValue;

                        foreach (var point in points)
                        {
                            double tempDistance = GetCircleToRectangleDistance(point, rectangle);
                            minDistance = Math.Min(minDistance, tempDistance);
                        }
                        return minDistance;
                    }
                default:
                    throw new NotSupportedException("Only one rectangle allowed atm.");
            }
        }

        public Rectangle GetRectangle()
        {
            return (Rectangle)Owner.SelectedItem.Shapes.FirstOrDefault(x => x.GetType() == typeof(Rectangle));
        }

        public static double GetCircleToRectangleDistance(Point point, Rectangle rectangle)
        {
            if (point.X < rectangle.X || point.X > rectangle.X + rectangle.Width ||
                point.Y < rectangle.Y || point.Y > rectangle.Y + rectangle.Height)
            {
                return -100;
            }

            List<double> doubles = new List<double>()
            {
                point.X - rectangle.X,
                rectangle.X + rectangle.Width-point.X,
                point.Y - rectangle.Y,
                rectangle.Y + rectangle.Height-point.Y
            };
            return doubles.Min();
        }

        public static bool CircleCenterOutsideRectangle(Circle circle, Rectangle rectangle)
        {
            //This just uses the center of the point. Why not use a point?
            return circle.X < rectangle.X || circle.X > rectangle.X + rectangle.Width ||
                    circle.Y < rectangle.Y || circle.Y > rectangle.Y + rectangle.Height;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}