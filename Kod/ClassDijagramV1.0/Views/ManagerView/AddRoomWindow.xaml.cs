using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        private AddRoomViewModel _addRoomViewModel;
        public AddRoomWindow()
        {
            InitializeComponent();
            _addRoomViewModel = new AddRoomViewModel(this);
            this.DataContext = _addRoomViewModel;
        }

    }
}
