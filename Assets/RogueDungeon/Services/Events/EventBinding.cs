using System;

namespace RogueDungeon.Services.Events
{
    public abstract class EventBinding : IDisposable
    {
        public abstract void Dispose();
    }

    public sealed class EventBinding<TEvent, THandler> : EventBinding where THandler : class, IEventHandler<TEvent>
    {
        private IEventBus _eventBus;
        private THandler _handler;

        public EventBinding(THandler handler, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _handler = handler;
            _eventBus.AddHandler(handler);
        }

        public override void Dispose()
        {
            _eventBus.RemoveHandler(_handler);
            _eventBus = null;
            _handler = null;
        }
    }
}