using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System.ComponentModel;

namespace ClassDijagramV1._0.Service
{
    public class ReportsService
    {
        private ReportsRepo reportsRepo;
        public ReportsService(ReportsRepo reportsRepo)
        {
            this.reportsRepo = reportsRepo;
        }
        public Reports? GetReport(string reportID)
        {
            return reportsRepo.GetReport(reportID);
        }

        public BindingList<Reports> GetAllReports()
        {
            return reportsRepo.GetAllReports();
        }
    }
}
