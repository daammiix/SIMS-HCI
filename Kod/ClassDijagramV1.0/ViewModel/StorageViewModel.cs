using ClassDijagramV1._0.Util;
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
    class StorageViewModel : ObservableObject
    {
        public RelayCommand StorageMedicalEquipmentViewCommand { get; set; }
        public RelayCommand StorageSuppliesViewCommand { get; set; }
        public RelayCommand StorageInventoryViewCommand { get; set; }
        public RelayCommand StorageDrugsViewCommand { get; set; }

        public StorageMedicalEquipmentViewModel StorageMedicalEquipmentVM { get; set; }
        public StorageSuppliesViewModel StorageSuppliesVM { get; set; }
        public StorageInventoryViewModel StorageInventoryVM { get; set; }
        public StorageDrugsViewModel StorageDrugsVM { get; set; }

        private object _currentStorageView;

        public object CurrentStorageView
        {
            get { return _currentStorageView; }
            set
            {
                _currentStorageView = value;
                OnPropertyChanged("CurrentStorageView");
            }
        }

        public StorageViewModel()
        {

            StorageMedicalEquipmentVM = new StorageMedicalEquipmentViewModel();
            StorageSuppliesVM = new StorageSuppliesViewModel();
            StorageInventoryVM = new StorageInventoryViewModel();
            StorageDrugsVM = new StorageDrugsViewModel();

            CurrentStorageView = StorageMedicalEquipmentVM;


            StorageMedicalEquipmentViewCommand = new RelayCommand(o =>
            {
                CurrentStorageView = StorageMedicalEquipmentVM;
            });

            StorageSuppliesViewCommand = new RelayCommand(o =>
            {
                CurrentStorageView = StorageSuppliesVM;
            });

            StorageInventoryViewCommand = new RelayCommand(o =>
            {
                CurrentStorageView = StorageInventoryVM;
            });

            StorageDrugsViewCommand = new RelayCommand(o =>
            {
                CurrentStorageView = StorageDrugsVM;
            });
        }
    }
}
