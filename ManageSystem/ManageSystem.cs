using DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;

namespace ManageSystem
{
    public class ManageSystem<T> where T : IContainable<T>
    {
        IConfigItems ConfigItems { get; set; }
        readonly BST<ProductData<T>> storage;
        readonly DoubleLinkedList<TimeData<T>> productsByTime;
        readonly INotify<T> notify;
        Timer timer;

        public ManageSystem(INotify<T> notify, IConfigItems config)
        {
            this.notify = notify;
            ConfigItems = config;
            storage = new BST<ProductData<T>>();
            productsByTime = new DoubleLinkedList<TimeData<T>>();
            timer = new Timer(RemoveOldProducts, null, new TimeSpan(24, 0, 0), new TimeSpan(24, 0, 0));
        }

        public void AddSupply(T item, uint amount) // O(logN)
        {
            uint returnedSupply = 0;
            var temp = new ProductData<T>(item, amount);
            var x = storage.SearchRef(temp, out bool isfound); // O(logN)
            if (isfound)
            {
                if (x.Amount + amount > ConfigItems.MaxItemsCount)
                {
                    returnedSupply = x.Amount + amount - ConfigItems.MaxItemsCount;
                    x.Amount = ConfigItems.MaxItemsCount;
                }
                else x.Amount += amount;
            }
            else
            {
                if (amount > ConfigItems.MaxItemsCount)
                {
                    returnedSupply = amount - ConfigItems.MaxItemsCount;
                    temp.Amount = ConfigItems.MaxItemsCount;
                }
                productsByTime.AddLast(new TimeData<T>(item)); // O(1)
                temp.TimeRef = productsByTime.Tail;
                storage.Add(temp); // O(logN)
            }
            notify.LogAdd(amount - returnedSupply, item);
            if (returnedSupply != 0) notify.LogReturned(returnedSupply, item);
        }
        public void Sell(T item, uint amount) // O(logN)
        {
            DoubleLinkedList<ProductData<T>> itemsToSell = new DoubleLinkedList<ProductData<T>>();
            if (!OrganizeProductsToSell(itemsToSell, item, amount, item.Volume, 0) && itemsToSell.Count == 0)
                notify.LogNotFound(item); // O(1)
            else if (notify.LogAskBuy(itemsToSell)) SellProducts(itemsToSell); // O(1)
            //OrganizeProductsToSell(itemsToSell, item, amount, item.Volume, 0);
            //SellProducts(itemsToSell);
        }
        bool OrganizeProductsToSell(DoubleLinkedList<ProductData<T>> itemsToSell, T item, uint amount, double firstVol, int devisionCounter) // O(logN)
        {
            if (devisionCounter == 0) itemsToSell.Clear(); // O(1)
            if (storage.IsEmpty) return false;
            if (devisionCounter > ConfigItems.MaxSplitsAllowed) return !(itemsToSell.Count == 0);
            if (item.CompareTo(storage.Biggest.Product) > 0) return !(itemsToSell.Count == 0);
            var temp = new ProductData<T>(item, amount);
            var itemNode = storage.SearchRef(temp, out bool isFound); // O(logN)
            if (devisionCounter == 0 && isFound)
            {
                if (itemNode.Amount >= amount)
                {
                    itemsToSell.AddLast(new ProductData<T>(item, amount)); // O(1)
                    return true;
                }
                else
                {
                    itemsToSell.AddLast(new ProductData<T>(item, itemNode.Amount)); // O(1)
                    return OrganizeProductsToSell(itemsToSell, item, amount - itemNode.Amount, firstVol, devisionCounter + 1); // O(logN)
                }
            }
            else
            {
                var biggerProduct = storage.GetNextValue(temp); // O(LogN)
                if (biggerProduct is null) return false;
                while (biggerProduct.Product.Contain(temp.Product) == 0)
                {
                    biggerProduct = storage.GetNextValue(biggerProduct); // O(1)
                    if (biggerProduct is null) return false;
                }
                if (biggerProduct.Product.Volume * ConfigItems.MaxDeviationAllowed > firstVol) return false;
                if (biggerProduct.Amount >= amount)
                {
                    itemsToSell.AddLast(new ProductData<T>(biggerProduct.Product, amount));
                    return true;
                }
                itemsToSell.AddLast(new ProductData<T>(biggerProduct.Product, biggerProduct.Amount));
                return OrganizeProductsToSell(itemsToSell, biggerProduct.Product, amount - biggerProduct.Amount, firstVol, devisionCounter + 1);
            }
        }
        void SellProducts(DoubleLinkedList<ProductData<T>> itemsToSell) // O(logN)
        {
            ProductData<T> tmp;
            foreach (var item in itemsToSell)
            {
                tmp = storage.SearchRef(item, out _); // O(logN)

                var productDataTime = new TimeData<T>(tmp.Product);
                productsByTime.Remove(tmp.TimeRef);

                if (tmp.Amount <= item.Amount)
                {
                    storage.Remove(tmp); // O(logN)
                    notify.LogRemoved(tmp.Amount, tmp.Product); // O(1)
                }
                else
                {
                    tmp.Amount -= item.Amount;
                    productsByTime.AddLast(productDataTime);// O(1)
                    tmp.TimeRef = new DoubleLinkedListNode<TimeData<T>>(productDataTime);
                    notify.LogRemoved(item.Amount, item.Product);
                    if (tmp.Amount < ConfigItems.MinItemsCount) notify.LogLowStock(tmp.Product);
                }
            }
            itemsToSell.Clear();
        }
        public DataStructures.LinkedList<T> ShowByTime(TimeSpan ts)
        {
            var list = new DataStructures.LinkedList<T>();
            var temp = productsByTime.Head;
            while (temp != null && DateTime.Now - temp.Value.LastTimeSold <= ts)
            {
                list.AddLast(temp.Value.ProductProperty);
                temp = temp.Next;
            }
            return list;
        }
        DataStructures.LinkedList<ProductData<T>> ShowProductsByTime(TimeSpan ts)
        {
            var list = new DataStructures.LinkedList<ProductData<T>>();
            if (productsByTime.Count != 0)
            {
                var temp = productsByTime.Head;
                while (temp != null && DateTime.Now - temp.Value.LastTimeSold >= ts)
                {
                    var item = storage.SearchRef(new ProductData<T>(temp.Value.ProductProperty, 1), out _);
                    list.AddLast(item);
                    temp = temp.Next;
                }
            }
            return list;
        }
        void RemoveOldProducts(object state)
        {
            var old = ShowProductsByTime(ConfigItems.DaysBeforeOld);
            foreach (var item in old)
                storage.Remove(item);
            for (int i = 0; i < old.Count; i++)
                productsByTime.RemoveFirst();
        }
        public ICollection ShowAll()
        {
            List<ProductData<T>> products = new List<ProductData<T>>();
            storage.Scan(p => products.Add(p));
            return products;
        }
    }
}
