using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class AddEquipmentViewModel : ObservableObject
    {
        private String _equipmentID;
        private String _equipmentName;
        private String _equipmentType;
        private String _quantity;

        public RoomController roomController;
        public EquipmentController equipmentController;
        private IRefreshableEquipmentView equipmentView;
        public AddEquipmentWindow addEquipment;

        public String ErrorMessage { get; set; }

        private RelayCommand _saveNewEquipment;
        private RelayCommand _cancelNewEquipment;

        public AddEquipmentViewModel(AddEquipmentWindow addEquipment, IRefreshableEquipmentView equipmentView, String type)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentController = app.equipmentController;
            this.addEquipment = addEquipment;
            this.equipmentView = equipmentView;
            _equipmentType = type;

            resetFields();
        }

        private void resetFields()
        {
            _equipmentID = null;
            _equipmentName = null;
            _quantity = null;
        }

        public String EquipmentID
        {
            get { return _equipmentID; }
            set
            {
                if (_equipmentID == value) { return; }
                _equipmentID = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra i naziv ne smeju biti prazni";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public String EquipmentName
        {
            get { return _equipmentName; }
            set
            {
                if (_equipmentName == value) { return; }
                _equipmentName = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra i naziv ne smeju biti prazni";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public String EquipmentType
        {
            get { return _equipmentType; }
            set
            {
                if (_equipmentType == value) { return; }
                _equipmentType = value;
            }
        }
        public String Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
                int quantity;
                bool is_number = int.TryParse(value, out quantity);
                if (!is_number)
                {
                    ErrorMessage = "Uneta vrednost mora biti broj";
                    OnPropertyChanged("ErrorMessage");
                }
                else if (quantity < 1)
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

        public RelayCommand SaveNewEquipment
        {
            get
            {
                _saveNewEquipment = new RelayCommand(o =>
                {
                    if(_equipmentID == null || _equipmentName == null || _quantity == null
                    || _equipmentID == "" || _equipmentName == "" || _quantity == "")
                    {
                        ErrorMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
                    SaveNewEquipmentAction();
                });

                return _saveNewEquipment;
            }
        }

        public RelayCommand CancelNewEquipment
        {
            get
            {
                _cancelNewEquipment = new RelayCommand(o =>
                {
                    addEquipment.Close();
                });

                return _cancelNewEquipment;
            }
        }

        private Equipment EquipmentFromTextBoxes()
        {
            return new Equipment(EquipmentID, EquipmentName, EquipmentType, 100, UnitsType.Units);
        }

        public void SaveNewEquipmentAction()
        {
            var equipment = EquipmentFromTextBoxes();
            var quantity = Int32.Parse(Quantity);
            equipmentController.AddEquipment(equipment);
            ((Storage)roomController.GetRoom("storage")).addNewEquipment(equipment, quantity);
            equipmentView.RefreshEquipment();

            addEquipment.Close();
        }
    }
}
