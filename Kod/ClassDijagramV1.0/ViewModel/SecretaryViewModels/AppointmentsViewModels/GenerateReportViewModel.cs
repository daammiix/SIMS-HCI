using ClassDijagramV1._0.Util;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using System.Windows;
using Model;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class GenerateReportViewModel
    {
        #region Fields

        private RelayCommand _generateCommand;

        private AppointmentController _appointmentController;

        private DocumentGenerator _documentGenerator;

        #endregion

        #region Properties

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public RelayCommand GenerateCommand
        {
            get
            {
                if (_generateCommand == null)
                {
                    _generateCommand = new RelayCommand(o => { GenerateDocument(o as Window); }, GenerateDocumentCanExecute);
                }

                return _generateCommand;
            }
        }

        #endregion

        #region Constructor

        public GenerateReportViewModel()
        {
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;

            _documentGenerator = new DocumentGenerator("Report", "Arial");
        }

        #endregion

        #region Private Helpers

        private void GenerateDocument(Window dialog)
        {
            List<Appointment> appointments = _appointmentController.GetAppointmentsForRange(From, To);

            List<AppointmentViewModel> appointmentsVM = appointments
                .Select<Appointment, AppointmentViewModel>(appointment => new AppointmentViewModel(appointment)).ToList();

            _documentGenerator.SetHeader($"Report on scheduled operations and examinations from {From.ToShortDateString()} to {To.ToShortDateString()}");

            string[] columnHeaders = { "Patient Name", "Doctor Name", "Room Number", "From", "To" };

            if (appointmentsVM.Any())
            {
                _documentGenerator.GenerateTableHeaders(columnHeaders);
            }
            _documentGenerator.GenerateAppointmentData(appointmentsVM);
            _documentGenerator.SetFooter("Damjan");
            _documentGenerator.SaveDocument(@"C:\Temp\Report.pdf");

            dialog.Close();
        }

        private bool GenerateDocumentCanExecute()
        {
            if (From != DateTime.MinValue && To != DateTime.MinValue)
            {
                return true;
            }

            return false;
        }

        #endregion

    }
}
