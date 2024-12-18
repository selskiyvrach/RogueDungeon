using System;
using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    internal class AttackFinishState : BoundToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IAttackIdleTransitionDuration _duration;
        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_LEFT, _duration.Value),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_RIGHT, _duration.Value),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackFinishState(IAnimator animator, IComboInfo comboInfo, IComboCounter comboCounter, IAttackIdleTransitionDuration duration) 
            : base(animator)
        {
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
            _duration = duration;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(IsTimerOff)
                stateChanger.To<IdleState>();
        }
    }
}