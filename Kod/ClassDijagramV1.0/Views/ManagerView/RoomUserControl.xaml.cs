using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RoomUserControl : UserControl
    {

        public RoomController roomController;
        public ObservableCollection<Room> Rooms
        {
            get;
            set;
        }
        public RoomUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            roomController = app.roomController;
            //roomController = new RoomController();
            Rooms = roomController.GetAllRooms();
        }

        private void Delete_room_Click(object sender, RoutedEventArgs e)
        {
            roomController.DeleteRoom(roomIdTextBox.Text);
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                roomIdTextBox.Text,
                (RoomName)Enum.Parse(typeof(RoomName), roomNameTextBox.Text),
                Int32.Parse(roomFloorTextBox.Text),
                Int32.Parse(roomNumberTextBox.Text),
                (RoomStatus)Enum.Parse(typeof(RoomStatus), roomStatusTextBox.Text)
            );
        }

        private void Add_room_Click(object sender, RoutedEventArgs e)
        {
            roomController.AddRoom(RoomFromTextboxes());
        }

        private void Change_room_Click(object sender, RoutedEventArgs e)
        {
            roomController.ChangeRoom(RoomFromTextboxes());
        }
    }
}
