using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : UserControl
    {
        private RoomController roomController;
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public RoomsView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            roomController = app.roomController;
            Rooms = roomController.GetAllRooms();
        }


        private void GenerateRoom_Click(object sender, RoutedEventArgs e)
        {
            RenovatingAndChangingPurpose renovatingAndChangingPurpose = new RenovatingAndChangingPurpose();
            renovatingAndChangingPurpose.Show();
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            AddRoomWindow addRoom = new AddRoomWindow();
            addRoom.Show();
        }

        private void ChangeRoom_Click(object sender, RoutedEventArgs e)
        {
            Room selected = (Room)RoomList.SelectedItem;
            if (selected != null)
            {
                ChangeRoomWindow changeRoom = new ChangeRoomWindow(selected);
                changeRoom.Show();
            }
            else {
                Warning warning = new Warning();
                warning.Show();
            }

        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Room selected = (Room)RoomList.SelectedItem;
            if (selected != null)
            {
                String roomId = selected.RoomID;
                DeleteRoomWindow deleteRoom = new DeleteRoomWindow(roomId);
                deleteRoom.Show();
            }
            else { 
                Warning warning = new Warning();
                warning.Show();
            }

        }

        public void OnEquipmentClick(object sender, RoutedEventArgs e)
        {
            Room selected = (Room)RoomList.SelectedItem;
            ListOfEquipment listOfEquipment = new ListOfEquipment(selected);
            listOfEquipment.Show();
        }

        public void OnReservationClick(object sender, RoutedEventArgs e)
        {
            Room selected = (Room)RoomList.SelectedItem;
            ReservationOfRoom reservationOfRoom = new ReservationOfRoom(selected);
            reservationOfRoom.Show();
        }
    }
}
