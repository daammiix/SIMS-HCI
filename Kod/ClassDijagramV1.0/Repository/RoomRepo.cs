// File:    RoomRepo.cs
// Author:  Milana
// Created: 08 April 2022 13:49:38
// Purpose: Definition of Class RoomRepo

using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;

namespace Repository
{
    public interface IRoomRepo
    {
        public Room? GetRoom(String roomID);

        public BindingList<Room> GetAllRooms();

        public Room? SetRoom(Room room);

        public Room CreateNewRoom(Room room);

        public void DeleteRoom(String roomID);

        public void ChangeStorageQuantity(string equipmentId, int quantity);
        public void ChangeStorageMedicineQuantity(string medicineId, int quantity);

        public void SaveRooms();

    }

    public class RoomRepoJson : IRoomRepo
    {
        private FileHandler<BindingList<Room>> _roomFileHandler;
        private FileHandler<BindingList<Storage>> _storageFileHandler;
        private BindingList<Room> rooms = new BindingList<Room>();
        private Storage storage;

        public RoomRepoJson(FileHandler<BindingList<Room>> roomFileHandler, FileHandler<BindingList<Storage>> storageFileHandler)
        {
            _roomFileHandler = roomFileHandler;
            rooms = _roomFileHandler.Read();

            _storageFileHandler = storageFileHandler;
            storage = _storageFileHandler.Read()[0];
        }

        private int findRoom(String roomId)
        {
            int i = 0;
            foreach (Room room in rooms)
            {
                if (room.RoomID == roomId)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public Room? GetRoom(String roomID)
        {
            if (roomID == "storage")
            {
                return GetStorage();
            }
            foreach (var room in rooms)
            {
                if (room.RoomID == roomID)
                {
                    return room;
                }
            }
            return null;
        }
        public Storage GetStorage()
        {
            return storage;
        }


        public BindingList<Room> GetAllRooms()
        {
            return rooms;
        }

        public Room? SetRoom(Room room)
        {
            int index = findRoom(room.RoomID);
            if (index == -1)
            {
                return null;
            }
            rooms.RemoveAt(index);
            rooms.Insert(index, room);
            return room;
        }

        public Room CreateNewRoom(Room room)
        {
            rooms.Add(room);
            return room;
        }

        public void DeleteRoom(String roomID)
        {
            foreach (var room in rooms)
            {
                if (room.RoomID == roomID)
                {
                    rooms.Remove(room);
                    break;
                }
            }
        }

        public void ChangeStorageQuantity(string equipmentId, int quantity)
        {
            var equipmentList = storage.EquipmentList;
            foreach (var e in equipmentList)
            {
                if (e.EquipmentID == equipmentId)
                {
                    e.Quantity = quantity;
                }
            }
        }

        public void ChangeStorageMedicineQuantity(string medicineId, int quantity)
        {
            var medicineList = storage.MedicineList;
            foreach (var medicine in medicineList)
            {
                if (medicine.MedicineID == medicineId)
                {
                    medicine.Quantity = quantity;
                }
            }
        }

        public void SaveRooms()
        {
            _roomFileHandler.Write(rooms);
            _storageFileHandler.Write(new BindingList<Storage>() { storage });
        }
    }
}