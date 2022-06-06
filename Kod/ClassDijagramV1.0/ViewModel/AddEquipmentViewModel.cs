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
    public class AddEquipmentViewModel
    {
        private String _equipmentID;
        private String _equipmentName;
        private String _equipmentType;
        private String _quantity;

        public RoomController roomController;
        public EquipmentController equipmentController;
        private IRefreshableEquipmentView equipmentView;
        public AddEquipmentWindow addEquipment;

        private RelayCommand _saveNewEquipment;
        private RelayCommand _cancelNewEquipment;

        public AddEquipmentViewModel(AddEquipmentWindow addEquipment, IRefreshableEquipmentView equipmentView, String type)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentController = app.equipmentController;
            this.addEquipment = addEquipment;
            this.equipmentView = equipmentView;
            EquipmentType = type;
        }

        public String EquipmentID
        {
            get { return _equipmentID; }
            set
            {
                if (_equipmentID == value) { return; }
                _equipmentID = value;
            }
        }
        public String EquipmentName
        {
            get { return _equipmentName; }
            set
            {
                if (_equipmentName == value) { return; }
                _equipmentName = value;
            }
        }
        public String EquipmentType
        {
            get { return _equipmentType; }
            set
            {
                if (_equipmentType == value) { return; }
                _equipmentType = value;
            }
        }
        public String Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
            }
        }

        public RelayCommand SaveNewEquipment
        {
            get
            {
                _saveNewEquipment = new RelayCommand(o =>
                {
                    SaveNewEquipmentAction();
                });

                return _saveNewEquipment;
            }
        }

        public RelayCommand CancelNewEquipment
        {
            get
            {
                _cancelNewEquipment = new RelayCommand(o =>
                {
                    addEquipment.Close();
                });

                return _cancelNewEquipment;
            }
        }

        private Equipment EquipmentFromTextBoxes()
        {
            return new Equipment(EquipmentID, EquipmentName, EquipmentType, 100, UnitsType.Units);
        }

        public void SaveNewEquipmentAction()
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(Quantity);
            equipmentController.AddEquipment(equipment);
            ((Storage)roomController.GetRoom("storage")).addNewEquipment(equipment, quantity);
            equipmentView.RefreshEquipment();
            addEquipment.Close();
        }
    }
}
