using System;
using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;
using RogueDungeon.Items;
using RogueDungeon.Items.Weapons;
using RogueDungeon.Player;
using RogueDungeon.Player.Input;
using UnityEngine.Assertions;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackExecutionState : BoundToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IInput _input;
        private readonly IControlState _controlState;
        private readonly IAttackExecutionDuration _duration;

        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_LEFT, _duration.Value),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_RIGHT, _duration.Value),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IComboInfo comboInfo, IComboCounter comboCounter, IInput input, IControlState controlState, IAttackExecutionDuration duration) : 
            base(animator)
        {
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
            _input = input;
            _controlState = controlState;
            _duration = duration;
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