using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    internal struct Availability
    {
        public DateTime From;
        public DateTime To;

        public Availability(DateTime from, DateTime to)
        {
            From = from; To = to;
        }
    }
    public class EquipViewModel
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public RoomController roomController;
        public EquipmentController equipmentController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        private MainRoomsViewModel mainRoomsViewModel;

        public BindingList<Room> Rooms { get; set; }
        public BindingList<Equipment> Equipments
        {
            get
            {
                return _equipments;
            }
            set
            {
                if (_equipments == value)
                    return;

                _equipments = value;
            }
        }

        public Room? destinationRoom { get; set; }
        public Room _sourceRoom;
        private Equipment _selectedEqipment;
        public int _quantity;

        public BindingList<String> RoomsFromAvailable { get; set; }
        public BindingList<String> RoomsToAvailable { get; set; }
        public BindingList<String> EquipmentAvailable { get; set; }

        public String FromDate;
        public String FromTime { get; set; }
        public String ToDate;
        public String ToTime { get; set; }

        private BindingList<Availability> availabilities { get; set; }

        private RelayCommand _saveEquipmentAppointment;
        private RelayCommand _cancelEquipmentAppointment;
        private BindingList<Equipment> _equipments;

        private BindingList<EquipmentAppointment> fakeAppointments
        {
            get
            {
                var now = DateTime.Now;
                return new BindingList<EquipmentAppointment>
                {
                    new EquipmentAppointment("", "", _equipments[0], 0, now + TimeSpan.FromHours(1), now + TimeSpan.FromHours(2)),
                    new EquipmentAppointment("", "", _equipments[0], 0, now - TimeSpan.FromMinutes(15), now + TimeSpan.FromMinutes(15)),
                };
            }
        }

        public EquipViewModel(MainRoomsViewModel mainRoomsViewModel)
        {
            var app = Application.Current as App;

            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            equipmentController = app.equipmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;

            this.mainRoomsViewModel = mainRoomsViewModel;

            this.destinationRoom = null;

            Rooms = new BindingList<Room>();
            RoomsFromAvailable = new BindingList<String>();
            RoomsToAvailable = new BindingList<String>();
            EquipmentAvailable = new BindingList<String>();

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            Equipments = equipmentController.GetAllEquipments();
            availabilities = new BindingList<Availability>();
        }

        public RelayCommand SaveEquipmentAppointment
        {
            get
            {
                _saveEquipmentAppointment = new RelayCommand(o =>
                {
                    SaveEquipmentAppointmentAction();
                });

                return _saveEquipmentAppointment;
            }
        }

        public RelayCommand CancelEquipmentAppointment
        {
            get
            {
                _cancelEquipmentAppointment = new RelayCommand(o =>
                {
                    this.mainRoomsViewModel.ResetView();
                });

                return _cancelEquipmentAppointment;
            }
        }

        public Equipment selectedEquipment
        {
            get
            {
                return _selectedEqipment;
            }
            set
            {
                if (_selectedEqipment == value)
                    return;
                _selectedEqipment = value;
                UpdateAvailableRooms();
                ListsHandler();
            }
        }
        public int selectedQuantity
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
                UpdateAvailableRooms();
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
                ListsHandler();
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
                ListsHandler();
            }
        }

        private void SaveEquipmentAppointmentAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var equipmentAppointment = new EquipmentAppointment(_sourceRoom.RoomID, destinationRoom.RoomID, selectedEquipment, selectedQuantity, fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);

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

        public void UpdateAvailableRooms()
        {
            selectedSourceRoom = null; // TODO check
            Rooms.Clear();
            try
            {
                _quantity = Convert.ToInt32(selectedQuantity);
            }
            catch (FormatException ex)
            {
                _quantity = 0;
                return;
            }
            BindingList<Room> rooms = roomController.GetAllRooms();
            foreach (Room room in rooms)
            {
                if (room == destinationRoom)
                {
                    continue;
                }
                foreach (var biding in room.EquipmentList)
                {
                    if (selectedEquipment.EquipmentID == biding.EquipmentID && biding.Quantity >= selectedQuantity)
                    {
                        Rooms.Add(room);
                    }
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
            RoomsFromAvailable.Clear();
            RoomsToAvailable.Clear();
            EquipmentAvailable.Clear();

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

            foreach (var appointment in fakeAppointments)
            {
                var aptFrom = appointment.FromDateTime.Date;
                var aptTo = appointment.ToDateTime.Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    EquipmentAvailable.Add(formatAvailableTime(aptFrom, aptTo));
                }
            }

            if (selectedSourceRoom == null)
            {
                return;
            }
            foreach (var equipmentAppointment in equipmentAppointments)
            {
                var aptFrom = equipmentAppointment.FromDateTime.Date;
                var aptTo = equipmentAppointment.ToDateTime.Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (equipmentAppointment.RoomFrom == selectedSourceRoom.RoomID || equipmentAppointment.RoomTo == selectedSourceRoom.RoomID)
                    {
                        RoomsToAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                    if (destinationRoom != null && (equipmentAppointment.RoomFrom == destinationRoom.RoomID || equipmentAppointment.RoomTo == destinationRoom.RoomID))
                    {
                        RoomsFromAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
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
                        RoomsToAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                    if (roomAppointment.roomId == destinationRoom.RoomID)
                    {
                        RoomsFromAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
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
                        RoomsToAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                    if (appointment.RoomId == destinationRoom.RoomID)
                    {
                        RoomsFromAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }
        }
    }
}
