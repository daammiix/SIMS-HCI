using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Dialog;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Repository;
using ClassDijagramV1._0.Service;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.SecretaryView
{
    /// <summary>
    /// Interaction logic for Sekretar.xaml
    /// </summary>
    public partial class SecretaryViewUC : UserControl
    {
        #region Fields

        private AccountController _accountController;

        private PatientController _patientController;

        #endregion

        #region Properties

        public ObservableCollection<Account> Accounts { get; set; }

        public ObservableCollection<Patient> Patients { get; set; }

        #endregion

        #region Constructor

        public SecretaryViewUC()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            _accountController = app.AccountController;
            _patientController = app.PatientController;

            Accounts = _accountController.GetAccounts();
            Patients = _patientController.GetPatients();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Ako je guest check-box cekiran generise random username i isti password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuestCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string username = "Guest" + new Random().Next(10001);
            this.Username.Text = username;
            this.Password.Text = username;
        }

        /// <summary>
        /// Dodaj novi account u tabelu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            createPatientAndAccountFromInput();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Ucitava vrednosti iz forme i pravi novi account
        /// </summary>
        /// <returns></returns>
        public void createPatientAndAccountFromInput()
        {
            String name = this.Name.Text;
            String surname = this.Surname.Text;
            String jmbg = this.Jmbg.Text;
            String phoneNumber = this.PhoneNumber.Text;
            String email = this.Email.Text;
            if (!DateTime.TryParse(this.DateOfBirth.Text, out DateTime dateOfBirth))
                dateOfBirth = DateTime.MinValue;

            String gender = this.Gender.Text;
            String username = this.Username.Text;
            String password = this.Password.Text;

            String socialNumber = this.SecurityNumber.Text;

            bool isGuest = this.GuestCheckBox.IsChecked.Value;

            Patient p = new Patient(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, socialNumber);
            _patientController.AddPatient(p);
            Account acc = new Account(p.Id, Role.Patient, username, password, isGuest);
            _accountController.AddAccount(acc);

        }

        /// <summary>
        /// Ban i unban selectovanog usera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BanUnbanButton_Click(object sender, RoutedEventArgs e)
        {
            Account? selectedAccount = this.AccountList.SelectedItem as Account;
            if (selectedAccount != null)
            {
                if (selectedAccount.Banned == false)
                {
                    selectedAccount.Banned = true;
                }
                else
                {
                    selectedAccount.Banned = false;
                }
            }
        }

        #endregion
    }
}
