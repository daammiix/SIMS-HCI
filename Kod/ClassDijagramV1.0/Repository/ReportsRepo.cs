using ClassDijagramV1._0.Model;
using System;
using System.ComponentModel;
using System.Text.Json;

namespace ClassDijagramV1._0.Repository
{
    public class ReportsRepo
    {
        private String path = "..\\..\\..\\Data\\reports.json";
        private BindingList<Reports> reports = new BindingList<Reports>();
        public ReportsRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<Reports>? jsonReports = JsonSerializer.Deserialize<BindingList<Reports>>(jsonData);
            if (jsonReports != null)
            {
                this.reports = jsonReports;
            }
        }

        public Reports? GetReport(String reportID)
        {
            foreach (var report in reports)
            {
                if (report.ID == reportID)
                {
                    return report;
                }
            }
            return null;
        }

        public BindingList<Reports> GetAllReports()
        {
            return reports;
        }
    }
}
