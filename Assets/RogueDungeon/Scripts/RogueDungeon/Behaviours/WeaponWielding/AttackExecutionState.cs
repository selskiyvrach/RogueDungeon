using System;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Characters;
using RogueDungeon.Fsm;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInput;
using UnityEngine.Assertions;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackExecutionState : TiedToAnimationState
    {
        private readonly IParameters _durations;
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IInput _input;
        private readonly IControlState _controlState;

        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_LEFT, _durations.Get(ParameterKeys.ATTACK_EXECUTION_DURATION)),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_RIGHT, _durations.Get(ParameterKeys.ATTACK_EXECUTION_DURATION)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IParameters durations, IComboInfo comboInfo, IComboCounter comboCounter, IInput input, IControlState controlState) : 
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
            _controlState.IsAttackInHardPhase = true;
        }

        public override void Exit()
        {
            base.Exit();
            _controlState.IsAttackInHardPhase = false;
        }

        protected override void OnEvent(string name)
        {
            Assert.AreEqual(name, AnimationNames.HIT_EVENT);
            // run some logic
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if (_controlState.CanAttack() && _input.TryConsume(Input.Attack))
                stateChanger.To<AttackToAttackTransitionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}