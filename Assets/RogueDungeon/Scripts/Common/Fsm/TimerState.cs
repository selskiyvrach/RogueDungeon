namespace Common.Fsm
{
    public abstract class TimerState : State
    {
        private float _timePassed;
        protected abstract float Duration { get; }
        protected bool IsTimerOff => _timePassed >= Duration;

        public override void Enter() => 
            _timePassed = 0;

        public override void Tick(float timeDelta) => 
            _timePassed += timeDelta;
    }
}