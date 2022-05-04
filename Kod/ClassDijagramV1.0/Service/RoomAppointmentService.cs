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
    public class RoomAppointmentService
    {
        private RoomAppointmentRepo roomAppointmentRepo;
        private RoomController roomController;

        public RoomAppointmentService(RoomAppointmentRepo roomAppointmentRepo)
        {
            this.roomAppointmentRepo = roomAppointmentRepo;
            var app = Application.Current as App;
            roomController = app.roomController;
        }
        public void AddRoomAppointment(RoomAppointment roomAppointment)
        {
            if (this.CheckIfUniq(roomAppointment, false))
            {
                roomAppointmentRepo.AddRoomAppointment(roomAppointment);
            }
        }

        public void DeleteRoomAppointment(string roomAppointmentID)
        {
            roomAppointmentRepo.DeleteRoomAppointment(roomAppointmentID);
        }

        public void ChangeRoomAppointment(RoomAppointment roomAppointment)
        {
            if (this.CheckIfUniq(roomAppointment, true))
            {
                roomAppointmentRepo.ChangeRoomAppointment(roomAppointment);
            }
        }

        public BindingList<RoomAppointment> GetAllRoomAppointments()
        {
            return roomAppointmentRepo.GetAllRoomAppointments();
        }

        public RoomAppointment? GetRoomAppointmentByID(string roomAppointmentID)
        {
            return roomAppointmentRepo.GetRoomAppointmentByID(roomAppointmentID);
        }

        public Boolean CheckIfUniq(RoomAppointment roomAppointment, bool existingRoomAppointment)
        {
            var roomAppointments = roomAppointmentRepo.GetAllRoomAppointments();
            foreach (var r in roomAppointments)
            {
                if (!existingRoomAppointment && roomAppointment.appointmentID == r.appointmentID)
                {
                    return false;
                }
            }
            return true;
        }

        public void SaveRoomAppointment()
        {
            roomAppointmentRepo.SaveRoomAppointment();
        }

        public void ScheduledAppointments()
        {
            BindingList<RoomAppointment> roomAppointments = roomAppointmentRepo.GetAllRoomAppointments();
            for (int i = 0; i < roomAppointments.Count; i++)
            {
                var appointment = roomAppointments[i];
                if (DateTime.Now < appointment.startDate + appointment.duration)
                {
                    continue;
                }
                if (appointment.newRoomName != null)
                {
                    Room room = roomController.GetARoom(appointment.roomId);
                    room.RoomName = appointment.newRoomName;
                }
                roomAppointmentRepo.DeleteRoomAppointmentAt(i--);
            }
        }
    }
}
