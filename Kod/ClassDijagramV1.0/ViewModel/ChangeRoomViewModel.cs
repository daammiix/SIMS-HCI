using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeRoomViewModel
    {
        private String _roomID;
        private String _roomName;
        private String _floor;
        private String _roomNumber;
        private String _roomStatus = "Aktivna";

        public RoomController roomController;

        public ChangeRoomWindow changeRoom;
        public Room room { get; set; }

        public BindingList<Room> Rooms
        {
            get;
            set;
        }

        private RelayCommand _saveChangedRoom;
        private RelayCommand _quitChangededRoom;

        public ChangeRoomViewModel(ChangeRoomWindow changeRoom, Room room)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            Rooms = roomController.GetAllRooms();

            this.changeRoom = changeRoom;
            this.room = room;

            this.RoomID = room.RoomID;
            this.RoomName = room.RoomName;
            this.Floor = room.Floor.ToString();
            this.RoomNumber = room.RoomNumber.ToString();
        }

        public String RoomID
        {
            get { return _roomID; }
            set
            {
                if (_roomID == value) { return; }
                _roomID = value;
            }
        }
        public String RoomName
        {
            get { return _roomName; }
            set
            {
                if (_roomName == value) { return; }
                _roomName = value;
            }
        }
        public String Floor
        {
            get { return _floor; }
            set
            {
                if (_floor == value) { return; }
                _floor = value;
            }
        }
        public String RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber == value) { return; }
                _roomNumber = value;
            }
        }
        public String RoomStatus
        {
            get { return _roomStatus; }
            set
            {
                if (_roomStatus == value) { return; }
                _roomStatus = value;
            }
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                RoomID,
                RoomName,
                Int32.Parse(Floor),
                Int32.Parse(RoomNumber),
                RoomStatus,
                room.EquipmentList,
                room.MedicineList
            );
        }

        public RelayCommand SaveChangedRoom
        {
            get
            {
                _saveChangedRoom = new RelayCommand(o =>
                {
                    ChangeRoomAction();
                });

                return _saveChangedRoom;
            }
        }

        public RelayCommand QuitChangededRoom
        {
            get
            {
                _quitChangededRoom = new RelayCommand(o =>
                {
                    changeRoom.Close();
                });

                return _quitChangededRoom;
            }
        }

        private void ChangeRoomAction()
        {
            roomController.ChangeRoom(RoomFromTextboxes());
            changeRoom.Close();
        }
    }
}
