using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangingPurposeViewModel : ObservableObject
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String timeFormat = "HH:mm";
        readonly private String dateFormat = "dd/MM/yyyy";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public RoomController roomController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        private MainRoomsViewModel mainRoomsViewModel;
        public Room selectedRoom { get; set; }

        public BindingList<String> RoomsAvailable { get; set; }
        private BindingList<Availability> availabilities { get; set; }

        public String ErrorMessage { get; set; }

        public String FromDate;
        public String FromTime { get; set; }
        public String ToDate;
        public String ToTime { get; set; }

        private DateTime _selectedDate;
        private String _newNameRoom;

        private readonly Random _random = new Random();

        private RelayCommand _saveRenovatingRoom;
        private RelayCommand _cancelRenovatingRoom;

        public ChangingPurposeViewModel(MainRoomsViewModel mainRoomsViewModel)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;

            this.mainRoomsViewModel = mainRoomsViewModel;

            resetFields();
        }

        private void resetFields()
        {
            this.selectedRoom = null;
            _newNameRoom = null;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        public RelayCommand SaveRenovatingRoom
        {
            get
            {
                _saveRenovatingRoom = new RelayCommand(o =>
                {
                    if(selectedFromDate == "" || selectedFromTime == "" || selectedToDate == "" || selectedToTime == "" 
                       || selectedNewNameRoom == null || selectedNewNameRoom == "")
                    {
                        ErrorMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
                    SaveChangingPurposeAction();
                });

                return _saveRenovatingRoom;
            }
        }

        public RelayCommand CancelRenovatingRoom
        {
            get
            {
                _cancelRenovatingRoom = new RelayCommand(o =>
                {
                    resetFields();
                    this.mainRoomsViewModel.ResetView();
                });

                return _cancelRenovatingRoom;
            }
        }

        public String selectedFromDate
        {
            get
            {
                return FromDate;
            }
            set
            {
                if (FromDate == value)
                    return;
                FromDate = value;
                DateTime date;
                bool format = DateTime.TryParseExact(value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out date);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    ListsHandler();
                }
            }
        }

        public String selectedFromTime
        {
            get
            {
                return FromTime;
            }
            set
            {
                if (FromTime == value)
                    return;
                FromTime = value;
                DateTime time;
                bool format = DateTime.TryParseExact(value, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out time);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    ListsHandler();
                }
            }
        }

        public String selectedToDate
        {
            get
            {
                return ToDate;
            }
            set
            {
                if (ToDate == value)
                    return;
                ToDate = value;
                DateTime date;
                bool format = DateTime.TryParseExact(value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out date);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    ListsHandler();
                }
            }
        }

        public String selectedToTime
        {
            get
            {
                return ToTime;
            }
            set
            {
                if (ToTime == value)
                    return;
                ToTime = value;
                DateTime time;
                bool format = DateTime.TryParseExact(value, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out time);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                    ListsHandler();
                }
            }
        }
        public String selectedNewNameRoom
        {
            get
            {
                return _newNameRoom;
            }
            set
            {
                value = value.Substring(38);
                if (_newNameRoom == value)
                    return;
                _newNameRoom = value;
                ListsHandler();
            }
        }

        public DateTime selectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (_selectedDate == value)
                    return;
                _selectedDate = value;
                ListsHandler();
            }
        }

        public void SaveChangingPurposeAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var appointmentId = RandomPassword();

            var roomAppointment = new RoomAppointment(appointmentId, selectedRoom.RoomID, fromDatetime, toDatetime - fromDatetime);
            roomAppointment.newRoomName = selectedNewNameRoom;
            roomAppointmentController.AddRoomAppointment(roomAppointment);

            resetFields();
            this.mainRoomsViewModel.ResetView();
        }

        private bool checkTimeSpansOverlap(DateTime fromDatetimeA, DateTime toDatetimeA, DateTime fromDatetimeB, DateTime toDatetimeB)
        {
            return fromDatetimeA <= toDatetimeB && fromDatetimeB <= toDatetimeA;
        }

        private bool checkDateTimeAvailable(DateTime fromDatetime, DateTime toDatetime)
        {
            foreach (var avaible in availabilities)
            {
                if (checkTimeSpansOverlap(fromDatetime, toDatetime, avaible.From, avaible.To))
                {
                    return false;
                }
            }
            return true;
        }

        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));

            return start.ToString(fullFormat) + " - " + end.ToString(fullFormat);
        }

        public void ListsHandler()
        {
            RoomsAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            DateTime selectedFrom, selectedTo;
            try
            {
                selectedFrom = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null);
                selectedTo = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null);
            }
            catch (FormatException)
            {
                return;
            }

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                var aptFrom = equipmentAppointment.FromDateTime.Date;
                var aptTo = equipmentAppointment.ToDateTime.Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (selectedRoom != null && (equipmentAppointment.RoomFrom == selectedRoom.RoomID || equipmentAppointment.RoomTo == selectedRoom.RoomID))
                    {
                        RoomsAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                var aptFrom = roomAppointment.startDate.Date;
                var aptTo = (roomAppointment.startDate + roomAppointment.duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (roomAppointment.roomId == selectedRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                }
            }
            foreach (var appointment in appointments)
            {
                var aptFrom = appointment.AppointmentDate.Date;
                var aptTo = (appointment.AppointmentDate + appointment.Duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (appointment.RoomId == selectedRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);


            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 10 and 99
            passwordBuilder.Append(RandomNumber(10, 99));

            return passwordBuilder.ToString();
        }
    }
}
