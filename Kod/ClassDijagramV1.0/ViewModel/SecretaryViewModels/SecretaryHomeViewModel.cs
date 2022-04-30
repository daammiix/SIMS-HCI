using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels
{
    public class SecretaryHomeViewModel : ObservableObject
    {

        #region Fields

        // Treba da se dopuni da se salje u konstruktor Sekretar
        private Secretary? _secretary;

        private object? _currentView;

        PreglediOperacijeViewModel _preglediOperacijeViewModel;

        AccountsMainViewModel _accountsMainViewModel;

        MedicalRecordsMainViewModel _medicalRecordsMainViewModel;

        // Commands

        RelayCommand _preglediOperacijeViewCommand;
        RelayCommand _accountsViewCommand;
        RelayCommand _medicalRecordsViewCommand;

        #endregion

        #region Commands

        public RelayCommand PreglediOperacijeViewCommand
        {
            get
            {
                if (_preglediOperacijeViewCommand == null)
                {
                    _preglediOperacijeViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _preglediOperacijeViewModel;
                    });
                }

                return _preglediOperacijeViewCommand;
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
            _preglediOperacijeViewModel = new PreglediOperacijeViewModel();
            _accountsMainViewModel = new AccountsMainViewModel();
            _medicalRecordsMainViewModel = new MedicalRecordsMainViewModel();

            // Na pocetku stavimo currentView na preglediOperacije(to je prvi menu item)
            _currentView = _preglediOperacijeViewModel;
        }

        #endregion
    }
}
