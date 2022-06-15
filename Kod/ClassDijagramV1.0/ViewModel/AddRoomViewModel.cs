using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class AddRoomViewModel : ObservableObject
    {
        public RoomController roomController;

        private String _roomID;
        private String _roomName;
        private String _floor;
        private String _roomNumber;
        private String _roomStatus = "Aktivna";

        public String ErrorMessage { get; set; }

        private RelayCommand _saveNewRoom;
        private RelayCommand _cancelNewRoom;

        AddRoomWindow addRoom;

        public AddRoomViewModel(AddRoomWindow addRoom)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            this.addRoom = addRoom;

            resetFields();
        }

        private void resetFields()
        {
            _roomID = null;
            _roomName = null;
            _floor = null;
            _roomNumber = null;
        }

        public String RoomID
        {
            get { return _roomID; }
            set
            {
                if (_roomID == value) { return; }
                _roomID = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra ne sme biti prazna";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public String RoomName
        {
            get { return _roomName; }
            set
            {
                value = value.Substring(38);
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
                int floor;
                bool is_number = int.TryParse(value, out floor);
                if (!is_number)
                {
                    ErrorMessage = "Uneta vrednost mora biti broj";
                    OnPropertyChanged("ErrorMessage");
                }
                else if (floor < 1)
                {
                    ErrorMessage = "Broj mora biti veći od 0";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public String RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber == value) { return; }
                _roomNumber = value;
                int number;
                bool is_number = int.TryParse(value, out number);
                if (!is_number)
                {
                    ErrorMessage = "Uneta vrednost mora biti broj";
                    OnPropertyChanged("ErrorMessage");
                }
                else if (number < 1)
                {
                    ErrorMessage = "Broj mora biti veći od 0";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
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
                RoomStatus
            );
        }

        public RelayCommand SaveNewRoom
        {
            get
            {
                _saveNewRoom = new RelayCommand(o =>
                {
                    if(_roomID == null || _roomName == null || _roomNumber == null || _floor == null
                    || _roomID == "" || _roomNumber == "" || _floor == "")
                    {
                        ErrorMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
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
                    resetFields();
                    addRoom.Close();
                });

                return _cancelNewRoom;
            }
        }

        private void SaveNewRoomAction()
        {
            roomController.AddRoom(RoomFromTextboxes());
            resetFields();
            addRoom.Close();
        }

    }
}
