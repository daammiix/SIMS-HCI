using ClassDijagramV1._0.Views;
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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Dialog
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentDialog.xaml
    /// </summary>
    public partial class UpdateAppointmentDialog : Window
    {
        public AppointmentController _appointmentController;
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public UpdateAppointmentDialog()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _appointmentController = app.appointmentController;
            Appointments = _appointmentController.GetAllAppointments("djordje");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date1 = new DateTime(2022, 8, 18, 13, 30, 30);
            Random rnd = new Random();
            int card = rnd.Next(50);

            Doctor d1 = new Doctor("noviDoktor" + card.ToString(), "novidoktor", "123", "musko", "3875432", "the292200", date1);

            var oldAppointment = PatientView.selectedAppointment;
            var updatedAppointment = oldAppointment;
            updatedAppointment.Date = promjenaKalendar.SelectedDate.Value;
            updatedAppointment.Doctor = d1;

            _appointmentController.UpdateAppointment(oldAppointment.Id, updatedAppointment);
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
