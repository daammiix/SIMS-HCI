/***********************************************************************
 * Module:  Room.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using ClassDijagramV1._0;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Util;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Model
{
    public class Room : ObservableObject
    {
        private String _roomID;
        private RoomName _roomName;
        private int _floor;
        private int _roomNumber;
        private RoomStatus _roomStatus;

        public String RoomID
        {
            get
            {
                return _roomID;
            }
            set
            {
                if (_roomID != value)
                {
                    _roomID = value;
                    onPropertyChanged("RoomID");
                }
            }
        }
        public RoomName RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                if (_roomName != value)
                {
                    _roomName = value;
                    onPropertyChanged("RoomName");
                }
            }
        }
        public int Floor
        {
            get
            {
                return _floor;
            }
            set
            {
                if (_floor != value)
                {
                    _floor = value;
                    onPropertyChanged("Floor");
                }
            }
        }
        public int RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                if (_roomNumber != value)
                {
                    _roomNumber = value;
                    onPropertyChanged("RoomNumber");
                }
            }
        }
        public RoomStatus RoomStatus
        {
            get
            {
                return _roomStatus;
            }
            set
            {
                if (_roomStatus != value)
                {
                    _roomStatus = value;
                    onPropertyChanged("RoomStatus");
                }
            }
        }

        public Room()
        {

        }
        public Room(String RoomID, RoomName RoomName, int Floor, int RoomNumber, RoomStatus RoomStatus)
        {
            this.RoomID = RoomID;
            this.RoomName = RoomName;
            this.Floor = Floor;
            this.RoomNumber = RoomNumber;
            this.RoomStatus = RoomStatus;
        }
        

        public bool isFree(DateTime start, DateTime end) 
        {
            bool retVal = true;
           // AppointmentFileHandler ap = new AppointmentFileHandler("../../../Data/appointments.json");

            App app = Application.Current as App;

            //AppointmentRepo ap = new AppointmentRepo();
            ObservableCollection<Appointment> termini = app.appointmentController.GetAllAppointments("djordje");

            foreach (Appointment termin in termini)
            {
                if (termin.Room.RoomID.Equals(this.RoomID)/* && termin.AppointmentStatus == AppointmentStatus.scheduled*/)
                {    
                    if (start >= termin.AppointmentDate && start <= termin.AppointmentDate.Add(termin.Duration))
                    {
                        retVal = false;
                        break;
                    }
                    if (end >= termin.AppointmentDate && end <= termin.AppointmentDate.Add(termin.Duration))
                    {
                        retVal = false;
                        break;
                    }
                    if (start <= termin.AppointmentDate && end >= termin.AppointmentDate.Add(termin.Duration))
                    {
                        retVal = false;
                        break;
                    }
                }
            }
            return retVal;
        }

    }
}



