using ClassDijagramV1._0.Controller;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for OldAppointmentsPage.xaml
    /// </summary>
    public partial class OldAppointmentsPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<AppointmentViewModel> OldAppointments { get; set; }
        #endregion
        public OldAppointmentsPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> oldAppointmentViewModels)
        {
            InitializeComponent();
            this.DataContext = this;
            //this.DataContext = new OldAppointmentsViewModel();
            parent = patientMain;
            OldAppointments = oldAppointmentViewModels;
        }

        private void openSpecificAppointmentClick(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel selectedAppointment = (AppointmentViewModel)tabelaStariPregledi.SelectedItem;
            parent.startWindow.Content = new AppointmentReportPage(parent, selectedAppointment.Appointment);
        }
    }
}
