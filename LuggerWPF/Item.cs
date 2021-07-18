using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
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
            set { _thickness = value; OnPropertyChanged(); }
        }

        public double Percent
        {
            get => _percent;
            set { _percent = value; OnPropertyChanged(); OnPropertyChanged(nameof(Passes)); }
        }



        /// <summary>
        /// This is the function that will set Percent. It really should be invoked in another way.
        /// </summary>
        public static double Calculate(ref Item item)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            List<Circle> circles = new List<Circle>();

            foreach (var shape in item.Shapes) //If this were .net 5+ and lang=latest, I could use a cool switch statement.
            {
                if (shape is Rectangle rectangle)
                {
                    rectangles.Add(rectangle);
                    //TODO add these two types to a List<T> then do the math.
                }
                else if (shape is Circle circle)
                {
                    circles.Add(circle);
                }
                else
                {
                    throw new NotSupportedException("Fix this.");
                }
            }

            if (rectangles.Count != 1)
            {
                //It shouldn't be too hard to identify which circle is in which rectangle if there were multiple.
                throw new NotSupportedException("Only one rectangle allowed atm.");
            }


            //TODO: check all clear distances. Return (2* distance / thickness)

            double minDistance = 0;

            for (var i = 0; i < circles.Count; i++)
            {
                var circle = circles[i];

                for (var j = 0; j < circles.Count; j++)
                {
                    if (i == j) { continue; }
                    var secondaryCircle = circles[j];
                    //get clear distance for circle to circle
                }
            }
            return item.Thickness / (2.0 * minDistance);
        }

        public static double GetDistance(IShape circle, IShape anyShape)
        {

            if (circle == null || anyShape == null) { throw new NullReferenceException("Only one rectangle allowed atm."); }
            if (anyShape is Circle cicrcle)
            {
                //circle->Circle distance
                return (Math.Sqrt((cicrcle.X - circle.X) * (cicrcle.X - circle.X) +
                                  (cicrcle.Y - circle.Y) * (cicrcle.Y - circle.Y)) -
                                    circle.Diameter / 2.0 -
                                    cicrcle.Diameter / 2.0);
            }
            else if (anyShape is Rectangle rectangle)
            {
                //Do circle-> Edge distance

                //Alright, so I should actually create line segments first.
                //I'm still assuming that circle centers are inside the rectangle.



                return 0;
            }
            else
            {
                throw new NotSupportedException("Only one rectangle allowed atm.");
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