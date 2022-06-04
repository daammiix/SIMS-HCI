using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeEquipmentViewModel
    {
        public RoomController roomController;
        public EquipmentController equipmentController;
        public Storage storage;
        private IRefreshableEquipmentView equipmentView;
        public QuantifiedEquipment quantifiedEquipment { get; set; }
        public ChangeEquipmentWindow changeEquipment;

        private RelayCommand _saveChangedEquipment;
        private RelayCommand _cancelChangedEquipment;

        public ChangeEquipmentViewModel(ChangeEquipmentWindow changeEquipment, QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentController = app.equipmentController;
            this.equipmentView = equipmentView;
            this.changeEquipment = changeEquipment;
            this.quantifiedEquipment = (QuantifiedEquipment)quantifiedEquipment;
            storage = (Storage)roomController.GetRoom("storage");
        }

        public RelayCommand SaveEquipmentEquipment
        {
            get
            {
                _saveChangedEquipment = new RelayCommand(o =>
                {
                    SaveChangedEquipmentAction();
                });

                return _saveChangedEquipment;
            }
        }

        public RelayCommand CancelEquipmentEquipment
        {
            get
            {
                _cancelChangedEquipment = new RelayCommand(o =>
                {
                    changeEquipment.Close();
                });

                return _cancelChangedEquipment;
            }
        }

        private Equipment EquipmentFromTextBoxes()
        {
            return new Equipment(changeEquipment.ChangeEquipmentId.Text, changeEquipment.ChangeEquipmentName.Text, changeEquipment.ChangeType.Text, 100, UnitsType.Units);
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

        public void SaveChangedEquipmentAction()
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(changeEquipment.ChangeEquipmentQuantity.Text);
            String equipmentId = quantifiedEquipment.Equipment.EquipmentID;
            int index = findEquipment(equipmentId);

            equipmentController.ChangeEquipment(equipment);
            roomController.ChangeStorageQuantity(equipmentId, quantity);

            equipmentView.RefreshEquipment();
            changeEquipment.Close();
        }
    }

}
