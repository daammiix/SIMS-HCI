using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class ScheduleViewModel
    {

        #region Fields

        private AppointmentController _appointmentController;

        #endregion

        #region Properties

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public ScheduleViewModel()
        {
            this.Appointments = new ObservableCollection<AppointmentViewModel>();

            App app = Application.Current as App;

            // Uzmemo kontroler da bi ucitali appointmente
            this._appointmentController = app.AppointmentController;

            // Za svaki appointment napravimo viewModel i dodamo u listu appointmenta
            foreach (Appointment appointment in _appointmentController.GetAppointments())
            {
                Appointments.Add(new AppointmentViewModel(appointment));
            }
        }

        #endregion
    }
}
