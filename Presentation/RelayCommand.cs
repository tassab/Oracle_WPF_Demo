using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
            : this(execute, _ => true)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException("execute");
            this._canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public void Destroy()
        {
            this._canExecute = _ => false;
            this._execute = _ => { return; };
        }
    }
}
