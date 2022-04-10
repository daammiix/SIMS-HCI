// File:    RoomRepo.cs
// Author:  Milana
// Created: 08 April 2022 13:49:38
// Purpose: Definition of Class RoomRepo

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Repository
{
    public interface IRoomRepo
    {
        public Room? GetRoom(String roomID);

        public ObservableCollection<Room> GetAllRooms();

        public Room? SetRoom(Room room);

        public Room CreateNewRoom(Room room);

        public void DeleteRoom(String roomID);

    }

    public class RoomRepoJson : IRoomRepo
    {
        private String path = "..\\..\\..\\Data\\rooms.json";
        private ObservableCollection<Room> rooms = new ObservableCollection<Room>();

        public RoomRepoJson()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            ObservableCollection<Room>? jsonRooms = JsonSerializer.Deserialize<ObservableCollection<Room>>(jsonData);
            if (jsonRooms != null)
            {
                this.rooms = jsonRooms;
            }
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

        public ObservableCollection<Room> GetAllRooms()
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
            writeRooms();
            return room;
        }

        public Room CreateNewRoom(Room room)
        {
            rooms.Add(room);
            writeRooms();
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
            this.writeRooms();
        }
        private void writeRooms()  // TODO: Add to class diagram
        {
            String jsonString = JsonSerializer.Serialize(rooms);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}