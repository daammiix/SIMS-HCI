using ClassDijagramV1._0.FileHandlers;
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
    public class StatisticalDataMainViewModel
    {
        #region Fields

        private FileHandler<DoctorRating> _fileHandler;

        private List<DoctorRating> _doctorsRatings;

        private DoctorController _doctorController;

        #endregion

        #region Properties

        public List<DoctorWithAverageGrade> DoctorsRatings { get; set; }

        public List<GradeWithNumber> Grades { get; set; }

        #endregion

        #region Constructor

        public StatisticalDataMainViewModel()
        {
            // Ucitamo raitinge i napravimo VM od njih
            _fileHandler = new FileHandler<DoctorRating>(App._doctorRatingsFilePath);

            _doctorsRatings = _fileHandler.GetItems();

            App app = Application.Current as App;

            _doctorController = app.DoctorController;

            CalculateGrades();

            CalculateDoctorsRatings();
        }

        #endregion

        #region Private Helpers

        private void CalculateGrades()
        {
            Grades = new List<GradeWithNumber>();
            Dictionary<int, int> numberOfGrades = new Dictionary<int, int>();

            for (int i = 1; i < 6; i++)
            {
                numberOfGrades.Add(i, 0);
            }

            _doctorsRatings.ForEach(doctorRating =>
            {
                // Na trecoj poziciji je ocena aplikacije
                numberOfGrades[doctorRating.Grades[3]] += 1;
            });

            for (int i = 1; i < 6; i++)
            {
                Grades.Add(new GradeWithNumber(i, numberOfGrades[i]));
            }
        }

        private void CalculateDoctorsRatings()
        {
            DoctorsRatings = new List<DoctorWithAverageGrade>();

            var query = _doctorsRatings.GroupBy(doctorRating => doctorRating.DoctorId);

            foreach (IGrouping<int, DoctorRating> group in query)
            {
                double avarageGrade = 0;
                foreach (DoctorRating doctorRating in group)
                {
                    avarageGrade += doctorRating.AverageGrade;
                }
                avarageGrade /= group.Count();

                string doctorFullName = GetDoctorFullName(group.Key);

                DoctorsRatings.Add(new DoctorWithAverageGrade(doctorFullName, avarageGrade));
            }
        }

        private string GetDoctorFullName(int id)
        {
            Doctor doctor = _doctorController.GetDoctorById(id);

            return doctor.Name + " " + doctor.Surname;
        }

        #endregion
    }
}
