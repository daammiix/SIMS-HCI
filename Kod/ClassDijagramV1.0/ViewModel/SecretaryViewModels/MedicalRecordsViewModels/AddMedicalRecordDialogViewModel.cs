using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels
{
    public class AddMedicalRecordDialogViewModel
    {
        #region Fields

        private PatientController _patientController;

        private MedicalRecordController _medicalRecordController;

        private RelayCommand _addMedicalRecord;

        // Kad napravimo medicinski karton acc vise nije guest
        private AccountController _accountController;

        #endregion

        #region Properties

        // Za combo boxove
        public Array MaritalStatuses
        {
            get
            {
                return Enum.GetValues(typeof(MaritalStatus));
            }
        }

        public Array BloodTypes
        {
            get
            {
                return Enum.GetValues(typeof(BloodType));
            }
        }

        public ObservableCollection<Patient> Patients { get; set; }

        public Patient SelectedPatient { get; set; }

        public string ParentName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string HealthCardNumber { get; set; }

        public string SocialSecurityNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public BloodType? BloodType { get; set; }

        public Gender Gender { get; set; }

        public List<string> Allergens { get; set; }

        public List<string> Diseases { get; set; }

        public RelayCommand AddMedicalRecord
        {
            get
            {
                if (_addMedicalRecord == null)
                {
                    _addMedicalRecord = new RelayCommand(o => AddMedicalRecordExecute(o), AddMedicalRecordCanExecute);
                }

                return _addMedicalRecord;
            }
        }

        // Da bi ga dodali u tabelu
        public ObservableCollection<MedicalRecordViewModel> MedicalRecords { get; private set; }

        #endregion

        #region Constructor

        public AddMedicalRecordDialogViewModel(ObservableCollection<MedicalRecordViewModel> medicalRecordViewModels)
        {
            MedicalRecords = medicalRecordViewModels;

            App app = Application.Current as App;

            _patientController = app.PatientController;

            _medicalRecordController = app.MedicalRecordController;

            _accountController = app.AccountController;

            Patients = new ObservableCollection<Patient>();

            // Dodamo sve pacijente koji nemaju karton u Patients
            foreach (Patient p in _patientController.GetPatients())
            {
                if (p.MedicalRecordNumber == null)
                {
                    Patients.Add(p);
                }
            }
        }

        #endregion

        #region Private Helpres

        /// <summary>
        /// Dodaje medical record i zatvara window
        /// </summary>
        /// <param name="o"></param>
        private void AddMedicalRecordExecute(object o)
        {
            // Kastujemo object u Window
            Window window = o as Window;

            // Popunimo informacije pacijenta
            FillPatientInfo();

            // Napravi medical record i njegov view model
            MedicalRecord newMedicalRecord = new MedicalRecord(SelectedPatient.Id, ParentName, MaritalStatus.Value, HealthCardNumber,
                BloodType.Value, Allergens, Diseases);
            MedicalRecordViewModel newMedicalRecordViewModel = new MedicalRecordViewModel(newMedicalRecord);

            // Dodamo medical Record u bazu i njegov ViewModel u tabelu
            _medicalRecordController.AddMedicalRecord(newMedicalRecord);
            MedicalRecords.Add(newMedicalRecordViewModel);

            // Stavimo pacijentov acc da ne bude vise guest
            ChangeAccountIsGuest();

            // Izbacimo pacijenta iz liste pacijenata koji nemaju karton
            Patients.Remove(SelectedPatient);

            // Zatvorimo window na kraju
            if (window != null)
                window.Close();
        }

        /// <summary>
        /// Popunjava informacije vezane za pacijenta iz inputa
        /// </summary>
        private void FillPatientInfo()
        {
            SelectedPatient.Email = Email;
            SelectedPatient.Gender = Gender;
            SelectedPatient.PhoneNumber = Phone;
            SelectedPatient.SocialSecurityNumber = SocialSecurityNumber;
            SelectedPatient.DateOfBirth = DateOfBirth;
            SelectedPatient.Address = new Address(Country, City, Street, StreetNumber);
        }

        /// <summary>
        /// Stavlja acc da ne bude guest, pozove nakon pravljenja kartona
        /// </summary>
        private void ChangeAccountIsGuest()
        {
            // Stavimo pacijentov acc da ne bude vise guest
            Account? acc = _accountController.GetAccountByPersonId(SelectedPatient.Id);
            if (acc != null)
            {
                acc.IsGuest = false;
            }
        }

        private bool AddMedicalRecordCanExecute()
        {
            if (SelectedPatient != null &&
                ParentName != "" &&
                HealthCardNumber != "" &&
                Phone != "" &&
                City != "" &&
                Country != "" &&
                Street != "" &&
                StreetNumber != "" &&
                MaritalStatus != null &&
                BloodType != null &&
                DateOfBirth != DateTime.MinValue)
            {
                return true;
            }

            return false;
        }


        #endregion
    }
}
