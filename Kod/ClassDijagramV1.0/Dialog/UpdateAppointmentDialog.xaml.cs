using ClassDijagramV1._0.Views;
using Controller;
using Model;
using System;
using System.Collections.Generic;
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
        public UpdateAppointmentDialog()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _appointmentController = app.appointmentController;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var oldAppointment = PatientView.selectedAppointment;
            var updatedAppointment = oldAppointment;
            updatedAppointment.Date = promjenaKalendar.SelectedDate.Value;

            _appointmentController.UpdateAppointment(oldAppointment.Id, updatedAppointment);
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
