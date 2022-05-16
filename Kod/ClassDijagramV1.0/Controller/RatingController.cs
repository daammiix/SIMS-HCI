using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class RatingController
    {
        private RatingService _ratingservice;

        public RatingController(RatingService ratingservice)
        {
            _ratingservice = ratingservice;
        }



        public void AddRatingHospital(HospitalRating hr)
        {
            _ratingservice.AddRatingHospital(hr);
        }

        public void AddRatingDoctor(DoctorRating dr)
        {
            _ratingservice.AddRatingDoctor(dr);
        }

        public ObservableCollection<HospitalRating> GetAllHospitalRatings()
        {
            return _ratingservice.GetAllHospitalRatings();
        }

        public ObservableCollection<DoctorRating> GetAllDoctorRatings()
        {
            return _ratingservice.GetAllDoctorRatings();
        }

        public void SaveHospitalRatings()
        {
            _ratingservice.SaveHospitalRatings();
        }
        public void SaveDoctorRatings()
        {
            _ratingservice.SaveDoctorRatings();
        }
    }
}
