using ManageSystemDll;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace BoxSystemInOne
{
    public partial class MainWindow : Window
    {
        public ManageSystem<Box> MS { get; set; }
        public ObservableCollection<TextBlock> LogText { get; set; }
        const int MaxLogCount = 23;

        public MainWindow()
        {
            InitializeComponent();
            LogText = new ObservableCollection<TextBlock>();
            log.ItemsSource = LogText;
            ConfigItems config = SetConfig();
            MS = new ManageSystem<Box>(new Notifier(LogText), config);
        }

        private ConfigItems SetConfig()
        {
            uint maxItems = uint.Parse(ConfigurationManager.AppSettings["maxIemsCount"]);
            uint minItems = uint.Parse(ConfigurationManager.AppSettings["minIemsCount"]);
            double maxDeviation = double.Parse(ConfigurationManager.AppSettings["maxDeviationAllowed"]);
            uint maxSplits = uint.Parse(ConfigurationManager.AppSettings["maxSplitsAllowed"]);
            TimeSpan days = new TimeSpan(int.Parse(ConfigurationManager.AppSettings["daysBeforeOld"]), 0, 0, 0);
            ConfigItems items = new ConfigItems(maxItems, minItems, maxDeviation, maxSplits, days);
            return items;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool b1 = double.TryParse(widthBox.Text, out double w);
            bool b2 = double.TryParse(heightBox.Text, out double h);
            bool b3 = uint.TryParse(amountBox.Text, out uint a);

            if (b1 && b2 && b3)
            {
                MS.AddSupply(new Box(w, h), a);
                while (LogText.Count > MaxLogCount) LogText.RemoveAt(0);
            }
        }
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            bool b1 = double.TryParse(widthBox.Text, out double w);
            bool b2 = double.TryParse(heightBox.Text, out double h);
            bool b3 = uint.TryParse(amountBox.Text, out uint a);

            if (b1 && b2 && b3)
            {
                MS.Sell(new Box(w, h), a);
                while (LogText.Count > MaxLogCount) LogText.RemoveAt(0);
            }
        }
        private void FilterProducts_Click(object sender, RoutedEventArgs e)
        {
            if (timeSpan.Text == String.Empty) return;
            if (!int.TryParse(timeSpan.Text, out int intTs)) return;
            TimeSpan ts = new TimeSpan(intTs, 0, 0, 0);
            //TimeSpan ts = new TimeSpan(0, 0, 0, intTs);
            products.ItemsSource = MS.ShowByTime(ts);
        }
    }
}
