using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.FileHandlers
{
    public class PatientFileHandler
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Constructor

        public PatientFileHandler(string filepath)
        {
            _filePath = filepath;
        }

        #endregion

        #region Methods

        public void SavePatients(List<Patient> patients)
        {
            string jsonAccounts = JsonSerializer.Serialize<List<Patient>>(patients, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_filePath, jsonAccounts);
        }

        public List<Patient> GetPatients()
        {
            using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_filePath) && stream.Length > 0)
                {
                    string patients = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Patient>>(patients);
                }
                else
                {
                    return new List<Patient>();
                }
            }
        }

        #endregion

    }
}
