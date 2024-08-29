using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Message
{
    public class SendModelListMessage<T> : IMessage
    {
        public ObservableCollection<T> List { get; set; }

        
    }
}
