/***********************************************************************
 * Module:  Room.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;

namespace Model
{
    public class Room
    {
        public Room()
        {

        }
        public Room(String RoomID, RoomName RoomName, int Floor, int RoomNumber, RoomStatus RoomStatus)
        {
            this.RoomID = RoomID;
            this.RoomName = RoomName;
            this.Floor = Floor;
            this.RoomNumber = RoomNumber;
            this.RoomStatus = RoomStatus;
        }
        public String RoomID { get; set; }
        public RoomName RoomName { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public RoomStatus RoomStatus { get; set; }
    }
}