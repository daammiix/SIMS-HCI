using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.Service
{
    public class RoomAppointmentService
    {
        private RoomAppointmentRepo roomAppointmentRepo;
        private RoomController roomController;
        private BindingList<RoomAppointment> roomAppointments;
        private BindingList<Room> roomsList;

        public RoomAppointmentService(RoomAppointmentRepo roomAppointmentRepo)
        {
            this.roomAppointmentRepo = roomAppointmentRepo;
            App app = Application.Current as App;
            roomController = app.roomController;
            roomAppointments = roomAppointmentRepo.GetAllRoomAppointments();
            roomsList = roomController.GetAllRooms();

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

        private Boolean CheckIfUniq(RoomAppointment roomAppointment, bool existingRoomAppointment)
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
            for (int i = 0; i < roomAppointments.Count; i++)
            {
                var appointment = roomAppointments[i];
                if (DateTime.Now < appointment.startDate)
                {
                    continue;
                }
                if (DateTime.Now < appointment.startDate + appointment.duration)
                {
                    ChangingStatusRenovating(appointment);
                    continue;
                }

                ChangingStatusActive(appointment);

                ChangePurpose(appointment);
                MergeRoom(appointment);
                SplitRoom(appointment);

                roomAppointmentRepo.DeleteRoomAppointmentAt(i--);
            }
        }


        private void SplitRoom(RoomAppointment appointment)
        {
            if (appointment.RoomToSplit != null)
            {
                roomController.AddRoom(appointment.RoomToSplit);
            }
        }

        private void ChangePurpose(RoomAppointment appointment)
        {
            if (appointment.newRoomName != null)
            {
                Room room = roomController.GetRoom(appointment.roomId);
                room.RoomName = appointment.newRoomName;
            }
        }

        private void MergeRoom(RoomAppointment appointment)
        {
            if (appointment.RoomIDToMerge != null)
            {
                for (int j = 0; j < roomsList.Count; j++)
                {
                    var currentRoom = roomsList[j];
                    if (currentRoom.RoomID == appointment.RoomIDToMerge)
                    {
                        roomController.DeleteRoom(currentRoom.RoomID);
                    }
                }
            }
        }

        private void ChangingStatusActive(RoomAppointment appointment)
        {
            foreach (var currentRoom in roomsList)
            {
                if (currentRoom.RoomID == appointment.roomId)
                {
                    currentRoom.RoomStatus = "Aktivna";
                }
            }
        }

        private void ChangingStatusRenovating(RoomAppointment appointment)
        {
            foreach (var currentRoom in roomsList)
            {
                if (currentRoom.RoomID == appointment.roomId || currentRoom.RoomID == appointment.RoomIDToMerge)
                {
                    currentRoom.RoomStatus = "Renoviranje";
                }
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
        /// <returns></returns>
        public Room GetFreeRoom(DateTime start, DateTime end)
        {
            foreach (Room room in roomsList)
            {
                if (IsRoomFree(room, start, end))
                {
                    return room;
                }
            }
            return null;
        }

        /// <summary>
        /// Provjerava da li je soba prazna
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool IsRoomFree(Room room, DateTime start, DateTime end)
        {
            bool roomFree = true;
            App app = Application.Current as App;
            AppointmentController appointmentController = app.AppointmentController;
            foreach (Appointment termin in appointmentController.GetAppointments())
            {
                if (termin.RoomId.Equals(room.RoomID) && room.RoomStatus.Equals("Aktivna"))
                {
                    if ((start >= termin.AppointmentDate && start <= termin.AppointmentDate.Add(termin.Duration))
                         || (end >= termin.AppointmentDate && end <= termin.AppointmentDate.Add(termin.Duration))
                         || (start <= termin.AppointmentDate && end >= termin.AppointmentDate.Add(termin.Duration)))
                    {
                        roomFree = false;
                        break;
                    }
                }
            }
            return roomFree;
        }
    }
}
