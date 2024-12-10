using System;

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
            AddExitHandler(_timer.Cancel);
        }
        
        public TimerState(Func<float> duration)
        {
            _timer = new Timer();
            AddEnterHandler(()=> _timer.Start(duration.Invoke()));
            AddExitHandler(_timer.Cancel);
        }
    }
}