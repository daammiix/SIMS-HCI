using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class EquipmentRepo
    {
        private String path = "..\\..\\..\\Data\\equipment.json";
        private BindingList<Equipment> equipments = new BindingList<Equipment>();

        public EquipmentRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<Equipment>? jsonEquipments = JsonSerializer.Deserialize<BindingList<Equipment>>(jsonData);
            if (jsonEquipments != null)
            {
                this.equipments = jsonEquipments;
            }
        }

        private int findEquipment(String equipmentId)
        {
            int index = 0;
            foreach (Equipment equipment in equipments)
            {
                if (equipment.EquipmentID == equipmentId)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public Equipment? GetEquipment(String equipmentID)
        {
            foreach (var equipment in equipments)
            {
                if (equipment.EquipmentID == equipmentID)
                {
                    return equipment;
                }
            }
            return null;
        }

        public BindingList<Equipment> GetAllEquipments()
        {
            return equipments;
        }

        public Equipment? SetEquipment(Equipment equipment)
        {
            int index = findEquipment(equipment.EquipmentID);
            if (index == -1)
            {
                return null;
            }
            equipments.RemoveAt(index);
            equipments.Insert(index, equipment);
            writeEquipments();
            return equipment;
        }

        public Equipment CreateNewEquipment(Equipment equipment)
        {
            equipments.Add(equipment);
            writeEquipments();
            return equipment;
        }

        public void DeleteEquipment(String equipmentID)
        {
            foreach (var equipment in equipments)
            {
                if (equipment.EquipmentID == equipmentID)
                {
                    equipments.Remove(equipment);
                    break;
                }
            }
            this.writeEquipments();
        }
        private void writeEquipments()
        {
            String jsonString = JsonSerializer.Serialize(equipments);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
