using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class RoomAppointmentRepo
    {
        private FileHandler<BindingList<RoomAppointment>> _roomAppointmentsFileHandler;
        private BindingList<RoomAppointment> roomAppointments = new BindingList<RoomAppointment>();

        public RoomAppointmentRepo(FileHandler<BindingList<RoomAppointment>> roomAppointmentFileHandler)
        {
            _roomAppointmentsFileHandler = roomAppointmentFileHandler;
            roomAppointments = _roomAppointmentsFileHandler.Read();
            return;
        }
        public RoomAppointment AddRoomAppointment(RoomAppointment roomAppointment)
        {
            roomAppointments.Add(roomAppointment);
            return roomAppointment;
        }

        public void DeleteRoomAppointment(string roomAppointmentID)
        {
            foreach (var roomAppointment in roomAppointments)
            {
                if (roomAppointment.appointmentID == roomAppointmentID)
                {
                    roomAppointments.Remove(roomAppointment);
                    break;
                }
            }
        }
        public void DeleteRoomAppointmentAt(int index)
        {
            roomAppointments.RemoveAt(index);
        }

        public RoomAppointment ChangeRoomAppointment(RoomAppointment roomAppointment)
        {
            int index = findRoomAppointment(roomAppointment.appointmentID);
            if (index == -1)
            {
                return null;
            }
            roomAppointments.RemoveAt(index);
            roomAppointments.Insert(index, roomAppointment);
            return roomAppointment;
        }

        public BindingList<RoomAppointment> GetAllRoomAppointments()
        {
            return roomAppointments;
        }

        public RoomAppointment? GetRoomAppointmentByID(string roomAppointmentID)
        {
            foreach (var roomAppointment in roomAppointments)
            {
                if (roomAppointment.appointmentID == roomAppointmentID)
                {
                    return roomAppointment;
                }
            }
            return null;
        }

        public void SaveRoomAppointment()
        {
            _roomAppointmentsFileHandler.Write(roomAppointments);
        }

        private int findRoomAppointment(String roomAppointmentID)
        {
            int index = 0;
            foreach (RoomAppointment roomAppointment in roomAppointments)
            {
                if (roomAppointment.appointmentID == roomAppointmentID)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
