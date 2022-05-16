using ClassDijagramV1._0.Controller;
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
    /// Interaction logic for StorageSuppliesView.xaml
    /// </summary>
    public partial class StorageSuppliesView : UserControl, IRefreshableEquipmentView
    {
        EquipmentController equipmentController;
        RoomController roomController;
        BindingList<Equipment> allEquipment;
        Storage storage;
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }
        public StorageSuppliesView()
        {
            InitializeComponent();
            var app = Application.Current as App;
            EquipmentList = new BindingList<QuantifiedEquipment>();
            equipmentController = app.equipmentController;
            roomController = app.roomController;
            allEquipment = equipmentController.GetAllEquipments();
            storage = (Storage)roomController.GetRoom("storage");
            RefreshEquipment();

            this.DataContext = this;
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow(this, "Potrošna roba");
            addEquipmentWindow.Show();
        }

        private void ChangeEquipment_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedEquipment? quantifiedEquipment = (QuantifiedEquipment?)EquipmentListGrid.SelectedItem;
            if (quantifiedEquipment != null)
            {
                ChangeEquipmentWindow changeEquipmentWindow = new ChangeEquipmentWindow(quantifiedEquipment, this);
                changeEquipmentWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedEquipment? quantifiedEquipment = (QuantifiedEquipment?)EquipmentListGrid.SelectedItem;
            if (quantifiedEquipment != null)
            {
                var equipmentId = quantifiedEquipment;
                DeleteEquipmentWindow deleteEquipmentWindow = new DeleteEquipmentWindow(equipmentId, this);
                deleteEquipmentWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        public void RefreshEquipment()
        {
            EquipmentList.Clear();
            foreach (var binding in storage.EquipmentList)
            {
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == binding.EquipmentID)
                    {
                        EquipmentList.Add(new QuantifiedEquipment(e, binding.Quantity));
                    }
                }
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var quantifiedEquipment = (QuantifiedEquipment?)EquipmentListGrid.SelectedItem;
            if (quantifiedEquipment != null)
            {
                StorageEquip storageEquip = new StorageEquip(quantifiedEquipment);
                storageEquip.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = EquipmentList.Where(r => (r.Equipment.EquipmentID.ToLower().Contains(txb.Text.ToLower()) || r.Equipment.Name.ToLower().Contains(txb.Text.ToLower()) || r.Quantity.ToString().Contains(txb.Text)));
                EquipmentListGrid.ItemsSource = null;
                EquipmentListGrid.ItemsSource = filteredList;
            }
            else
            {
                EquipmentListGrid.ItemsSource = EquipmentList;
            }
        }
    }
}
