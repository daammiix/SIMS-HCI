using ClassDijagramV1._0.ViewModel;
using Model;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ReservationOfRoom.xaml
    /// </summary>
    public partial class ReservationOfRoom : Window
    {
        public ReservationOfRoom(MainRoomsViewModel _mainRooms, Room selectedRoom)
        {
            InitializeComponent();
            _mainRooms.SetReservationWindowState(this, selectedRoom);
            this.DataContext = _mainRooms;
        }
    }
}
