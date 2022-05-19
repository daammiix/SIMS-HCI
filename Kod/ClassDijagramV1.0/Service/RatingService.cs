using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class RatingService
    {
        private RatingRepo _ratingRepo;

        public RatingService(RatingRepo ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        public void AddRatingHospital(HospitalRating hr)
        {
            _ratingRepo.AddRatingHospital(hr);
        }
        public void AddRatingDoctor(DoctorRating dr)
        {
            _ratingRepo.AddRatingDoctor(dr);
        }

        public ObservableCollection<HospitalRating> GetAllHospitalRatings()
        {
            return _ratingRepo.GetAllHospitalRatings();
        }

        public ObservableCollection<DoctorRating> GetAllDoctorRatings()
        {
            return _ratingRepo.GetAllDoctorRatings();
        }

        public void SaveHospitalRatings()
        {
            _ratingRepo.SaveHospitalRatings();
        }
        public void SaveDoctorRatings()
        {
            _ratingRepo.SaveDoctorRatings();
        }
    }
}
