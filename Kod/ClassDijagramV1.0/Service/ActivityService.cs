using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;

namespace ClassDijagramV1._0.Service
{
    public class ActivityService
    {
        private ActivityRepo _activityRepo;

        public ActivityService(ActivityRepo activityRepo)
        {
            _activityRepo = activityRepo;
        }
        public int NumberOfActivity(int PatientID, TypeOfActivity type)
        {
            int counterOfActivity = 0;
            List<ActivityLog> activities = _activityRepo.GetAllActivity();
            foreach (ActivityLog activity in activities)
            {
                if ((activity.PatientID == PatientID)
                    && activity.Type.Equals(type)
                    && activity.Date > DateTime.Now.AddDays(-10))
                {
                    ++counterOfActivity;
                }
            }
            return counterOfActivity;
        }

        public void SaveActivity()
        {
            _activityRepo.SaveActivity();
        }

        public void AddActivity(ActivityLog newActivity)
        {
            _activityRepo.AddActivity(newActivity);
        }
    }
}
