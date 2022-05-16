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

        /// <summary>
        /// Vraca prvu praznu sobu
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool GetFreeRoom(Room room, DateTime start, DateTime end)
        {
            bool retVal = true;
            App app = Application.Current as App;
            AppointmentController appointmentController = app.AppointmentController;

            foreach (Appointment termin in appointmentController.GetAppointments())
            {
                if (termin.RoomId.Equals(room.RoomID) && room.RoomStatus.Equals("Aktivna"))
                {
                    if ( (start >= termin.AppointmentDate && start <= termin.AppointmentDate.Add(termin.Duration))
                         || (end >= termin.AppointmentDate && end <= termin.AppointmentDate.Add(termin.Duration)) 
                         || (start <= termin.AppointmentDate && end >= termin.AppointmentDate.Add(termin.Duration)) )
                    {
                        retVal = false;
                        break;
                    }
                }
            }
            return retVal;
        }
    }
}
