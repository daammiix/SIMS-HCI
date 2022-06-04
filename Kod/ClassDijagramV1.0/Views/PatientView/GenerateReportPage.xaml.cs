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
            _printDialog.PrintVisual(new ReportPDF(_patientAppointments, from, to), "PDFIzvjestaj");
            parent.startWindow.Content = new AppointmentsViewPage(parent, _patientAppointments);
        }
    }
}
