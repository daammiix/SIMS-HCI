using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.DTO;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AddAppointmentDialogViewModel : ObservableObject
    {
        // Za obicne appointmente

        #region Fields

        private AppointmentController _appointmentController;

        private PatientController _patientController;

        private RoomController _roomController;

        private DoctorController _doctorController;

        private AccountController _accountController;

        private EquipmentAppointmentController _equipmentAppointmentController;

        private RoomAppointmentController _roomAppointmentController;

        // Liste za vreme

        private List<int> _hours;

        private List<int> _minutes;

        // Komande

        private RelayCommand _addAppointmentCommand;

        private RelayCommand _addUrgentAppointmentCommand;

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

        // Za urgentne appointmente

        #region Fields Urgent

        private DoctorType _selectedSpecialization;

        private int _durationUrgent;

        private int _selectedHourUrgent;

        private int _selectedMinuteUrgent;

        private Room _selectedRoomUrgent;

        private Doctor _selectedDoctorUrgent;

        #endregion

        #region Properties Urgent

        public Account SelectedPatientAccountUrgent { set; get; }

        // Sve specijalizacije

        public Array Specializations
        {
            get
            {
                return Enum.GetValues(typeof(DoctorType));
            }
        }

        public int DurationUrgent
        {
            get
            {
                return _durationUrgent;
            }
            set
            {
                if (_durationUrgent != value)
                {
                    _durationUrgent = value;
                    // Predlog termina i onda onpropertychange hour i minute
                    DoctorWithTerminAndRoomDTO? doctorWithTermin = _appointmentController
                        .FindClosestFreeTerminForSpecialization(_selectedSpecialization, SelectedDate, DurationUrgent);
                    // Doctor With Termin bice null ako uopste nemamo doktora te specijalizacije
                    if (doctorWithTermin != null)
                    {
                        DoctorUrgent = doctorWithTermin.Doctor;
                        RoomUrgent = doctorWithTermin.Room;
                        SelectedHourUrgent = doctorWithTermin.Termin.Hour;
                        SelectedMinuteUrgent = doctorWithTermin.Termin.Minute;
                    }
                    OnPropertyChanged("DurationUrgent");
                }
            }
        }

        public Doctor DoctorUrgent
        {
            get
            {
                return _selectedDoctorUrgent;
            }
            set
            {
                if (_selectedDoctorUrgent != value)
                {
                    _selectedDoctorUrgent = value;
                    OnPropertyChanged("DoctorUrgent");
                }
            }
        }

        public Room RoomUrgent
        {
            get
            {
                return _selectedRoomUrgent;
            }
            set
            {
                if (_selectedRoomUrgent != value)
                {
                    _selectedRoomUrgent = value;
                    OnPropertyChanged("RoomUrgent");
                }
            }
        }


        // Izabrana specijalizacija
        public DoctorType SelectedSpecialization
        {
            get
            {
                return _selectedSpecialization;
            }
            set
            {
                if (_selectedSpecialization != value)
                {
                    _selectedSpecialization = value;
                    OnPropertyChanged("SelectedSpecialization");
                }
            }
        }

        public int SelectedHourUrgent
        {
            get
            {
                return _selectedHourUrgent;
            }
            set
            {
                if (_selectedHourUrgent != value)
                {
                    _selectedHourUrgent = value;
                    OnPropertyChanged("SelectedHourUrgent");
                }
            }
        }

        public int SelectedMinuteUrgent
        {
            get
            {
                return _selectedMinuteUrgent;
            }
            set
            {
                if (_selectedMinuteUrgent != value)
                {
                    _selectedMinuteUrgent = value;
                    OnPropertyChanged("SelectedMinuteUrgent");
                }
            }
        }

        public AppointmentType SelectedAppointmentTypeUrgent { set; get; }

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

        public RelayCommand AddUrgentAppointmentCommand
        {
            get
            {
                if (_addUrgentAppointmentCommand == null)
                {
                    _addUrgentAppointmentCommand = new RelayCommand(o => ExecuteAddUrgentAppointmentCommand(o),
                        CanExecuteAddUrgentAppointmentCommand);
                }

                return _addUrgentAppointmentCommand;
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
            _equipmentAppointmentController = app.equipmentAppointmentController;
            _roomAppointmentController = app.roomAppointmentController;

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
            if (SelectedPatientAccount.Banned)
            {
                ShowBannedAccountMessage(SelectedPatientAccount.Banned);
                return;
            }

            // Uzmemo vreme
            DateTime start = GetAppointmentDate(SelectedHour, SelectedMinute);
            // Nece duration da bude null nikad ovde jer ne moze da se pritisne dugme kao je null
            DateTime end = start.AddMinutes((double)Duration);
            TimeSpan duration = end - start;

            // Napravimo novi appointment i njegov ViewModel
            Appointment appointment = new Appointment(SelectedPatientAccount.PersonId, SelectedDoctor.Id
                , SelectedRoom.RoomID, start, duration, SelectedAppointmentType);

            // Ako je termin slobodan napravimo i viewModel i dodamo appointment i u view i u bazu
            Appointment? addedAppointment = AddNewAppointment(appointment, start, duration, SelectedRoom);

            // Ako je addedAppointment != null sto znaci da smo ga uspesno dodali zatvorimo window
            if (win != null && addedAppointment != null)
            {
                win.Close();
            }
        }

        /// <summary>
        /// Proverava da li addAppointmentCommand moze da se izvrsi
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteAddAppointmentCommand()
        {

            if (SelectedDoctor == null || SelectedPatientAccount == null || SelectedRoom == null || Duration == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Izvrsava komandu dodavanja urgent appointmenta i zatvara window
        /// </summary>
        /// <param name="o"></param>
        private void ExecuteAddUrgentAppointmentCommand(object o)
        {
            // Castujemo objecat u window
            Window win = o as Window;

            // Ako je nalog banovan prikazemo gresku 
            if (SelectedPatientAccountUrgent.Banned)
            {
                ShowBannedAccountMessage(SelectedPatientAccountUrgent.Banned);
                return;
            }
            // Uzmemo vreme
            DateTime start = GetAppointmentDate(SelectedHourUrgent, SelectedMinuteUrgent);
            // Nece duration da bude null nikad ovde jer ne moze da se pritisne dugme ako je null
            DateTime end = start.AddMinutes((double)DurationUrgent);
            TimeSpan duration = end - start;

            // Napravimo novi appointment i njegov ViewModel
            Appointment appointment = new Appointment(SelectedPatientAccountUrgent.PersonId, DoctorUrgent.Id
                , RoomUrgent.RoomID, start, duration, SelectedAppointmentTypeUrgent);

            // Ako je termin slobodan napravimo i viewModel i dodamo appointment i u view i u bazu
            Appointment? addedAppointment = AddNewAppointment(appointment, start, duration, RoomUrgent);

            // Ako je addedAppointment != null sto znaci da smo ga uspesno dodali zatvorimo window
            if (win != null && addedAppointment != null)
            {
                win.Close();
            }

        }

        /// <summary>
        /// Proverava da li addUrgentAppointmentCommand moze da se izvrsi
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteAddUrgentAppointmentCommand()
        {

            if (DoctorUrgent == null || SelectedPatientAccountUrgent == null || RoomUrgent == null || DurationUrgent == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vraca DateTime pocetka appointmenta
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private DateTime GetAppointmentDate(int hour, int minute)
        {
            int year = SelectedDate.Year;
            int month = SelectedDate.Month;
            int day = SelectedDate.Day;
            DateTime start = new DateTime(year, month, day, hour, minute, 0);

            return start;
        }

        /// <summary>
        /// Prikazuje poruku o gresci ako je acc banovan
        /// </summary>
        /// <param name="isBanned"> da li je banovan</param>
        private void ShowBannedAccountMessage(bool isBanned)
        {
            if (isBanned)
            {
                MessageBox.Show("Akaunt banovan!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        /// <summary>
        /// Dodaje novi appointment u bazu kao i njegov viewModel u view, ispisuje poruku o gresci ako termin nije slobodan
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="start"></param>
        /// <param name="duration"></param>
        /// <param name="room"></param>
        /// <returns> Vraca dodati appointment ili null ako nije dodat zbog zauzetosti termina </returns>
        private Appointment? AddNewAppointment(Appointment appointment, DateTime start, TimeSpan duration, Room room)
        {
            // Ret val
            Appointment? ret = null;

            if (_appointmentController.CheckIsTerminFree(appointment) &&
                _equipmentAppointmentController.CheckIsTerminFree(start, duration, room) &&
                _roomAppointmentController.CheckIsTerminFree(start, duration, room))
            {
                AppointmentViewModel appointmentViewModel = new AppointmentViewModel(appointment);

                // Dodamo ih i u bazu i u view
                _appointmentController.AddAppointment(appointment);
                Appointments.Add(appointmentViewModel);

                ret = appointment;
            }
            else
            {
                // Ako nije prikazemo gresku
                MessageBox.Show("Doktor ili sala su zauzeti u ovom terminu", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return ret;
        }



        #endregion
    }
}
