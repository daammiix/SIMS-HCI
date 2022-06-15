using ClassDijagramV1._0.ViewModel.PatientViewModels;
using Controller;
using Model;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentReportPage.xaml
    /// </summary>
    public partial class AppointmentReportPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        private Appointment _appointment { get; set; }
        private DoctorController _doctorController;
        #endregion
        public AppointmentReportPage(PatientMainWindow patientMain, Appointment appointment)
        {
            InitializeComponent();
            parent = patientMain;
            _appointment = appointment;
            this.DataContext = new AppointmentReportViewModel(_appointment);
        }

        private void addNoteClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AddNotePage(parent, _appointment);
        }
    }
}
