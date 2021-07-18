namespace LuggerWPF
{
    public class Point
    //It would be stupid if this was the base class of `Circle`, right?
    {
        public double X, Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}