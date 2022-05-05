﻿using ClassDijagramV1._0.Views;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Dialog
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentDialog.xaml
    /// </summary>
    public partial class UpdateAppointmentDialog : Window
    {
        #region Fields

        private AppointmentController _appointmentController;
        private RoomController _roomController;
        private DoctorController _doctorController;

        #endregion

        #region Properties

        public AppointmentViewModel SelectedAppointment { get; set; }

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedAppointment">Selektovani appointment koji updatujemo</param>
        public UpdateAppointmentDialog(AppointmentViewModel selectedAppointment)
        {
            InitializeComponent();

            SelectedAppointment = selectedAppointment;

            this.DataContext = this;
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _roomController = app.roomController;
            _doctorController = app.DoctorController;

            Rooms = _roomController.GetAllRooms();
            //Appointments = _appointmentController.GetAllAppointments("djordje"); // ulgovani korisnik ali ovo je za doktora
            Doctors = _doctorController.GetAllDoctors();

            BlackoutDates();
            promjenaKalendar.SelectedDate = SelectedAppointment.AppointmentDate;

        }

        private void BlackoutDates()
        {
            var oldDate = SelectedAppointment.AppointmentDate;
            var firstDate = DateTime.MinValue;
            var lastDate = DateTime.MaxValue;
            double NDays = 5;
            DateTime NDaysBefore = oldDate.AddDays(-NDays);
            DateTime NDaysAfter = oldDate.AddDays(NDays);
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, NDaysBefore));
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(NDaysAfter, DateTime.MaxValue));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var oldAppointment = SelectedAppointment;
            var updatedAppointment = oldAppointment;

            DateTime date1 = promjenaKalendar.SelectedDate.Value;
            /*DateTime date2 = date1.AddMinutes(15);
            TimeSpan interval = date2 - date1;*/
            Doctor d1 = (Doctor)dodavanjPregledaDoktor.SelectedItem;
            Room r1 = getFreeRoom(date1, date1.AddMinutes(15));

            // Ovo menja i view i bazu 
            updatedAppointment.AppointmentDate = promjenaKalendar.SelectedDate.Value;
            // Menjamo u samoj bazi(sam appointment model)
            updatedAppointment.Appointment.DoctorId = d1.Id;
            updatedAppointment.Appointment.RoomId = r1.RoomID;
            // Menjamo i view
            updatedAppointment.DoctorName = d1.Name;
            updatedAppointment.Doctor = d1;
            updatedAppointment.Room = r1;

            // ovo sad ne treba updatovan je odmah ovde
            //_appointmentController.UpdateAppointment(oldAppointment.Id, updatedAppointment.Appointment);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Room getFreeRoom(DateTime start, DateTime end) //FJA ZA DODJELU PRVE PRAZNE SOBE
        {
            //var freeRooms = new List<Room>;
            foreach (Room r in Rooms)
            {
                if (r.isFree(start, end)) // dodaj sebi polje isFree
                {
                    //freeRooms.Add(r);
                    return r;
                }

            }
            return null;
        }
    }
}
