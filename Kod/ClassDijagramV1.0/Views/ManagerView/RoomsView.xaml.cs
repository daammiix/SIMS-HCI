using ClassDijagramV1._0.ViewModel;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : UserControl
    {
        //private RoomsViewModel _roomsViewModel;
        public RoomsView()
        {
            InitializeComponent();

            //_roomsViewModel = new RoomsViewModel();
            //this.DataContext = _roomsViewModel;

        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var txb = sender as TextBox;
        //    if (txb.Text != "")
        //    {
        //        var filteredList = Rooms.Where(r => (r.RoomID.ToLower().Contains(txb.Text.ToLower()) || r.RoomName.ToLower().Contains(txb.Text.ToLower()) || r.Floor.ToString().Contains(txb.Text) || r.RoomNumber.ToString().Contains(txb.Text) || r.RoomStatus.ToLower().Contains(txb.Text.ToLower())));
        //        RoomList.ItemsSource = null;
        //        RoomList.ItemsSource = filteredList;
        //    }
        //    else
        //    {
        //        RoomList.ItemsSource = Rooms;
        //    }

        //}
    }
}
