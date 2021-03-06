using Model;
using Service;
using System;
using System.ComponentModel;

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

        public Room? GetRoom(String roomID)
        {
            return roomService.GetARoom(roomID);
        }

        public BindingList<Room> GetAllRooms()
        {
            return roomService.GetAllRooms();
        }

        public void ChangeStorageQuantity(string equipmentId, int quantity)
        {
            roomService.ChangeStorageQuantity(equipmentId, quantity);
        }

        public void ChangeStorageMedicineQuantity(string meidicineId, int quantity)
        {
            roomService.ChangeStorageMedicineQuantity(meidicineId, quantity);
        }

        public void SaveRooms()
        {
            roomService.SaveRooms();
        }

    }
}