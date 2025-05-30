namespace Libs.Movesets
{
    public interface IAnimationEventHandler
    {
        string EventName { get; }
        void HandleEvent();
    }
}