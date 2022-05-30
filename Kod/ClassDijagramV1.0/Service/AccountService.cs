using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class AccountService
    {
        #region Fields

        private readonly AccountRepo _accountRepo;

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
        public ObservableCollection<Account> GetAccounts()
        {
            return _accountRepo.GetAccounts();
        }

        /// <summary>
        /// Cuva accountove u fajl
        /// </summary>
        public void SaveAccounts()
        {
            _accountRepo.SaveAccounts();
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

        /// <summary>
        /// Proverava da li postoji account sa zadatim usernameom i passwordom
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isLoginValid(string username, string password)
        {
            bool ret = false;
            foreach (Account a in _accountRepo.GetAccounts())
            {
                if (a.Username.Equals(username) && a.Password.Equals(password))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Proverava da li postoji acc sa istim id-em sto bi znacilo da osoba vec ima acc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DoesAccountWithSameIdExist(int id)
        {
            bool ret = false;
            foreach (Account a in _accountRepo.GetAccounts())
            {
                if (a.PersonId == id)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Proverava da li postoji account sa istim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <param name="acc">U slucaju promene zelimo da prosledimo i trenutni acc jer njega necemo da gledamo</param>
        /// <returns></returns>
        public bool DoesAccountWithSameUsernameExist(string username, Account acc = null)
        {
            bool ret = false;
            foreach (Account a in _accountRepo.GetAccounts())
            {
                // U slucaju da je prosledjen acc koji treba preskociti samo ga preskocimo
                if (acc != null && a == acc)
                {
                    continue;
                }
                if (a.Username == username)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Vraca true ako osoba ima acc, u suprotnom vraca false
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool PersonHasAccount(int personId)
        {
            bool hasAccount = false;
            foreach (Account a in _accountRepo.GetAccounts())
            {
                if (a.PersonId == personId)
                {
                    hasAccount = true;
                    break;
                }
            }

            return hasAccount;
        }

        /// <summary>
        /// Vraca acc osobe za prosledjenim id-em
        /// </summary>
        /// <param name="id">Id osobe ciji acc trazimo</param>
        /// <returns>Account osobe ili null ako osoba nema acc</returns>
        public Account? GetAccountByPersonId(int id)
        {
            // ret val
            Account? ret = null;

            foreach (Account a in _accountRepo.GetAccounts())
            {
                if (a.PersonId == id)
                {
                    ret = a;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Vraca observable collection svih akaunta pacijenata
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Account> GetPatientsAccounts()
        {
            // Ret value
            ObservableCollection<Account> accounts = new ObservableCollection<Account>();

            foreach (Account a in _accountRepo.GetAccounts())
            {
                if (a.Role == Role.Patient)
                {
                    accounts.Add(a);
                }
            }

            return accounts;
        }

        #endregion
    }
}