using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassDijagramV1._0.Repository
{
    public class ActivityRepo
    {
        private FileHandler<ActivityLog> _activityFileHandler;
        public ObservableCollection<ActivityLog> Activities;

        public ActivityRepo(FileHandler<ActivityLog> fileHandler)
        {
            _activityFileHandler = fileHandler;
            Activities = new ObservableCollection<ActivityLog>(_activityFileHandler.GetItems());
        }

        public List<ActivityLog> GetAllActivity()
        {
            return Activities.ToList();
        }

        public void AddActivity(ActivityLog newActivity)
        {
            Activities.Add(newActivity);
        }

        public void SaveActivity()
        {
            _activityFileHandler.SaveItems(Activities.ToList());
        }
    }
}
