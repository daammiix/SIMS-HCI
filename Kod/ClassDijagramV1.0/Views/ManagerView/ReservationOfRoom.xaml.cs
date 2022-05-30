using ClassDijagramV1._0.ViewModel;
using ClassDijagramV1._0.Views.ManagerView;
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
