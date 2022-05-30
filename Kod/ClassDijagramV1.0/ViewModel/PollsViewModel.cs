using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
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
using System.Windows.Input;

namespace ClassDijagramV1._0.ViewModel
{
    public class PollsViewModel : ObservableObject
    {
        RatingController ratingController;
        ReportsController reportsController;

        ObservableCollection<DoctorRating> doctorRatings;
        public ObservableCollection<HospitalRating> hospitalRatings { get; set; }
        private BindingList<Reports> _reports = new BindingList<Reports>();
        private BindingList<Reports> _allReports;

        String _searchText = "";
        public Reports selectedReport { get; set; }
        Window window;

        public ObservableCollection<DoctorRating> doctor4Results { get; set; }
        public ObservableCollection<DoctorRating> doctor5Results { get; set; }

        private RelayCommand _openReport;
        private RelayCommand _closeReport;

        public PollsViewModel(Window window)
        {
            var app = Application.Current as App;

            ratingController = app.RatingController;
            reportsController = app.reportsController;

            doctorRatings = ratingController.GetAllDoctorRatings();
            hospitalRatings = ratingController.GetAllHospitalRatings();

            doctor4Results = new ObservableCollection<DoctorRating>();
            doctor5Results = new ObservableCollection<DoctorRating>();

            _allReports = reportsController.GetAllReports();
            _allReports.ListChanged += _allReportsChanged;

            this.window = window;

            RefreshReports();
            doctorRatingResults();
        }

        public RelayCommand OpenReport
        {
            get
            {
                _openReport = new RelayCommand(o =>
                {
                    OpenReportsAction();
                });

                return _openReport;
            }
        }

        public RelayCommand CloseReport
        {
            get
            {
                _closeReport = new RelayCommand(o =>
                {
                    window.Close();
                });

                return _closeReport;
            }
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


        public void OpenReportsAction()
        {
            if (selectedReport.ID.Equals("id4"))
            {
                Doctor4Results doctor4Results = new Doctor4Results();
                doctor4Results.Show();
            }
            if (selectedReport.ID.Equals("id5"))
            {
                Doctor5Results doctor5Results = new Doctor5Results();
                doctor5Results.Show();
            }
            if (selectedReport.ID.Equals("id8"))
            {
                HospitalResults hospitalResults = new HospitalResults();
                hospitalResults.Show();
            }
        }

    }
}
