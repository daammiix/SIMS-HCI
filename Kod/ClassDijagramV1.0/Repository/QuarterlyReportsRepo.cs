using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.ComponentModel;
using System.Text.Json;

namespace ClassDijagramV1._0.Repository
{
    public class QuarterlyReportsRepo
    {
        private FileHandler<BindingList<QuarterlyReport>> fileHandler;
        private BindingList<QuarterlyReport> quarterlyReports = new BindingList<QuarterlyReport>();
        public QuarterlyReportsRepo(FileHandler<BindingList<QuarterlyReport>> fileHandler)
        {
            this.fileHandler = fileHandler;
            quarterlyReports = this.fileHandler.Read();
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
    }
}
