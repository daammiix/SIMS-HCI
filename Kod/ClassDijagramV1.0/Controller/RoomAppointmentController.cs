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
    public class RoomAppointmentController
    {
        private RoomAppointmentService roomAppointmentService;

        public RoomAppointmentController(RoomAppointmentService roomAppointmentService)
        {
            this.roomAppointmentService = roomAppointmentService;
        }

        public void AddRoomAppointment(RoomAppointment roomAppointment)
        {
            roomAppointmentService.AddRoomAppointment(roomAppointment);
        }

        public void DeleteRoomAppointment(String roomAppointmentID)
        {
            roomAppointmentService.DeleteRoomAppointment(roomAppointmentID);
        }

        public void ChangeRoomAppointment(RoomAppointment roomAppointment)
        {
            roomAppointmentService.ChangeRoomAppointment(roomAppointment);
        }

        public BindingList<RoomAppointment> GetAllRoomAppointments()
        {
            return roomAppointmentService.GetAllRoomAppointments();
        }

        public RoomAppointment? GetRoomAppointmentByID(String roomAppointmentID)
        {
            return roomAppointmentService.GetRoomAppointmentByID(roomAppointmentID);
        }

        public void ScheduledAppointments()
        {
            roomAppointmentService.ScheduledAppointments();
        }

        public void SaveRoomAppointment()
        {
            roomAppointmentService.SaveRoomAppointment();
        }

        /// <summary>
        /// Proverava da li je soba u datom terminu slobodna odnosno nema nikakvi roomAppointmenta
        /// </summary>
        /// <param name="from"></param>
        /// <param name="duration"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool CheckIsTerminFree(DateTime from, TimeSpan duration, Room room)
        {
            return roomAppointmentService.CheckIsTerminFree(from, duration, room);
        }
    }
}
