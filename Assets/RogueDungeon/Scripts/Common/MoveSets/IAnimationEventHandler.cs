namespace Common.MoveSets
{
    public interface IAnimationEventHandler
    {
        string EventName { get; }
        void HandleEvent();
    }
}