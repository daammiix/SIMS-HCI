using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;

namespace ClassDijagramV1._0.Service
{
    public class BanningPatientService
    {
        private ActivityService _activityService;
        private AccountRepo _accountRepository;

        public BanningPatientService(ActivityService activityService, AccountRepo accountRepository)
        {
            _activityService = activityService;
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
                BanPatient(account);
                IsBanned = true;
            }
            return IsBanned;

        }

        private void BanPatient(Account account)
        {
            account.Banned = true;
            _accountRepository.UpdateAccount(account);
        }
    }
}
