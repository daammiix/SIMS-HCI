using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels
{
    public class AccountViewModel : ObservableObject
    {

        private Account _account;

        // Osoba na koju se account odnosi, treba za row details, za sad samo za pacijente
        private Person _person;

        private PatientController _patientController;

        private SecretaryController _secretaryController;

        private DoctorController _doctorController;

        private ManagerController _managerController;

        #region Properties

        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string Username
        {
            get
            {
                return _account.Username;
            }
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
            get
            {
                return _account.Password;
            }
            set
            {
                if (_account.Password != value)
                {
                    _account.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public bool Banned
        {
            get
            {
                return _account.Banned;
            }
            set
            {
                if (_account.Banned != value)
                {
                    _account.Banned = value;
                    OnPropertyChanged("Banned");
                }
            }
        }

        public bool IsGuest
        {
            get
            {
                return _account.IsGuest;
            }
            set
            {
                if (_account.IsGuest != value)
                {
                    _account.IsGuest = value;
                    OnPropertyChanged("IsGuest");
                }
            }
        }

        public int PersonId
        {
            get { return _account.PersonId; }
            set
            {
                if (_account.PersonId != value)
                {
                    _account.PersonId = value;
                    OnPropertyChanged("PersonId");
                }
            }
        }

        public Role Role
        {
            get { return _account.Role; }
            set
            {
                if (_account.Role != value)
                {
                    _account.Role = value;
                    OnPropertyChanged("Role");
                }
            }
        }

        // Za row details

        public string Name
        {
            get
            {
                // provera zato sto je trenutno implementirano samo za pacijente, posle nece trebati
                return (_person == null) ? "" : _person.Name;
            }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get
            {
                return (_person == null) ? "" : _person.Surname;
            }
            set { _person.Surname = value; }
        }

        public string Email
        {
            get
            {
                return (_person == null) ? "" : _person.Email;
            }
            set { _person.Email = value; }
        }


        #endregion

        #region Constructor

        public AccountViewModel(Account a)
        {
            _account = a;
            App app = Application.Current as App;

            _patientController = app.PatientController;
            _secretaryController = app.SecretaryController;
            _doctorController = app.DoctorController;
            _managerController = app.ManagerController;

            switch (_account.Role)
            {
                case Role.Doctor:
                    {
                        _person = _doctorController.GetDoctorById(a.PersonId);
                        break;
                    }
                case Role.Patient:
                    {
                        _person = _patientController.GetPatientById(a.PersonId);
                        break;
                    }
                case Role.Secretary:
                    {
                        _person = _secretaryController.GetSecretaryById(a.PersonId);
                        break;
                    }
                case Role.Manager:
                    {
                        _person = _managerController.GetManagerById(a.PersonId);
                        break;
                    }
            }

        }

        #endregion

    }
}
