using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace ClassDijagramV1._0.FileHandlers
{
    public class DoctorFileHandler
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Constructor

        public DoctorFileHandler(string filepath)
        {
            _filePath = filepath;
        }

        #endregion

        #region Methods

        public void SaveDoctors(List<Doctor> doctors)
        {
            string jsonAccounts = JsonSerializer.Serialize<List<Doctor>>(doctors, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_filePath, jsonAccounts);
        }

        public List<Doctor> GetDoctors()
        {
            using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_filePath) && stream.Length > 0)
                {
                    string doctors = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Doctor>>(doctors);
                }
                else
                {
                    return new List<Doctor>();
                }
            }
        }

        #endregion

    }
}

