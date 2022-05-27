using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class RoomsViewModel
    {
        private RoomController roomController;
        public BindingList<Room> Rooms
        {
            get;
            set;
        }

        public Room selectedRoom { get; set; }
        private MainRoomsViewModel mainRoomsViewModel;

        String _searchText;
        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                RefreshRooms();
            }
        }

        private RelayCommand _generateReport;
        private RelayCommand _addRoom;
        private RelayCommand _changeRoom;
        private RelayCommand _deleteRoom;
        private RelayCommand _roomEquipment;
        private RelayCommand _roomReservation;

        public RoomsViewModel(MainRoomsViewModel mainRoomsViewModel)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            this.mainRoomsViewModel = mainRoomsViewModel;
            _searchText = "";
            Rooms =  new BindingList<Room>();
            RefreshRooms();
        }

        public RelayCommand GenerateReport
        {
            get
            {
                _generateReport = new RelayCommand(o =>
                {
                    //TODO
                });

                return _generateReport;
            }
        }

        public RelayCommand AddRoom
        {
            get
            {

                _addRoom = new RelayCommand(o =>
                {
                    ShowAddRoomDialog();
                });

                return _addRoom;
            }
        }

        public RelayCommand ChangeRoom
        {
            get
            {
                _changeRoom = new RelayCommand(o =>
                {
                    ShowChangeRoomDialog();
                });

                return _changeRoom;
            }
        }

        public RelayCommand DeleteRoom
        {
            get
            {
                _deleteRoom = new RelayCommand(o =>
                {
                    ShowDeleteRoomDialog();
                });

                return _deleteRoom;
            }
        }

        public RelayCommand RoomEquipment
        {
            get
            {
                _roomEquipment = new RelayCommand(o =>
                {
                    ShowRoomEquipmentDialog();
                });

                return _roomEquipment;
            }
        }

        public RelayCommand RoomReservation
        {
            get
            {
                _roomReservation = new RelayCommand(o =>
                {
                    ShowRoomReservationDialog();
                });

                return _roomReservation;
            }
        }

        private void ShowAddRoomDialog()
        {
            AddRoomWindow addRoom = new AddRoomWindow();
            addRoom.Show();
        }

        private void ShowChangeRoomDialog()
        {
            if (selectedRoom != null)
            {
                ChangeRoomWindow changeRoom = new ChangeRoomWindow(selectedRoom);
                changeRoom.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void ShowDeleteRoomDialog()
        {
            if (selectedRoom != null)
            {
                String roomId = selectedRoom.RoomID;
                DeleteRoomWindow deleteRoom = new DeleteRoomWindow(roomId);
                deleteRoom.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void ShowRoomEquipmentDialog()
        {
            ListOfEquipment listOfEquipment = new ListOfEquipment(selectedRoom);
            listOfEquipment.Show();
        }

        private void ShowRoomReservationDialog()
        {
            ReservationOfRoom reservationOfRoom = new ReservationOfRoom(mainRoomsViewModel, selectedRoom);
            reservationOfRoom.Show();
        }

        public void RefreshRooms()
        {
            Rooms.Clear();
            BindingList<Room> allRooms = roomController.GetAllRooms();
            foreach (Room room in allRooms)
            {
                String search = _searchText.ToLower();
                if (
                    !room.RoomID.ToLower().Contains(search) &&
                    !room.RoomName.ToLower().Contains(search) &&
                    !room.Floor.ToString().Contains(search) &&
                    !room.RoomNumber.ToString().Contains(search) && 
                    !room.RoomStatus.ToLower().Contains(search))
                {
                    continue;
                }
                Rooms.Add(room);
            }
        }
    }
}
