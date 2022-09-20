using GalaSoft.MvvmLight;
using ManageSystem;
using Models;
using Services;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BoxSystem.ViewModel
{
    public class ProductsViewModel : ViewModelBase
    {
        public ObservableCollection<TextBlock> List { get; set; }
        ManageSystem<Box> system;
        public string Span { get; set; }
        public ButtonCommand FilterCommand { get; set; }
        public ProductsViewModel(ManageSystem<Box> system)
        {
            this.system = system;
            List = new ObservableCollection<TextBlock>();
            FilterCommand = new ButtonCommand(FilterProducts);
        }
        void FilterProducts()
        {
            if (int.TryParse(Span, out int res))
            {
                List.Clear();
                system.ShowByTime(new TimeSpan(res, 0, 0, 0)).ForEach(b =>
                {
                    var block = new TextBlock();
                    block.Text = b.ToString();
                    List.Add(block);
                }
                );

            }
        }
    }
}
