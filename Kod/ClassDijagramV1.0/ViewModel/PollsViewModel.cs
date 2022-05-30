using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class PollsViewModel
    {
        RatingController ratingController;
        DoctorController doctorController;
        ReportsController reportsController;

        ObservableCollection<DoctorRating> doctorRatings;
        public ObservableCollection<HospitalRating> hospitalRatings { get; set; }
        private BindingList<Reports> _reports = new BindingList<Reports>();
        private BindingList<Reports> _allReports;

        String _searchText = "";

        public ObservableCollection<DoctorRating> doctor4Results { get; set; }
        public ObservableCollection<DoctorRating> doctor5Results { get; set; }

        Doctor doctor4;
        Doctor doctor5;

        public PollsViewModel()
        {
            var app = Application.Current as App;

            ratingController = app.RatingController;
            doctorController = app.DoctorController;
            reportsController = app.reportsController;

            doctorRatings = ratingController.GetAllDoctorRatings();
            hospitalRatings = ratingController.GetAllHospitalRatings();

            doctor4Results = new ObservableCollection<DoctorRating>();
            doctor5Results = new ObservableCollection<DoctorRating>();

            doctor4 = doctorController.GetDoctorById(4);
            doctor5 = doctorController.GetDoctorById(5);

            _allReports = reportsController.GetAllReports();
            _allReports.ListChanged += _allReportsChanged;

            doctorRatingResults();
        }

        public BindingList<Reports> AllReports
        {
            get
            {
                return _reports;
            }
            set
            {
                if (_reports == value)
                {
                    return;
                }
                _reports = value;
            }
        }

        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                RefreshReports();
            }
        }

        public void doctorRatingResults()
        { 
            foreach (var doctor in doctorRatings)
            {
                if(doctor.DoctorId == 4)
                {
                    doctor4Results.Add(doctor);
                }
                else if(doctor.DoctorId == 5)
                {
                    doctor5Results.Add(doctor);
                }
            }
        }

        public void RefreshReports()
        {
            AllReports.Clear();
            foreach (Reports report in _allReports)
            {
                String search = _searchText.ToLower();
                if (
                    !report.Name.ToLower().Contains(search) &&
                    !report.Date.ToLower().Contains(search))
                {
                    continue;
                }
                AllReports.Add(report);
            }
        }

        public void _allReportsChanged(object? sender, ListChangedEventArgs e)
        {
            RefreshReports();
        }
    }
}
