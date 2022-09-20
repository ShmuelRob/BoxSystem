using ManageSystem;
using Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using DataStructures;
using BoxSystem.ViewModel;

namespace BoxSystem.Services
{
    public class Notifier : INotify<Box>
    {
        public ICollection<TextBlock> Log { get; set; }

        public Notifier(LogViewModel logViewModel) => Log = logViewModel.Log;

        public void LogAdd(uint amount, Box item) =>
            Log.Add(new TextBlock { Text = $"{amount} Boxes of {item} were added", FontSize = 10 });

        public void LogReturned(uint amount, Box item) =>
            Log.Add(new TextBlock { Text = $"{amount} Boxes of {item} returned to the supplier", FontSize = 10 });

        public bool LogAskBuy(DoubleLinkedList<ProductData<Box>> products) =>
            MessageBox.Show($"Do You want to buy:{products}", "Do you want to buy this?", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

        public void LogRemoved(uint amount, Box item) =>
            Log.Add(new TextBlock { Text = $"{amount} Boxes of {item} were removed", FontSize = 10 });

        public void LogNotFound(Box item) =>
            Log.Add(new TextBlock { Text = $"We cannot find Box That will fit {item}", FontSize = 10 });

        public void LogError(string message) => Log.Add(new TextBlock { Text = $"Error: {message}", FontSize = 10 });

        public void LogLowStock(Box item) =>
            Log.Add(new TextBlock { Text = $"{item} is going to run out", FontSize = 10 });
    }
}
