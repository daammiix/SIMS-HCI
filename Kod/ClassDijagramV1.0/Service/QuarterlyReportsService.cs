using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class QuarterlyReportsService
    {
        private QuarterlyReportsRepo quarterlyReportsRepo;
        public QuarterlyReportsService(QuarterlyReportsRepo quarterlyReportsRepo)
        {
            this.quarterlyReportsRepo = quarterlyReportsRepo;
        }
        public QuarterlyReport? GetQuarterlyReport(String quarterlyReportID)
        {
            return quarterlyReportsRepo.GetQuarterlyReport(quarterlyReportID);
        }

        public BindingList<QuarterlyReport> GetAllQuarterlyReports()
        {
            return quarterlyReportsRepo.GetAllQuarterlyReports();
        }
    }
}
