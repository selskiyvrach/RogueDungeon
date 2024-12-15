namespace Common.Fsm
{
    public abstract class TimerState : IState, ITickableState
    {
        private float _timePassed;
        protected abstract float Duration { get; }
        protected bool IsTimerOff => _timePassed >= Duration;

        public virtual void Enter() => 
            _timePassed = 0;

        public void Tick(float timeDelta) => 
            _timePassed += timeDelta;

        public abstract void CheckTransitions(IStateChanger stateChanger);
    }
}