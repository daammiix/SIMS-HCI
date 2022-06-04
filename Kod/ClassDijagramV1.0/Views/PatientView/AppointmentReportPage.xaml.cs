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

            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            Doctor doctor = _doctorController.GetDoctorById(_appointment.DoctorId);

            datum.Content = _appointment.AppointmentDate;
            doktor.Content = doctor.Name + " " + doctor.Surname;
            doctorReport.Text = _appointment.MedicalReport.Description;
            note.Text = _appointment.MedicalReport.Note;
        }

        private void addNoteClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AddNotePage(parent, _appointment);
        }
    }
}
