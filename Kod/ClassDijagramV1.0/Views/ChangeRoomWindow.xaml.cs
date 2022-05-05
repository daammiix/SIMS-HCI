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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for ChangeRoomWindow.xaml
    /// </summary>
    public partial class ChangeRoomWindow : Window
    {
        public RoomController roomController;

        public Room room;

        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public ChangeRoomWindow(Room room)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            Rooms = roomController.GetAllRooms();
            this.room = room;
            this.DataContext = room;
        }
        private Room RoomFromTextboxes()
        {
            return new Room(
                room.RoomID,
                ChangeName.Text,
                Int32.Parse(ChangeFloor.Text),
                Int32.Parse(ChangeNumber.Text),
                ChangeStatus.Text,
                room.EquipmentList
            );
        }

        private void SaveChangedRoom_Click(object sender, RoutedEventArgs e)
        {
            roomController.ChangeRoom(RoomFromTextboxes());
            this.Close();
        }

        private void QuitChangededRoom_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
