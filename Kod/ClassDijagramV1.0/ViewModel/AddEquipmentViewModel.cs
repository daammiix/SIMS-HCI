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
            addEquipment.AddType.Text = type;
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
            return new Equipment(addEquipment.AddEquipmentId.Text, addEquipment.AddEquipmentNAme.Text, addEquipment.AddType.Text, 100, UnitsType.Units);
        }

        public void SaveNewEquipmentAction()
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(addEquipment.AddEquipmentQuantity.Text);
            equipmentController.AddEquipment(equipment);
            ((Storage)roomController.GetRoom("storage")).addNewEquipment(equipment, quantity);
            equipmentView.RefreshEquipment();
            addEquipment.Close();
        }
    }
}
