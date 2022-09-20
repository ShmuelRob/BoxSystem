using ManageSystem;

namespace Models
{
    public struct Box : IContainable<Box>
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Box(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public int CompareTo(Box other) => Width == other.Width ? Height.CompareTo(other.Height) : Width.CompareTo(other.Width);

        public double Volume => Width * Width * Height;
        public int Contain(Box other)
        {
            if (Width.CompareTo(other.Width) >= 0 && Height.CompareTo(other.Height) >= 0) return 1;
            if (Width.CompareTo(other.Width) <= 0 && Height.CompareTo(other.Height) <= 0) return -1;
            return 0;
        }
        public override string ToString() => $"[{Width}, {Height}]";
    }
}