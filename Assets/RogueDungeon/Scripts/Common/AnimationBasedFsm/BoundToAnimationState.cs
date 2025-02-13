using Common.Animations;
using Common.Fsm;

namespace Common.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : TimerState, IExitableState
    {
        private readonly IAnimator _animator;
        protected abstract AnimationData Animation { get; }
        protected override float Duration => Animation.Duration;

        protected BoundToAnimationState(IAnimator animator) => 
            _animator = animator;

        public override void Enter()
        {
            base.Enter();
            _animator.OnEvent += OnAnimationEvent;
            _animator.Play(Animation);
        }

        public virtual void Exit() => 
            _animator.OnEvent -= OnAnimationEvent;

        protected virtual void OnAnimationEvent(string name)
        {
        }
    }
}