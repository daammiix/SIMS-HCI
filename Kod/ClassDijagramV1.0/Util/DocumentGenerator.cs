using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using Model;
using PdfSharp.Drawing.Layout;

namespace ClassDijagramV1._0.Util
{
    /// <summary>
    /// Specijalizovano da radi za appointmente, mada ne treba mnogo izmena da bi radilo i za druge podatke
    /// </summary>
    public class DocumentGenerator
    {
        #region Fields

        private PdfDocument _document;

        private List<PdfPage> _pages = new List<PdfPage>();

        private XGraphics _graphics;

        private XFont _paragraphFont;

        private XFont _headerFont;

        #endregion

        #region Constructor

        public DocumentGenerator(string title, string font)
        {
            PrepareDocument(title, font);

        }

        #endregion

        #region Methods

        public void SetHeader(string header)
        {
            _graphics.DrawString(header, _headerFont, XBrushes.Red,
                new XRect(0, 0, _pages[0].Width, _pages[0].Height), XStringFormats.TopCenter);

            _graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 30), new XPoint(820, 30));
        }

        /// <summary>
        /// Pozvati nakon iscrtavanja podataka da bi _graphics referencirao poslednju stranu
        /// </summary>
        /// <param name="secretaryName"></param>
        public void SetFooter(string secretaryName)
        {
            string footer = $"Zdravo Klinika d.o.o\nSecretary: {secretaryName}\nDate of issue:{DateTime.Now.ToShortDateString()}";
            XSize pageSize = _graphics.PageSize;
            XTextFormatter tf = new XTextFormatter(_graphics);
            tf.DrawString(footer, _paragraphFont, XBrushes.Red, new XRect(pageSize.Width - 170, pageSize.Height - 65, 170, 65), XStringFormats.TopLeft);

        }

        public void GenerateTableHeaders(string[] columnNames)
        {
            XFont headerFont = new XFont("Arial", 15, XFontStyle.Bold);
            for (int i = 0; i < columnNames.Length; i++)
            {
                // Crtamo headere i svaki sledeci pomeramo 150px desno od prethodnog 
                if (columnNames[i].Equals("From"))
                {
                    _graphics.DrawString(columnNames[i], headerFont, XBrushes.Black, new XPoint(80 + 100 * i + 75, 150));
                }
                else if (columnNames[i].Equals("To"))
                {
                    _graphics.DrawString(columnNames[i], headerFont, XBrushes.Black, new XPoint(80 + 100 * i + 120, 150));
                }
                else
                {
                    _graphics.DrawString(columnNames[i], headerFont, XBrushes.Black, new XPoint(80 + 100 * i, 150));
                }
            }

            _graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(80, 155), new XPoint(740, 155));
        }

        /// <summary>
        /// Generise podatke vezane za appointmente kao tabelu
        /// </summary>
        /// <param name="data"></param>
        public void GenerateAppointmentData(List<AppointmentViewModel> data)
        {
            DrawAppointmentsNumber(data.Count);
            if (data.Count == 0) return;
            // Ako nema vise od 30 appointmenta stavimo ih sve na prvu stranu
            if (data.Count <= 20)
            {
                DrawOnOnePage(data);
            }
            else
            {
                DrawOnMultiplePages(data);
            }
        }

        public void SaveDocument(string filePath)
        {
            _document.Save(filePath);
        }

        #endregion

        #region Private Helpers

        private void PrepareDocument(string title, string font)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _document = new PdfDocument();
            PdfPage newPage = _document.AddPage();
            newPage.Orientation = PdfSharp.PageOrientation.Landscape;
            _pages.Add(newPage);
            _graphics = XGraphics.FromPdfPage(_pages.Last());
            _paragraphFont = new XFont(font, 14);
            _headerFont = new XFont(font, 20, XFontStyle.Bold);

            _document.Info.Title = title;
        }

        /// <summary>
        /// Ako imamo malo podataka dovoljna nam je jedna strana gde nam je i header i sve
        /// </summary>
        /// <param name="data"></param>
        private void DrawOnOnePage(List<AppointmentViewModel> data)
        {
            // Linija headera nam je na 155 ispod krecu podaci
            int currentYposition_values = 170; // y pozicija za vrednosti odnosno sam red
            int currentYposition_line = 175; // y pozicija za liniji koja odvaja red od novog

            foreach (AppointmentViewModel aViewModel in data)
            {
                DrawRow(aViewModel, currentYposition_values, currentYposition_line);

                currentYposition_values += 20;
                currentYposition_line += 20;
            }
        }

        private void DrawOnMultiplePages(List<AppointmentViewModel> data)
        {
            // Linija headera nam je na 155 ispod krecu podaci
            int currentYposition_values = 170; // y pozicija za vrednosti odnosno sam red
            int currentYposition_line = 175; // y pozicija za liniji koja odvaja red od novog

            // Prvih 18 redova iscrtamo na prvoj strani
            List<AppointmentViewModel> alreadyDrawn = new List<AppointmentViewModel>();
            for (int i = 0; i < 18; i++)
            {
                DrawRow(data[i], currentYposition_values, currentYposition_line);

                currentYposition_values += 20;
                currentYposition_line += 20;
                alreadyDrawn.Add(data[i]);
            }

            // Izbacimo redove koje smo iscrtali
            alreadyDrawn.ForEach(appointmentVM => data.Remove(appointmentVM));

            CreateNewPageAndSetYPosition(ref currentYposition_values, ref currentYposition_line);

            for (int i = 0; i < data.Count; i++)
            {
                // na svakih 22 napravimo novu stranicu
                if (i != 0 && i % 22 == 0)
                {
                    CreateNewPageAndSetYPosition(ref currentYposition_values, ref currentYposition_line);
                }

                DrawRow(data[i], currentYposition_values, currentYposition_line);
                currentYposition_values += 20;
                currentYposition_line += 20;
            }
        }

        /// <summary>
        /// Iscrtava podatke prosledjenog appointmenta u red, i liniju ispod reda
        /// </summary>
        /// <param name="aViewModel"></param>
        private void DrawRow(AppointmentViewModel aViewModel, int currentYposition_values, int currentYposition_line)
        {
            _graphics.DrawString(aViewModel.PatientName, _paragraphFont, XBrushes.Black, new XPoint(95, currentYposition_values));
            _graphics.DrawString(aViewModel.DoctorName, _paragraphFont, XBrushes.Black, new XPoint(195, currentYposition_values));
            _graphics.DrawString(aViewModel.RoomNumber.ToString(), _paragraphFont, XBrushes.Black, new XPoint(320, currentYposition_values));
            _graphics.DrawString(aViewModel.From.ToString("dd/MM/yyyy hh:mm tt"), _paragraphFont, XBrushes.Black, new XPoint(405, currentYposition_values));
            _graphics.DrawString(aViewModel.To.ToString("dd/MM/yyyy hh:mm tt"), _paragraphFont, XBrushes.Black, new XPoint(565, currentYposition_values));

            _graphics.DrawLine(new XPen(XColor.FromArgb(40, 30, 200)), new XPoint(80, currentYposition_line), new XPoint(740, currentYposition_line));

        }

        /// <summary>
        /// Formira novu stranicu i stavlja _graphics da mozemo da crtamo po njoj
        /// </summary>
        /// <returns></returns>
        private void CreateNewPageAndSetYPosition(ref int currentYPosition_values, ref int currentYPosition_line)
        {
            PdfPage newPage = _document.AddPage();
            newPage.Orientation = PdfSharp.PageOrientation.Landscape;
            _pages.Add(newPage);
            _graphics = XGraphics.FromPdfPage(_pages.Last());

            currentYPosition_values = 33;
            currentYPosition_line = 40;
        }

        private void DrawAppointmentsNumber(int appointmentsNumber)
        {
            _graphics.DrawString($"Appointments number: {appointmentsNumber}", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(80, 100));
        }

        #endregion

    }
}
