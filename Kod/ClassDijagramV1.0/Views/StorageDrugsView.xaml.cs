using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Converters;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for StorageDrugsView.xaml
    /// </summary>
    public partial class StorageDrugsView : UserControl
    {
        DrugsController drugsController;
        Drugs drug;
        RoomController roomController;
        BindingList<Equipment> allEquipment;
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }
        public StorageDrugsView()
        {
            InitializeComponent();
        }

        private void AddDrugs_Click(object sender, RoutedEventArgs e)
        {
            AddDrugsWindow addDrugsWindow = new AddDrugsWindow();
            addDrugsWindow.Show();
        }

        private void ChangeDrugs_Click(object sender, RoutedEventArgs e)
        {
            ChangeDrugsWindow changeDrugsWindow = new ChangeDrugsWindow();
            changeDrugsWindow.Show();
        }

        private void DeleteDrugs_Click(object sender, RoutedEventArgs e)
        {
            DeleteDrugsWindow deleteDrugsWindow = new DeleteDrugsWindow();
            deleteDrugsWindow.Show();
        }
    }
}
