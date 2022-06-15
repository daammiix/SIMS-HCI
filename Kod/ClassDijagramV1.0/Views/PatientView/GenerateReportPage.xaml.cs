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
    /// Interaction logic for GenerateReportPage.xaml
    /// </summary>
    public partial class GenerateReportPage : Page
    {
        #region Fields
        private PrintDialog _printDialog = new PrintDialog();
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _patientAppointments;
        private ObservableCollection<AppointmentViewModel> appointmentsToReport { get; set; }
        #endregion
        public GenerateReportPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> patientAppointments)
        {
            InitializeComponent();
            parent = patientMain;
            _patientAppointments = patientAppointments;

        }

        private void ReportPDFClick(object sender, RoutedEventArgs e)
        {
            DateTime from = (DateTime)kalendarod.SelectedDate;
            DateTime to = (DateTime)kalendardo.SelectedDate;
            appointmentsToReport = UpdateTable(from, to);
            _printDialog.PrintVisual(new ReportPDF(appointmentsToReport, from, to), "PDFIzvjestaj");
            parent.startWindow.Content = new AppointmentsViewPage(parent, _patientAppointments);
        }
        public ObservableCollection<AppointmentViewModel> UpdateTable(DateTime from, DateTime to)
        {
            List<AppointmentViewModel> appointments = new List<AppointmentViewModel>();

            foreach (AppointmentViewModel appointment in _patientAppointments)
            {
                if (appointment.AppointmentDate >= from && appointment.AppointmentDate <= to)
                {
                    appointments.Add(appointment);
                }
            }
            appointments.Sort((x, y) => DateTime.Compare(x.AppointmentDate, y.AppointmentDate));

            ObservableCollection<AppointmentViewModel> app =  new ObservableCollection<AppointmentViewModel>(appointments);
            return app;
        }
    }
}
