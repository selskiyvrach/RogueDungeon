namespace Common.Events
{
    public interface IEventHandler<TEvent>
    {
        void HandleEvent(TEvent @event);
    }
}