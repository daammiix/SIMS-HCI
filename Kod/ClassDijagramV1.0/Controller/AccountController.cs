using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System.Collections.ObjectModel;

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

        /// <summary>
        /// Proverava da li postoji account sa unetim podacima
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isLoginValid(string username, string password)
        {
            return _accountService.isLoginValid(username, password);
        }

        /// <summary>
        /// Proverava da li account sa zadatim id-em postoji
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DoesAccountWithSameIdExist(int id)
        {
            return _accountService.DoesAccountWithSameIdExist(id);
        }

        /// <summary>
        /// Proverava da li postoji account sa istim username-om
        /// </summary>
        /// <param name="username"></param>
        /// <param name="acc">U slucaju promene zelimo da prosledimo i trenutni acc jer njega necemo da gledamo</param>
        /// <returns></returns>
        public bool DoesAccountWithSameUsernameExist(string username, Account acc = null)
        {
            return _accountService.DoesAccountWithSameUsernameExist(username, acc);
        }

        /// <summary>
        /// Vraca true ako osoba ima acc, u suprotnom vraca false
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool PersonHasAccount(int personId)
        {
            return _accountService.PersonHasAccount(personId);
        }

        /// <summary>
        /// Vraca observable collection svih akaunta pacijenata
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Account> GetPatientsAccounts()
        {
            return _accountService.GetPatientsAccounts();
        }

        /// <summary>
        /// Vraca acc osobe za prosledjenim id-em
        /// </summary>
        /// <param name="id">Id osobe ciji acc trazimo</param>
        /// <returns>Account osobe ili null ako osoba nema acc</returns>
        public Account? GetAccountByPersonId(int id)
        {
            return _accountService.GetAccountByPersonId(id);
        }

        #endregion
    }
}
