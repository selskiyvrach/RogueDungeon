using System;
using System.Collections.Generic;
using RogueDungeon.Utils;

namespace RogueDungeon.Services.Events
{
    public class EventBus : EventBus<object>
    {
    }
    
    public class EventBus<TEventType> : IEventBus<TEventType>
    {
        private readonly Dictionary<Type, object> _listeners = new();

        public void Subscribe<T>(Action<T> listener) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var existingListeners))
            {
                existingListeners = new List<Action<T>>();
                _listeners[typeof(T)] = existingListeners;
            }
            ((List<Action<T>>)existingListeners).Add(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var existingListeners))
                return;
            
            var listeners = (List<Action<T>>)existingListeners;
            var listenerIndex = listeners.IndexOf(listener);
            if(!listeners.IndexOutOfBounds(listenerIndex))
                listeners[listenerIndex] = null;
        }

        public void Fire<T>(T @event) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var listenersObj)) 
                return;
            
            var listeners = (List<Action<T>>)listenersObj;
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                if(listeners[i] == null)
                    listeners.RemoveAt(i);
                else
                    listeners[i].Invoke(@event);
            }
        }
    }
}