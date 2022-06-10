using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class StorageEquipViewModel : ObservableObject
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String timeFormat = "HH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public EquipmentController equipmentController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;
        public RoomController roomController;

        public BindingList<Room> Rooms { get; set; }
        private Room _sourceRoom;
        public Storage storage;
        public QuantifiedEquipment QEquipment { get; set; }
        private String _quantity;

        public String ErrorMessage { get; set; }
        public String ErrorFormatMessage { get; set; }

        public BindingList<String> RoomsAvailable { get; set; }
        public String FromDate;
        public String FromTime;
        public String ToDate;
        public String ToTime;
        private BindingList<Availability> availabilities { get; set; }

        private RelayCommand _saveStorageEquip;
        private RelayCommand _cancelStorageEquip;
        public object PreviousView { get; set; }
        private StorageViewModel storageViewModel;

        public StorageEquipViewModel(StorageViewModel storageViewModel)
        {
            var app = Application.Current as App;
            equipmentAppointmentController = app.equipmentAppointmentController;
            equipmentController = app.equipmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;
            roomController = app.roomController;
            this.storageViewModel = storageViewModel;

            Rooms = roomController.GetAllRooms();
            storage = (Storage)roomController.GetRoom("storage");

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        public RelayCommand SaveStorageEquip
        {
            get
            {
                _saveStorageEquip = new RelayCommand(o =>
                {
                    if (selectedFromDate == "" || selectedFromTime == "" || selectedToDate == "" || selectedToTime == ""
                    || _sourceRoom == null || _quantity == null || _quantity == "")
                    {
                        ErrorFormatMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorFormatMessage");
                        return;
                    }
                    SaveStorageEquipAction();
                });

                return _saveStorageEquip;
            }
        }

        public RelayCommand CancelStorageEquip
        {
            get
            {
                _cancelStorageEquip = new RelayCommand(o =>
                {
                    ResetAllSelectedEquipments();
                    storageViewModel.CurrentStorageView = PreviousView;
                });

                return _cancelStorageEquip;
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

        public String selectedQuantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity == value)
                    return;
                _quantity = value;
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
                    UpdateAvailabilityOfEquipment();
                }
            }
        }

        public Room selectedSourceRoom
        {
            get
            {
                return _sourceRoom;
            }
            set
            {
                if (_sourceRoom == value)
                    return;
                _sourceRoom = value;
                ListsHandler();
            }
        }

        private void SaveStorageEquipAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var equipmentAppointment = new EquipmentAppointment(storage.RoomID, selectedSourceRoom.RoomID, QEquipment.Equipment, Int32.Parse(_quantity), fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);
            selectedQuantity = "";
            selectedSourceRoom = null;
            storageViewModel.CurrentStorageView = PreviousView;
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

        public void UpdateAvailabilityOfEquipment()
        {
            if (selectedQuantity != "")
            {
                var newselecteQuantity = Int32.Parse(selectedQuantity);
                var quantity = Int32.Parse(_quantity);
                if (!(newselecteQuantity >= quantity))
                {
                    WarningQuantity warningQuantity = new WarningQuantity();
                    warningQuantity.Show();
                    _quantity = "";
                    return;
                }
            }

        }

        public void ListsHandler()
        {
            RoomsAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            if (selectedSourceRoom == null)
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
                    if (selectedSourceRoom != null && (equipmentAppointment.RoomFrom == selectedSourceRoom.RoomID || equipmentAppointment.RoomTo == selectedSourceRoom.RoomID))
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
                    if (roomAppointment.roomId == selectedSourceRoom.RoomID)
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
                    if (appointment.RoomId == selectedSourceRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }
        }

        public void ResetAllSelectedEquipments()
        {
            storageViewModel.StorageSuppliesVM.ResetSelectedEquipment();
            storageViewModel.StorageInventoryVM.ResetSelectedEquipment();
            storageViewModel.StorageMedicalEquipmentVM.ResetSelectedEquipment();
        }
    }
}
