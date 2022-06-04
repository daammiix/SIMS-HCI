using ClassDijagramV1._0.Util;

namespace ClassDijagramV1._0.ViewModel
{
    public class StorageViewModel : ObservableObject
    {
        private RelayCommand _storageMedicalEquipment;
        private RelayCommand _storageSupplies;
        private RelayCommand _storageInventory;
        private RelayCommand _storageDrugs;
        private RelayCommand _equipmentReservation;

        public StorageMedicalEquipmentViewModel StorageMedicalEquipmentVM { get; set; }
        public StorageSuppliesViewModel StorageSuppliesVM { get; set; }
        public StorageInventoryViewModel StorageInventoryVM { get; set; }
        public StorageDrugsViewModel StorageDrugsVM { get; set; }
        public StorageEquipViewModel StorageEquipVM { get; set; }

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

            StorageMedicalEquipmentVM = new StorageMedicalEquipmentViewModel(this);
            StorageSuppliesVM = new StorageSuppliesViewModel(this);
            StorageInventoryVM = new StorageInventoryViewModel(this);
            StorageDrugsVM = new StorageDrugsViewModel();
            StorageEquipVM = new StorageEquipViewModel(this);

            CurrentStorageView = StorageMedicalEquipmentVM;
        }

        public RelayCommand StorageMedicalEquipment
        {
            get
            {
                _storageMedicalEquipment = new RelayCommand(o =>
                {
                    CurrentStorageView = StorageMedicalEquipmentVM;
                });

                return _storageMedicalEquipment;
            }
        }

        public RelayCommand StorageSupplies
        {
            get
            {
                _storageSupplies = new RelayCommand(o =>
                {
                    CurrentStorageView = StorageSuppliesVM;
                });

                return _storageSupplies;
            }
        }

        public RelayCommand StorageInventory
        {
            get
            {
                _storageInventory = new RelayCommand(o =>
                {
                    CurrentStorageView = StorageInventoryVM;
                });

                return _storageInventory;
            }
        }

        public RelayCommand StorageDrugs
        {
            get
            {
                _storageDrugs = new RelayCommand(o =>
                {
                    CurrentStorageView = StorageDrugsVM;
                });

                return _storageDrugs;
            }
        }

        public RelayCommand EquipmentReservation
        {
            get
            {
                _equipmentReservation = new RelayCommand(o =>
                {
                    CurrentStorageView = StorageEquipVM;
                });

                return _equipmentReservation;
            }
        }
    }
}
