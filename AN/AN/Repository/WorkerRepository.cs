using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using AN.Models;
using System.Text.Json;
using System.IO;

namespace AN.Repository
{
     class WorkerRepository : IWorkerRepository
    {
        private ObservableCollection<Worker> Workers;

        private const string path = "Data/Workers.json";
        private const string reservepath = "Data/ReserveWorkers.json";
        public WorkerRepository()
        {
            Load();
        }

        public void AddWorker(Worker Worker)
        {
            Workers.Add(Worker);
            Save();
            RezerveSave();
        }

        public ObservableCollection<Worker> GetAllWorkers()
        {
            return Workers;
        }

        public Worker GetWorker(string Name)
        {
            return Workers.First(i => i.FirstName.Equals(Name));
        }

        public void RemoveWorker(Worker Worker)
        {
            Workers.Remove(Worker);
            Save();
        }

        public void RemoveAllWorkers()
        {
            Workers.Clear();
            Save();
        }

        private void Save()
        {
            string jsondata = JsonSerializer.Serialize(Workers);
            File.WriteAllText(path, jsondata);
        }

        private void RezerveSave()
        {
            string jsondata = JsonSerializer.Serialize(Workers);
            File.WriteAllText(reservepath, jsondata);
        }

        private void Load()
        {
            if (File.Exists(path))
            {
                string jsondata = File.ReadAllText(path);
                Workers = JsonSerializer.Deserialize<ObservableCollection<Worker>>(jsondata);
            }
            else
            {
                Workers = new ObservableCollection<Worker>();
            }
        }
    }
}
