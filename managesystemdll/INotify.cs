using DataStructures;

namespace ManageSystemDll
{
    public interface INotify<T> where T : IContainable<T>
    {
        void LogAdd(uint amount, T item);
        void LogReturned(uint amount, T item);
        bool LogAskBuy(DoubleLinkedList<ProductData<T>> products);
        void LogRemoved(uint amount, T item);
        void LogNotFound(T item);
        void LogError(string message);
        void LogLowStock(T item);
    }
}
