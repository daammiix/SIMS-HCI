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
        readonly private String timeFormat = "HH:mm";

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
        public String FromDate { get; set; }
        public String FromTime { get; set; }
        public String ToDate { get; set; }
        public String ToTime{ get; set; }

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
            appointmentController = app.appointmentController;
            this.selectedRoom = selectedRoom;
            Rooms = new BindingList<Room>();
            Equipments = equipmentController.GetAllEquipments();
            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString(timeFormat);
            ToTime = DateTime.Now.ToString(timeFormat);
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

            DateTime fromDatetime, toDatetime;
            DateTime.TryParseExact(FromDate + "T" + FromTime, format, null, System.Globalization.DateTimeStyles.None, out fromDatetime);
            DateTime.TryParseExact(ToDate + "T" + ToTime, format, null, System.Globalization.DateTimeStyles.None, out toDatetime);

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

        private bool checkDateTimeAvailable(DateTime fromDatetime, DateTime toDatetime)
        {
            foreach (var avaible in availabilities) 
            {
                if (fromDatetime < avaible.To && avaible.From < toDatetime)
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
        }


        private void Quantity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateAvailableRooms();
        }
        private void PickerDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        public void UpdateAvailableRooms()
        {
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

            return start.ToString(timeFormat) + " - " + end.ToString(timeFormat);
        }

        public void ListsHandler()
        {
            RoomsFromAvailable.Clear();
            RoomsToAvailable.Clear();
            EquipmentAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            DateTime selectedDate = DateTime.ParseExact(PickerDate.Text, "dd/MM/yyyy", null);
            sourceRoom = (Room)MovingFrom.SelectedItem;
            Equipment selectedEquipment = (Equipment)EquipmentBox.SelectedItem;

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                if (equipmentAppointment.FromDateTime.Date == selectedDate)
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
                if (roomAppointment.startDate.Date == selectedDate)
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
                if (appointment.AppointmentDate.Date == selectedDate)
                {
                    if (appointment.Room.RoomID == sourceRoom.RoomID)
                    {
                        RoomsToAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                    if (appointment.Room.RoomID == selectedRoom.RoomID)
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
