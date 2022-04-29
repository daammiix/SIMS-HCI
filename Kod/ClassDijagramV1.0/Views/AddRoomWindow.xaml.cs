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
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public RoomController roomController;

        public AddRoomWindow()
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                AddId.Text,
                AddName.Text,
                Int32.Parse(AddFloor.Text),
                Int32.Parse(AddNumber.Text),
                AddStatus.Text
            );
        }

        private void SaveAddedRoom_Click(object sender, RoutedEventArgs e)
        {
            roomController.AddRoom(RoomFromTextboxes());
            this.Close();
        }

        private void QuitAddedRoom_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
