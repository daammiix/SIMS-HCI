// File:    RoomService.cs
// Author:  Milana
// Created: 08 April 2022 14:44:24
// Purpose: Definition of Class RoomService

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClassDijagramV1._0.Model;
using Model;
using Repository;

namespace Service
{
    public class RoomService
    {
        IRoomRepo repo;
        public RoomService(IRoomRepo roomRepository)
        {
            this.repo = roomRepository;
        }

        public void AddRoom(Room room)
        {
            if (this.CheckIfUniq(room, false))
            {
                repo.CreateNewRoom(room);
            }
        }

        public void DeleteRoom(String roomID)
        {
            repo.DeleteRoom(roomID);
        }

        public void ChangeRoom(Room room)
        {
            if (this.CheckIfUniq(room, true))
            {
                repo.SetRoom(room);
            }
        }

        public Room? GetARoom(String roomID)
        {
            return repo.GetRoom(roomID);
        }

        public BindingList<Room> GetAllRooms()
        {
            return repo.GetAllRooms();
        }

        public Boolean CheckIfUniq(Room room, bool existingRoom)
        {
            var rooms = repo.GetAllRooms();
            foreach (var r in rooms)
            {
                if (!existingRoom && room.RoomID == r.RoomID)
                {
                    return false;
                }
                if((room.Floor > 4) || (room.RoomNumber > 499))
                {
                    return false;
                }
                if (!existingRoom && room.Floor == r.Floor)
                {
                    if ((room.RoomNumber == r.RoomNumber))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SaveRooms()
        {
            repo.SaveRooms();
        }
    }
}