using DataStructures;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ManageSystem
{
    public interface INotify<T> where T : IContainable<T>
    {
        ICollection<TextBlock> Log { get; set; }
        void LogAdd(uint amount, T item);
        void LogReturned(uint amount, T item);
        bool LogAskBuy(DoubleLinkedList<ProductData<T>> products);
        void LogRemoved(uint amount, T item);
        void LogNotFound(T item);
        void LogError(string message);
        void LogLowStock(T item);
    }
}
