using Controller;
using Model;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AddNotePage.xaml
    /// </summary>
    public partial class AddNotePage : Page
    {
        private PatientMainWindow parent { get; set; }
        private Appointment _appointment { get; set; }
        public DoctorController _doctorController;
        public AddNotePage(PatientMainWindow patientMain, Appointment appointment)
        {
            InitializeComponent();
            parent = patientMain;
            _appointment = appointment;
            biljeska.Text = _appointment.MedicalReport.Note;
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            Doctor doctor = _doctorController.GetDoctorById(_appointment.DoctorId);
            datum.Content = _appointment.AppointmentDate;
            doktor.Content = doctor.Name + " " + doctor.Surname;
        }

        private void addNoteClick(object sender, RoutedEventArgs e)
        {
            _appointment.MedicalReport.Note = biljeska.Text;
            parent.startWindow.Content = new AppointmentReportPage(parent, _appointment);
        }
    }
}
