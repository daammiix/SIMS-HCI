using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentUpdatePage.xaml
    /// </summary>
    public partial class AppointmentUpdatePage : Page
    {
        #region Fields

        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;

        #endregion
        public AppointmentUpdatePage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            _appointmentViewModels = appointmentViewModels;
                       
            doctorRB.IsChecked = true;
            dr.Text = AppointmentsViewPage.SelectedAppointment.Doctor.Name + " " + AppointmentsViewPage.SelectedAppointment.Doctor.Surname;
            date.Text = AppointmentsViewPage.SelectedAppointment.AppointmentDate.ToString("HH:mm dd.MM.yyyy.");
            room.Text = AppointmentsViewPage.SelectedAppointment.Room.RoomName.ToString();
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new UpdatePriorityDoctor(parent, _appointmentViewModels);
        }

        private void timeRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new UpdatePriorityTime(parent, _appointmentViewModels);
        }

    }
}
