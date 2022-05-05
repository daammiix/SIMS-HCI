/***********************************************************************
 * Module:  Room.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using ClassDijagramV1._0;
using ClassDijagramV1._0.Model;
using Controller;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Model
{
    public class Room
    {
        public String RoomID { get; set; }
        public String RoomName { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public String RoomStatus { get; set; }
        public BindingList<RoomEquipmentBinding> EquipmentList { get; set; } = new BindingList<RoomEquipmentBinding>();

        public Room()
        {

        }

        public Room(String RoomID, String RoomName, int Floor, int RoomNumber, String RoomStatus, BindingList<RoomEquipmentBinding>? EquipmentList = null)
        {
            this.RoomID = RoomID;
            this.RoomName = RoomName;
            this.Floor = Floor;
            this.RoomNumber = RoomNumber;
            this.RoomStatus = RoomStatus;
            if (EquipmentList != null) { this.EquipmentList = EquipmentList; }
        }

        private RoomEquipmentBinding? getBinding(Equipment e)
        {
            foreach (var binding in EquipmentList)
            {
                if (binding.EquipmentID == e.EquipmentID)
                {
                    return binding;
                }
            }
            return null;
        }

        public void addEquipment(Equipment e, int quantity)
        {
            RoomEquipmentBinding? binding = getBinding(e);
            if (binding == null)
            {
                EquipmentList.Add(new RoomEquipmentBinding(e.EquipmentID, quantity));
            }
            else
            {
                binding.Quantity += quantity;
            }
        }

        public void removeEquipment(Equipment e, int quantity)
        {
            RoomEquipmentBinding? binding = getBinding(e);
            if (binding == null)
            {
                throw new Exception("Binding not found");
            }
            if (binding.Quantity == quantity)
            {
                EquipmentList.Remove(binding);
            }
            else
            {
                binding.Quantity -= quantity;
            }
        }

        public bool isFree(DateTime start, DateTime end)
        {
            bool retVal = true;
            // AppointmentFileHandler ap = new AppointmentFileHandler("../../../Data/appointments.json");

            App app = Application.Current as App;

            //AppointmentRepo ap = new AppointmentRepo();
            //ObservableCollection<Appointment> termini = app.AppointmentController.GetAllAppointments("djordje");

            AppointmentController _appointmentController = app.AppointmentController;

            foreach (Appointment termin in _appointmentController.GetAppointments())
            {
                if (termin.RoomId.Equals(this.RoomID)/* && termin.AppointmentStatus == AppointmentStatus.scheduled*/)
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

    public class RoomEquipmentBinding
    {
        public String EquipmentID { get; set; }
        public int Quantity { get; set; }

        public RoomEquipmentBinding(String equipmentID, int quantity)
        {
            this.EquipmentID = equipmentID;
            this.Quantity = quantity;
        }
    }
}
