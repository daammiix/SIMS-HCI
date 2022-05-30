using ClassDijagramV1._0;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using ClassDijagramV1._0.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ClassDijagramV1._0.Util;

namespace Model
{
    public class Room : ObservableObject
    {
        private String _roomID;
        private String _roomName;
        private int _floor;
        private int _roomNumber;
        private String _roomStatus;
        private BindingList<RoomEquipmentBinding> _equipmentList = new BindingList<RoomEquipmentBinding>();
        private BindingList<RoomMedicineBinding> _medicineList = new BindingList<RoomMedicineBinding>();

        public String RoomID
        {
            get { return _roomID; }
            set
            {
                if (_roomID == value) { return; }
                _roomID = value;
                OnPropertyChanged("RoomID");
            }
        }
        public String RoomName
        {
            get { return _roomName; }
            set
            {
                if (_roomName == value) { return; }
                _roomName = value;
                OnPropertyChanged("RoomName");
            }
        }
        public int Floor
        {
            get { return _floor; }
            set
            {
                if (_floor == value) { return; }
                _floor = value;
                OnPropertyChanged("Floor");
            }
        }
        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber == value) { return; }
                _roomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }
        public String RoomStatus
        {
            get { return _roomStatus; }
            set
            {
                if (_roomStatus == value) { return; }
                _roomStatus = value;
                OnPropertyChanged("RoomStatus");
            }
        }
        public BindingList<RoomEquipmentBinding> EquipmentList
        {
            get { return _equipmentList; }
            set
            {
                if (_equipmentList == value) { return; }
                _equipmentList = value;
                OnPropertyChanged("EquipmentList");
            }
        }

        public BindingList<RoomMedicineBinding> MedicineList
        {
            get { return _medicineList; }
            set
            {
                if (_medicineList == value) { return; }
                _medicineList = value;
                OnPropertyChanged("MedicineList");
            }
        }
        public Room()
        {

        }

        public Room(String RoomID, String RoomName, int Floor, int RoomNumber, String RoomStatus, BindingList<RoomEquipmentBinding>? EquipmentList = null, BindingList<RoomMedicineBinding>? MedicineList = null)
        {
            this.RoomID = RoomID;
            this.RoomName = RoomName;
            this.Floor = Floor;
            this.RoomNumber = RoomNumber;
            this.RoomStatus = RoomStatus;
            if (EquipmentList != null) { this.EquipmentList = EquipmentList; }
            if (MedicineList != null) { this.MedicineList = MedicineList; }
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

        private RoomMedicineBinding? getMedicineBinding(Medicines medicine)
        {
            foreach (var binding in MedicineList)
            {
                if (binding.MedicineID == medicine.ID)
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

        public void addMedicine(Medicines m, int quantity)
        {
            RoomMedicineBinding? binding = getMedicineBinding(m);
            if (binding == null)
            {
                MedicineList.Add(new RoomMedicineBinding(m.ID, quantity));
            }
            else
            {
                binding.Quantity += quantity;
            }
        }

        public void addNewEquipment(Equipment e, int quantity)
        {
            RoomEquipmentBinding? binding = getBinding(e);
            if (binding == null)
            {
                EquipmentList.Add(new RoomEquipmentBinding(e.EquipmentID, quantity));
            }
            else
            {
                WarningId warningId = new WarningId();
                warningId.Show();
            }
        }

        public void addNewMedicine(Medicines m, int quantity)
        {
            RoomMedicineBinding? binding = getMedicineBinding(m);
            if (binding == null)
            {
                MedicineList.Add(new RoomMedicineBinding(m.ID, quantity));
            }
            else
            {
                WarningId warningId = new WarningId();
                warningId.Show();
            }
        }

        public void insertChangedEquipment(Equipment e, int quantity, int index)
        {
            RoomEquipmentBinding? binding = getBinding(e);
            if (binding != null)
            {
                EquipmentList.Insert(index, new RoomEquipmentBinding(e.EquipmentID, quantity));
            }
            else
            {
                WarningId warningId = new WarningId();
                warningId.Show();
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

        public void removeMedicine(Medicines m, int quantity)
        {
            RoomMedicineBinding? binding = getMedicineBinding(m);
            if (binding == null)
            {
                throw new Exception("Binding not found");
            }
            if (binding.Quantity == quantity)
            {
                MedicineList.Remove(binding);
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
            AppointmentController appointmentController = app.AppointmentController;

            foreach (Appointment termin in appointmentController.GetAppointments())
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

    public class RoomEquipmentBinding : ObservableObject
    {
        String _equipmentID;
        int _quantity;

        public String EquipmentID {
            get { return _equipmentID; }
            set
            {
                if (_equipmentID == value) { return; }
                _equipmentID = value;
                OnPropertyChanged("EquipmentID");
            }
        }
        public int Quantity {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public RoomEquipmentBinding(String equipmentID, int quantity)
        {
            _equipmentID = equipmentID;
            _quantity = quantity;
        }
    }

    public class RoomMedicineBinding : ObservableObject
    {
        private String _medicineID;
        private int _quantity;
        public String MedicineID
        {
            get { return _medicineID; }
            set
            {
                if (_medicineID == value) { return; }
                _medicineID = value;
                OnPropertyChanged("MedicineID");
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public RoomMedicineBinding(string medicineID, int quantity)
        {
            MedicineID = medicineID;
            Quantity = quantity;
        }
    }
}
