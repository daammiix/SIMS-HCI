using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
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

        // Commands

        RelayCommand _preglediOperacijeViewCommand;
        RelayCommand _naloziViewCommand;

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

        public RelayCommand NaloziViewCommand
        {
            get
            {
                if (_naloziViewCommand == null)
                {
                    _naloziViewCommand = new RelayCommand(o =>
                    {
                        CurrentView = _accountsMainViewModel;
                    });
                }

                return _naloziViewCommand;
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
                    onPropertyChanged("CurrentView");
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

            // Na pocetku stavimo currentView na preglediOperacije(to je prvi menu item)
            _currentView = _preglediOperacijeViewModel;
        }

        #endregion
    }
}
