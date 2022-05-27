using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views;
using ClassDijagramV1._0.Views.SecretaryView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ClassDijagramV1._0.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand _roomsViewCommand;

        public RelayCommand _calendarViewCommand;

        public RelayCommand _storageViewCommand;

        public MainRoomsViewModel MainRoomsVM { get; set; }

        public CalendarViewModel CalendarVM { get; set; }

        public StorageViewModel StorageVM { get; set; }


        private object _currentView;

        public RelayCommand RoomsViewCommand
        {
            get
            {
                if (_roomsViewCommand == null)
                {
                    _roomsViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = MainRoomsVM;
                    });
                }

                return _roomsViewCommand;
            }
        }

        public RelayCommand CalendarViewCommand
        {
            get
            {
                if (_calendarViewCommand == null)
                {
                    _calendarViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = CalendarVM;
                    });
                }

                return _calendarViewCommand;
            }
        }

        public RelayCommand StorageViewCommand
        {
            get
            {
                if (_storageViewCommand == null)
                {
                    _storageViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = StorageVM;
                    });
                }

                return _storageViewCommand;
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged("CurrentView");
                }
            }

        }


        public MainViewModel()
        {
            MainRoomsVM = new MainRoomsViewModel();
            CalendarVM = new CalendarViewModel();
            StorageVM = new StorageViewModel();

            CurrentView = CalendarVM;
        }
    }
}
