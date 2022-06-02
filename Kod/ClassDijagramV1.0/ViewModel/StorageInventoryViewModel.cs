using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class StorageInventoryViewModel : IRefreshableEquipmentView
    {
        EquipmentController equipmentController;
        RoomController roomController;
        StorageViewModel storageViewModel;

        Storage storage;
        BindingList<Equipment> allEquipment;
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
                RefreshEquipment();
            }
        }
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }

        public QuantifiedEquipment selectedEquipment { get; set; }

        private RelayCommand _addEquipment;
        private RelayCommand _changeEquipment;
        private RelayCommand _deleteEquipment;
        private RelayCommand _equipmentReservation;

        public StorageInventoryViewModel(StorageViewModel storageViewModel)
        {
            var app = Application.Current as App;
            EquipmentList = new BindingList<QuantifiedEquipment>();
            equipmentController = app.equipmentController;
            roomController = app.roomController;
            this.storageViewModel = storageViewModel;
            allEquipment = equipmentController.GetAllEquipments();
            storage = (Storage)roomController.GetRoom("storage");
            _searchText = "";
            RefreshEquipment();
            foreach (var eq in storage.EquipmentList)
            {
                eq.PropertyChanged += new PropertyChangedEventHandler((object? o, PropertyChangedEventArgs e) => RefreshEquipment());
            }
        }

        public RelayCommand AddEquipment
        {
            get
            {
                _addEquipment = new RelayCommand(o =>
                {
                    ShowAddEquipmentDialog();
                });

                return _addEquipment;
            }
        }

        public RelayCommand ChangeEquipment
        {
            get
            {
                _changeEquipment = new RelayCommand(o =>
                {
                    ShowChangeEquipmentDialog();
                });

                return _changeEquipment;
            }
        }

        public RelayCommand DeleteEquipment
        {
            get
            {
                _deleteEquipment = new RelayCommand(o =>
                {
                    ShowDeleteEquipmentDialog();
                });

                return _deleteEquipment;
            }
        }

        public RelayCommand EquipmentReservation
        {
            get
            {
                _equipmentReservation = new RelayCommand(o =>
                {
                    storageViewModel.StorageEquipVM.PreviousView = this;
                    storageViewModel.StorageEquipVM.QEquipment = selectedEquipment;
                    storageViewModel.CurrentStorageView = storageViewModel.StorageEquipVM;
                });

                return _equipmentReservation;
            }
        }

        private void ShowAddEquipmentDialog()
        {
            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow(this, "Inventar");
            addEquipmentWindow.Show();
        }

        private void ShowChangeEquipmentDialog()
        {
            QuantifiedEquipment? quantifiedEquipment = (QuantifiedEquipment?)selectedEquipment;
            if (quantifiedEquipment != null)
            {
                ChangeEquipmentWindow changeEquipmentWindow = new ChangeEquipmentWindow(quantifiedEquipment, this);
                changeEquipmentWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void ShowDeleteEquipmentDialog()
        {
            QuantifiedEquipment? quantifiedEquipment = (QuantifiedEquipment?)selectedEquipment;
            if (quantifiedEquipment != null)
            {
                var equipmentId = quantifiedEquipment;
                DeleteEquipmentWindow deleteEquipmentWindow = new DeleteEquipmentWindow(equipmentId, this);
                deleteEquipmentWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        public void RefreshEquipment()
        {
            EquipmentList.Clear();
            foreach (var binding in storage.EquipmentList)
            {
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == binding.EquipmentID)
                    {
                        String search = _searchText.ToLower();
                        if (
                            !e.EquipmentID.ToLower().Contains(search) &&
                            !e.Name.ToLower().Contains(search) &&
                            !binding.Quantity.ToString().Contains(search)
                        )
                        {
                            continue;
                        }
                        EquipmentList.Add(new QuantifiedEquipment(e, binding.Quantity));
                    }
                }
            }
        }
    }
}
