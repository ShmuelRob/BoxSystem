using ManageSystemDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Box)) return false;
        //    Box other = (Box)obj;
        //    if (Width == other.Width && Height == other.Height) return true;
        //    return false;
        //}
        //public override int GetHashCode() => base.GetHashCode();
    }
}
