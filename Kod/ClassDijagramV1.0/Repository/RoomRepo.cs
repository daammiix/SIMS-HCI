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

        public void SaveRooms();

    }

    public class RoomRepoJson : IRoomRepo
    {
        private FileHandler<BindingList<Room>> _roomFileHandler;
        private BindingList<Room> rooms = new BindingList<Room>();

        public RoomRepoJson(FileHandler<BindingList<Room>> roomFileHandler)
        {
            _roomFileHandler = roomFileHandler;
            rooms = _roomFileHandler.Read();
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
            foreach (var room in rooms)
            {
                if (room.RoomID == roomID)
                {
                    return room;
                }
            }
            return null;
        }

        public BindingList<Room> GetAllRooms()
        {
            return rooms;
        }

        public Room? SetRoom(Room room)
        {
            int i = findRoom(room.RoomID);
            if (i == -1)
            {
                return null;
            }
            rooms.RemoveAt(i);
            rooms.Insert(i, room);
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

        public void SaveRooms()
        {
            _roomFileHandler.Write(rooms);
        }
    }
}