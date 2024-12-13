using System;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal class AttackFinishState : TiedToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        protected override Animation Animation => _comboInfo.Directions[_comboCounter.Count] switch
        {
            AttackDirection.BottomLeft => Animation.FinishFromBottomLeftAttack,
            AttackDirection.BottomRight => Animation.FinishFromBottomRightAttack,
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackFinishState(IAnimator animator, IDurations durations, IComboInfo comboInfo, IComboCounter comboCounter) 
            : base(animator, durations, WeaponBehaviour.Duration.AttackIdleTransition)
        {
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