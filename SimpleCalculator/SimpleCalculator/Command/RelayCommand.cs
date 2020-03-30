using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleCalculator.Command
{
    class RelayCommand : ICommand
    {
        Action<Object> executeAction;
        Func<Object,bool> canExecuteFunc;
        bool canExecutechanged;

        public RelayCommand(Action<Object> executeAction, Func<Object,bool> canExecuteFunc, bool canExecuteChanged)
        {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
            canExecutechanged = canExecuteChanged;
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {

                CommandManager.RequerySuggested += value;

            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteFunc == null)
            {
                return false;
            }
            else
            {
                return canExecuteFunc(parameter);
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
