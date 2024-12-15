using System;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Weapons;
using RogueDungeon.Parameters;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackFinishState : BoundToAnimationState
    {
        private readonly IParameters _durations;
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_LEFT, _durations.Get(ParameterKeys.ATTACK_IDLE_TRANSITION_DURATION)),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_RIGHT, _durations.Get(ParameterKeys.ATTACK_IDLE_TRANSITION_DURATION)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackFinishState(IAnimator animator, IParameters durations, IComboInfo comboInfo, IComboCounter comboCounter) 
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