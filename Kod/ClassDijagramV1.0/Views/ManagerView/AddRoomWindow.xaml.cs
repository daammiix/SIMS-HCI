using ClassDijagramV1._0.ViewModel;
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
