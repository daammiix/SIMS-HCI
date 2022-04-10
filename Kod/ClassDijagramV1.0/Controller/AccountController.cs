using ClassDijagramV1._0.Model.Accounts;
using ClassDijagramV1._0.Service;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class AccountController
    {
        #region Fields

        private AccountService _accountService;

        private RelayCommand? _addGuestAccountCommand;

        #endregion

        #region Properties

        public ObservableCollection<Account> RegularAccounts { get; private set; }

        public ObservableCollection<Account> GuestAccounts { get; private set; }

        public RelayCommand AddGuestAccountCommand
        {
            get
            {
                if (_addGuestAccountCommand == null)
                    _addGuestAccountCommand = new RelayCommand(() =>
                    {
                        GuestAccounts.Add(new GuestAccount());
                    });

                return _addGuestAccountCommand;
            }
        }

        #endregion

        #region Constructor

        public AccountController(AccountService accService)
        {
            _accountService = accService;

            RegularAccounts = new ObservableCollection<Account>();
            GuestAccounts = new ObservableCollection<Account>();

            foreach (Account acc in _accountService.GetAccounts())
            {
                if (acc is RegularAccount)
                {
                    RegularAccounts.Add(acc);
                }
                else
                {
                    GuestAccounts.Add(acc);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registruje novi account
        /// </summary>
        /// <param name="newAccount"></param>
        public void AddAccount(Account newAccount)
        {
            _accountService.AddAccount(newAccount);
        }

        /// <summary>
        /// Dobavlja listu svih account-a
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            return _accountService.GetAccounts();
        }

        /// <summary>
        /// Brise account sa zadatim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteAccount(string username)
        {
            return _accountService.DeleteAccount(username);
        }

        /// <summary>
        /// Vraca account sa zadatim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Account GetAccount(string username)
        {
            return _accountService.GetAccount(username);
        }

        /// <summary>
        /// Updatuje account sa zadatim username-om
        /// </summary>
        /// <param name="account"></param>
        public void updateAccount(Account account)
        {
            _accountService.UpdateAccount(account);
        }

        #endregion
    }
}
