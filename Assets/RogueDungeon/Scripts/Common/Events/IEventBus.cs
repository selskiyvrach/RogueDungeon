using System;

namespace Common.Events
{
    public interface IEventBus : IEventBus<object>
    {
    }
    
    public interface IEventBus<TEventType> : IDisposable
    {
        void AddHandler<T>(IEventHandler<T> listener) where T : TEventType;
        void RemoveHandler<T>(IEventHandler<T> listener) where T : TEventType;
        void AddHandler<T>(Action<T> listener) where T : TEventType;
        void RemoveHandler<T>(Action<T> listener) where T : TEventType;
        void Fire<T>(T @event) where T : TEventType;
    }
}