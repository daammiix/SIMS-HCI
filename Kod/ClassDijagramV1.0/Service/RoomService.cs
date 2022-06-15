// File:    RoomService.cs
// Author:  Milana
// Created: 08 April 2022 14:44:24
// Purpose: Definition of Class RoomService

using Model;
using Repository;
using System;
using System.ComponentModel;

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
            if (this.CheckIfUniq(room, false) && this.CheckIfValid(room))
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
            if (this.CheckIfUniq(room, true) && this.CheckIfValid(room))
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

        public void ChangeStorageQuantity(string equipmentId, int quantity)
        {
            repo.ChangeStorageQuantity(equipmentId, quantity);
        }

        public void ChangeStorageMedicineQuantity(string meidicineId, int quantity)
        {
            repo.ChangeStorageMedicineQuantity(meidicineId, quantity);
        }

        private Boolean CheckIfUniq(Room room, bool existingRoom)
        {
            var rooms = repo.GetAllRooms();
            foreach (var r in rooms)
            {
                if (!existingRoom && room.RoomID == r.RoomID)
                {
                    return false;
                }
                if ((room.Floor > 4) || (room.RoomNumber > 499))
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

        private Boolean CheckIfValid(Room room)
        {
            if ((room.Floor > 4) || (room.RoomNumber > 499))
            {
                return false;
            }
            return true;
        }

        public void SaveRooms()
        {
            repo.SaveRooms();
        }

    }
}