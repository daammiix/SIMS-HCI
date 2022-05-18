using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
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
using ClassDijagramV1._0.Helpers;
using Controller;
using ClassDijagramV1._0.Model.Enums;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for ChangeEquipmentWindow.xaml
    /// </summary>
    public partial class ChangeEquipmentWindow : Window
    {
        public RoomController roomController;
        public EquipmentController equipmentController;
        public Storage storage;
        private IRefreshableEquipmentView equipmentView;
        QuantifiedEquipment quantifiedEquipment;

        public ChangeEquipmentWindow(QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentController = app.equipmentController;
            this.equipmentView = equipmentView;
            this.quantifiedEquipment = (QuantifiedEquipment)quantifiedEquipment;
            storage = (Storage)roomController.GetRoom("storage");
            this.DataContext = quantifiedEquipment;
        }

        private Equipment EquipmentFromTextBoxes()
        {
            return new Equipment(ChangeEquipmentId.Text, ChangeEquipmentName.Text, ChangeType.Text, 100, UnitsType.Units);
        }

        public int findEquipment(String equipmentId)
        {
            int i = 0;
            var equipmentList = ((Storage)roomController.GetRoom("storage")).EquipmentList;
            foreach (var e in equipmentList)
            {
                if (e.EquipmentID == equipmentId)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void SaveChangedEquipment_Click(object sender, RoutedEventArgs e)
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(ChangeEquipmentQuantity.Text);
            String equipmentId = quantifiedEquipment.Equipment.EquipmentID;
            int index = findEquipment(equipmentId);

            equipmentController.ChangeEquipment(equipment);
            roomController.ChangeStorageQuantity(equipmentId, quantity);

            equipmentView.RefreshEquipment();
            this.Close();
        }

        private void QuitChangededEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
