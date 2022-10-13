using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Help
{
    internal class RelayCommands : ICommand
    {
        readonly Action _execute; //ce executam
        readonly Func<bool> _canExecute; //daca se poate sau nu executa

        public RelayCommands(Action execute, Func<bool> canExecute) //pasam un delegat
        {
            if (execute == null) throw new ArgumentNullException("Execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommands(Action execute) : this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
