using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class RatingRepo
    {
        private String Path;
        private FileHandler<HospitalRating> _hospitalRatingtFileHandler;
        private FileHandler<DoctorRating> _doctorRatingtFileHandler;

        public ObservableCollection<HospitalRating> HospitalRatings;
        public ObservableCollection<DoctorRating> DoctorRatings;

        public RatingRepo(FileHandler<HospitalRating> fileHandlerHospital, FileHandler<DoctorRating> fileHandlerDoctor)
        {
            _hospitalRatingtFileHandler = fileHandlerHospital;
            _doctorRatingtFileHandler = fileHandlerDoctor;
            HospitalRatings = new ObservableCollection<HospitalRating>(_hospitalRatingtFileHandler.GetItems());
            DoctorRatings = new ObservableCollection<DoctorRating>(_doctorRatingtFileHandler.GetItems());
        }

        public ObservableCollection<HospitalRating> GetAllHospitalRatings()
        {
            return HospitalRatings;
        }
        public ObservableCollection<DoctorRating> GetAllDoctorRatings()
        {
            return DoctorRatings;
        }
        public void AddRatingHospital(HospitalRating hr)
        {
            HospitalRatings.Add(hr);
        }
        public void AddRatingDoctor(DoctorRating dr)
        {
            DoctorRatings.Add(dr);
        }

        public void SaveHospitalRatings()
        {
            _hospitalRatingtFileHandler.SaveItems(HospitalRatings.ToList());
        }
        public void SaveDoctorRatings()
        {
            _doctorRatingtFileHandler.SaveItems(DoctorRatings.ToList());
        }
    }
}
