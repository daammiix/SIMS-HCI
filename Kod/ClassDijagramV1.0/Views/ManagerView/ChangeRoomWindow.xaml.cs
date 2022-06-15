using ClassDijagramV1._0.ViewModel;
using Model;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeRoomWindow.xaml
    /// </summary>
    public partial class ChangeRoomWindow : Window
    {
        private ChangeRoomViewModel _changeRoomViewModel;
        public ChangeRoomWindow(Room room)
        {
            InitializeComponent();
            _changeRoomViewModel = new ChangeRoomViewModel(this, room);
            this.DataContext = _changeRoomViewModel;
        }
    }
}
