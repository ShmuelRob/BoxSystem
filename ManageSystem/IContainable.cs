using System;

namespace ManageSystem
{
    public interface IContainable<T> : IComparable<T>
    {
        /// <summary>
        /// Check the volume of an object. if you dont need it, just return your T item
        /// </summary>
        double Volume { get; }
        /// <summary>
        /// Checks if one object contains another object.
        /// If you dont need to use it, return in the function the CompareTo(other)
        /// </summary>
        /// <param name="other">the partner to checkwho contains who</param>
        /// <returns>More than 0 if this contains the other.
        /// Less than 0 if the other contains this.
        /// 0 if they are not contain each other</returns>
        int Contain(T other);
    }
}
