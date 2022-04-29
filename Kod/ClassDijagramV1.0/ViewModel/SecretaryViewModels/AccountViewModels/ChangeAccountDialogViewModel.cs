using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels
{
    public class ChangeAccountDialogViewModel : ObservableObject
    {
        #region Fields

        private AccountViewModel _accountViewModel;

        private RelayCommand _changeAccountCommand;

        private AccountController _accountController;


        #endregion

        #region Properties

        public AccountViewModel AccountViewModel
        {
            get { return _accountViewModel; }
            set { _accountViewModel = value; }
        }

        public RelayCommand ChangeAccountCommand
        {
            get
            {
                if (_changeAccountCommand == null)
                {
                    _changeAccountCommand = new RelayCommand(ChangeAccount, CanChangeAccount);
                }

                return _changeAccountCommand;
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsGuest { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a">Account koji menjamo</param>
        public ChangeAccountDialogViewModel(AccountViewModel a)
        {
            _accountViewModel = a;

            App app = Application.Current as App;

            _accountController = app.AccountController;

            // Inicijalizujemo propertije na propertije iz accounta
            Username = _accountViewModel.Username;
            Password = _accountViewModel.Password;
            IsGuest = _accountViewModel.IsGuest;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Samo treba da zatvori dialog sve se promeni preko bindinga
        /// </summary>
        /// <param name="o">Castuj u window</param>
        private void ChangeAccount(object o)
        {
            Window win = o as Window;

            if (win != null)
            {
                _accountViewModel.Username = Username;
                _accountViewModel.Password = Password;
                _accountViewModel.IsGuest = IsGuest;
                win.Close();
            }
        }

        /// <summary>
        /// Ne moze da se promeni account ako username vec postoji
        /// </summary>
        /// <returns></returns>
        private bool CanChangeAccount()
        {
            if (_accountController.DoesAccountWithSameUsernameExists(Username, _accountViewModel.Account) ||
                Username == "" || Password == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
