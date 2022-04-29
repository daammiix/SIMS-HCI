using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for Equip.xaml
    /// </summary>
    public partial class Equip : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        public RoomController roomController;
        public Room selectedRoom;
        public Room destinationRoom;
        public Equipment selectedEquipment;
        public int quantity;
        public Equip(Room selectedRoom)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            this.selectedRoom = selectedRoom;
        }

        private void SaveEquip_Click(object sender, RoutedEventArgs e)
        {
            destinationRoom = (Room)MovingFrom.SelectedItem;
            quantity = Convert.ToInt32(Quantity.Text);

            DateTime fromDatetime, toDatetime;
            String date = FromDate.Text;
            String time = FromTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out fromDatetime);
            
            date = ToDate.Text;
            time = ToTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out toDatetime);

            Equipment selectedEquipment = (Equipment)EquipmentBox.SelectedItem;
            //roomController.RelocationEquipment(selectedRoom, destinationRoom, selectedEquipment, quantity);
            this.Close();
        }

        private void QuitEquip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
