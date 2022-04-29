using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class EquipmentAppointmentService
    {
        private EquipmentAppointmentRepo equipmentAppointmentRepository;
        public EquipmentAppointmentService(EquipmentAppointmentRepo equipmentAppointmentServiceRepository)
        {
            this.equipmentAppointmentRepository = equipmentAppointmentServiceRepository;
        }

        public void AddEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            if (this.CheckIfUniq(equipmentAppointment, false))
            {
                equipmentAppointmentRepository.CreateNewEquipmentAppointment(equipmentAppointment);
            }
        }

        public void DeleteEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            equipmentAppointmentRepository.DeleteEquipmentAppointment(equipmentAppointment);
        }


        public EquipmentAppointment? GetAEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            return equipmentAppointmentRepository.GetEquipmentAppointment(equipmentAppointment);
        }

        public BindingList<EquipmentAppointment> GetAllEquipmentAppointment()
        {
            return equipmentAppointmentRepository.GetAllEquipmentAppointments();
        }

        public Boolean CheckIfUniq(EquipmentAppointment equipmentAppointment, bool existingEquipmentAppointment)
        {
            // TODO
            return false;
        }

        public void ScheduledAppointment()
        {
            BindingList<EquipmentAppointment> equipmentAppointments = GetAllEquipmentAppointment();

            foreach (EquipmentAppointment equipmentAppointment in equipmentAppointments)
            {
                if(DateTime.Now > equipmentAppointment.FromDateTime)
                {
                    equipmentAppointment.RoomFrom.removeEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                }
                if (DateTime.Now > equipmentAppointment.ToDateTime)
                {
                    equipmentAppointment.RoomTo.addEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                }
            }
        }
    }
}
