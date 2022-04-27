using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.FileHandlers
{
    public class FileHandler<T>
        where T : class
    {
        #region Fields

        private string _path;

        #endregion

        #region Constructor

        public FileHandler(string path)
        {
            _path = path;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Upisuje sve iteme u fajl
        /// </summary>
        /// <param name="items"></param>
        public void SaveItems(List<T> items)
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }
            FileStream fs = new FileStream(_path, FileMode.Truncate, FileAccess.Write);
            Utf8JsonWriter writer = new Utf8JsonWriter(fs);
            if (items.Count > 0)
                JsonSerializer.Serialize<List<T>>(writer, items, new JsonSerializerOptions() { WriteIndented = true });

            fs.Close();
        }

        /// <summary>
        /// Dobavlja listu itema iz fajla
        /// </summary>
        /// <returns></returns>
        public List<T> GetItems()
        {
            List<T>? ret = new List<T>();
            if (File.Exists(_path))
            {
                FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read);
                ret = JsonSerializer.Deserialize<List<T>>(fs);

                fs.Close();
            }

            return ret;
        }

        #endregion
    }
}
