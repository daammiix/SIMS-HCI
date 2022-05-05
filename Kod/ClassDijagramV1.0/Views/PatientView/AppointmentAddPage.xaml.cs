﻿using System;
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
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentAddPage.xaml
    /// </summary>
    public partial class AppointmentAddPage : Page//, INotifyPropertyChanged
    {
        #region Fields

        // prosledjena lista u koju se dodaj appointmentViewModel kako bi se view azurirao
        private ObservableCollection<AppointmentViewModel> _appointments;

        // ulogovan pacijent
        private Patient _logedPatient;

        #endregion


        /*public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }*/
        private PatientMainWindow parent { get; set; }
        /*public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }
        public ObservableCollection<String> DoctorsAppointmentsTime
        {
            get;
            set;
        }*/
        //private List<string> availableTimes;

        public AppointmentAddPage(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels, Patient logedPatient)
        {
            InitializeComponent();
            _logedPatient = logedPatient;

            this.DataContext = this;
            parent = patientMain;

            _appointments = appointmentViewModels;


            doctorRB.IsChecked = true;
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityDoctor(parent, _appointments, _logedPatient);
        }

        private void timeRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityTime();
        }
    }
}
