using Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClassDijagramV1._0.FileHandlers
{

    public class AppointmentFileHandler
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Constructor

        public AppointmentFileHandler(string filepath)
        {
            _filePath = filepath;
        }

        #endregion

        #region Methods

        public void SaveAppointments(List<Appointment> accounts)
        {
            string jsonAccounts = JsonSerializer.Serialize<List<Appointment>>(accounts, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_filePath, jsonAccounts);
        }

        public List<Appointment> GetAppointments()
        {
            using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_filePath) && stream.Length > 0)
                {
                    string appointments = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Appointment>>(appointments);
                }
                else
                {
                    return new List<Appointment>();
                }
            }
        }

        #endregion

    }
}
