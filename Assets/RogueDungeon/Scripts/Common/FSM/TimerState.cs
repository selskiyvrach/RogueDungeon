namespace Common.FSM
{
    public class TimerState : State, IFinishableState
    {
        private readonly float _duration;
        private readonly Timer _timer;
        public bool IsFinished => _timer.IsFinished;

        public TimerState(float duration)
        {
            _duration = duration;
            _timer = new Timer(duration);
            AddEnterHandler(_timer.Start);
            AddExitHandler(_timer.Stop);
        }
    }
}