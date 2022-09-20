using GalaSoft.MvvmLight;
using ManageSystem;
using Models;
using Services;

namespace BoxSystem.ViewModel
{
    public class ControlViewModel : ViewModelBase
    {
        readonly ManageSystem<Box> manageSystem;
        public string Height { get; set; }
        public string Width { get; set; }
        public string Amount { get; set; }
        public ButtonCommand AddCommand { get; set; }
        public ButtonCommand SellCommand { get; set; }

        public ControlViewModel(ManageSystem<Box> manageSystem)
        {
            this.manageSystem = manageSystem;
            AddCommand = new ButtonCommand(AddSupply);
            SellCommand = new ButtonCommand(Sell);
        }

        void AddSupply()
        {
            bool h = double.TryParse(Height, out double height);
            bool w = double.TryParse(Width, out double width);
            bool a = uint.TryParse(Amount, out uint amount);
            if (h && w && a)
                manageSystem.AddSupply(new Box(width, height), amount);
        }

        void Sell()
        {
            bool h = double.TryParse(Height, out double height);
            bool w = double.TryParse(Width, out double width);
            bool a = uint.TryParse(Amount, out uint amount);
            if (h && w && a)
                manageSystem.Sell(new Box(width, height), amount);
        }
    }
}
