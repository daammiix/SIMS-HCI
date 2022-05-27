using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Views.ManagerView;
using ClassDijagramV1._0.Converters;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
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
    /// Interaction logic for StorageDrugsView.xaml
    /// </summary>
    public partial class StorageDrugsView : UserControl
    {
        private StorageDrugsViewModel _storageDrugs;
        public StorageDrugsView()
        {
            InitializeComponent();
            
            _storageDrugs = new StorageDrugsViewModel();
            this.DataContext = _storageDrugs;
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var txb = sender as TextBox;
        //    if (txb.Text != "")
        //    {
        //        var filteredList = MedicinesList.Where(r => (r.Medicines.ID.ToLower().Contains(txb.Text.ToLower()) || r.Medicines.Name.ToLower().Contains(txb.Text.ToLower()) || r.Medicines.Status.ToLower().Contains(txb.Text.ToLower()) || r.Quantity.ToString().Contains(txb.Text)));
        //        DrugsListGrid.ItemsSource = null;
        //        DrugsListGrid.ItemsSource = filteredList;
        //    }
        //    else
        //    {
        //        DrugsListGrid.ItemsSource = MedicinesList;
        //    }
        //}
    }
}
