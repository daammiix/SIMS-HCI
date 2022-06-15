using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class ChangeAppointmentDialogViewModel
    {
        #region Fields

        private AppointmentViewModel _appointmentToChange;

        private ObservableCollection<AppointmentViewModel> _appointments;

        private RoomController _roomController;

        private DoctorController _doctorController;

        private AppointmentController _appointmentController;

        private EquipmentAppointmentController _equipmentAppointmentController;

        private RoomAppointmentController _roomAppointmentController;

        // Liste za vreme

        private List<int> _hours;

        private List<int> _minutes;

        // Commande

        private RelayCommand _saveCommand;

        private RelayCommand _deleteCommand;


        #endregion

        #region Properties

        public DateTime SelectedDate { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int? Duration { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

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


        #endregion

        #region Commands

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(o => ExecuteSaveCommand(o), CanExecuteSaveCommand);
                }

                return _saveCommand;
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(o => ExecuteDeleteCommand(o));
                }

                return _deleteCommand;
            }
        }

        #endregion

        #region Constructor

        public ChangeAppointmentDialogViewModel(int appointmentToChangeId, ObservableCollection<AppointmentViewModel>
            appointments)
        {
            _appointments = appointments;
            foreach (AppointmentViewModel appointment in _appointments)
            {
                if (appointment.Id == appointmentToChangeId)
                {
                    _appointmentToChange = appointment;
                    break;
                }
            }

            // Pokupimo kontrolere
            App app = Application.Current as App;

            _roomController = app.roomController;
            _doctorController = app.DoctorController;
            _appointmentController = app.AppointmentController;
            _equipmentAppointmentController = app.equipmentAppointmentController;
            _roomAppointmentController = app.roomAppointmentController;

            // Popunimo liste
            Rooms = new ObservableCollection<Room>(_roomController.GetAllRooms());
            Doctors = _doctorController.GetAllDoctors();

            // Popunimo informacije
            if (_appointmentToChange != null)
            {

                SelectedDate = _appointmentToChange.From;

                Hour = SelectedDate.Hour;
                Minute = SelectedDate.Minute;

                DateTime end = _appointmentToChange.To;
                TimeSpan duration = end - SelectedDate;

                Duration = (int)duration.TotalMinutes;

                Doctor = _appointmentToChange.Doctor;
                Room = _appointmentToChange.Room;
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Cuva izmene appointmenta
        /// </summary>
        /// <param name="o">Ovo ce da bude window da bi zatvorili dialog</param>
        private void ExecuteSaveCommand(Object o)
        {
            Window win = o as Window;

            // sredimo vreme
            int year = SelectedDate.Year;
            int month = SelectedDate.Month;
            int day = SelectedDate.Day;
            DateTime start = new DateTime(year, month, day, Hour, Minute, 0);
            // Nece duration da bude null nikad ovde jer ne moze da se pritisne dugme kao je null
            DateTime end = start.AddMinutes((double)Duration);
            // trajanje
            TimeSpan duration = end - start;

            // Napravimo dummy appointment samo da bi proverili da li je termin slobodan 
            // Patient id nije bitan za proveru termina
            Appointment a = new Appointment(-1, Doctor.Id, Room.RoomID, start, end - start, AppointmentType.generalPractitionerCheckup);
            // Stavimo mu Id isti kao ovaj sto menjamo da bi ga preskocili prilikom provere
            a.Id = _appointmentToChange.Id;
            // Umanjimo brojac zbog dummy appointmenta
            Appointment.idCounter--;

            // Ako je termin free updatujemo appointment
            if (_appointmentController.CheckIsTerminFree(a) &&
                _equipmentAppointmentController.CheckIsTerminFree(start, duration, Room) &&
                _roomAppointmentController.CheckIsTerminFree(start, duration, Room))
            {
                _appointmentToChange.From = start;
                _appointmentToChange.To = end;
                _appointmentToChange.Room = Room;
                _appointmentToChange.Doctor = Doctor;

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

        /// <summary>
        /// Proverava da li commanda save moze da se izvrsi
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteSaveCommand()
        {
            // Samo duration moze da bude null ovo ostalo su combo boxevi
            if (Duration == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Brise appointment
        /// </summary>
        /// <param name="o">Window da bi zatvorili dialog</param>
        private void ExecuteDeleteCommand(object o)
        {
            // Castujemo object u window
            Window win = o as Window;

            // Brisemo i iz baze i iz view-a
            _appointmentController.RemoveAppointment(_appointmentToChange.Id);
            _appointments.Remove(_appointmentToChange);

            // Zatvorimo dialog
            if (win != null)
            {
                win.Close();
            }
        }

        #endregion
    }
}
