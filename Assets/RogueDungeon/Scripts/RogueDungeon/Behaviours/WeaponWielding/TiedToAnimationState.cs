using System;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal abstract class TiedToAnimationState : TimerState
    {
        private readonly IAnimator _animator;
        protected abstract AnimationData Animation { get; }
        protected override float Duration => Animation.Duration;

        protected TiedToAnimationState(IAnimator animator) => 
            _animator = animator;

        public override void Enter()
        {
            base.Enter();
            _animator.OnEvent += OnEvent;
            _animator.Play(Animation);
        }

        public override void Exit()
        {
            base.Exit();
            _animator.OnEvent -= OnEvent;
        }

        protected virtual void OnEvent(string name)
        {
            throw new NotImplementedException();
        }
    }
}