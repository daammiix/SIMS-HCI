using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
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

        private FileHandler<Patient> _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<Patient> Patients { get; set; }

        #endregion

        #region Constructor

        public PatientRepo(FileHandler<Patient> fileHandler)
        {
            _fileHandler = fileHandler;

            Patients = new ObservableCollection<Patient>(fileHandler.GetItems());

            // Sad procitamo i zdravstvene kartone da bi vezali svakog pacijenta za svoj zdravstveni karton
            FileHandler<MedicalRecord> _fileHandlerMedicalRepo = new FileHandler<MedicalRecord>(App._medicalRecordFilePath);

            // Vezemo pacijente za njihove kartone
            foreach (Patient patient in Patients)
            {
                foreach (MedicalRecord medicalRecord in _fileHandlerMedicalRepo.GetItems())
                {
                    if (medicalRecord.PatientId == patient.Id)
                    {
                        patient.MedicalRecordNumber = medicalRecord.Number;
                    }
                }
            }

        }

        #endregion

        #region Methods

        public ObservableCollection<Patient> GetPatients()
        {
            return Patients;
        }

        public void SavePatients()
        {
            _fileHandler.SaveItems(Patients.ToList());
        }

        public void AddPatient(Patient newPatient)
        {
            // Ako nema pacijenta sa istim idem i jmbg-om dodajemo novog, ako postoji pregazimo starog
            bool exists = false;
            Patient? toUpdate = null;
            foreach (Patient pat in Patients)
            {
                if (pat.Id == newPatient.Id || pat.Jmbg.Equals(newPatient.Jmbg))
                {
                    toUpdate = pat;
                    exists = true;
                    break;
                }
            }
            if (toUpdate == null)
            {
                toUpdate = newPatient;
            }
            if (!exists)
            {
                Patients.Add(newPatient);
            }
        }

        public bool RemovePatient(int id)
        {
            Patient patientToRemove = null;
            foreach (Patient patient in Patients)
            {
                if (patient.Id == id)
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
                if (patient.Id == pat.Id)
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

        /// <summary>
        /// Vraca pacijenta sa zadatim id-em u suprotnom vraca null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetPatientById(int id)
        {
            Patient? ret = null;
            foreach (Patient patient in Patients)
            {
                if (patient.Id == id)
                {
                    ret = patient;
                }
            }

            return ret;
        }

        #endregion
    }
}
