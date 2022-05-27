using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class MainRoomsViewModel : ObservableObject
    {
        ReservationOfRoom reservationOfRoom;
        public Room selectedRoom;

        public RelayCommand _equip;
        public RelayCommand _renovatingWallPainting;
        public RelayCommand _renovatingMerge;
        public RelayCommand _renovatingSplit;
        public RelayCommand _changingPurpose;
        public RelayCommand _closeReservatioOfRoom;

        public RoomsViewModel RoomsVM { get; set; }
        public EquipViewModel EquipVM { get; set; }
        public RenovatingMergeViewModel RenovatingVM { get; set; }
        public RenovatingSplitViewModel RenovatingSplitVM { get; set; }

        private object _currentRoomView;

        public object CurrentRoomView
        {
            get { return _currentRoomView; }
            set
            {
                _currentRoomView = value;
                OnPropertyChanged("CurrentRoomView");
            }
        }

        public MainRoomsViewModel()
        {
            RoomsVM = new RoomsViewModel(this);
            EquipVM = new EquipViewModel(this);
            RenovatingVM = new RenovatingMergeViewModel();
            RenovatingSplitVM = new RenovatingSplitViewModel();

            ResetView();
        }

        public RelayCommand Equip
        {
            get
            {
                _equip = new RelayCommand(o =>
                {
                    EquipAction();
                });

                return _equip;
            }
        }
        public RelayCommand RenovatingWallPainting
        {
            get
            {
                _renovatingWallPainting = new RelayCommand(o =>
                {
                    reservationOfRoom.Close();
                });

                return _renovatingWallPainting;
            }
        }
        public RelayCommand RenovatingMerge
        {
            get
            {
                _renovatingMerge = new RelayCommand(o =>
                {
                    ReservationMergeAction();
                });

                return _renovatingMerge;
            }
        }
        public RelayCommand RenovatingSplit
        {
            get
            {
                _renovatingSplit = new RelayCommand(o =>
                {
                    ReservationSplitAction();
                });

                return _renovatingSplit;
            }
        }
        public RelayCommand ChangingPurpose
        {
            get
            {
                _changingPurpose = new RelayCommand(o =>
                {
                    //SaveNewEquipmentAction();
                });

                return _changingPurpose;
            }
        }

        public RelayCommand CloseReservationOfRoom
        {
            get
            {
                _closeReservatioOfRoom = new RelayCommand(o =>
                {
                    reservationOfRoom.Close();
                });

                return _closeReservatioOfRoom;
            }
        }

        public void EquipAction()
        {
            reservationOfRoom.Close();
            EquipVM.destinationRoom = selectedRoom;
            this.CurrentRoomView = EquipVM;
        }

        public void ReservationMergeAction()
        {
            reservationOfRoom.Close();
            RenovatingVM.selectedRoom = selectedRoom;
            RenovatingVM.UpdateAvailableRooms();
            this.CurrentRoomView = RenovatingVM;
        }

        public void ReservationSplitAction()
        {
            reservationOfRoom.Close();
            RenovatingSplitVM.selectedRoom = selectedRoom;
            this.CurrentRoomView = RenovatingSplitVM;
        }

        public void SetReservationWindowState(ReservationOfRoom reservationOfRoom, Room selectedRoom)
        {
            this.reservationOfRoom = reservationOfRoom;
            this.selectedRoom = selectedRoom;
        }

        public void ResetView()
        {
            CurrentRoomView = RoomsVM;
        }
    }
}
