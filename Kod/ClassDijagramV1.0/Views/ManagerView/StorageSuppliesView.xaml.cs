using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Views.ManagerView;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using Controller;
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
using ClassDijagramV1._0.ViewModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for StorageSuppliesView.xaml
    /// </summary>
    public partial class StorageSuppliesView : UserControl
    {
        private StorageSuppliesViewModel _storageSupplies;
        public StorageSuppliesView()
        {
            InitializeComponent();

            _storageSupplies = new StorageSuppliesViewModel();
            this.DataContext = _storageSupplies;
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

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var txb = sender as TextBox;
        //    if (txb.Text != "")
        //    {
        //        var filteredList = EquipmentList.Where(r => (r.Equipment.EquipmentID.ToLower().Contains(txb.Text.ToLower()) || r.Equipment.Name.ToLower().Contains(txb.Text.ToLower()) || r.Quantity.ToString().Contains(txb.Text)));
        //        EquipmentListGrid.ItemsSource = null;
        //        EquipmentListGrid.ItemsSource = filteredList;
        //    }
        //    else
        //    {
        //        EquipmentListGrid.ItemsSource = EquipmentList;
        //    }
        //}
    }
}
