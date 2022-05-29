using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.PatientViewModels
{
    public class OldAppointmentsViewModel
    {
        public ObservableCollection<Appointment> OldAppointments { get; set; }
        private AppointmentController _appointmentController;

        public OldAppointmentsViewModel()
        {
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            //OldAppointments = _appointmentController.GetOldAppointments();
        }
    }
}
