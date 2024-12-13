using System;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackExecutionState : TiedToAnimationState
    {
        private readonly IDurations _durations;
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IInput _input;
        private readonly IControlState _controlState;

        protected override AnimationData Animation => _comboInfo.Directions[_comboCounter.Count] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_LEFT, _durations.Get(WeaponWielding.Duration.Attack)),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_RIGHT, _durations.Get(WeaponWielding.Duration.Attack)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IDurations durations, IComboInfo comboInfo, IComboCounter comboCounter, IInput input, IControlState controlState) : 
            base(animator)
        {
            _durations = durations;
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