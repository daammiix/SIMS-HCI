using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Service
{
    public class EquipmentService
    {
        private EquipmentRepo equipmentRepository;
        public EquipmentService(EquipmentRepo equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        public void AddEquipment(Equipment equipment)
        {
            if (this.CheckIfUniq(equipment, false))
            {
                equipmentRepository.CreateNewEquipment(equipment);
            }
        }

        public void DeleteEquipment(String equipmentID)
        {
            equipmentRepository.DeleteEquipment(equipmentID);
        }

        public void ChangeEquipment(Equipment equipment)
        {
            if (this.CheckIfUniq(equipment, true))
            {
                equipmentRepository.SetEquipment(equipment);
            }
        }

        public Equipment? GetAEquipment(String equipmentID)
        {
            return equipmentRepository.GetEquipment(equipmentID);
        }

        public BindingList<Equipment> GetAllEquipments()
        {
            return equipmentRepository.GetAllEquipments();
        }

        public Boolean CheckIfUniq(Equipment equipment, bool existingEquipment)
        {
            var equipments = equipmentRepository.GetAllEquipments();
            foreach (var r in equipments)
            {
                if (!existingEquipment && equipment.EquipmentID == r.EquipmentID)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
