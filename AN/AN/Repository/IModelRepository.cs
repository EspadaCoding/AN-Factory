using AN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Repository
{
    public interface IModelRepository
    {
        void AddModel(WatchModel watchModel);
        void RemoveModel(WatchModel watchModel);
        void RemoveAllModels();
        WatchModel GetModel(string ModelName);
        ObservableCollection<WatchModel> GetAllModels();
    }
}
