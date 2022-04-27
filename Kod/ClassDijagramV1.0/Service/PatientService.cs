using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Service
{
    public class PatientService
    {
        #region Fields

        PatientRepo _patientRepo;

        #endregion

        #region Constructor

        public PatientService(PatientRepo repo)
        {
            _patientRepo = repo;
        }

        #endregion

        #region Methods

        public ObservableCollection<Patient> GetPatients()
        {
            return _patientRepo.GetPatients();
        }

        public void SavePatients()
        {
            _patientRepo.SavePatients();
        }

        public void AddPatient(Patient newPatient)
        {
            _patientRepo.AddPatient(newPatient);
        }

        public void RemovePatient(int id)
        {
            _patientRepo.RemovePatient(id);
        }

        public void UpdatePatient(Patient patient)
        {
            _patientRepo.UpdatePatient(patient);
        }

        public Patient GetPatientById(int id)
        {
            return _patientRepo.GetPatientById(id);
        }

        #endregion
    }
}