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
        private ObservableCollection<AppointmentViewModel> Appointments;

        public ReportPDF(ObservableCollection<AppointmentViewModel> patientAppointments,DateTime from, DateTime to)
        {
            InitializeComponent();
            this.DataContext = this;
            From = from;
            To = to;
            Appointments = patientAppointments;
            //UpdateTable();
            FromLabel.Content = From.ToString("dd/MM/yyyy");
            ToLabel.Content = To.ToString("dd/MM/yyyy");
            DateOfPrinting.Content = DateTime.Now.ToString("dd/MM/yyyy");
            AppointmentPreview.ItemsSource = Appointments;
        }
    }
}
