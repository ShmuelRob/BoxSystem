using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BoxSystem.ViewModel
{
    public class LogViewModel : ViewModelBase
    {
        public ObservableCollection<TextBlock> Log { get; set; }
        public LogViewModel() => Log = new ObservableCollection<TextBlock>();
    }
}
