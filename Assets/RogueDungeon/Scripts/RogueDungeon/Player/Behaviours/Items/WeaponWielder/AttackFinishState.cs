using System;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    internal class AttackFinishState<T> : BoundToAnimationState where T : IAttackDirection
    {
        private readonly IParameter<IAttackIdleTransitionDuration> _duration;
        protected override AnimationData Animation => new(typeof(T) == typeof(LeftDirection) 
                ? AnimationNames.ATTACK_FINISH_FROM_BOTTOM_LEFT 
                : typeof(T) == typeof(RightDirection) 
                    ? AnimationNames.ATTACK_FINISH_FROM_BOTTOM_RIGHT 
                    : throw new ArgumentOutOfRangeException(), 
            _duration.Value);

        public AttackFinishState(IAnimator animator, IParameter<IAttackIdleTransitionDuration> duration) 
            : base(animator) =>
            _duration = duration;

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(IsTimerOff)
                stateChanger.To<IdleState>();
        }
    }

    internal interface IAttackDirection
    {
        
    }

    internal class RightDirection : IAttackDirection
    {
        
    }

    internal class LeftDirection : IAttackDirection
    {
        
    }
}