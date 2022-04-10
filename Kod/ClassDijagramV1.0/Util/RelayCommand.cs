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
        // Akcija koju ce komanda da izvrsava
        private Action _action;

        #endregion

        public event EventHandler? CanExecuteChanged;

        #region Constructor

        public RelayCommand(Action a)
        {
            _action = a;
        }

        #endregion

        /// <summary>
        /// Relay command moze uvek da se izvrsi
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
