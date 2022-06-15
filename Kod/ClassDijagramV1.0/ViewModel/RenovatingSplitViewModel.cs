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
    public class RenovatingSplitViewModel : ObservableObject
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public String FromDate;
        public String FromTime { get; set; }
        public String ToDate;
        public String ToTime { get; set; }

        public RoomController roomController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        private MainRoomsViewModel mainRoomsViewModel;

        public BindingList<String> RoomsAvailable { get; set; }
        private BindingList<Availability> availabilities { get; set; }

        public Room? selectedRoom { get; set; }
        public Room newRoom;

        public String ErrorFormatMessage { get; set; }
        public String ErrorMessage { get; set; }

        private String _newRoomID;
        private String _newRoomName;
        private String _newRoomNumber;

        private readonly Random _random = new Random();

        private RelayCommand _saveRenovatingSplit;
        private RelayCommand _cancelRenovatingSplit;

        public RenovatingSplitViewModel(MainRoomsViewModel mainRoomsViewModel)
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
            newRoom = null;
            _newRoomID = null;
            _newRoomName = null;
            _newRoomNumber = null;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        public RelayCommand SaveRenovatingSplit
        {
            get
            {
                _saveRenovatingSplit = new RelayCommand(o =>
                {
                    if (selectedFromDate == "" || selectedFromTime == "" || selectedToDate == "" || selectedToTime == ""
                        || _newRoomID == null || _newRoomName == null || _newRoomNumber == null
                        || _newRoomID == "" || _newRoomName == "" || _newRoomNumber == "")
                    {
                        ErrorFormatMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorFormatMessage");
                        return;
                    }
                    SaveRenovatingSplitAction();
                });

                return _saveRenovatingSplit;
            }
        }

        public RelayCommand CancelRenovatingSplit
        {
            get
            {
                _cancelRenovatingSplit = new RelayCommand(o =>
                {
                    resetFields();
                    this.mainRoomsViewModel.ResetView();
                });

                return _cancelRenovatingSplit;
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
                    ErrorFormatMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorFormatMessage");
                }
                else
                {
                    ErrorFormatMessage = "";
                    OnPropertyChanged("ErrorFormatMessage");
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
                    ErrorFormatMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorFormatMessage");
                }
                else
                {
                    ErrorFormatMessage = "";
                    OnPropertyChanged("ErrorFormatMessage");
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
                    ErrorFormatMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorFormatMessage");
                }
                else
                {
                    ErrorFormatMessage = "";
                    OnPropertyChanged("ErrorFormatMessage");
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
                    ErrorFormatMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorFormatMessage");
                }
                else
                {
                    ErrorFormatMessage = "";
                    OnPropertyChanged("ErrorFormatMessage");
                    ListsHandler();
                }
            }
        }

        public String selectedNewRoomID
        {
            get
            {
                return _newRoomID;
            }
            set
            {
                if (_newRoomID == value)
                    return;
                _newRoomID = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra ne sme biti prazna";
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

        public String selectedNewRoomName
        {
            get
            {
                return _newRoomName;
            }
            set
            {
                value = value.Substring(38);
                if (_newRoomName == value)
                    return;
                _newRoomName = value;
                ListsHandler();
            }
        }

        public String selectedNewRoomNumber
        {
            get
            {
                return _newRoomNumber;
            }
            set
            {
                if (_newRoomNumber == value)
                    return;
                _newRoomNumber = value;
                int quantity;
                bool is_number = int.TryParse(value, out quantity);
                if (!is_number)
                {
                    ErrorMessage = "Uneta vrednost mora biti broj";
                    OnPropertyChanged("ErrorMessage");
                }
                else if (quantity < 1)
                {
                    ErrorMessage = "Broj mora biti veći od 0";
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

        private void SaveRenovatingSplitAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var appointmentId = RandomId();

            newRoom = RoomFromTextboxes();

            var roomAppointment = new RoomAppointment(appointmentId, selectedRoom.RoomID, fromDatetime, toDatetime - fromDatetime);
            roomAppointment.RoomToSplit = newRoom;
            roomAppointmentController.AddRoomAppointment(roomAppointment);

            resetFields();
            this.mainRoomsViewModel.ResetView();
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                selectedNewRoomID,
                selectedNewRoomName,
                selectedRoom.Floor,
                Int32.Parse(selectedNewRoomNumber),
                "Aktivna"
            );
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

        public string RandomId()
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
