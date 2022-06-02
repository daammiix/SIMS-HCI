using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;

namespace ClassDijagramV1._0.Controller
{
    public class ActivityController
    {
        private ActivityService _activityService;

        public ActivityController(ActivityService activityService)
        {
            _activityService = activityService;
        }
        public int NumberOfActivity(int PatientID, TypeOfActivity type)
        {
            return _activityService.NumberOfActivity(PatientID, type);
        }

        public void AddActivity(ActivityLog newActivity)
        {
            _activityService.AddActivity(newActivity);
        }

        public void SaveActivity()
        {
            _activityService.SaveActivity();
        }
    }
}
