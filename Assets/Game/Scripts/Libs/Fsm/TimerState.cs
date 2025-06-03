using Libs.Lifecycle;

namespace Libs.Fsm
{
    public abstract class TimerState : IState, ITickable, IEnterableState
    {
        private float _timePassed;
        protected abstract float Duration { get; }
        protected bool IsFinished => _timePassed >= Duration;

        public virtual void Enter() => 
            _timePassed = 0;

        public virtual void Tick(float timeDelta) => 
            _timePassed += timeDelta;
    }
}