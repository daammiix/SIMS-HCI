using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AddAppointmentDialogViewModel : ObservableObject
    {
        #region Fields

        private AppointmentController _appointmentController;

        private PatientController _patientController;

        private RoomController _roomController;

        private DoctorController _doctorController;

        private AccountController _accountController;

        // Liste za vreme

        private List<int> _hours;

        private List<int> _minutes;

        private RelayCommand _addAppointmentCommand;

        #endregion

        #region Properties

        // Observable kolekcija u koju dodajemo appointmentViewModel
        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }
        public ObservableCollection<Account> PatientsAccounts { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        // Liste za vreme
        public List<int> Hours
        {
            get
            {
                if (_hours == null)
                {
                    _hours = new List<int>();
                    for (int i = 0; i < 24; i++)
                    {
                        _hours.Add(i);
                    }
                }
                return _hours;
            }
            private set { _hours = value; }
        }

        public List<int> Minutes
        {
            get
            {
                if (_minutes == null)
                {
                    _minutes = new List<int>();
                    for (int i = 0; i < 60; i++)
                    {
                        _minutes.Add(i);
                    }
                }
                return _minutes;
            }
            private set { _minutes = value; }
        }

        public DateTime SelectedDate { get; set; }
        public int SelectedHour { set; get; }
        public int SelectedMinute { set; get; }
        public int? Duration { set; get; }
        public Doctor SelectedDoctor { set; get; }
        public Room SelectedRoom { set; get; }
        public Account SelectedPatientAccount { set; get; }
        public AppointmentType SelectedAppointmentType { set; get; }


        #endregion

        #region Commands

        public RelayCommand AddAppointmentCommand
        {
            get
            {
                if (_addAppointmentCommand == null)
                {
                    _addAppointmentCommand = new RelayCommand(o => ExecuteAddAppointmentCommand(o),
                        CanExecuteAddAppointmentCommand);
                }

                return _addAppointmentCommand;
            }
        }

        #endregion

        #region Constructor

        public AddAppointmentDialogViewModel(ObservableCollection<AppointmentViewModel> appointmentViewModels,
            DateTime selectedDate)
        {
            Appointments = appointmentViewModels;
            SelectedDate = selectedDate;

            App app = Application.Current as App;

            // Ucitamo sve kontrolere
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _roomController = app.roomController;
            _doctorController = app.DoctorController;
            _accountController = app.AccountController;

            PatientsAccounts = _accountController.GetPatientsAccounts();
            Rooms = new ObservableCollection<Room>(_roomController.GetAllRooms());
            Doctors = _doctorController.GetAllDoctors();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Izvrsava komandu dodavanja appointmenta i zatvara window
        /// </summary>
        /// <param name="o"></param>
        private void ExecuteAddAppointmentCommand(object o)
        {
            // Castujemo objecat u window
            Window win = o as Window;

            // Ako je nalog banovan prikazemo gresku 
            if (SelectedPatientAccount.Banned == true)
            {
                MessageBox.Show("Akaunt banovan!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // sredimo vreme
            int year = SelectedDate.Year;
            int month = SelectedDate.Month;
            int day = SelectedDate.Day;
            DateTime start = new DateTime(year, month, day, SelectedHour, SelectedMinute, 0);
            // Nece duration da bude null nikad ovde jer ne moze da se pritisne dugme kao je null
            DateTime end = start.AddMinutes((double)Duration);
            TimeSpan duration = end - start;

            // moramo da proverimo sta je sve zauzeto

            // Napravimo novi appointment i njegov ViewModel
            Appointment appointment = new Appointment(SelectedPatientAccount.PersonId, SelectedDoctor.Id
                , SelectedRoom.RoomID, start, duration, SelectedAppointmentType);

            // Ako je termin slobodan napravimo i viewModel i dodamo appointment i u view i u bazu
            if (_appointmentController.CheckIsTerminFree(appointment))
            {
                AppointmentViewModel appointmentViewModel = new AppointmentViewModel(appointment);

                // Dodamo ih i u bazu i u view
                _appointmentController.AddAppointment(appointment);
                Appointments.Add(appointmentViewModel);

                // Zatvorimo window
                if (win != null)
                {
                    win.Close();
                }
            }
            else
            {
                // Ako nije prikazemo gresku
                MessageBox.Show("Doktor ili sala su zauzeti u ovom terminu", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool CanExecuteAddAppointmentCommand()
        {

            if (SelectedDoctor == null || SelectedPatientAccount == null || SelectedRoom == null || Duration == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
