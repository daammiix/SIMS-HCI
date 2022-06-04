using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.DTO;
using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
﻿using Model;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for TerapyPage.xaml
    /// </summary>
    public partial class TerapyPage : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _appointments;
        public ObservableCollection<Appointment> Appointments { get; set; }

        public AppointmentController _appointmentController;
        public TerapyPage(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
            _appointments = appointmentViewModels;
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            //Appointments = _appointmentController.GetAppointments();
            //kalendar.ItemsSource = _appointments;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();
            List<TherapyDTO> terapija = FindCurrentTherapies();

            foreach (TherapyDTO t in terapija)
            {
                ScheduleAppointment sa = new ScheduleAppointment();
                sa.StartTime = t.Date;
                sa.Subject = t.Name;
                sa.EndTime = t.Date.AddMinutes(10);
                sa.IsAllDay = false;
                sac.Add(sa);
            }
            kalendar.ItemsSource = sac;
            }

        private void click(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }

        public List<TherapyDTO> FindCurrentTherapies()
        {
            List<TherapyDTO> therapies = new List<TherapyDTO>();
            
            foreach(AppointmentViewModel app in _appointments)
            {
                foreach (MedicineDrug t in app.Appointment.MedicalReport.Medicine)
                {
                    DateTime iterator = t.StartTaking;
                    while (iterator < t.StopTaking)
                    {
                        iterator = iterator.AddHours(t.Interval);
                        therapies.Add(new TherapyDTO(t.MedicineName, iterator));
                    }
                }
            }
            return therapies;
        }
    }
}



/*public partial class PatientCalendar : Page
{
    public PatientCalendar()
    {
        InitializeComponent();
        App app = Application.Current as App;
        AppointmentManagementController ac = app.appointmentController;
        PatientController pc = app.patientController;
        ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();
        foreach (Appointment a in ac.GetAppointmentByPatient(app.userController.CurentLoggedUser.Username))
        {
            ScheduleAppointment sa = new ScheduleAppointment();
            sa.StartTime = a.StartTime;
            sa.EndTime = a.StartTime.AddMinutes(30);
            sa.IsAllDay = false;
            sac.Add(sa);
        }
        foreach (TherapyDTO t in pc.FindCurrentMonthTherapies(app.userController.CurentLoggedUser.Username))
        {
            ScheduleAppointment sa = new ScheduleAppointment();
            sa.StartTime = t.Date;
            sa.EndTime = t.Date.AddMinutes(10);
            sa.IsAllDay = false;
            sac.Add(sa);
        }
        appointmentCalendar.ItemsSource = sac;
    }
}*/