using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
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

            // Pokrenemo timer koji proverava da li ima appointmenta koji su se zavrsili
            StartAppointmentsTimer();
        }

        #endregion

        #region Private Helpers 

        /// <summary>
        /// Proverava da li postoji appointmenti koji su se zavrsili kako bi ih izbrisali
        /// </summary>
        private void StartAppointmentsTimer()
        {
            // Timer koji se elapsuje na 1 sekundu
            Timer timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += OnTimedEvent;

            timer.Enabled = true;
        }

        /// <summary>
        /// Event handler za timerov Elapse event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            // Lista AppointmentViewModela za brisanje
            List<AppointmentViewModel> toRemove = new List<AppointmentViewModel>();

            foreach (AppointmentViewModel appointment in Appointments)
            {
                if (appointment.To < DateTime.Now)
                {
                    toRemove.Add(appointment);
                }
            }

            foreach (AppointmentViewModel appointment in toRemove)
            {
                // Samo thread koji je napravio objekte moze da im pristupa to je u nasem slucaju Dispatcher
                // Single Thread Affinity
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // Brisemo ih iz view-a
                    Appointments.Remove(appointment);

                    // Brisemo ih iz repozitorijuma preko kontrolera
                    _appointmentController.RemoveAppointment(appointment.Id);
                }));
            }
        }

        #endregion
    }
}
