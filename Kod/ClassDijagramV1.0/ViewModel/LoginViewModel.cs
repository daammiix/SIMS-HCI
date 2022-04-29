using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels;
using ClassDijagramV1._0.Views;
using ClassDijagramV1._0.Views.DoctorView;
using ClassDijagramV1._0.Views.ManagerView;
using ClassDijagramV1._0.Views.PatientView;
using ClassDijagramV1._0.Views.SecretaryView;
using Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class LoginViewModel
    {
        #region Fields

        AccountController _accountController;
        PatientController _pattientController;

        RelayCommand _submitCommand;

        #endregion

        #region Properties

        public ObservableCollection<Account> Accounts { get; set; }

        public RelayCommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new RelayCommand((o) =>
                    {
                        // Instance of MainWindow cause we need to set CurrentView property
                        MainWindow mainWindow = (MainWindow)o;
                        // Handles login
                        Login(mainWindow);
                    });
                }
                return _submitCommand;
            }
        }

        public string Username { get; set; }

        public string Password { get; set; }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            // Load accounts
            var app = Application.Current as App;

            // Load controllers
            _accountController = app.AccountController;
            _pattientController = app.PatientController;

            // Load accounts
            Accounts = _accountController.GetAccounts();

        }

        #endregion

        #region Private Helpers

        private void setAppropriateView(MainWindow mainWindow)
        {
            Account a = _accountController.GetAccount(Username);
            // Zavisno od uloge pokazemo odgovarajuci prozor
            switch (a.Role)
            {
                case Role.Manager:
                    {
                        ManagerMainWindow managerMainWindow = new ManagerMainWindow();
                        managerMainWindow.Show();
                        break;
                    }
                case Role.Doctor:
                    {
                        DoctorMainWindow doctorMainWindow = new DoctorMainWindow();
                        doctorMainWindow.Show();
                        break;
                    }
                case Role.Patient:
                    {
                        PatientMainWindow patientMainWindow = new PatientMainWindow();
                        patientMainWindow.Show();
                        break;
                    }
                case Role.Secretary:
                    {
                        SecretaryMainWindow secretaryWindow = new SecretaryMainWindow();
                        // Tu se sad stavi dataContext na odgovarajuci viewModel kom se prosledi odgovarajuca osoba tako
                        // da moze da se rukuje sa podacima vezanim za nju
                        secretaryWindow.Show();
                        break;
                    }
            }
            // Zatvorimo glavni prozor
            mainWindow.Close();
        }

        /// <summary>
        /// Logs in user and set appropriate view
        /// </summary>
        private void Login(MainWindow mainWindow)
        {
            // Ako postoji account sa zadatim username-om i password-om onda prikazemo odgovarajuci view, u suprotnom prikazemo gresku
            if (_accountController.isLoginValid(Username, Password))
            {
                // Set appropriate view
                setAppropriateView(mainWindow);
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
