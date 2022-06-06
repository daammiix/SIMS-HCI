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

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for ReportPDF.xaml
    /// </summary>
    public partial class ReportPDF : Page
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        private ObservableCollection<AppointmentViewModel> _patientAppointments;
        private AppointmentController _appointmentController;

        public ReportPDF(ObservableCollection<AppointmentViewModel> patientAppointments,DateTime from, DateTime to)
        {
            InitializeComponent();
            this.DataContext = this;
            From = from;
            To = to;
            _patientAppointments = patientAppointments;
            UpdateTable();

        }

        public void UpdateTable()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (AppointmentViewModel appointment in _patientAppointments)
            {
                if (appointment.AppointmentDate >= From && appointment.AppointmentDate <= To)
                {
                    appointments.Add(appointment.Appointment);
                }    
            }

            appointments.Sort((x, y) => DateTime.Compare(x.AppointmentDate, y.AppointmentDate));
            AppointmentPreview.ItemsSource = appointments;
            FromLabel.Content = From.ToString("dd/MM/yyyy");
            ToLabel.Content = To.ToString("dd/MM/yyyy");
            DateOfPrinting.Content = DateTime.Now.ToString("dd/MM/yyyy");

        }
    }
}
