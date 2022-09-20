using System;
using System.Windows.Input;

namespace Services
{
    public class ButtonCommand : ICommand
    {
        readonly Action action;
        public event EventHandler CanExecuteChanged;
        public ButtonCommand(Action act) => action = act;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => action();
    }
}
