using ClassDijagramV1._0.Model;
using System;
using System.ComponentModel;
using System.Text.Json;

namespace ClassDijagramV1._0.Repository
{
    public class QuarterlyReportsRepo
    {
        private String path = "..\\..\\..\\Data\\quarterlyReports.json";
        private BindingList<QuarterlyReport> quarterlyReports = new BindingList<QuarterlyReport>();
        public QuarterlyReportsRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<QuarterlyReport>? jsonQuarterlyReport = JsonSerializer.Deserialize<BindingList<QuarterlyReport>>(jsonData);
            if (jsonQuarterlyReport != null)
            {
                this.quarterlyReports = jsonQuarterlyReport;
            }
        }

        public QuarterlyReport? GetQuarterlyReport(String quarterlyReportsID)
        {
            foreach (var quarterlyReports in quarterlyReports)
            {
                if (quarterlyReports.ID == quarterlyReportsID)
                {
                    return quarterlyReports;
                }
            }
            return null;
        }

        public BindingList<QuarterlyReport> GetAllQuarterlyReports()
        {
            return quarterlyReports;
        }

        private void writeReports()
        {
            String jsonString = JsonSerializer.Serialize(quarterlyReports);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
