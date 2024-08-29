using System;
using System.Collections.Generic;
using System.Text;

namespace AN.Service
{
    public interface ISaveLoad
    {
        void Save<T>(string filePath, List<T> data);
        List<T> Load<T>(string filePath);
    }
}
