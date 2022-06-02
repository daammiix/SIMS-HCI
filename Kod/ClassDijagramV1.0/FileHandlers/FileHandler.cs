using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClassDijagramV1._0.FileHandlers
{

    public class FileHandler<T>
        where T : class, new()
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
                FileStream fileStream = File.Create(_path);
                fileStream.Close();
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

        public void Write(T obj)
        {
            string jsonRooms = JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_path, jsonRooms);
        }

        public T Read()
        {
            using (Stream stream = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_path) && stream.Length > 0)
                {
                    string jsonData = File.ReadAllText(_path);
                    T? jsonRooms = JsonSerializer.Deserialize<T>(jsonData);
                    if (jsonRooms != null)
                    {
                        return jsonRooms;
                    }
                    throw new Exception("Json file is null");
                }
                else
                {
                    return new T();
                }
            }

        }

        #endregion
    }
}

