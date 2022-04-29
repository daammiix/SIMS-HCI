// File:    RoomController.cs
// Author:  Milana
// Created: 08 April 2022 14:22:33
// Purpose: Definition of Class RoomController

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClassDijagramV1._0.Model;
using Model;
using Service;

namespace Controller
{
    public class RoomController
    {
        private RoomService roomService;

        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }

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

        public Room? GetARoom(String roomID)
        {
            return roomService.GetARoom(roomID);
        }

        public BindingList<Room> GetAllRooms()
        {
            return roomService.GetAllRooms();
        }

        public void SaveRooms()
        {
            roomService.SaveRooms();
        }
    }
}