using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public AppointmentController appointmentController { get; set; }

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Appointment> appointments = new List<Appointment>();


            var appointmentRepository = new AppointmentRepo();
            //var patientRepository = new PatientRepo();
            var patientService = new PatientService();

            appointmentController = new AppointmentController();
        }
    }
}
