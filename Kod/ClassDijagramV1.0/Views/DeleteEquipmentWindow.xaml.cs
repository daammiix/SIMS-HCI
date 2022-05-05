using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using Controller;
using System;
using System.Windows;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for DeleteEquipmentWindow.xaml
    /// </summary>
    public partial class DeleteEquipmentWindow : Window
    {
        EquipmentController equipmentController;
        RoomController roomController;
        QuantifiedEquipment quantifiedEquipment;
        private IRefreshableEquipmentView equipmentView;
        public DeleteEquipmentWindow(QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            equipmentController = app.equipmentController;
            roomController = app.roomController;
            this.equipmentView = equipmentView;
            this.quantifiedEquipment = (QuantifiedEquipment)quantifiedEquipment;
        }

        private void DeleteYesEquipment_Click(object sender, RoutedEventArgs e)
        {
            String equipmentId = quantifiedEquipment.Equipment.EquipmentID;
            int i = findEquipment(equipmentId);
            equipmentController.DeleteEquipment(equipmentId);
            if (i == -1)
            {
                throw new Exception();
            }
            ((Storage)roomController.GetARoom("storage")).EquipmentList.RemoveAt(i);
            equipmentView.RefreshEquipment();
            this.Close();
        }

        public int findEquipment(String equipmentId)
        {
            int i = 0;
            var equipmentList = ((Storage)roomController.GetARoom("storage")).EquipmentList;
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

        private void DeleteNoEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
