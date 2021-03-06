using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Controller
{
    public class EquipmentController
    {
        private EquipmentService equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        public void AddEquipment(Equipment equipment)
        {
            equipmentService.AddEquipment(equipment);
        }

        public void DeleteEquipment(String equipmentID)
        {
            equipmentService.DeleteEquipment(equipmentID);
        }

        public void ChangeEquipment(Equipment equipment)
        {
            equipmentService.ChangeEquipment(equipment);
        }

        public BindingList<Equipment> GetAllEquipments()
        {
            return equipmentService.GetAllEquipments();
        }

        public Equipment? GetEquipmentByID(String EquipmentID)
        {
            return equipmentService.GetAEquipment(EquipmentID);
        }
    }
}
