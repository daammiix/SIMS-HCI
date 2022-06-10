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
    public class RenovatingMergeViewModel : ObservableObject
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

        public BindingList<Room> Rooms { get; set; }
        public BindingList<String> MergingToRoomAvailable { get; set; }
        public BindingList<String> MergingRoomAvailable { get; set; }
        private BindingList<Availability> availabilities { get; set; }

        private readonly Random _random = new Random();

        public String ErrorFormatMessage { get; set; }

        public Room? selectedRoom { get; set; }
        public Room mergingRoom;

        private RelayCommand _saveRenovatingMerge;
        private RelayCommand _cancelRenovatingMerge;
        public RenovatingMergeViewModel(MainRoomsViewModel mainRoomsViewModel)
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
            this.mergingRoom = null;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            Rooms = new BindingList<Room>();
            MergingToRoomAvailable = new BindingList<String>();
            MergingRoomAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        public RelayCommand SaveRenovatingMerge
        {
            get
            {
                _saveRenovatingMerge = new RelayCommand(o =>
                {
                    if (selectedFromDate == "" || selectedFromTime == "" || selectedToDate == "" || selectedToTime == "" || mergingRoom == null)
                    {
                        ErrorFormatMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorFormatMessage");
                        return;
                    }
                    SaveRenovatingMergeAction();
                });

                return _saveRenovatingMerge;
            }
        }

        public RelayCommand CancelRenovatingMerge
        {
            get
            {
                _cancelRenovatingMerge = new RelayCommand(o =>
                {
                    resetFields();
                    this.mainRoomsViewModel.ResetView();
                });

                return _cancelRenovatingMerge;
            }
        }

        private void SaveRenovatingMergeAction()
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

            var roomAppointment = new RoomAppointment(appointmentId, selectedRoom.RoomID, fromDatetime, toDatetime - fromDatetime);
            roomAppointment.RoomIDToMerge = selectedMergingRoom.RoomID;
            roomAppointmentController.AddRoomAppointment(roomAppointment);

            resetFields();
            this.mainRoomsViewModel.ResetView();
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

        public Room selectedMergingRoom
        {
            get
            {
                return mergingRoom;
            }
            set
            {
                if (mergingRoom == value)
                    return;
                mergingRoom = value;
                ListsHandler();
            }
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

        public void UpdateAvailableRooms()
        {

            BindingList<Room> rooms = roomController.GetAllRooms();
            foreach (Room room in rooms)
            {
                if (room == selectedRoom)
                {
                    continue;
                }
                if (room.Floor == selectedRoom.Floor)
                {
                    Rooms.Add(room);
                }
            }
        }
        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));
            return start.ToString(fullFormat) + " - " + end.ToString(fullFormat);
        }

        public void ListsHandler()
        {
            MergingToRoomAvailable.Clear();
            MergingRoomAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            if (selectedMergingRoom == null)
            {
                return;
            }

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
                    if (equipmentAppointment.RoomFrom == selectedMergingRoom.RoomID || equipmentAppointment.RoomTo == selectedMergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                    if (selectedRoom != null && (equipmentAppointment.RoomFrom == selectedRoom.RoomID || equipmentAppointment.RoomTo == selectedRoom.RoomID))
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                var aptFrom = roomAppointment.startDate.Date;
                var aptTo = (roomAppointment.startDate + roomAppointment.duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (roomAppointment.roomId == selectedMergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                    if (roomAppointment.roomId == selectedRoom.RoomID)
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                }
            }
            foreach (var appointment in appointments)
            {
                var aptFrom = appointment.AppointmentDate.Date;
                var aptTo = (appointment.AppointmentDate + appointment.Duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (appointment.RoomId == selectedMergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                    if (appointment.RoomId == selectedRoom.RoomID)
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
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
