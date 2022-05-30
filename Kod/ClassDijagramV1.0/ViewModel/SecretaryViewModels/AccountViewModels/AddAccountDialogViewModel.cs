using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels
{
    public class AddAccountDialogViewModel : ObservableObject
    {
        #region Fields

        private string _username = "";
        private string _password = "";
        private string _name = "";
        private string _surname = "";
        private string _jmbg = "";
        // Izabrana osoba ciji acc pravimo
        private ObservableCollection<Person> _persons;
        private ObservableCollection<Account> _accounts;

        private AccountController _accountController;
        private PatientController _patientController;
        private SecretaryController _secretaryController;
        private DoctorController _doctorController;
        private ManagerController _managerController;

        private RelayCommand _addAccountCommand;

        #endregion

        #region Properties

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Jmbg
        {
            get { return _jmbg; }
            set { _jmbg = value; }
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            private set { _persons = value; }
        }

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            private set { _accounts = value; }
        }

        // Da bi ga dodali i u tabelu
        public ObservableCollection<AccountViewModel> AccountsViewModels { get; set; }

        #endregion

        #region Commands 

        public RelayCommand AddAccountCommand
        {
            get
            {
                if (_addAccountCommand == null)
                {
                    _addAccountCommand = new RelayCommand(AddAccount, CanAddAccount);
                }

                return _addAccountCommand;
            }
        }

        #endregion

        #region Constructor

        public AddAccountDialogViewModel(ObservableCollection<AccountViewModel> accountViewModels)
        {
            // Da bi dodali u tabelu
            AccountsViewModels = accountViewModels;

            App app = Application.Current as App;

            _accountController = app.AccountController;
            _patientController = app.PatientController;
            _secretaryController = app.SecretaryController;
            _doctorController = app.DoctorController;
            _managerController = app.ManagerController;

            _accounts = _accountController.GetAccounts();

            // ucitamo sve osobe i spojimo ih u persons
            var patients = _patientController.GetPatients();
            var secretaries = _secretaryController.GetSecretaries();
            var doctors = _doctorController.GetAllDoctors();
            var managers = _managerController.GetManagers();

            _persons = new ObservableCollection<Person>();

            // Dodamo samo osobe koje nemaju acc
            foreach (var patient in patients)
            {
                if (!_accountController.PersonHasAccount(patient.Id))
                {
                    _persons.Add(patient);
                }
            }
            foreach (var secretary in secretaries)
            {
                if (!_accountController.PersonHasAccount(secretary.Id))
                {
                    _persons.Add(secretary);
                }
            }
            foreach (var doctor in doctors)
            {
                if (!_accountController.PersonHasAccount(doctor.Id))
                {
                    _persons.Add(doctor);
                }
            }
            foreach (var manager in managers)
            {
                if (!_accountController.PersonHasAccount(manager.Id))
                {
                    _persons.Add(manager);
                }
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Parametar o bice window da bi ga zatvorili kad se komanda izvrsi
        /// </summary>
        /// <returns>Da li je acc uspesno dodat</returns>
        private void AddAccount(object o)
        {
            Window window = o as Window;
            // Napravimo novog pacijena od inputa
            Patient p = CreatePatientFromInput();
            // napravimo novi account od inputa
            Account newAccount = new Account(p.Id, Role.Patient, Username, Password, true);
            // proverimo da li account sa istim username-om postoji, jedan username moze da bude vezan samo za jedan acc
            if (_accountController.DoesAccountWithSameUsernameExist(Username))
            {
                // Priakzemo samo error dialog
                MessageBox.Show("Username already in use or person already has account!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Dodamo pacijenta u bazu
                _patientController.AddPatient(p);
                // Dodamo account i u bazu i u tabelu preko AccountViewModela i zatvorimo window
                _accountController.AddAccount(newAccount);
                AccountsViewModels.Add(new AccountViewModel(newAccount));

                window.Close();
            }

        }

        /// <summary>
        /// Pravi pacijenta na osnovu inputa
        /// </summary>
        private Patient CreatePatientFromInput()
        {
            Patient p = new Patient()
            {
                Id = ++Patient.idCounter,
                Name = this.Name,
                Surname = this.Surname,
                Jmbg = this.Jmbg,
                Appointments = new List<Appointment>(),
                MedicalRecordNumber = null
            };

            return p;
        }

        /// <summary>
        /// Da li addAccount commanda moze da se izvrsi
        /// </summary>
        /// <returns></returns>
        private bool CanAddAccount()
        {
            // Moze ako polja nisu prazna
            if (Username.Equals("") || Password.Equals("") || Name.Equals("") || Surname.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
