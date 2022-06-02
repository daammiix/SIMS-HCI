using ClassDijagramV1._0.ViewModel;
using Model;
using System.Windows;

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
