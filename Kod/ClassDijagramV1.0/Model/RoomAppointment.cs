using Model;
using System;

namespace ClassDijagramV1._0.Model
{
    public class RoomAppointment
    {
        public String appointmentID { get; set; }
        public String roomId { get; set; }
        public DateTime startDate { get; set; }
        public TimeSpan duration { get; set; }
        public String? newRoomName { get; set; } = null;
        public String? RoomIDToMerge { get; set; } = null;
        public Room? RoomToSplit { get; set; } = null;

        public RoomAppointment()
        { }

        public RoomAppointment(String appointmentID, String roomId, DateTime startDate, TimeSpan duration)
        {
            this.appointmentID = appointmentID;
            this.roomId = roomId;
            this.startDate = startDate;
            this.duration = duration;
            this.newRoomName = null;
            this.RoomIDToMerge = null;
            this.RoomToSplit = null;
        }
    }
}
