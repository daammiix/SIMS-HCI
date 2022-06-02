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
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                room.RoomID,
                changeRoom.ChangeName.Text,
                Int32.Parse(changeRoom.ChangeFloor.Text),
                Int32.Parse(changeRoom.ChangeNumber.Text),
                changeRoom.ChangeStatus.Text,
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
