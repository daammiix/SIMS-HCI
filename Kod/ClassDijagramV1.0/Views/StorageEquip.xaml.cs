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
        public String FromDate { get; set; }
        public String FromTime { get; set; }
        public String ToDate { get; set; }
        public String ToTime { get; set; }
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

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString(timeFormat);
            ToTime = DateTime.Now.ToString(timeFormat);

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();
        }

        private void SaveStorageEquip_Click(object sender, RoutedEventArgs e)
        {
            selectedToRoom = (Room)MovingTo.SelectedItem;
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

            var equipmentAppointment = new EquipmentAppointment(storage.RoomID, selectedToRoom.RoomID, selectedEquipment, quantity, fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);

            this.Close();
        }

        private void QuitStorageEquip_Click(object sender, RoutedEventArgs e)
        {
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

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAvailabilityOfEquipment();
        }

        private void PickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));

            return start.ToString(timeFormat) + " - " + end.ToString(timeFormat);
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

            DateTime selectedDate = DateTime.ParseExact(PickerDate.Text, "dd/MM/yyyy", null);
            selectedToRoom = (Room)MovingTo.SelectedItem;

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                if (equipmentAppointment.FromDateTime.Date == selectedDate)
                {
                    if (selectedToRoom != null && (equipmentAppointment.RoomFrom == selectedToRoom.RoomID || equipmentAppointment.RoomTo == selectedToRoom.RoomID))
                    {
                        RoomsAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                if (roomAppointment.startDate.Date == selectedDate)
                {
                    if (roomAppointment.roomId == selectedToRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                }
            }
            foreach (var appointment in appointments)
            {
                if (appointment.AppointmentDate.Date == selectedDate)
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
