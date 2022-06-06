using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.FreeDaysViewModels
{
    public class FreeDayRequestResolvedViewModel
    {
        #region Fields

        private FreeDayRequestResolved _freeDayRequestResolved;

        private DoctorController _doctorController;

        // Doktor koji je podneo zahtev
        private Doctor _doctor;

        #endregion

        #region Properties

        // Mora zbog brisanja iz baze
        public FreeDayRequestResolved FreeDayRequestResolved
        {
            get { return _freeDayRequestResolved; }
        }

        public Doctor Doctor
        {
            get { return _doctor; }
        }

        public DateTime From
        {
            get { return _freeDayRequestResolved.From; }
        }

        public DateTime To
        {
            get { return _freeDayRequestResolved.To; }
        }

        public bool Accepted
        {
            get { return _freeDayRequestResolved.Accepted; }
            set
            {
                if (_freeDayRequestResolved.Accepted != value)
                {
                    _freeDayRequestResolved.Accepted = value;
                }
            }
        }

        public string Description
        {
            get { return _freeDayRequestResolved.Description; }
            set
            {
                if (_freeDayRequestResolved.Description != value)
                {
                    _freeDayRequestResolved.Description = value;
                }
            }
        }

        #endregion

        #region Constructor

        public FreeDayRequestResolvedViewModel(FreeDayRequestResolved freeDayRequestResolved)
        {
            _freeDayRequestResolved = freeDayRequestResolved;

            App app = Application.Current as App;

            _doctorController = app.DoctorController;

            _doctor = _doctorController.GetDoctorById(freeDayRequestResolved.DoctorId);
        }

        #endregion

    }
}
