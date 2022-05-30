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
    public class ReportsService
    {
        private ReportsRepo reportsRepo;
        public ReportsService(ReportsRepo reportsRepo)
        {
            this.reportsRepo = reportsRepo;
        }
        internal Reports? GetReport(string reportID)
        {
            return reportsRepo.GetReport(reportID);
        }

        internal BindingList<Reports> GetAllReports()
        {
            return reportsRepo.GetAllReports();
        }
    }
}
