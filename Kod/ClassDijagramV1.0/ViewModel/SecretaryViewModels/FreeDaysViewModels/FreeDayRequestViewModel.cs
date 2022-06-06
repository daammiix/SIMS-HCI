using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.FreeDaysViewModels
{
    public class FreeDayRequestViewModel
    {
        #region Fields

        private FreeDayRequest _freeDayRequest;

        private DoctorController _doctorController;

        // Doktor koji je poslao zahtev za slobodne dane
        private Doctor _doctor;

        #endregion

        #region Properties

        // Mora da bi mogo da se obrise iz repozitorijuma, pogledaj FreeDaysMainViewModel AcceptExecute
        public FreeDayRequest FreeDayRequest
        {
            get { return _freeDayRequest; }
        }

        public Doctor Doctor
        {
            get { return _doctor; }
        }

        public DateTime From
        {
            get { return _freeDayRequest.From; }
        }

        public DateTime To
        {
            get { return _freeDayRequest.To; }
        }

        public string Description
        {
            get { return _freeDayRequest.Description; }
            set
            {
                if (_freeDayRequest.Description != value)
                {
                    _freeDayRequest.Description = value;
                }
            }
        }

        #endregion

        #region Constructor

        public FreeDayRequestViewModel(FreeDayRequest freeDayRequest)
        {
            _freeDayRequest = freeDayRequest;

            App app = Application.Current as App;

            _doctorController = app.DoctorController;

            _doctor = _doctorController.GetDoctorById(_freeDayRequest.DoctorId);
        }

        #endregion

    }
}
