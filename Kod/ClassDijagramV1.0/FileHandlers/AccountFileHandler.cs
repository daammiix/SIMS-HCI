using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace ClassDijagramV1._0.FileHandlers
{
    public class AccountFileHandler
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Constructor

        public AccountFileHandler(string filepath)
        {
            _filePath = filepath;
        }

        #endregion

        #region Methods

        public void saveAccounts(List<Account> accounts)
        {
            string jsonAccounts = JsonSerializer.Serialize<List<Account>>(accounts, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_filePath, jsonAccounts);
        }

        public List<Account> getAccounts()
        {
            using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_filePath) && stream.Length > 0)
                {
                    string accounts = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<Account>>(accounts);
                }
                else
                {
                    return new List<Account>();
                }
            }
        }

        #endregion

    }
}
