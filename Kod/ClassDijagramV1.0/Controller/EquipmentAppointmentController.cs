using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using Model;
using System;
using System.ComponentModel;

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

        /// <summary>
        /// Proverava da li je sobe zauzeta u odgovarajucem periodu zbog premestanja opreme
        /// </summary>
        /// <param name="from"></param>
        /// <param name="duration"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool CheckIsTerminFree(DateTime from, TimeSpan duration, Room room)
        {
            return equipmentAppointmentService.CheckIsTerminFree(from, duration, room);
        }
    }
}
