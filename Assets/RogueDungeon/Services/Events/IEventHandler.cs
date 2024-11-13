namespace RogueDungeon.Services.Events
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent @event);
    }
}