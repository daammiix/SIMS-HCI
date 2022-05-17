using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class BanningPatientService
    {
        private ActivityService _activityService;
        private PatientRepo _patientRepository;
        private AccountRepo _accountRepository;

        public BanningPatientService(ActivityService activityService, PatientRepo patientRepository, AccountRepo accountRepository)
        {
            _activityService = activityService;
            _patientRepository = patientRepository;
            _accountRepository = accountRepository;
        }

        public Boolean CheckStatusOfPatient(int patientID, Account account)
        {
            Boolean IsBanned = false;
            int numberMake = _activityService.NumberOfActivity(patientID, TypeOfActivity.makeAppointment);
            int numberEdit = _activityService.NumberOfActivity(patientID, TypeOfActivity.editAppointment);
            int numberCancel = _activityService.NumberOfActivity(patientID, TypeOfActivity.cancelAppointment);

            if (numberMake > 5 || numberEdit > 5 || numberCancel > 5)
            {
                BanPatient(patientID, account);
                IsBanned = true;
            }

            return IsBanned;

        }

        private void BanPatient(int patientID, Account account)
        {
            foreach (Patient p in _patientRepository.GetPatients())
            {
                if (p.Id == patientID)
                {
                    SetInformationsAboutBanning(patientID, account);
                }
            }
        }

        private void SetInformationsAboutBanning(int patientID, Account account)
        {
            account.Banned = true;
            _accountRepository.UpdateAccount(account);
        }

    }
}
