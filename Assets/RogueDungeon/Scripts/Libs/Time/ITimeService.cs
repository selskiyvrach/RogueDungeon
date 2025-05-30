namespace Libs.Time
{
    public interface ITimeService
    {
        void Register(ITickable tickable);
        bool Unregister(ITickable tickable);
    }
}