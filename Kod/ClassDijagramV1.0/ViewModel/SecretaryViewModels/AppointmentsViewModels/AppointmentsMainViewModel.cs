namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AppointmentsMainViewModel
    {
        #region Properties

        public ScheduleViewModel ScheduleViewModel { get; set; }

        #endregion

        #region Constructor

        public AppointmentsMainViewModel()
        {
            ScheduleViewModel = new ScheduleViewModel();
        }

        #endregion

    }
}
