using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class DeleteEquipmentViewModel
    {
        EquipmentController equipmentController;
        RoomController roomController;
        DeleteEquipmentWindow deleteEquipment;
        QuantifiedEquipment quantifiedEquipment;
        private IRefreshableEquipmentView equipmentView;

        private RelayCommand _deleteYesEquipment;
        private RelayCommand _deleteNoEquipment;

        public DeleteEquipmentViewModel(DeleteEquipmentWindow deleteEquipment, QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            var app = Application.Current as App;
            equipmentController = app.equipmentController;
            roomController = app.roomController;
            this.deleteEquipment = deleteEquipment;
            this.quantifiedEquipment = (QuantifiedEquipment)quantifiedEquipment;
            this.equipmentView = equipmentView;

        }

        public RelayCommand DeleteYesEquipment
        {
            get
            {
                _deleteYesEquipment = new RelayCommand(o =>
                {
                    DeleteEquipmentAction();
                });

                return _deleteYesEquipment;
            }
        }

        public RelayCommand DeleteNoEquipment
        {
            get
            {
                _deleteNoEquipment = new RelayCommand(o =>
                {
                    deleteEquipment.Close();
                });

                return _deleteNoEquipment;
            }
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

        private void DeleteEquipmentAction()
        {
            String equipmentId = quantifiedEquipment.Equipment.EquipmentID;
            int i = findEquipment(equipmentId);
            equipmentController.DeleteEquipment(equipmentId);
            if (i == -1)
            {
                throw new Exception();
            }
            ((Storage)roomController.GetRoom("storage")).EquipmentList.RemoveAt(i);
            equipmentView.RefreshEquipment();
            deleteEquipment.Close();
        }
    }
}
