using System;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackFinishState : TiedToAnimationState
    {
        private readonly IDurations _durations;
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        protected override AnimationData Animation => _comboInfo.Directions[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_LEFT, _durations.Get(WeaponWielding.Duration.AttackIdleTransition)),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_RIGHT, _durations.Get(WeaponWielding.Duration.AttackIdleTransition)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackFinishState(IAnimator animator, IDurations durations, IComboInfo comboInfo, IComboCounter comboCounter) 
            : base(animator)
        {
            _durations = durations;
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(IsTimerOff)
                stateChanger.To<IdleState>();
        }
    }
}