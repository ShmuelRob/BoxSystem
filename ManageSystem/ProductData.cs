using DataStructures;
using System;

namespace ManageSystem
{
    public class ProductData<T> : IComparable<ProductData<T>> where T : IContainable<T>
    {
        public T Product { get; set; }
        public uint Amount { get; set; }
        internal DoubleLinkedListNode<TimeData<T>> TimeRef { get; set; }

        public ProductData(T product, uint amount)
        {
            Product = product;
            Amount = amount;
        }

        public int CompareTo(ProductData<T> other) => Product.CompareTo(other.Product);
        public override string ToString() => $"{Product}, Quantity: {Amount}";
    }
}
