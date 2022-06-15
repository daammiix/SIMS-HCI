using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Controller
{
    public class ReportsController
    {
        private ReportsService reportsService;

        public ReportsController(ReportsService reportsService)
        {
            this.reportsService = reportsService;
        }
        public Reports? GetReport(String reportID)
        {
            return reportsService.GetReport(reportID);
        }

        public BindingList<Reports> GetAllReports()
        {
            return reportsService.GetAllReports();
        }
    }
}
