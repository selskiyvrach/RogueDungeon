using RogueDungeon.Entities.Properties;

namespace Common.FSM
{
    public class TimerState : State, IFinishableState
    {
        private readonly Timer _timer;
        public bool IsFinished => _timer.IsFinished;

        public TimerState(float duration)
        {
            _timer = new Timer(duration);
            AddEnterHandler(_timer.Start);
            AddExitHandler(_timer.Stop);
        }
        
        public TimerState(IReadOnlyProperty<float> duration)
        {
            _timer = new Timer();
            AddEnterHandler(() => _timer.Start(duration.Value));
            AddExitHandler(_timer.Stop);
        }
    }
}