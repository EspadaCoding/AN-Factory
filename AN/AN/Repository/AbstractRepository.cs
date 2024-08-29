using AN.Service; 
using System;
using System.Collections.Generic;
using System.Text;

namespace AN.Repository
{
    public abstract class AbstractRepository<T> where T : class
    {
        protected ISaveLoad saveLoad;
        protected string sourcePath;
        protected List<T> data;
        public AbstractRepository(ISaveLoad saveLoad, string sourcePath)
        {
            this.saveLoad = saveLoad;
            this.sourcePath = sourcePath;
            this.data = saveLoad.Load<T>(sourcePath);
        }
        public abstract void Add(T item);
        public abstract void Remove(T item);
        public abstract List<T> GetAll();
    }
}
