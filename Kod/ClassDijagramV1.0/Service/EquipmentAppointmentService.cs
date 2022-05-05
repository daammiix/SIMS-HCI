using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.Service
{
    public class EquipmentAppointmentService
    {
        private EquipmentAppointmentRepo equipmentAppointmentRepository;
        private RoomController roomController;
        public EquipmentAppointmentService(EquipmentAppointmentRepo equipmentAppointmentServiceRepository)
        {
            this.equipmentAppointmentRepository = equipmentAppointmentServiceRepository;
            var app = Application.Current as App;
            roomController = app.roomController;
        }

        public void AddEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
             equipmentAppointmentRepository.CreateNewEquipmentAppointment(equipmentAppointment);
        }

        public EquipmentAppointment? GetAEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            return equipmentAppointmentRepository.GetEquipmentAppointment(equipmentAppointment);
        }

        public BindingList<EquipmentAppointment> GetAllEquipmentAppointment()
        {
            return equipmentAppointmentRepository.GetAllEquipmentAppointments();
        }

        public void ScheduledAppointment()
        {
            BindingList<EquipmentAppointment> equipmentAppointments = GetAllEquipmentAppointment();

            for (int i = 0; i < equipmentAppointments.Count; i++)
            {
                var equipmentAppointment = equipmentAppointments[i];
                if (equipmentAppointment.Started)
                {
                    continue;
                }
                if(DateTime.Now > equipmentAppointment.FromDateTime)
                {
                    Room room = roomController.GetARoom(equipmentAppointment.RoomFrom);
                    room.removeEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                    equipmentAppointment.Started = true;
                }
                if (DateTime.Now > equipmentAppointment.ToDateTime)
                {
                    Room room = roomController.GetARoom(equipmentAppointment.RoomTo);
                    room.addEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                    equipmentAppointmentRepository.DeleteEquipmentAppointment(i--);
                }
            }
        }
    }
}
