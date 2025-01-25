using Common.Animations;
using Common.Fsm;
using UniRx;

namespace Common.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : TimerState, IExitableState, IAnimationEventObservable
    {
        private readonly IAnimator _animator;
        protected abstract AnimationData Animation { get; }
        protected override float Duration => Animation.Duration;
        ISubject<AnimationEvent> IAnimationEventObservable.OnEvent { get; } = new Subject<AnimationEvent>();

        protected BoundToAnimationState(IAnimator animator) => 
            _animator = animator;

        public override void Enter()
        {
            base.Enter();
            _animator.OnEvent += OnEvent;
            _animator.Play(Animation);
        }

        public virtual void Exit() => 
            _animator.OnEvent -= OnEvent;

        private void OnEvent(string name) => 
            ((IAnimationEventObservable)this).OnEvent.OnNext(new AnimationEvent(name));
    }
}