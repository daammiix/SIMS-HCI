using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.ProfileViewModels
{
    public class ProfileMainViewModel : ObservableObject
    {
        #region Fields

        private RelayCommand _logoutCommand;

        private Account _account;

        private Secretary _secretary;

        #endregion

        #region Properties

        //public Account Account { get; private set; } // acc ulogovanog sekretara
        //public Secretary Secretary { get; private set; } // sekretar koji je vezan za acc

        public string Username
        {
            get { return _account.Username; }
            set
            {
                if (_account.Username != value)
                {
                    _account.Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _account.Password; }
            set
            {
                if (_account.Password != value)
                {
                    _account.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Name
        {
            get { return _secretary.Name; }
            set
            {
                if (_secretary.Name != value)
                {
                    _secretary.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get { return _secretary.Surname; }
            set
            {
                if (_secretary.Surname != value)
                {
                    _secretary.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string Email
        {
            get { return _secretary.Email; }
            set
            {
                if (_secretary.Email != value)
                {
                    _secretary.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new RelayCommand(o =>
                    {
                        Logout();
                    });
                }

                return _logoutCommand;
            }
        }

        #endregion

        #region Constructor

        public ProfileMainViewModel(Account acc)
        {
            _account = acc;

            App app = Application.Current as App;
            SecretaryController secretaryController = app.SecretaryController;

            _secretary = secretaryController.GetSecretaryById(_account.PersonId);

        }

        #endregion

        #region Private Helpers

        private void Logout()
        {
            // prikazemo main window i zatvorimo trenutni window
            var windows = Application.Current.Windows;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            windows[0].Close(); // to trenutni window
        }

        #endregion
    }
}
