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

        public RelayCommand RoomsViewCommand { get; set; }

        public RelayCommand CalendarViewCommand { get; set; }

        public RelayCommand StorageViewCommand { get; set; }

        public RoomsViewModel RoomsVM { get; set; }

        public CalendarViewModel CalendarVM { get; set; }

        public StorageViewModel StorageVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }

        }


        public MainViewModel()
        {
            RoomsVM = new RoomsViewModel();
            CalendarVM = new CalendarViewModel();
            StorageVM = new StorageViewModel();


            CurrentView = CalendarVM;

            RoomsViewCommand = new RelayCommand(o =>
            {
                CurrentView = RoomsVM;
            });

            CalendarViewCommand = new RelayCommand(o =>
            {
                CurrentView = CalendarVM;
            });

            StorageViewCommand = new RelayCommand(o =>
            {
                CurrentView = StorageVM;
            });

        }
    }
}
