using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassDijagramV1._0.Helpers;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for StorageEquip.xaml
    /// </summary>
    public partial class StorageEquip : Window
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
        public Room selectedToRoom { get; set; }
        public Storage storage;
        public Equipment selectedEquipment;
        public QuantifiedEquipment QEquipment { get; set; }
        public int quantity;

        public BindingList<String> RoomsAvailable { get; set; }
        public String FromDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String FromTime { get; set; } = DateTime.Now.ToString("HH:mm");
        public String ToDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String ToTime { get; set; } = DateTime.Now.ToString("HH:mm");
        private BindingList<Availability> availabilities { get; set; }

        public StorageEquip(QuantifiedEquipment qEquipment)
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            equipmentAppointmentController = app.equipmentAppointmentController;
            equipmentController = app.equipmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.appointmentController;
            roomController = app.roomController;

            Rooms = roomController.GetAllRooms();
            storage = (Storage)roomController.GetARoom("storage");
            this.QEquipment = qEquipment;
            this.selectedEquipment = QEquipment.Equipment;

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        private void SaveStorageEquip_Click(object sender, RoutedEventArgs e)
        {
            selectedToRoom = (Room)MovingTo.SelectedItem;
            quantity = Convert.ToInt32(Quantity.Text);

            DateTime fromDatetime = DateTime.ParseExact(FromDateField.Text + "T" + FromTimeField.Text, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDateField.Text + "T" + ToTimeField.Text, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var equipmentAppointment = new EquipmentAppointment(storage.RoomID, selectedToRoom.RoomID, selectedEquipment, quantity, fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);

            this.Close();
        }

        private void QuitStorageEquip_Click(object sender, RoutedEventArgs e)
        {
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

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAvailabilityOfEquipment();
        }

        private void FromDateField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListsHandler();
        }

        private void MovingTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));

            return start.ToString(fullFormat) + " - " + end.ToString(fullFormat);
        }

        public void UpdateAvailabilityOfEquipment()
        {
            try
            {
                quantity = Convert.ToInt32(Quantity.Text);
            }
            catch (FormatException ex)
            {
                quantity = 0;
                return;
            }
            if (!(QEquipment.Quantity >= quantity))
            {
                WarningQuantity warningQuantity = new WarningQuantity();
                warningQuantity.Show();
                Quantity.Clear();
                return;
            }
        }

        public void ListsHandler()
        {
            RoomsAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            Room? selectedToRoom = (Room?)MovingTo.SelectedItem;
            if (selectedToRoom == null)
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
                    if (selectedToRoom != null && (equipmentAppointment.RoomFrom == selectedToRoom.RoomID || equipmentAppointment.RoomTo == selectedToRoom.RoomID))
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
                    if (roomAppointment.roomId == selectedToRoom.RoomID)
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
                    if (appointment.Room.RoomID == selectedToRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }
        }
    }
}
