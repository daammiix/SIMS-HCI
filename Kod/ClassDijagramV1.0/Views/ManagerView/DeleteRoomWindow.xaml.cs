using ClassDijagramV1._0.ViewModel;
using System;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for DeleteRoomWindow.xaml
    /// </summary>
    public partial class DeleteRoomWindow : Window
    {
        private DeleteRoomViewModel _deleteRoomViewModel;
        public DeleteRoomWindow(String roomID)
        {
            InitializeComponent();
            _deleteRoomViewModel = new DeleteRoomViewModel(this, roomID);
            this.DataContext = _deleteRoomViewModel;
        }
    }
}
