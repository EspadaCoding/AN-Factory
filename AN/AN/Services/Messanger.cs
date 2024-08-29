﻿using AN.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace AN.Services
{
    public class Messanger : IMessanger
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscriptions = new();

        public void Send<T>(T message) where T : IMessage
        {
            Type type = typeof(T);
            if(_subscriptions.ContainsKey(type))
            {
                foreach(Action<object> action in _subscriptions[type])
                {
                    action.Invoke(message);
                }
            }
        }

        public void Subcribe<T>(Action<object> action) where T : IMessage
        {
            Type type = typeof(T);
            if(!_subscriptions.ContainsKey(type))
            {
                _subscriptions[type] = new List<Action<object>>();
            }
            _subscriptions[type].Add(action);
        }
    }
}
