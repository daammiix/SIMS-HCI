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
            foreach (var r in roomAppointments)
            {
                if (r.appointmentID == roomAppointmentID)
                {
                    roomAppointments.Remove(r);
                    break;
                }
            }
        }
        public void DeleteRoomAppointmentAt(int i)
        {
            roomAppointments.RemoveAt(i);
        }

        public RoomAppointment ChangeRoomAppointment(RoomAppointment roomAppointment)
        {
            int i = findRoomAppointment(roomAppointment.appointmentID);
            if (i == -1)
            {
                return null;
            }
            roomAppointments.RemoveAt(i);
            roomAppointments.Insert(i, roomAppointment);
            return roomAppointment;
        }

        public BindingList<RoomAppointment> GetAllRoomAppointments()
        {
            return roomAppointments;
        }

        public RoomAppointment? GetRoomAppointmentByID(string roomAppointmentID)
        {
            foreach (var r in roomAppointments)
            {
                if (r.appointmentID == roomAppointmentID)
                {
                    return r;
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
            int i = 0;
            foreach (RoomAppointment r in roomAppointments)
            {
                if (r.appointmentID == roomAppointmentID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}
