using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.FreeDaysViewModels
{
    public class EnterDescriptionDialogViewModel
    {
        #region Fields

        // Resolvovan request ciji description popunjavamo
        private FreeDayRequestResolved _freeDayRequestResolved;

        // Observable kolekcija u koju ubacimo _freeDayRequestResolvedViewModel kad ga popunimo
        private ObservableCollection<FreeDayRequestResolvedViewModel> _freeDayRequestsResolved;

        private FreeDaysRequestResolvedController _freeDaysRequestResolvedController;

        // Commands
        private RelayCommand _saveCommand;

        #endregion

        #region Properties

        public string Description { get; set; } = String.Empty;

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(o => { SaveExecute(o as Window); }, SaveCanExecute);
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Constructor

        public EnterDescriptionDialogViewModel(FreeDayRequestResolved freeDayRequestResolved,
            ObservableCollection<FreeDayRequestResolvedViewModel> freeDayRequestsResolved)
        {
            _freeDayRequestResolved = freeDayRequestResolved;
            _freeDayRequestsResolved = freeDayRequestsResolved;

            App app = Application.Current as App;
            _freeDaysRequestResolvedController = app.FreeDaysRequestResolvedController;
        }

        #endregion

        #region Private Helpers

        private void SaveExecute(Window dialog)
        {
            // Settujemo description
            _freeDayRequestResolved.Description = Description;

            // Ubacimo u observable kolekciju
            _freeDayRequestsResolved.Add(new FreeDayRequestResolvedViewModel(_freeDayRequestResolved));

            // Ubacimo u bazu
            _freeDaysRequestResolvedController.AddFreeDayRequestResolved(_freeDayRequestResolved);

            // Zatvorimo dijalog
            dialog.Close();
        }

        private bool SaveCanExecute()
        {
            return Description.Equals("") ? false : true;
        }

        #endregion
    }
}
