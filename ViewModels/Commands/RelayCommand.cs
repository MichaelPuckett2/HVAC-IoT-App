using System;
using System.Windows.Input;

namespace HVAC_iot_App.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        readonly Action<object> ExecuteCommand;
        readonly Predicate<object> CanExecuteCommand;

        public RelayCommand(Action<object> execute)
        {
            ExecuteCommand = execute;
            CanExecuteCommand = (obj) => true;
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            ExecuteCommand = execute;
            CanExecuteCommand = canExecute;
        }

        public bool CanExecute(object parameter) => (CanExecuteCommand?.Invoke(parameter)).GetValueOrDefault(false);
        public void Execute(object parameter) => ExecuteCommand?.Invoke(parameter);

        public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
