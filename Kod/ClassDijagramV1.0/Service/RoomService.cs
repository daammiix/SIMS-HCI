// File:    RoomService.cs
// Author:  Milana
// Created: 08 April 2022 14:44:24
// Purpose: Definition of Class RoomService

using System;
using System.Collections.ObjectModel;
using Model;

namespace Service
{
    public class RoomService
    {
        Repository.IRoomRepo repo;
        public RoomService(String repoType)
        {
            if (repoType == "json")
            {
                repo = new Repository.RoomRepoJson();
            }
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

        public ObservableCollection<Room> GetAllRooms()
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
                if (room.Floor == r.Floor)
                {
                    if (room.RoomNumber == r.RoomNumber)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}