using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace AN.Service
{
    public class JsonSaveLoad : ISaveLoad
    {
        public List<T> Load<T>(string filePath)
        {
            if(File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonData);
            }
            else
            {
                return new List<T>();
            }
        }

        public void Save<T>(string filePath, List<T> data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonData);
        }
    }
}
