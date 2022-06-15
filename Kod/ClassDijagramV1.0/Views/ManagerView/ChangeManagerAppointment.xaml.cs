using ClassDijagramV1._0.ViewModel;
using System.Windows;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Helpers;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeManagerAppointment.xaml
    /// </summary>
    public partial class ChangeManagerAppointment : Window
    {
        private ChangeManagerAppointmentViewModel _changeManagerAppointmentViewModel;
        public ChangeManagerAppointment(ManagerAppointment managerAppointment, IRefreshableManagerAppointmentView managerAppointmentView)
        {
            InitializeComponent();

            _changeManagerAppointmentViewModel = new ChangeManagerAppointmentViewModel(this, managerAppointment, managerAppointmentView);
            this.DataContext = _changeManagerAppointmentViewModel;
        }
    }
}
