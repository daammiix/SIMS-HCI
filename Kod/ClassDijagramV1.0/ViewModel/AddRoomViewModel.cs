using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class AddRoomViewModel
    {
        public RoomController roomController;

        public String RoomID { get; set; }
        public String RoomName { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public String RoomStatus { get; set; }

        private RelayCommand _saveNewRoom;
        private RelayCommand _cancelNewRoom;

        AddRoomWindow addRoom;

        public AddRoomViewModel(AddRoomWindow addRoom)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            this.addRoom = addRoom;
        }

        private Room RoomFromTextboxes()
        {
            return new Room(
                addRoom.AddId.Text,
                addRoom.AddName.Text,
                Int32.Parse(addRoom.AddFloor.Text),
                Int32.Parse(addRoom.AddNumber.Text),
                addRoom.AddStatus.Text
            );
        }

        public RelayCommand SaveNewRoom
        {
            get
            {
                _saveNewRoom = new RelayCommand(o =>
                {
                    SaveNewRoomAction();
                });

                return _saveNewRoom;
            }
        }

        public RelayCommand CancelNewRoom
        {
            get
            {
                _cancelNewRoom = new RelayCommand(o =>
                {
                    addRoom.Close();
                });

                return _cancelNewRoom;
            }
        }

        private void SaveNewRoomAction()
        {
            roomController.AddRoom(RoomFromTextboxes());
            addRoom.Close();
        }

    }
}
