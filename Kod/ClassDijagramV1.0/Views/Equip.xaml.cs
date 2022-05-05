using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.Views
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

    /// <summary>
    /// Interaction logic for Equip.xaml
    /// </summary>
    public partial class Equip : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public RoomController roomController;
        public EquipmentController equipmentController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        public BindingList<Room> Rooms { get; set; }
        public BindingList<Equipment> Equipments { get; set; }

        public Room selectedRoom { get; set; }
        public Room sourceRoom;
        public Equipment selectedEquipment;
        public int quantity;

        public BindingList<String> RoomsFromAvailable { get; set; }
        public BindingList<String> RoomsToAvailable { get; set; }
        public BindingList<String> EquipmentAvailable { get; set; }
        public String FromDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String FromTime { get; set; } = DateTime.Now.ToString("HH:mm");
        public String ToDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String ToTime { get; set; } = DateTime.Now.ToString("HH:mm");

        private BindingList<Availability> availabilities { get; set; }

        public Equip(Room selectedRoom)
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            equipmentController = app.equipmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;
            this.selectedRoom = selectedRoom;
            Rooms = new BindingList<Room>();
            Equipments = equipmentController.GetAllEquipments();
            RoomsFromAvailable = new BindingList<String>();
            RoomsToAvailable = new BindingList<String>();
            EquipmentAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        private void SaveEquip_Click(object sender, RoutedEventArgs e)
        {
            sourceRoom = (Room)MovingFrom.SelectedItem;
            Equipment selectedEquipment = (Equipment)EquipmentBox.SelectedItem;
            quantity = Convert.ToInt32(Quantity.Text);
            DateTime fromDatetime = DateTime.ParseExact(FromDateField.Text + "T" + FromTimeField.Text, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDateField.Text + "T" + ToTimeField.Text, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var equipmentAppointment = new EquipmentAppointment(sourceRoom.RoomID, selectedRoom.RoomID, selectedEquipment, quantity, fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);
            this.Close();
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

        private void QuitEquip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EquipmentBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateAvailableRooms();
            ListsHandler();
        }

        private void Quantity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateAvailableRooms();
        }

        private void DateField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ListsHandler();
        }

        private void MovingFrom_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        public void UpdateAvailableRooms()
        {
            MovingFrom.SelectedIndex = -1;
            Rooms.Clear();
            try
            {
                quantity = Convert.ToInt32(Quantity.Text);
            }
            catch (FormatException ex)
            {
                quantity = 0;
                return;
            }
            BindingList<Room> rooms = roomController.GetAllRooms();
            foreach (Room room in rooms)
            {
                if (room == selectedRoom)
                {
                    continue;
                }
                foreach (var biding in room.EquipmentList)
                {
                    Equipment selectedEquipment = (Equipment)EquipmentBox.SelectedItem;
                    if (selectedEquipment.EquipmentID == biding.EquipmentID && biding.Quantity >= quantity)
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

            Room? sourceRoom = (Room?)MovingFrom.SelectedItem;
            if (sourceRoom == null)
            {
                return;
            }
            DateTime selectedFrom, selectedTo;
            try
            {
                selectedFrom = DateTime.ParseExact(FromDateField.Text, "dd/MM/yyyy", null);
                selectedTo = DateTime.ParseExact(ToDateField.Text, "dd/MM/yyyy", null);
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
                    if (equipmentAppointment.RoomFrom == sourceRoom.RoomID || equipmentAppointment.RoomTo == sourceRoom.RoomID)
                    {
                        RoomsToAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                    if (selectedRoom != null && (equipmentAppointment.RoomFrom == selectedRoom.RoomID || equipmentAppointment.RoomTo == selectedRoom.RoomID))
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
                    if (roomAppointment.roomId == sourceRoom.RoomID)
                    {
                        RoomsToAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                    if (roomAppointment.roomId == selectedRoom.RoomID)
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
                    if (appointment.RoomId == sourceRoom.RoomID)
                    {
                        RoomsToAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                    if (appointment.RoomId == selectedRoom.RoomID)
                    {
                        RoomsFromAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }

            var now = DateTime.Now;
            EquipmentAvailable.Add(formatAvailableTime(now + TimeSpan.FromHours(1), now + TimeSpan.FromHours(2)));
            EquipmentAvailable.Add(formatAvailableTime(now - TimeSpan.FromMinutes(15), now + TimeSpan.FromMinutes(15)));
        }
    }
}
