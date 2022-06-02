using ClassDijagramV1._0.Util;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ReportsViewModel : ObservableObject
    {
        private RelayCommand _polls;
        private RelayCommand _quarterlyReports;

        public PollsViewModel PollsVM { get; set; }
        public QuarterlyReportsViewModel QuarterlyReportsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        public ReportsViewModel()
        {
            Window window = new Window();
            window = null;
            PollsVM = new PollsViewModel(window);
            QuarterlyReportsVM = new QuarterlyReportsViewModel(window);

            CurrentView = PollsVM;
        }

        public RelayCommand Polls
        {
            get
            {
                _polls = new RelayCommand(o =>
                {
                    CurrentView = PollsVM;
                });

                return _polls;
            }
        }

        public RelayCommand QuarterlyReports
        {
            get
            {
                _quarterlyReports = new RelayCommand(o =>
                {
                    CurrentView = QuarterlyReportsVM;
                });

                return _quarterlyReports;
            }
        }
    }
}
