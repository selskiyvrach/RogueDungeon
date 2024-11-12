using System;

namespace RogueDungeon.Services.Events
{
    public interface IEventBus : IEventBus<object>
    {
    }
    
    public interface IEventBus<TEventType>
    {
        void Subscribe<T>(Action<T> listener) where T : TEventType;
        void Unsubscribe<T>(Action<T> listener) where T : TEventType;
        void Fire<T>(T @event) where T : TEventType;
    }
}