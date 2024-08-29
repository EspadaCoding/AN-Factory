using AN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Message
{
    public class ModelMessage : IMessage
    {
        public WatchModel Model { get; set; }
    }
}
