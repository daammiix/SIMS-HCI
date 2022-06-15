using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.SecretaryView.FreeDaysView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.FreeDaysViewModels
{
    public class FreeDaysMainViewModel
    {
        #region Fields

        // Controllers

        private FreeDaysRequestController _freeDaysRequestController;

        private FreeDaysRequestResolvedController _freeDaysRequestResolvedController;

        // Commands

        private RelayCommand _searchCommand;

        private RelayCommand _acceptCommand;

        private RelayCommand _refuseCommand;

        #endregion


        #region Properties

        public string? SearchText { get; set; }

        public ObservableCollection<FreeDayRequestViewModel> FreeDayRequests { get; set; }

        public ObservableCollection<FreeDayRequestResolvedViewModel> FreeDayRequestsResolved { get; set; }

        public FreeDayRequestViewModel SelectedFreeDayRequest { get; set; }

        #endregion

        #region Commands

        public RelayCommand AcceptCommand
        {
            get
            {
                if (_acceptCommand == null)
                {
                    _acceptCommand = new RelayCommand(o => { AcceptExecute(); }, AcceptRefuseCanExecute);
                }

                return _acceptCommand;
            }
        }

        public RelayCommand RefuseCommand
        {
            get
            {
                if (_refuseCommand == null)
                {
                    _refuseCommand = new RelayCommand(o => { RefuseExecute(); }, AcceptRefuseCanExecute);
                }

                return _refuseCommand;
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(o => SearchExecute(o as DataGrid));
                }

                return _searchCommand;
            }
        }

        #endregion

        #region Constructor

        public FreeDaysMainViewModel()
        {
            App app = Application.Current as App;

            _freeDaysRequestController = app.FreeDaysRequestController;
            _freeDaysRequestResolvedController = app.FreeDaysRequestResolvedController;

            // Ucitamo podatke
            FreeDayRequests = new ObservableCollection<FreeDayRequestViewModel>();
            FreeDayRequestsResolved = new ObservableCollection<FreeDayRequestResolvedViewModel>();

            LoadRequests();

            // Timer koji proverava koje su requestovi prosli i brise ih
            StartTimer();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Ucitava requestove i pravi od njih ViewModele
        /// </summary>
        private void LoadRequests()
        {
            _freeDaysRequestController.GetFreeDayRequests().ForEach(freeDayRequest =>
            {
                FreeDayRequests.Add(new FreeDayRequestViewModel(freeDayRequest));
            });

            _freeDaysRequestResolvedController.GetFreeDayRequests().ForEach(freeDayRequest =>
            {
                FreeDayRequestsResolved.Add(new FreeDayRequestResolvedViewModel(freeDayRequest));
            });
        }


        private void AcceptExecute()
        {
            // Uklonimo selektovan request iz liste novih odnosno ne resolvovanih i ubacimo ga u listu resolvovanih

            FreeDayRequestResolved freeDayRequestsResolved = new FreeDayRequestResolved(SelectedFreeDayRequest.Doctor.Id,
                SelectedFreeDayRequest.From, SelectedFreeDayRequest.To, true, "Prihvaćeno");
            FreeDayRequestsResolved.Add(new FreeDayRequestResolvedViewModel(freeDayRequestsResolved));

            // TODO: Updatujemo bazu
            _freeDaysRequestController.Remove(SelectedFreeDayRequest.FreeDayRequest);
            _freeDaysRequestResolvedController.AddFreeDayRequestResolved(freeDayRequestsResolved);

            FreeDayRequests.Remove(SelectedFreeDayRequest);
        }

        private bool AcceptRefuseCanExecute()
        {
            return SelectedFreeDayRequest == null ? false : true;
        }

        private void RefuseExecute()
        {
            // Napravimo resolved request, description stavimo na prazan string jer ga popunjavamo u dijalogu
            FreeDayRequestResolved freeDayRequestsResolved = new FreeDayRequestResolved(SelectedFreeDayRequest.Doctor.Id,
                SelectedFreeDayRequest.From, SelectedFreeDayRequest.To, false, "");

            // Prikazemo dijalog za unos opisa(razloga zasto je zahtev odbijen)
            EnterDescriptionDialog enterDescriptionDialog = new EnterDescriptionDialog();
            enterDescriptionDialog.DataContext = new EnterDescriptionDialogViewModel(freeDayRequestsResolved, FreeDayRequestsResolved);
            enterDescriptionDialog.Owner = Application.Current.MainWindow;
            enterDescriptionDialog.Show();

            // Izbrisemo iz baze freeDayRequest jer ga sad rezolvujemo a dodajemo novi u enterDescription dijalogu
            _freeDaysRequestController.Remove(SelectedFreeDayRequest.FreeDayRequest);

            FreeDayRequests.Remove(SelectedFreeDayRequest);
        }

        private void StartTimer()
        {
            Timer timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += onTimedEvent;
            timer.Enabled = true;
        }

        /// <summary>
        /// Proverava koji su requestovi prosli i brise ih i iz view-a i iz baze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimedEvent(object? sender, ElapsedEventArgs e)
        {
            List<FreeDayRequestViewModel> toRemove = new List<FreeDayRequestViewModel>();
            List<FreeDayRequestResolvedViewModel> toRemoveResolved = new List<FreeDayRequestResolvedViewModel>();
            foreach (var freeDayRequestVM in FreeDayRequests)
            {
                if (freeDayRequestVM.From <= DateTime.Now)
                {
                    toRemove.Add(freeDayRequestVM);
                }
            }

            foreach (var freeDayRequestResolvedVM in FreeDayRequestsResolved)
            {
                if (freeDayRequestResolvedVM.From <= DateTime.Now)
                {
                    toRemoveResolved.Add(freeDayRequestResolvedVM);
                }
            }

            // Brisemo iz view-a mora iz dispatcher threda jer je on napravio
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    toRemove.ForEach(freeDayRequestViewModel => FreeDayRequests.Remove(freeDayRequestViewModel));
                    toRemoveResolved.ForEach(freeDayRequestResolvedViewModel => FreeDayRequestsResolved.Remove(freeDayRequestResolvedViewModel));
                }));
                // Brisemo iz baze
                DeleteFromBase(toRemove, toRemoveResolved);
            }
        }

        /// <summary>
        /// Brise requestove iz baze
        /// </summary>
        /// <param name="toRemove"></param>
        /// <param name="toRemoveResolved"></param>
        private void DeleteFromBase(List<FreeDayRequestViewModel> toRemove, List<FreeDayRequestResolvedViewModel> toRemoveResolved)
        {
            toRemove.ForEach(freeDayRequestViewModel =>
            {
                _freeDaysRequestController.Remove(freeDayRequestViewModel.FreeDayRequest);
            });

            toRemoveResolved.ForEach(freeDayRequestResolvedViewModel =>
            {
                _freeDaysRequestResolvedController.Remove(freeDayRequestResolvedViewModel.FreeDayRequestResolved);
            });
        }

        private void SearchExecute(DataGrid dg)
        {
            if (!SearchText.Equals(""))
            {
                DateTime searchedDateTime;
                DateTime.TryParse(SearchText, out searchedDateTime);
                var filteredFreeDayRequestsResolved = FreeDayRequestsResolved
                                .Where(item => item.Doctor.Name.ToLower().Contains(SearchText.ToLower()) ||
                                               item.Doctor.Jmbg.Contains(SearchText));
                if (searchedDateTime != null)
                {
                    filteredFreeDayRequestsResolved.Concat(FreeDayRequestsResolved
                        .Where(item => (item.From <= searchedDateTime && item.To >= searchedDateTime)));
                }
                dg.ItemsSource = filteredFreeDayRequestsResolved;
            }
            else
            {
                dg.ItemsSource = FreeDayRequestsResolved;
            }
        }

        #endregion

    }
}
