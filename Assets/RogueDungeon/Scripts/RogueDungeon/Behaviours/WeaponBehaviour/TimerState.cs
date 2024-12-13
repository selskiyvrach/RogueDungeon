namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal abstract class TimerState : State
    {
        private float _timePassed;
        protected abstract float Duration { get; }
        public override void Enter() => 
            _timePassed = 0;
        protected bool IsTimerOff => _timePassed >= Duration;
        public override void Tick(float timeDelta) => 
            _timePassed += timeDelta;
    }
}