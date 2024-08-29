using AN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AN.Repository
{
    class ModelRepository : IModelRepository
    {
        private ObservableCollection<WatchModel> Models;

        private const string path = "Data/Models.json";
        private const string reservepath = "Data/ReserveModels.json";

        public ModelRepository()
        {
            Load();
        }

        public void AddModel(WatchModel watchModel)
        {
            Models.Add(watchModel);
            Save();
            RezerveSave();
        }

        public void RemoveModel(WatchModel watchModel)
        {
            for(int i = 0; i < Models.Count;i++)
            {
                if(Models[i].Name == watchModel.Name)
                {
                    Models.RemoveAt(i);
                    break;
                }
            }
            Save();
        }

        public WatchModel GetModel(string ModelName)
        {
            return Models.First(i => i.Name.Equals(ModelName));
        }

        public ObservableCollection<WatchModel> GetAllModels()
        {
            return Models;
        }

        public void RemoveAllModels()
        {
            Models.Clear();
            Save();
        }

        private void Save()
        {
            string jsondata = JsonSerializer.Serialize(Models);
            File.WriteAllText(path, jsondata);
        }

        private void RezerveSave()
        {
            string jsondata = JsonSerializer.Serialize(Models);
            File.WriteAllText(reservepath, jsondata);
        }

        private void Load()
        {
            if (File.Exists(path))
            {
                string jsondata = File.ReadAllText(path);
                Models = JsonSerializer.Deserialize<ObservableCollection<WatchModel>>(jsondata);
            }
            else
            {
                Models = new ObservableCollection<WatchModel>();
            }
        }
    }
}
