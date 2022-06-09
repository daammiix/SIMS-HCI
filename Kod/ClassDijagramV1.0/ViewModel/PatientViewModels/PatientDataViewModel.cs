using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.PatientViewModels
{
    public class PatientDataViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Jmbg { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string Blood { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        private PatientMainWindow parent { get; set; }

        public PatientDataViewModel(PatientMainWindow patientMain)
        {
            parent = patientMain;
            FillFields();
        }

        private void FillFields()
        {
            App app = Application.Current as App;
            MedicalRecordController _medicalRecordController = app.MedicalRecordController;

            Patient patient = parent.Patient;
            MedicalRecord medicalRecord = _medicalRecordController.GetPatientsMedicalRecord(patient.Id);

            Name = patient.Name + " " + patient.Surname;
            Jmbg = patient.Jmbg;
            DateOfBirth = patient.DateOfBirth.ToString("dd.MM.yyyy");
            Address = patient.Address.StreetName + " " +
                            patient.Address.StreetNumber + ", " +
                            patient.Address.City;
            Gender = (patient.Gender.ToString() == "Male") ? "Muško" : "Žensko";
            MedicalRecordNumber = patient.MedicalRecordNumber.ToString();
            Blood = medicalRecord.BloodType.ToString();
            PhoneNumber = patient.PhoneNumber;
            Email = patient.Email;  
        }
    }
}
