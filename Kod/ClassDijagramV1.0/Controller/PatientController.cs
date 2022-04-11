using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class PatientController
    {
        #region Fields

        private PatientService _patientService;

        #endregion

        #region Constructor

        public PatientController(PatientService ps)
        {
            _patientService = ps;
        }

        #endregion
        public ObservableCollection<Patient> GetPatients()
        {
            return _patientService.GetPatients();
        }

        public void SavePatients()
        {
            _patientService.SavePatients();
        }

        public void AddPatient(Patient newPatient)
        {
            _patientService.AddPatient(newPatient);
        }

        public void RemovePatient(String username)
        {
            _patientService.RemovePatient(username);
        }

        public void UpdatePatient(Patient patient)
        {
            _patientService.UpdatePatient(patient);
        }

    }
}