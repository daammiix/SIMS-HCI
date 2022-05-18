using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using Controller;
using System;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for AddEquipmentWindow.xaml
    /// </summary>
    public partial class AddEquipmentWindow : Window
    {
        public RoomController roomController;
        public EquipmentController equipmentController;
        private IRefreshableEquipmentView equipmentView;
        public AddEquipmentWindow(IRefreshableEquipmentView equipmentView, String type)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentController = app.equipmentController;
            this.equipmentView = equipmentView;
            AddType.Text = type;
        }

        private Equipment EquipmentFromTextBoxes()
        {
            return new Equipment(AddEquipmentId.Text, AddEquipmentNAme.Text, AddType.Text, 100, UnitsType.Units);
        }

        private void SaveAddedEquipment_Click(object sender, RoutedEventArgs e)
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(AddEquipmentQuantity.Text);
            equipmentController.AddEquipment(equipment);
            ((Storage)roomController.GetARoom("storage")).addNewEquipment(equipment, quantity);
            equipmentView.RefreshEquipment();
            this.Close();
        }

        private void QuitAddedEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
