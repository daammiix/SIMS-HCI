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

        /// <summary>
        /// Provjerava status pacijenta, da li je banovan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean CheckStatusOfPatient(int patientID, Account account)
        {
            return _banningPatientService.CheckStatusOfPatient(patientID, account);
        }
    }
}
