using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class EquipmentAppointmentController
    {
        private EquipmentAppointmentService equipmentAppointmentService;

        public EquipmentAppointmentController(EquipmentAppointmentService equipmentAppointmentService)
        {
            this.equipmentAppointmentService = equipmentAppointmentService;
        }

        public void AddEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            equipmentAppointmentService.AddEquipmentAppointment(equipmentAppointment);
        }

        public void DeleteEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            equipmentAppointmentService.DeleteEquipmentAppointment(equipmentAppointment);
        }

        public EquipmentAppointment? GetAEquipmentAppointment(EquipmentAppointment equipmentAppointment)
        {
            return equipmentAppointmentService.GetAEquipmentAppointment(equipmentAppointment);
        }

        public BindingList<EquipmentAppointment> GetAllEquipmentAppointment()
        {
            return equipmentAppointmentService.GetAllEquipmentAppointment();
        }

        public void ScheduledAppointment()
        {
            equipmentAppointmentService.ScheduledAppointment();
        }
    }
}
