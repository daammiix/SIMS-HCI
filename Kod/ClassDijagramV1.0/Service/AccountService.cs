﻿using ClassDijagramV1._0.Model.Accounts;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class AccountService
    {
        #region Fields

        private AccountRepo _accountRepo;

        #endregion

        #region Constructor

        public AccountService(AccountRepo repo)
        {
            _accountRepo = repo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registruje novi account
        /// </summary>
        /// <param name="newAccount"></param>
        public void AddAccount(Account newAccount)
        {
            _accountRepo.AddAccount(newAccount);
        }

        /// <summary>
        /// Dobavlja listu svih account-a
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            return _accountRepo.GetAccounts();
        }

        /// <summary>
        /// Brise account sa zadatim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteAccount(string username)
        {
            return _accountRepo.DeleteAccount(username);
        }

        /// <summary>
        /// Vraca account sa zadatim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Account GetAccount(string username)
        {
            return _accountRepo.GetAccount(username);
        }

        /// <summary>
        /// Updatuje account sa zadatim username-om
        /// </summary>
        /// <param name="account"></param>
        public void UpdateAccount(Account account)
        {
            _accountRepo.UpdateAccount(account);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Vezuje regularan account i osobu
        /// </summary>
        /// <param name="p"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void RegisterPersonWithRegularAccount(Person p, string username, string password)
        {
            p.Account = new RegularAccount(username, password, p);
        }

        /// <summary>
        /// Vezuje guest account i osobu
        /// </summary>
        /// <param name="p"></param>
        public static void RegisterPersonWithGuestAccount(Person p)
        {
            p.Account = new GuestAccount();
        }

        #endregion
    }
}