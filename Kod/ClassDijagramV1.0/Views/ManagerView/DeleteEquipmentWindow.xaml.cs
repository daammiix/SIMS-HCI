using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for DeleteEquipmentWindow.xaml
    /// </summary>
    public partial class DeleteEquipmentWindow : Window
    {
        DeleteEquipmentViewModel _deleteEquipment;
        public DeleteEquipmentWindow(QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            InitializeComponent();

            _deleteEquipment = new DeleteEquipmentViewModel(this, quantifiedEquipment, equipmentView);
            this.DataContext = _deleteEquipment;
        }
    }
}
