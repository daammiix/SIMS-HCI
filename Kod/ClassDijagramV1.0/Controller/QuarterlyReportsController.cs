using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Controller
{
    public class QuarterlyReportsController
    {
        private QuarterlyReportsService quarterlyReportsService;
        public QuarterlyReportsController(QuarterlyReportsService quarterlyReportsService)
        {
            this.quarterlyReportsService = quarterlyReportsService;
        }
        public QuarterlyReport? GetQuarterlyReport(String quarterlyReportID)
        {
            return quarterlyReportsService.GetQuarterlyReport(quarterlyReportID);
        }

        public BindingList<QuarterlyReport> GetAllQuarterlyReports()
        {
            return quarterlyReportsService.GetAllQuarterlyReports();
        }
    }
}
