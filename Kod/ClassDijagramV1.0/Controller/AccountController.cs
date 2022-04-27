using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using ClassDijagramV1._0.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Controller
{
    public class AccountController
    {
        #region Fields

        private readonly AccountService _accountService;

        #endregion

        #region Constructor

        public AccountController(AccountService accService)
        {
            _accountService = accService;
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
        public ObservableCollection<Account> GetAccounts()
        {
            return _accountService.GetAccounts();
        }

        /// <summary>
        /// Cuva accountove u fajl
        /// </summary>
        public void SaveAccounts()
        {
            _accountService.SaveAccounts();
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
