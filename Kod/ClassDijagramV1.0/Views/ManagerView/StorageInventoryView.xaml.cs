using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Views.ManagerView;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using Controller;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClassDijagramV1._0.ViewModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for StorageInventoryView.xaml
    /// </summary>
    public partial class StorageInventoryView : UserControl
    {
        private StorageInventoryViewModel _storageInventory;
        public StorageInventoryView()
        {
            InitializeComponent();

            _storageInventory = new StorageInventoryViewModel();
            this.DataContext = _storageInventory;
        }

        //private void Hyperlink_Click(object sender, RoutedEventArgs e)
        //{
        //    var quantifiedEquipment = (QuantifiedEquipment?)EquipmentListGrid.SelectedItem;
        //    if (quantifiedEquipment != null)
        //    {
        //        StorageEquip storageEquip = new StorageEquip(quantifiedEquipment);
        //        storageEquip.Show();
        //    }
        //    else
        //    {
        //        Warning warning = new Warning();
        //        warning.Show();
        //    }
        //}
    }
}
