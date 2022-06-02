using ClassDijagramV1._0.Model;
using System;
using System.ComponentModel;
using System.Text.Json;

namespace ClassDijagramV1._0.Repository
{
    public class EquipmentAppointmentRepo
    {
        private String path = "..\\..\\..\\Data\\equipmentAppointment.json";
        private BindingList<EquipmentAppointment> equipmentAppointments = new BindingList<EquipmentAppointment>();

        public EquipmentAppointmentRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<EquipmentAppointment>? jsonEquipmentAppointments = JsonSerializer.Deserialize<BindingList<EquipmentAppointment>>(jsonData);
            if (jsonEquipmentAppointments != null)
            {
                this.equipmentAppointments = jsonEquipmentAppointments;
            }
        }

        public EquipmentAppointment? GetEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            foreach (var equipmentAppoint in equipmentAppointments)
            {
                if (equipmentAppoint == equipmentAppointment)
                {
                    return equipmentAppoint;
                }
            }
            return null;
        }

        public BindingList<EquipmentAppointment> GetAllEquipmentAppointments()
        {
            return equipmentAppointments;
        }


        public EquipmentAppointment CreateNewEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            equipmentAppointments.Add(equipmentAppointment);
            writeEquipmentAppointments();
            return equipmentAppointment;
        }

        public void DeleteEquipmentAppointment(int index)
        {
            equipmentAppointments.RemoveAt(index);
            this.writeEquipmentAppointments();
        }
        private void writeEquipmentAppointments()
        {
            String jsonString = JsonSerializer.Serialize(equipmentAppointments);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
