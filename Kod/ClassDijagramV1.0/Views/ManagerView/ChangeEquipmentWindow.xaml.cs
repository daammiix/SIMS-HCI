using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeEquipmentWindow.xaml
    /// </summary>
    public partial class ChangeEquipmentWindow : Window
    {
        private ChangeEquipmentViewModel _changeEquipment;

        public ChangeEquipmentWindow(QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            InitializeComponent();

            _changeEquipment = new ChangeEquipmentViewModel(this, quantifiedEquipment, equipmentView);
            this.DataContext = _changeEquipment;
        }
    }
}
