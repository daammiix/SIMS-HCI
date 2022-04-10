// File:    RoomController.cs
// Author:  Milana
// Created: 08 April 2022 14:22:33
// Purpose: Definition of Class RoomController

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Service;

namespace Controller
{
    public class RoomController
    {
        private RoomService roomService = new RoomService("json");

        public void AddRoom(Room room)
        {
            roomService.AddRoom(room);
        }

        public void DeleteRoom(String roomID)
        {
            roomService.DeleteRoom(roomID);
        }

        public void ChangeRoom(Room room)
        {
            roomService.ChangeRoom(room);
        }

        public Room GetARoom(String roomID)
        {
            return roomService.GetARoom(roomID);
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            return roomService.GetAllRooms();
        }
    }
}