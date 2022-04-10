
﻿using ClassDijagramV1._0.Dialog;
//using ClassDijagramV1._0.Model.Accounts;
﻿using ClassDijagramV1._0.Model;

using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for Pacijent.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        public AppointmentController _appointmentController;
        public static Appointment selectedAppointment;

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        public PatientView()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _appointmentController = app.appointmentController;
            Appointments = _appointmentController.GetAllAppointments("djordje");

        }


        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            //_appointmentController.AddAppointment(a1);
            var a = new AddAppointmentDialog();
            a.Show();
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {
            
            if (tabelaPregledi.SelectedIndex != -1)
            {
                selectedAppointment = (Appointment)tabelaPregledi.SelectedItem;
                var a = new UpdateAppointmentDialog();
                a.Show();
            }

        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPregledi.SelectedIndex != -1)
            {
                //Appointments.Remove((Appointment)tabelaPregledi.SelectedItem);
                _appointmentController.RemoveAppointment((Appointment)tabelaPregledi.SelectedItem);
            }

        }
    }
}
