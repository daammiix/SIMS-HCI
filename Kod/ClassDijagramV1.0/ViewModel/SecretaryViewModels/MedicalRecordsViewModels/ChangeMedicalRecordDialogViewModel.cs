using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System.Collections.Generic;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels
{
    public class ChangeMedicalRecordDialogViewModel
    {

        #region Fields

        private MedicalRecordViewModel _medicalRecordViewModel;

        private RelayCommand _saveCommand;

        private MedicalRecordController _medicalRecordController;

        #endregion

        #region Properties

        public int Number { get; set; }

        public string ParentName { get; set; }

        public string HealthCardNumber { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public BloodType BloodType { get; set; }

        public List<string> Allergens { get; set; }

        public List<string> Diseases { get; set; }

        public bool IsSingle { get; set; }

        public bool IsMarried { get; set; }

        public bool IsDivorced { get; set; }

        public bool IsWidow { get; set; }

        public bool Is0 { get; set; }

        public bool IsA { get; set; }

        public bool IsB { get; set; }

        public bool IsAB { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Cuva izmene zdravstvenog kartona i zatvara dialog
        /// </summary>
        public RelayCommand Save
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(o => SaveExecute(o), SaveCanExecute);
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Constructor

        public ChangeMedicalRecordDialogViewModel(MedicalRecordViewModel mrViewModel)
        {
            _medicalRecordViewModel = mrViewModel;

            App app = Application.Current as App;

            _medicalRecordController = app.MedicalRecordController;

            IsSingle = false;
            IsMarried = false;
            IsDivorced = false;
            IsWidow = false;

            // Stavimo polja na polaj iz mrViewModela
            Number = _medicalRecordViewModel.MedicalRecord.Number;
            ParentName = _medicalRecordViewModel.MedicalRecord.ParentName;
            HealthCardNumber = _medicalRecordViewModel.MedicalRecord.HealthCardNumber;
            MaritalStatus = _medicalRecordViewModel.MedicalRecord.MaritalStatus;
            SetMaritalStatusRadioButton();
            BloodType = _medicalRecordViewModel.MedicalRecord.BloodType;
            SetBloodTypeRadioButton();
            Allergens = _medicalRecordViewModel.MedicalRecord.Allergens;
            Diseases = _medicalRecordViewModel.MedicalRecord.Diseases;
        }

        #endregion

        #region Private Helpers

        private void SaveExecute(object o)
        {
            Window win = o as Window;

            if (win != null)
            {
                // Updatujemo medical record
                _medicalRecordViewModel.MedicalRecord.Number = Number;
                _medicalRecordViewModel.MedicalRecord.ParentName = ParentName;
                _medicalRecordViewModel.MedicalRecord.HealthCardNumber = HealthCardNumber;
                UpdateMaritalStatus();
                _medicalRecordViewModel.MedicalRecord.MaritalStatus = MaritalStatus;
                UpdateBloodType();
                _medicalRecordViewModel.MedicalRecord.BloodType = BloodType;
                _medicalRecordViewModel.MedicalRecord.Allergens = Allergens;
                _medicalRecordViewModel.MedicalRecord.Diseases = Diseases;
                win.Close();
            }
        }

        private bool SaveCanExecute()
        {
            if (ParentName != "" && HealthCardNumber != "" && Number.ToString() != ""
                && _medicalRecordController.IsMedicalRecordNumberUnique(Number, _medicalRecordViewModel.MedicalRecord))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Stavlja odredjeni properti na true u zavistnosti od Marital Statusa kako bi odgovarajuci radio button bio cekiran
        /// </summary>
        private void SetMaritalStatusRadioButton()
        {
            switch (MaritalStatus)
            {
                case MaritalStatus.Single:
                    {
                        IsSingle = true;
                        break;
                    }
                case MaritalStatus.Married:
                    {
                        IsMarried = true;
                        break;
                    }
                case MaritalStatus.Divorced:
                    {
                        IsDivorced = true;
                        break;
                    }
                case MaritalStatus.Widow:
                    {
                        IsWidow = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Updatuje Marital Status u zavisnosti od cekiranog radio buttona
        /// </summary>
        private void UpdateMaritalStatus()
        {
            if (IsSingle)
            {
                MaritalStatus = MaritalStatus.Single;
            }
            else if (IsMarried)
            {
                MaritalStatus = MaritalStatus.Married;
            }
            else if (IsDivorced)
            {
                MaritalStatus = MaritalStatus.Divorced;
            }
            else
            {
                MaritalStatus = MaritalStatus.Widow;
            }
        }

        /// <summary>
        /// Stavlja odredjeni properti na true kako bi odgovarajuci radio button bio cekiran
        /// </summary>
        public void SetBloodTypeRadioButton()
        {
            switch (BloodType)
            {
                case BloodType.O:
                    {
                        Is0 = true;
                        break;
                    }
                case BloodType.A:
                    {
                        IsA = true;
                        break;
                    }
                case BloodType.B:
                    {
                        IsB = true;
                        break;
                    }
                case BloodType.AB:
                    {
                        IsAB = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Updatuje Blood Type u zavisnosti od selektovanog radio buttona
        /// </summary>
        public void UpdateBloodType()
        {
            if (Is0)
            {
                BloodType = BloodType.O;
            }
            else if (IsA)
            {
                BloodType = BloodType.A;
            }
            else if (IsB)
            {
                BloodType = BloodType.B;
            }
            else
            {
                BloodType = BloodType.AB;
            }
        }

        #endregion
    }
}
