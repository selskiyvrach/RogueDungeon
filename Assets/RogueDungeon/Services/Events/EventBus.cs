using System;
using System.Collections.Generic;
using RogueDungeon.Services.Extensions;

namespace RogueDungeon.Services.Events
{
    public class EventBus : EventBus<object>
    {
    }
    
    public class EventBus<TEventType> : IEventBus<TEventType>
    {
        private readonly Dictionary<Type, object> _listeners = new();

        public void AddHandler<T>(IEventHandler<T> listener) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var existingListeners))
            {
                existingListeners = new List<Action<T>>();
                _listeners[typeof(T)] = existingListeners;
            }
            ((List<IEventHandler<T>>)existingListeners).Add(listener);
        }

        public void RemoveHandler<T>(IEventHandler<T> listener) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var existingListeners))
                return;
            
            var listeners = (List<IEventHandler<T>>)existingListeners;
            var listenerIndex = listeners.IndexOf(listener);
            if(!listeners.OutOfBounds(listenerIndex))
                listeners[listenerIndex] = null;
        }

        public void Fire<T>(T @event) where T : TEventType
        {
            if (!_listeners.TryGetValue(typeof(T), out var listenersObj)) 
                return;
            
            var listeners = (List<IEventHandler<T>>)listenersObj;
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                if(listeners[i] == null)
                    listeners.RemoveAt(i);
                else
                    listeners[i].Handle(@event);
            }
        }

        public void Dispose() => 
            _listeners.Clear();
    }
}