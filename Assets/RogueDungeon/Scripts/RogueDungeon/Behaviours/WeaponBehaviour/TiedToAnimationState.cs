namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal abstract class TiedToAnimationState : TimerState
    {
        private readonly IAnimator _animator;
        private readonly IDurations _durations;
        private readonly Duration _duration;

        protected abstract Animation Animation { get; }
        protected override float Duration => _durations.Get(_duration);

        protected TiedToAnimationState(IAnimator animator, IDurations durations, Duration duration)
        {
            _animator = animator;
            _durations = durations;
            _duration = duration;
        }

        public override void Enter()
        {
            base.Enter();
            _animator.Play(new PlayOptions(Animation, Duration));
        }
    }
}