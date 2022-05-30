using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentReportPage.xaml
    /// </summary>
    public partial class AppointmentReportPage : Page
    {
        private PatientMainWindow parent { get; set; }
        private Appointment _appointment { get; set; }
        public DoctorController _doctorController;
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
