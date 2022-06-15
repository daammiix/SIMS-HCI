using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System.Collections.ObjectModel;

namespace ClassDijagramV1._0.Controller
{
    public class RatingController
    {
        private RatingService _ratingservice;

        public RatingController(RatingService ratingservice)
        {
            _ratingservice = ratingservice;
        }

        /// <summary>
        /// Dodaje ocjenu bolnice
        /// </summary>
        /// <param name="hr"></param>
        /// <returns></returns>
        public void AddRatingHospital(HospitalRating hr)
        {
            _ratingservice.AddRatingHospital(hr);
        }

        /// <summary>
        /// Dodaje ocjenu doktora
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public void AddRatingDoctor(DoctorRating dr)
        {
            _ratingservice.AddRatingDoctor(dr);
        }

        /// <summary>
        /// Vraca sve ocjene bolnice
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<HospitalRating> GetAllHospitalRatings()
        {
            return _ratingservice.GetAllHospitalRatings();
        }

        /// <summary>
        /// Vraca sve ocjene doktora
        /// </summary>
        /// <returns></returns>
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
