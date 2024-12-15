using System;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Fsm
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
            _animator.OnEvent += OnEvent;
            _animator.Play(Animation);
        }

        public virtual void Exit() => 
            _animator.OnEvent -= OnEvent;

        protected virtual void OnEvent(string name)
        {
            throw new NotImplementedException();
        }
    }
}