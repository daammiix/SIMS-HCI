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
            foreach (var appointment in roomAppointments)
            {
                if (!existingRoomAppointment && roomAppointment.appointmentID == appointment.appointmentID)
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
            BindingList<Room> roomsList = roomController.GetAllRooms();
            for (int i = 0; i < roomAppointments.Count; i++)
            {
                var appointment = roomAppointments[i];
                if (DateTime.Now < appointment.startDate + appointment.duration)
                {
                    continue;
                }
                foreach (var currentRoom in roomsList)
                {
                    if (currentRoom.RoomID == appointment.roomId)
                    {
                        currentRoom.RoomStatus = "Renoviranje";
                    }
                }
                if (appointment.newRoomName != null)
                {
                    Room room = roomController.GetRoom(appointment.roomId);
                    room.RoomName = appointment.newRoomName;
                }
                if (appointment.RoomIDToMerge != null)
                {
                    for(int j = 0; j < roomsList.Count; j++)
                    {
                        var currentRoom = roomsList[j];
                        if (currentRoom.RoomID == appointment.RoomIDToMerge)
                        {
                            roomController.DeleteRoom(currentRoom.RoomID);
                        }
                    }

                }
                if (appointment.RoomToSplit != null)
                {
                    roomController.AddRoom(appointment.RoomToSplit);
                }
                roomAppointmentRepo.DeleteRoomAppointmentAt(i--);
            }
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
            // ret val
            bool free = true;
            // prodjemo kroz sve Room Appointmente
            foreach (RoomAppointment appointment in GetAllRoomAppointments())
            {
                // Proverimo da li je soba u koju mi zakazujemo ista kao soba iz roomAppointmenta
                if (appointment.roomId.Equals(room.RoomID))
                {
                    // Sad proverimo da li se vremena preklapaju
                    // 1. da li pocinje pre i zavrsava se posle pocetka termina premestaja
                    // 2. da li pocinje izmedju
                    if ((from < appointment.startDate && (from + duration) > (appointment.startDate + appointment.duration))
                        || (from >= appointment.startDate && from <= (appointment.startDate + appointment.duration)))
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
