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

        // Pointer na funkciju koja kaze da li commanda moze da se izvrsi, ne proslediti nista ako commanda uvek moze da
        // se izvrsi
        private Func<bool> _canExecute;
        #endregion

        #region Constructor

        public RelayCommand(Action<object> action, Func<bool> canExecute = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _action = action;
            _canExecute = canExecute;
        }

        #endregion

        public event EventHandler CanExecuteChanged
        {
            // Ovaj event se cesto desava tako da ce stalno da se proverava da li komanda moze da se izvrsi
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Proverava da li komanda moze da se izvrsi
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            // Ako je _canExecute null to znaci da komanda uvek moze da se izvrsi i vracamo true, u suprotnom pozivamo _canExecute
            return _canExecute == null ? true : _canExecute();
        }

        /// <summary>
        /// Ono sto komanda izvrsava
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            // Ako akcija nije null pozovemo je
            _action?.Invoke(parameter);
        }
    }
}
