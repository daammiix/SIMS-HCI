using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

            JsonSerializer.Serialize<List<T>>(writer, items, new JsonSerializerOptions() { WriteIndented = true });

            fs.Close();
        }

        /// <summary>
        /// Dobavlja listu itema iz fajla
        /// </summary>
        /// <returns></returns>
        public List<T>? GetItems()
        {
            List<T>? items = new List<T>();
            if (File.Exists(_path))
            {
                FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read);
                items = JsonSerializer.Deserialize<List<T>>(fs);

                fs.Close();
            }

            return items;
        }

        /// <summary>
        /// Dodaje item u fajl
        /// </summary>
        /// <param name="item"></param>
        public void AppendItem(T item)
        {
            List<T> items = GetItems();
            if (items != null)
            {
                items.Add(item);
            }

            SaveItems(items);
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

