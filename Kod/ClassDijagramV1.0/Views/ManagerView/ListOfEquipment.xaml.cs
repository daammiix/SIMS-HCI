using Model;
using System;
using Controller;
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
using System.Collections.ObjectModel;
using ClassDijagramV1._0.Controller;
using System.ComponentModel;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ListOfEquipment.xaml
    /// </summary>
    public partial class ListOfEquipment : Window
    {
        private ListOfEquipmentViewModel _listOfEquipmentViewModel;

        public ListOfEquipment(Room room)
        {
            InitializeComponent();
            _listOfEquipmentViewModel = new ListOfEquipmentViewModel(this, room);
            this.DataContext = _listOfEquipmentViewModel;
        }

    } 
}
