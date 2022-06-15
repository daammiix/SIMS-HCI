using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.SecretaryView.AccountsView;
using Controller;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels
{
    public class AccountsMainViewModel
    {
        #region Fields

        private AccountController _accountController;

        private PatientController _patientController;

        private RelayCommand _addAccountCommand;

        // Selectovan AccountViewModel iz tabele, na pocetku je null
        private AccountViewModel? _accountViewModel = null;

        private RelayCommand _removeAccountCommand;

        private RelayCommand _banUnbanAccountCommand;

        private RelayCommand _changeAccountCommand;

        private RelayCommand _searchCommand;

        #endregion

        #region Properties

        public ObservableCollection<AccountViewModel> Accounts { get; set; }

        public AccountViewModel AccountViewModel
        {
            get { return _accountViewModel; }
            set { _accountViewModel = value; }
        }

        public string SearchText { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Prikazuje dialog za dodavanje accounta
        /// </summary>
        public RelayCommand AddAccountCommand
        {
            get
            {
                if (_addAccountCommand == null)
                {
                    _addAccountCommand = new RelayCommand(o => { ShowAddAccountDialog(); });
                }

                return _addAccountCommand;
            }
        }

        public RelayCommand DeleteAccountCommand
        {
            get
            {
                if (_removeAccountCommand == null)
                {
                    _removeAccountCommand = new RelayCommand(o => { RemoveAccount(); }, IsAccountSelected);
                }

                return _removeAccountCommand;
            }
        }

        public RelayCommand BanUnbanAccountCommand
        {
            get
            {
                if (_banUnbanAccountCommand == null)
                {
                    _banUnbanAccountCommand = new RelayCommand(o => { BanUnbanAccount(); }, IsAccountSelected);
                }

                return _banUnbanAccountCommand;
            }
        }

        public RelayCommand ChangeAccountCommand
        {
            get
            {
                if (_changeAccountCommand == null)
                {
                    _changeAccountCommand = new RelayCommand(o => { ShowChangeAccountDialog(); }, IsAccountSelected);
                }

                return _changeAccountCommand;
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(o => { SearchExecute(o as DataGrid); });
                }

                return _searchCommand;
            }
        }

        #endregion

        #region Constructor

        public AccountsMainViewModel()
        {
            // Uzmemo accountController i ucitamo accounte
            var app = Application.Current as App;

            _accountController = app.AccountController;
            _patientController = app.PatientController;

            Accounts = new ObservableCollection<AccountViewModel>();

            // Popunimo kolekciju accountViewModela
            foreach (Account a in _accountController.GetAccounts())
            {
                Accounts.Add(new AccountViewModel(a));
            }
        }

        #endregion

        #region Private Helpers

        private void ShowAddAccountDialog()
        {
            AddAccountDialog addAccDialog = new AddAccountDialog(Accounts);
            addAccDialog.Owner = Application.Current.MainWindow;
            addAccDialog.ShowDialog();
        }

        /// <summary>
        /// Brise account
        /// </summary>
        private void RemoveAccount()
        {
            // izbrisemo pacijenta koji je bio vezan za acc
            RemoveAccountsPatient(_accountViewModel.PersonId);

            // izbrisemo ga i iz observable kolekcije accountViewModela i sam acc iz repozitorijuma
            _accountController.DeleteAccount(_accountViewModel.Username);
            Accounts.Remove(_accountViewModel);

        }

        /// <summary>
        /// Izbrisemo pacijenta koji je vezan za acc
        /// </summary>
        /// <param name="patientId">Id pacijenta kojeg brisemo</param>
        private void RemoveAccountsPatient(int patientId)
        {
            _patientController.RemovePatient(patientId);
        }

        /// <summary>
        /// Da li mogu da se izvrse komande koje zahtevaju da account bude selektovan
        /// </summary>
        /// <returns></returns>
        private bool IsAccountSelected()
        {
            if (_accountViewModel == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Banuje ili unbanuje account u zavistosti da li je bio banovan
        /// </summary>
        private void BanUnbanAccount()
        {
            if (_accountViewModel.Banned == false)
            {
                _accountViewModel.Banned = true;
            }
            else
            {
                _accountViewModel.Banned = false;
            }
        }

        /// <summary>
        /// Prikazuje dialog za izmenu account-a
        /// </summary>
        private void ShowChangeAccountDialog()
        {
            ChangeAccountDialog changeAccountDialog = new ChangeAccountDialog();
            // Stavimo data context na view model i prosledimo mu account koji menjamo
            changeAccountDialog.DataContext = new ChangeAccountDialogViewModel(AccountViewModel);
            changeAccountDialog.Owner = Application.Current.MainWindow;
            changeAccountDialog.ShowDialog();
        }

        private void SearchExecute(DataGrid dg)
        {
            if (!SearchText.Equals(""))
            {
                var filteredAccounts = Accounts
                    .Where(accVM => accVM.Username.ToLower().Contains(SearchText.ToLower()) ||
                                    accVM.Password.ToLower().Contains(SearchText.ToLower()) ||
                                    accVM.PersonId.ToString().Contains(SearchText));
                dg.ItemsSource = filteredAccounts;
            }
            else
            {
                dg.ItemsSource = Accounts;
            }
        }

        #endregion

    }
}
