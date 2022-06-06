using ClassDijagramV1._0.Model;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.ComponentModel;
using System.Text;

namespace ClassDijagramV1._0.Helpers
{
    public class PDFGenerator
    {
        BindingList<RoomToGenerate> roomsToGenerate { get; set; }

        public PDFGenerator(BindingList<RoomToGenerate> roomsToGenerate)
        {
            this.roomsToGenerate = roomsToGenerate;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            GeneratePDF();
        }
        private void GeneratePDF()
        {
            PdfDocument pdfDocument = new PdfDocument();
            //pdfDocument.Info.Title = "PDF Example";

            PdfPage page = pdfDocument.AddPage();

            XGraphics graphics = XGraphics.FromPdfPage(page);

            //title
            graphics.DrawString("Zauzetost sala", new XFont("Arial", 40, XFontStyle.BoldItalic), XBrushes.Salmon, new XPoint(170, 70));
            graphics.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.Gray)), new XPoint(100, 90), new XPoint(500, 90));

            //table
            graphics.DrawString("Šifra", new XFont("Arial", 15), XBrushes.Black, new XPoint(80, 200));
            graphics.DrawString("Naziv", new XFont("Arial", 15), XBrushes.Black, new XPoint(145, 200));
            graphics.DrawString("Početak", new XFont("Arial", 15), XBrushes.Black, new XPoint(275, 200));
            graphics.DrawString("Kraj", new XFont("Arial", 15), XBrushes.Black, new XPoint(410, 200));

            graphics.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.DarkSalmon)), new XPoint(50, 207), new XPoint(550, 207));

            int currentYposition_values = 223;
            int currentYposition_line = 230;

            for (int i = 0; i < roomsToGenerate.Count; i++)
            {
                graphics.DrawString(roomsToGenerate[i].ID, new XFont("Arial", 15), XBrushes.Black, new XPoint(80, currentYposition_values));
                graphics.DrawString(roomsToGenerate[i].Name, new XFont("Arial", 15), XBrushes.Black, new XPoint(145, currentYposition_values));
                graphics.DrawString(roomsToGenerate[i].Start, new XFont("Arial", 15), XBrushes.Black, new XPoint(275, currentYposition_values));
                graphics.DrawString(roomsToGenerate[i].End, new XFont("Arial", 15), XBrushes.Black, new XPoint(410, currentYposition_values));

                graphics.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.DarkSalmon)), new XPoint(50, currentYposition_line), new XPoint(550, currentYposition_line));

                currentYposition_values += 23;
                currentYposition_line += 23;
            }

            pdfDocument.Save("D:\\Fax\\SIMS\\SIMSProjekat\\SIMS - HCI\\Kod\\TestPdf.pdf");
        

        }
    }
}
