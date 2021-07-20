namespace LuggerWPF
{
    public interface IShape
    {
        Item Owner { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Diameter { get; set; }
        int Id { get; set; }
    }
}