using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for RatingDoctor.xaml
    /// </summary>
    public partial class RatingDoctor : Page
    {
        #region Fields

        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _oldAppointmentViewModels;
        private DoctorController _doctorController;
        private RatingController _ratingController;
        public ObservableCollection<Doctor> Doctors { get; set; }

        #endregion
        public RatingDoctor(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
            this.DataContext = this;
            _oldAppointmentViewModels = appointmentViewModels;

            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _ratingController = app.RatingController;

            Doctors = new ObservableCollection<Doctor>();
            FillDoctorsComboBox();
            
        }

        /// <summary>
        /// Puni listu doktora
        /// </summary>
        /// <returns></returns>
        private void FillDoctorsComboBox()
        {
            foreach (AppointmentViewModel a in _oldAppointmentViewModels)
            {
                if (!Doctors.Contains(_doctorController.GetDoctorById(a.Appointment.DoctorId)))
                {
                    Doctors.Add(_doctorController.GetDoctorById(a.Appointment.DoctorId));
                }
            }
        }

        /// <summary>
        /// Salje doktorovu ocjenu
        /// </summary>
        /// <returns></returns>
        private void SendRatingDoctorResult(object sender, RoutedEventArgs e)
        {
            List<int> ocjene = new List<int>() { pitanje1.Value, pitanje2.Value, pitanje3.Value, pitanje4.Value };
            string comment = komentar.Text;
            Doctor d1 = (Doctor)doktoriSaPregleda.SelectedItem;

            DoctorRating dr = new DoctorRating(d1.Id, ocjene, ocjene.Average(), comment);

            _ratingController.AddRatingDoctor(dr);

            parent.startWindow.Content = new PatientMainPage(parent);
        }

        /// <summary>
        /// Doktor izabran
        /// </summary>
        /// <returns></returns>
        private void DoctorChosen(object sender, SelectionChangedEventArgs e)
        {
            pitanje1.IsEnabled = true;
            pitanje2.IsEnabled = true;
            pitanje3.IsEnabled = true;
            pitanje4.IsEnabled = true;
        }

        /// <summary>
        /// Doktor ocjenjen
        /// </summary>
        /// <returns></returns>
        private void EverythingRated(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (pitanje1.Value != 0 && pitanje2.Value != 0 && pitanje2.Value != 0 && pitanje4.Value != 0)
            {
                ratingBtn.IsEnabled = true;
            }
        }

        
    }
}
