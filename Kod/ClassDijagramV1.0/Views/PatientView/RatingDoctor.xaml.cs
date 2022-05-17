using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RatingDoctor.xaml
    /// </summary>
    public partial class RatingDoctor : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        // pacijentovi appointmenti, lista appointmentViewModela prosledjena konstruktoru
        private ObservableCollection<AppointmentViewModel> _patientAppointments;
        //public ObservableCollection<AppointmentViewModel> Appointments;
        // kontroleri
        public AppointmentController _appointmentController;
        public DoctorController _doctorController;

        public ObservableCollection<Appointment> Appointments { get; private set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        private RatingController _ratingController;
        public ObservableCollection<DoctorRating> DoctorRatings { get; set; }
        public RatingDoctor(ObservableCollection<AppointmentViewModel> appointments,PatientMainWindow patientMain, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            this.DataContext = this;
            _logedPatient = logedPatient;
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _doctorController = app.DoctorController;
            _ratingController = app.RatingController;

            //Appointments = appointments;
            Appointments = _appointmentController.GetAppointments();
            Doctors = new ObservableCollection<Doctor>();
            foreach (Appointment a in Appointments)
            {
                if ( (a.PatientId == _logedPatient.Id)
                    && (a.AppointmentDate < DateTime.Now)
                    && (!Doctors.Contains(_doctorController.GetDoctorById(a.DoctorId))) )
                {
                    Doctors.Add(_doctorController.GetDoctorById(a.DoctorId));
                }
                
            }
            DoctorRatings = _ratingController.GetAllDoctorRatings();
        }

        private void SendRatingDoctorResult(object sender, RoutedEventArgs e)
        {
            List<int> ocjene = new List<int>() { pitanje1.Value, pitanje2.Value, pitanje3.Value, pitanje4.Value };
            string comment = komentar.Text;
            Doctor d1 = (Doctor)doktoriSaPregleda.SelectedItem;

            DoctorRating dr = new DoctorRating(d1.Id,ocjene, ocjene.Average(), comment);

            _ratingController.AddRatingDoctor(dr);

            parent.startWindow.Content = new PatientMainPage(parent, _logedPatient);
        }
        private void EverythingRated(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (pitanje1.Value != 0 && pitanje2.Value != 0 && pitanje2.Value != 0 && pitanje4.Value != 0)
            {
                ratingBtn.IsEnabled = true;
            }
        }
    }
}
