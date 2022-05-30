﻿using Model;
using System;

namespace ClassDijagramV1._0.Model
{
    public class RoomAppointment
    {
        public String appointmentID { get; set; }
        public String roomId { get; set; }
        public DateTime startDate { get; set; }
        public TimeSpan duration { get; set; }
        public String? newRoomName = null;
        public String? RoomIDToMerge = null;
        public Room? RoomToSplit = null;

        public RoomAppointment(String appointmentID, String roomId, DateTime startDate, TimeSpan duration)
        {
            this.appointmentID = appointmentID;
            this.roomId = roomId;
            this.startDate = startDate;
            this.duration = duration;
        }

        public RoomAppointment(String appointmentID, String roomId, DateTime startDate, TimeSpan duration, String RoomIDToMerge)
        {
            this.appointmentID = appointmentID;
            this.roomId = roomId;
            this.startDate = startDate;
            this.duration = duration;
            this.RoomIDToMerge = RoomIDToMerge;
        }

        public RoomAppointment(String appointmentID, String roomId, DateTime startDate, TimeSpan duration, Room RoomToSplit)
        {
            this.appointmentID = appointmentID;
            this.roomId = roomId;
            this.startDate = startDate;
            this.duration = duration;
            this.RoomToSplit = RoomToSplit;
        }
    }
}
