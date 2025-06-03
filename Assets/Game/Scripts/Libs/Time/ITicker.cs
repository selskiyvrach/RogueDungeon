namespace Libs.Time
{
    public interface ITicker
    {
        void Register(ITickable tickable);
        bool Unregister(ITickable tickable);
    }
}