using Libs.Lifecycle;

namespace Game.Libs.Time
{
    public interface IGameTime
    {
        void StartTicking(ITickable ticker, TickOrder order);
        void StopTicking(ITickable ticker);
    }
}