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
using System.Windows.Media;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for TerapyPage.xaml
    /// </summary>
    public partial class TerapyPage : Page
    {
        #region Fileds
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _appointments;
        public ObservableCollection<Appointment> Appointments { get; set; }
        public AppointmentController _appointmentController;
        private ScheduleAppointmentCollection sac;
        List<SolidColorBrush> boje;
        #endregion
        public TerapyPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
            _appointments = appointmentViewModels;

            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;

            sac = new ScheduleAppointmentCollection();
            boje = new List<SolidColorBrush>();
            boje.Add(new SolidColorBrush(Colors.Orchid));
            boje.Add(new SolidColorBrush(Colors.Orange));
            FillCalendar();
        }

        private void click(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }

        public void FillCalendar()
        {
            foreach (TherapyDTO t in FindCurrentTherapies())
            {
                Random rnd = new Random();
                ScheduleAppointment sa = new ScheduleAppointment();
                sa.StartTime = t.Date;
                sa.Subject = t.Name;
                sa.EndTime = t.Date.AddMinutes(10);
                sa.IsAllDay = false;
                sa.AppointmentBackground = boje[rnd.Next(0,2)];
                sac.Add(sa);
            }

            kalendar.ItemsSource = sac;
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
                        if(iterator>=DateTime.Now)
                            therapies.Add(new TherapyDTO(t.MedicineName, iterator));
                    }
                }
            }
            return therapies;
        }
    }
}