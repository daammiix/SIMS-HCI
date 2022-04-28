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
    public class FileHandler<T> where T : new()
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Constructor

        public FileHandler(string filepath)
        {
            _filePath = filepath;
        }

        #endregion

        #region Methods

        public void Write(T obj)
        {
            string jsonRooms = JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_filePath, jsonRooms);
        }

        public T Read()
        {
            using (Stream stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (File.Exists(_filePath) && stream.Length > 0)
                {
                    string jsonData = File.ReadAllText(_filePath);
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
