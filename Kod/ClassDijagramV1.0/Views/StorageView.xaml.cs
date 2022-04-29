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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for StorageView.xaml
    /// </summary>
    public partial class StorageView : UserControl
    {
        private RoomController roomController;
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public StorageView()
        {
            InitializeComponent();

            var app = Application.Current as App;
            roomController = app.roomController;
            Rooms = roomController.GetAllRooms();
        }

    }
}
