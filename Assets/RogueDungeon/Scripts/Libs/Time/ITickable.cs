namespace Libs.Time
{
    public interface ITickable
    {
        int TickOrder { get; }
        void Tick(float deltaTime);
    }
}