using System;
using System.Collections.Generic;
using System.Text;

namespace AN.Message
{
    public class NavigationMessage : IMessage
    {
        public Type ViewModelType { get; set; }
    }
}
