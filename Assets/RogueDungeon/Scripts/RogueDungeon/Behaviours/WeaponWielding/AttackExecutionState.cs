using System;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackExecutionState : TiedToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IInput _input;
        private readonly IControlState _controlState;

        protected override Animation Animation => _comboInfo.Directions[_comboCounter.Count] switch
        {
            AttackDirection.BottomLeft => Animation.AttackToBottomLeft,
            AttackDirection.BottomRight => Animation.AttackToBottomRight,
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IDurations durations, IComboInfo comboInfo, IComboCounter comboCounter, IInput input, IControlState controlState) : 
            base(animator, durations, WeaponWielding.Duration.Attack)
        {
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
            _input = input;
            _controlState = controlState;
        }

        public override void Enter()
        {
            base.Enter();
            _controlState.IsInUncancellableAnimation = true;
        }

        public override void Exit()
        {
            base.Exit();
            _controlState.IsInUncancellableAnimation = false;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if (_controlState.Is(AbleTo.StartAttack) && _input.TryConsume(Input.Attack))
            {
                _comboCounter.Count++;
                stateChanger.To<AttackToAttackTransitionState>();
            }
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}