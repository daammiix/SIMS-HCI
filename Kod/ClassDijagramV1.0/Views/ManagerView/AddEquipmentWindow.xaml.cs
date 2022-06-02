using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for AddEquipmentWindow.xaml
    /// </summary>
    public partial class AddEquipmentWindow : Window
    {
        private AddEquipmentViewModel _addEquipment;
        public AddEquipmentWindow(IRefreshableEquipmentView equipmentView, String type)
        {
            InitializeComponent();

            _addEquipment = new AddEquipmentViewModel(this, equipmentView, type);
            this.DataContext = _addEquipment;
        }

    }
}
