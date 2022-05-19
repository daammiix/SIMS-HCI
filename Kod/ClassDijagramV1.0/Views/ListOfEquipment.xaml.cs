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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for ListOfEquipment.xaml
    /// </summary>
    public partial class ListOfEquipment : Window
    {

        EquipmentController equipmentController;
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }

        public ListOfEquipment(Room room)
        {
            InitializeComponent();
            var app = Application.Current as App;
            EquipmentList = new BindingList<QuantifiedEquipment>();

            equipmentController = app.equipmentController;
            BindingList<Equipment> allEquipment = equipmentController.GetAllEquipments();
            foreach (var binding in room.EquipmentList)
            {
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == binding.EquipmentID)
                    {
                        EquipmentList.Add(new QuantifiedEquipment(e, binding.Quantity));
                    }
                }
            }
            this.DataContext = this;
        }
        public void CloseListOfEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = EquipmentList.Where(r => (r.Equipment.EquipmentID.ToLower().Contains(txb.Text.ToLower()) || r.Equipment.Name.ToLower().Contains(txb.Text.ToLower()) || r.Equipment.EquipmentType.ToLower().Contains(txb.Text.ToLower()) || r.Quantity.ToString().Contains(txb.Text)));
                Equipment.ItemsSource = null;
                Equipment.ItemsSource = filteredList;
            }
            else
            {
                Equipment.ItemsSource = EquipmentList;
            }
        }
    } 
}
