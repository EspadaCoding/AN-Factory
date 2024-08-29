using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AN.Models;

namespace AN.Repository
{
    public interface IWorkerRepository
    {
        void AddWorker(Worker worker);
        void RemoveWorker(Worker worker);
        void RemoveAllWorkers();
        Worker GetWorker(string Name);
        ObservableCollection<Worker> GetAllWorkers();
    }
}
