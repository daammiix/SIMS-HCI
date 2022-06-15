using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class DeleteRoomViewModel
    {
        public RoomController roomController;

        private RelayCommand _deleteYesRoom;
        private RelayCommand _deleteNoRoom;

        DeleteRoomWindow deleteRoom;
        public String roomID;

        public DeleteRoomViewModel(DeleteRoomWindow deleteRoom, String roomID)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            this.deleteRoom = deleteRoom;
            this.roomID = roomID;
        }

        public RelayCommand DeleteYesRoom
        {
            get
            {
                _deleteYesRoom = new RelayCommand(o =>
                {
                    DeleteRoomAction();
                });

                return _deleteYesRoom;
            }
        }

        public RelayCommand DeleteNoRoom
        {
            get
            {
                _deleteNoRoom = new RelayCommand(o =>
                {
                    deleteRoom.Close();
                });

                return _deleteNoRoom;
            }
        }

        private void DeleteRoomAction()
        {
            roomController.DeleteRoom(roomID);
            deleteRoom.Close();
        }
    }
}
