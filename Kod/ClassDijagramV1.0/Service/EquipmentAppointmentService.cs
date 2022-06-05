using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.Service
{
    public class EquipmentAppointmentService
    {
        private EquipmentAppointmentRepo equipmentAppointmentRepository;
        private RoomController roomController;
        private BindingList<EquipmentAppointment> equipmentAppointments;
        public EquipmentAppointmentService(EquipmentAppointmentRepo equipmentAppointmentServiceRepository)
        {
            this.equipmentAppointmentRepository = equipmentAppointmentServiceRepository;
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointments = GetAllEquipmentAppointment();
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
            for (int i = 0; i < equipmentAppointments.Count; i++)
            {
                var equipmentAppointment = equipmentAppointments[i];
                if (DateTime.Now >= equipmentAppointment.ToDateTime)
                {
                    Room roomTo = roomController.GetRoom(equipmentAppointment.RoomTo);
                    roomTo.addEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                    equipmentAppointmentRepository.DeleteEquipmentAppointment(i--);
                }
                if (equipmentAppointment.Started)
                {
                    continue;
                }
                if (DateTime.Now >= equipmentAppointment.FromDateTime)
                {
                    equipmentAppointment.Started = true;

                    Room roomFrom = roomController.GetRoom(equipmentAppointment.RoomFrom);
                    roomFrom.removeEquipment(equipmentAppointment.SelectedEquipment, equipmentAppointment.Quantity);
                }

            }
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
            // ret val
            bool free = true;

            foreach (EquipmentAppointment equipmentAppointment in GetAllEquipmentAppointment())
            {


                string roomFromId = equipmentAppointment.RoomFrom;
                string roomToId = equipmentAppointment.RoomTo;

                // prvo prvoerimo da li premestaj ima veze sa sobom u kojoj zakazujemo pregled ili sta vec
                if (roomFromId.Equals(room.RoomID) || roomToId.Equals(room.RoomID))
                {
                    // Sad proveravamo da li se vremena poklapaju
                    // 1. da li pocinje pre i zavrsava se posle pocetka termina premestaja
                    // 2. da li pocinje izmedju
                    if ((from < equipmentAppointment.FromDateTime && (from + duration) > equipmentAppointment.FromDateTime)
                        || (from >= equipmentAppointment.FromDateTime && from <= equipmentAppointment.ToDateTime))
                    {
                        // Ako se poklapaju i sobe i vreme vratimo false
                        free = false;
                        break;
                    }
                }

            }

            return free;
        }
    }
}
