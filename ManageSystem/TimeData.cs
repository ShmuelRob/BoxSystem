using System;

namespace ManageSystem
{
    internal class TimeData<T> : IComparable<TimeData<T>>
    {
        public T ProductProperty { get; set; }
        public DateTime LastTimeSold { get; set; }
        public TimeData(T value)
        {
            ProductProperty = value;
            LastTimeSold = DateTime.Now;
        }

        public int CompareTo(TimeData<T> other) =>
            LastTimeSold.CompareTo(other.LastTimeSold);
        public override bool Equals(object obj)
        {
            if (!(obj is TimeData<T>)) return false;
            var temp = obj as TimeData<T>;
            return ProductProperty.Equals(temp.ProductProperty);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
