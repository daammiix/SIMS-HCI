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

using ClassDijagramV1._0.Views;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentUpdatePage.xaml
    /// </summary>
    public partial class AppointmentUpdatePage : Page
    {
        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;

        private Patient _logedPatient;

        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        //public ObservableCollection<Room> Rooms { get; set; }
        public BindingList<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<String> DoctorsAppointmentsTime { get; set; }
        public AppointmentUpdatePage(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels, Patient logedPatient)
        {
            InitializeComponent();

            _appointmentViewModels = appointmentViewModels;

            _logedPatient = logedPatient;

            this.DataContext = this;
            parent = patientMain;
            doctorRB.IsChecked = true;
            dr.Text = AppointmentsViewPage.SelectedAppointment.Doctor.Name + " " + AppointmentsViewPage.SelectedAppointment.Doctor.Surname;
            date.Text = AppointmentsViewPage.SelectedAppointment.AppointmentDate.ToString("HH:mm dd.MM.yyyy.");
            room.Text = AppointmentsViewPage.SelectedAppointment.Room.RoomName.ToString();
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new UpdatePriorityDoctor(parent, _appointmentViewModels, _logedPatient);
        }

        private void timeRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new UpdatePriorityTime(parent, _appointmentViewModels, _logedPatient);
        }

    }
}
