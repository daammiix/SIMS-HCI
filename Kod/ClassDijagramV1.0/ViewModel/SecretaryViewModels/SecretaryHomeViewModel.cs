﻿using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels;
using Model;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels
{
    public class SecretaryHomeViewModel : ObservableObject
    {

        #region Fields

        // Treba da se dopuni da se salje u konstruktor Sekretar
        private Secretary? _secretary;

        private object? _currentView;

        AppointmentsMainViewModel _appointmentsViewModel;

        AccountsMainViewModel _accountsMainViewModel;

        MedicalRecordsMainViewModel _medicalRecordsMainViewModel;

        EquipmentMainViewModel _equipmentMainViewModel;

        // Commands

        RelayCommand _appointmentsViewCommand;
        RelayCommand _accountsViewCommand;
        RelayCommand _medicalRecordsViewCommand;
        RelayCommand _equipmentViewCommand;

        #endregion

        #region Commands

        public RelayCommand AppointmentsViewCommand
        {
            get
            {
                if (_appointmentsViewCommand == null)
                {
                    _appointmentsViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _appointmentsViewModel;
                    });
                }

                return _appointmentsViewCommand;
            }
        }

        public RelayCommand AccountsViewCommand
        {
            get
            {
                if (_accountsViewCommand == null)
                {
                    _accountsViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _accountsMainViewModel;
                    });
                }

                return _accountsViewCommand;
            }
        }

        public RelayCommand MedicalRecordsViewCommand
        {
            get
            {
                if (_medicalRecordsViewCommand == null)
                {
                    _medicalRecordsViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _medicalRecordsMainViewModel;
                    });
                }

                return _medicalRecordsViewCommand;
            }
        }

        public RelayCommand EquipmentViewCommand
        {
            get
            {
                if (_equipmentViewCommand == null)
                {
                    _equipmentViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _equipmentMainViewModel;
                    });
                }

                return _equipmentViewCommand;
            }
        }

        #endregion

        #region Properties

        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged("CurrentView");
                }
            }
        }

        public Secretary Secretary
        {
            get
            {
                return _secretary;
            }
            set
            {
                _secretary = value;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sc">Sekretar koji se prijavio</param>
        public SecretaryHomeViewModel()
        {
            // Init view models
            _appointmentsViewModel = new AppointmentsMainViewModel();
            _accountsMainViewModel = new AccountsMainViewModel();
            _medicalRecordsMainViewModel = new MedicalRecordsMainViewModel();
            _equipmentMainViewModel = new EquipmentMainViewModel();

            // Na pocetku stavimo currentView na preglediOperacije(to je prvi menu item)
            _currentView = _appointmentsViewModel;
        }

        #endregion
    }
}
