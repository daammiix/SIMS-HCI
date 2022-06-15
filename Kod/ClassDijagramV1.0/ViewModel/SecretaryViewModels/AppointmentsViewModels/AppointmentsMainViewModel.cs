using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.SecretaryView.AppointmentsView;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AppointmentsMainViewModel
    {
        #region Fields

        private RelayCommand _generateCommand;

        #endregion

        #region Properties

        public ScheduleViewModel ScheduleViewModel { get; set; }

        public RelayCommand GenerateCommand
        {
            get
            {
                if (_generateCommand == null)
                {
                    _generateCommand = new RelayCommand(o => { showGenerateWindow(); });
                }

                return _generateCommand;
            }
        }

        #endregion

        #region Constructor

        public AppointmentsMainViewModel()
        {
            ScheduleViewModel = new ScheduleViewModel();
        }

        #endregion

        #region Private Helpers

        private void showGenerateWindow()
        {
            Window generateWindow = new GenerateReportWindow();
            generateWindow.Owner = Application.Current.MainWindow;
            generateWindow.Show();
        }

        #endregion

    }
}
