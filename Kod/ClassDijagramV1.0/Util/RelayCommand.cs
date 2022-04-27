using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassDijagramV1._0.Util
{
    public class RelayCommand : ICommand
    {
        #region Fields

        // Akcija koju komanda izvrasava
        private Action<object> _action;

        #endregion

        #region Constructor

        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        #endregion

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter);
        }
    }
}
