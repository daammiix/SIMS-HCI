using ClassDijagramV1._0.FileHandlers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class PatientRepo
    {
        #region Fields

        private PatientFileHandler _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<Patient> Patients { get; set; }

        #endregion

        #region Constructor

        public PatientRepo(PatientFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            Patients = new ObservableCollection<Patient>(fileHandler.GetPatients());
        }

        #endregion

        #region Methods

        public ObservableCollection<Patient> GetPatients()
        {
            return Patients;
        }

        public void SavePatients()
        {
            _fileHandler.SavePatients(Patients.ToList());
        }

        public void AddPatient(Patient newPatient)
        {
            foreach (Patient pat in Patients)
            {
                if (pat.Account.Username.Equals(newPatient.Account.Username))
                    return;
            }
            // Ako ne postoji pacijent sa istim usernameom dodaj ga
            Patients.Add(newPatient);
        }

        public bool RemovePatient(String username)
        {
            Patient patientToRemove = null;
            foreach (Patient patient in Patients)
            {
                if (patient.Account != null)
                    if (patient.Account.Username.Equals(username))
                    {
                        patientToRemove = patient;
                        break;
                    }
            }
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
                // pacijent upesno obrisan
                return true;
            }
            // nije bilo pacijenta sa tim usernameom
            return false;
        }

        public bool UpdatePatient(Patient patient)
        {
            Patient patientToUpdate = null;
            foreach (Patient pat in Patients)
            {
                if (patient.Account != null && pat.Account != null)
                    if (pat.Account.Username.Equals(patient.Account.Username))
                    {
                        patientToUpdate = pat;
                        break;
                    }
            }

            if (patientToUpdate != null)
            {
                patientToUpdate = patient;
                return true;
            }

            return false;
        }

        #endregion
    }
}
