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
    /// Interaction logic for ReservationOfRoom.xaml
    /// </summary>
    public partial class ReservationOfRoom : Window
    {
        public Room selectedRoom;
        public ReservationOfRoom(Room selectedRoom)
        {
            InitializeComponent();
            var app = Application.Current as App;
            this.selectedRoom = selectedRoom;
        }

        private void CloseReservationOfRoom_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Equip_Click(object sender, RoutedEventArgs e)
        {
            Equip equip = new Equip(selectedRoom);
            equip.Show();
            this.Close();
        }

        private void Renovating_Click(object sender, RoutedEventArgs e)
        {
            RenovaitingWindow renovating = new RenovaitingWindow(selectedRoom);
            renovating.Show();
            this.Close();
        }

        private void ChangingPurpose_Click(object sender, RoutedEventArgs e)
        {
            ChangingPurposeWindow changingPurpose = new ChangingPurposeWindow();
            changingPurpose.Show();
            this.Close();
        }
    }
}
