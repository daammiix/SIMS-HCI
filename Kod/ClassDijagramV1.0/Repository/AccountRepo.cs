using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model.Accounts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class AccountRepo
    {
        #region Fields

        private AccountFileHandler _fileHandler;

        #endregion

        #region Properties

        public List<Account> Accounts { get; set; }

        #endregion

        #region Constructor

        public AccountRepo(AccountFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            //Accounts = _fileHandler.getAccounts();

            Accounts = new List<Account>();
            DateTime date1 = new DateTime(2008, 5, 1);
            Accounts.Add(new RegularAccount("pera123", "pera123",
                new Patient("Djordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1, null, "1234", date1)));
            Accounts[0].Banned = true;

            Accounts.Add(new GuestAccount());
            Accounts.Add(new GuestAccount());
            Accounts.Add(new GuestAccount());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve accounte
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            return Accounts;
        }

        /// <summary>
        /// Dodaje novi account u listu account-a ako ne postoji account sa istim username-om
        /// </summary>
        /// <param name="newAccount"></param>
        /// <returns></returns>
        public bool AddAccount(Account newAccount)
        {
            // Flag za proveru da li postoji account sa istim username-om kao novi account
            bool notexists = true;

            // Provera da li postoji account sa istim username-om
            Accounts.ForEach(account =>
            {
                if (account.Username.Equals(newAccount.Username))
                    notexists = false;
            });

            // Ako ne postoji dodaj ga i vrati true
            if (notexists)
            {
                Accounts.Add(newAccount);
                return true;
            }

            // U suprotnom vrati false
            return false;
        }

        /// <summary>
        /// Vraca account sa zadatim username-om ako ne postoji vraca null
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Trazeni account</returns>
        public Account GetAccount(string username)
        {
            // Proveri da li postoji account sa datim username-om
            foreach (Account account in Accounts)
            {
                if (account.Username.Equals(username))
                    return account;
            }

            // Ako ne postoji account sa tim username-om vrati null
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

            // Ako nije postojao account sa tim username-om vrati false
            return false;
        }

        /// <summary>
        /// Updateuje account, podrazumeva se da se username accounta ne moze promeniti tako da je samo 
        /// potrebno naci account sa zadatim usernameom, ako account ne postoji dodaje novi account
        /// </summary>
        /// <param name="account"></param>
        public void UpdateAccount(Account account)
        {
            // referenca na account za updatovanje
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
