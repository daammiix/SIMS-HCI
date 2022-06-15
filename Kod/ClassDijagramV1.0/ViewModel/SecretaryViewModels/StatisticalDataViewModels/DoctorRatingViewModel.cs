using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.StatisticalDataViewModels
{
    public class DoctorRatingViewModel
    {
        #region Fields

        private DoctorRating _doctorRating;

        private DoctorController _doctorController;

        // Doktor za kog je rating vezan
        private Doctor _doctor;

        #endregion

        #region Properties

        public Doctor Doctor
        {
            get { return _doctor; }
        }

        public DoctorRating DoctorRating
        {
            get { return _doctorRating; }
        }

        public double AvarageGrade
        {
            get
            {
                return _doctorRating.AverageGrade;
            }
        }

        public string DoctorFullName
        {
            get
            {
                return _doctor.Name + " " + _doctor.Surname;
            }
        }

        #endregion

        #region Constructor

        public DoctorRatingViewModel(DoctorRating doctorRating)
        {
            _doctorRating = doctorRating;

            App app = Application.Current as App;

            _doctorController = app.DoctorController;

            _doctor = _doctorController.GetDoctorById(_doctorRating.DoctorId);

        }

        #endregion
    }
}
