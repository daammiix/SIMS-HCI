using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class AccountRepo
    {
        #region Fields

        private FileHandler<Account> _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<Account> Accounts { get; set; }

        #endregion

        #region Constructor

        public AccountRepo(FileHandler<Account> fileHandler)
        {
            _fileHandler = fileHandler;

            Accounts = new ObservableCollection<Account>(_fileHandler.GetItems());

        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve accounte
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Account> GetAccounts()
        {
            return Accounts;
        }

        /// <summary>
        /// Za cuvanje accountova u fajl
        /// </summary>
        public void SaveAccounts()
        {
            _fileHandler.SaveItems(Accounts.ToList());
        }

        /// <summary>
        /// Dodaje novi account u listu account-a ako ne postoji account sa istim username-om
        /// </summary>
        /// <param name="newAccount"></param>
        /// <returns>true ako je novi acc uspesno dodat, u suprotnom false</returns>
        public bool AddAccount(Account newAccount)
        {
            bool notExists = true;
            Account? toUpdate = null;

            // Provera da li postoji account sa istim username-om
            foreach (Account account in Accounts)
            {
                if (account.Username.Equals(newAccount.Username))
                {
                    notExists = false;
                    toUpdate = account;
                    break;
                }
            }

            // Ako ne postoji dodaj ga i vrati true
            if (notExists)
            {
                Accounts.Add(newAccount);
                return true;
            }
            // Pregazi postojeci
            else
            {
                toUpdate = newAccount;
                return false;
            }

        }

        /// <summary>
        /// Vraca account sa zadatim username-om ako ne postoji vraca null
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Trazeni account</returns>
        public Account GetAccount(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account.Username.Equals(username))
                    return account;
            }
            return null;
        }

        /// <summary>
        /// Brise account
        /// </summary>
        /// <returns>true ako je uspesno obrisan, u suprotnom false</returns>
        public bool DeleteAccount(string username)
        {
            // Proveri da li postoji account sa zadatim username-om
            foreach (Account account in Accounts)
            {
                if (account.Username.Equals(username))
                {
                    Accounts.Remove(account);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updateuje account, podrazumeva se da se username accounta ne moze promeniti tako da je samo 
        /// potrebno naci account sa zadatim usernameom, ako account ne postoji dodaje novi account
        /// </summary>
        /// <param name="account"></param>
        public void UpdateAccount(Account account)
        {
            Account accForUpdate = null;

            // Proveri da li postoji trazeni account
            foreach (Account acc in Accounts)
            {
                if (account.Username.Equals(account.Username))
                {
                    accForUpdate = account;
                    break;
                }
            }

            // Ako postoji updatuj ga, ako ne postoji dodaj novi
            if (accForUpdate != null)
                accForUpdate = account;
            else
                Accounts.Add(account);
        }

        #endregion
    }
}
