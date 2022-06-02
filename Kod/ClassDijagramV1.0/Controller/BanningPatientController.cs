using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;

namespace ClassDijagramV1._0.Controller
{
    public class BanningPatientController
    {
        private BanningPatientService _banningPatientService;

        public BanningPatientController(BanningPatientService banningPatientService)
        {
            _banningPatientService = banningPatientService;
        }
        public Boolean CheckStatusOfPatient(int patientID, Account account)
        {
            return _banningPatientService.CheckStatusOfPatient(patientID, account);
        }
    }
}
