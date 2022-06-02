using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class QuarterlyReportsViewModel : ObservableObject
    {
        QuarterlyReportsController QuarterlyReportsController { get; set; }
        ReportsController reportsController;

        private BindingList<Reports> _reports = new BindingList<Reports>();
        private BindingList<Reports> _allReports;
        public BindingList<QuarterlyReport> quarterlyReports { get; set; }

        String _searchText = "";
        public Reports selectedReport { get; set; }
        Window? window;

        public BindingList<QuarterlyReport> report1 { get; set; }
        public BindingList<QuarterlyReport> report2 { get; set; }

        private RelayCommand _openReport;
        private RelayCommand _closeReport;

        public QuarterlyReportsViewModel(Window? window)
        {
            var app = Application.Current as App;

            QuarterlyReportsController = app.QuarterlyReportsController;
            reportsController = app.reportsController;

            this.quarterlyReports = QuarterlyReportsController.GetAllQuarterlyReports();
            report1 = new BindingList<QuarterlyReport>();
            report2 = new BindingList<QuarterlyReport>();

            _allReports = reportsController.GetAllReports();
            _allReports.ListChanged += _allReportsChanged;

            this.window = window;

            RefreshReports();
            quarterlyReportsResults();
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
                    if (window != null)
                    {
                        window.Close();
                    }
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

        public void quarterlyReportsResults()
        {
            foreach (var report in quarterlyReports)
            {
                if (report.Type == "Javne nabavke")
                {
                    report1.Add(report);
                }
                else if (report.Type == "Lekovi")
                {
                    report2.Add(report);
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
            if (selectedReport.ID.Equals("id16"))
            {
                QuarterlyReport1View quarterlyReport1View = new QuarterlyReport1View();
                quarterlyReport1View.Show();
            }
            if (selectedReport.ID.Equals("id18"))
            {
                QuarterlyReport2View quarterlyReport2View = new QuarterlyReport2View();
                quarterlyReport2View.Show();
            }
        }
    }
}
