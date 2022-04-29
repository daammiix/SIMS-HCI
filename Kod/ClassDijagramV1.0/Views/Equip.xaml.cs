using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for Equip.xaml
    /// </summary>
    public partial class Equip : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        public RoomController roomController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public BindingList<Room> Rooms { get; set; }
        public Room selectedRoom { get; set; }
        public Room destinationRoom;
        public Equipment selectedEquipment;
        public int quantity;
        public Equip(Room selectedRoom)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            this.selectedRoom = selectedRoom;
            Rooms = roomController.GetAllRooms();
            DataContext = this;
        }

        private void SaveEquip_Click(object sender, RoutedEventArgs e)
        {
            destinationRoom = (Room)MovingFrom.SelectedItem;
            Equipment selectedEquipment = (Equipment)EquipmentBox.SelectedItem;
            quantity = Convert.ToInt32(Quantity.Text);

            DateTime fromDatetime, toDatetime;
            String date = FromDate.Text;
            String time = FromTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out fromDatetime);

            date = ToDate.Text;
            time = ToTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out toDatetime);

            var equipmentAppointment = new EquipmentAppointment(selectedRoom, destinationRoom, selectedEquipment, quantity, fromDatetime, toDatetime);
            equipmentAppointmentController.AddEquipmentAppointment(equipmentAppointment);
            this.Close();
        }

        private void QuitEquip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
