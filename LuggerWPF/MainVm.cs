using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Commands.LuggerWPF;
using LuggerWPF.Annotations;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;

//TODO: Round thicknesses
//TODO: Save to Excel
//TODO: Read from Excel
//TODO: Figure out how to immediately renumber circles after one is deleted.
//TODO: Label circles in Canvas
//TODO: Validate circles on move
//TODO: Undo/Redo
//TODO: Parameterize unit tests
//"The object mother" //https://stackoverflow.com/questions/923319/what-is-an-objectmother
//Immediate mode vs Retain mode (hold state and update other guys = UI)

namespace LuggerWPF
{
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
        public AddAssemblyCommand AddAssemblyCommand { get; set; }
        public OpenExcelCommand OpenExcelCommand { get; set; }
        public SaveExcelCommand SaveExcelCommand { get; set; }
        public FixCommand FixCommand { get; set; }
        public DeleteShapeCommand DeleteShapeCommand { get; set; }
        private SKElement a = new SKElement();

        public Item SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); ThisIsBestPractice(); }
        }

        public MainVm()
        {
            AddAssemblyCommand = new AddAssemblyCommand(this);
            AddShapeCommand = new AddShapeCommand(this);
            OpenExcelCommand = new OpenExcelCommand(this);
            SaveExcelCommand = new SaveExcelCommand(this);
            FixCommand = new FixCommand(this);
            DeleteShapeCommand = new DeleteShapeCommand(this);


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
            RecalculateAllRatios();
        }

        public void RecalculateAllRatios()
        {
            foreach (var item in Items)
            {
                item.Ratio = Item.Calculate(item);
            }
        }

        public void AddView(MainWindow mw)
        {
            MainWindow = mw;
            mw.Skia.PaintSurface += OnSkiaOnPaintSurface; //To fix this: https://www.codeproject.com/Articles/5247780/Xamarin-SKIASharp-Guide-to-MVVM
            ThisIsBestPractice();
        }

        private void OnSkiaOnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (this.MainWindow is null)
            {
                return;
            }
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var scale = (float)PresentationSource.FromVisual(this.MainWindow).CompositionTarget.TransformToDevice.M11;
            var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas is blank
            canvas.Clear(SKColors.White);

            // draw some text
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };
            var coord = new SKPoint();
            coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
            canvas.DrawText("SkiaSharp", coord, paint);
            canvas.Save();
            canvas.RotateDegrees(45, scaledSize.Width / 2, scaledSize.Height / 2);
            coord = new SKPoint(scaledSize.Width / 2 + 0, (scaledSize.Height + paint.TextSize) / 2 + 0);
            canvas.DrawText("SkiaSharp2SkiaSharp2SkiaSharp2", coord, paint);
            canvas.Restore();
            canvas.DrawLine(0, 0, scaledSize.Width / 2, scaledSize.Height / 2, paint);

            float outerRadis = 10f;


            //var path = new SKPath();
            //path.AddCircle(scaledSize.Width / 2, scaledSize.Height / 2, scaledSize.Width / 2 - 1f);
            //canvas.ClipPath(path);
            paint.StrokeWidth = 1f;
            paint.IsStroke = true;
            canvas.DrawCircle(coord, outerRadis, paint);


            paint = new SKPaint() { Color = SKColors.Transparent };
            canvas.DrawCircle(coord, outerRadis - 1f, paint);
            //canvas.Save();

        }

        public void ThisIsBestPractice()
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
                    cir.StrokeThickness = 1;
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
                    rec.StrokeThickness = 2;
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