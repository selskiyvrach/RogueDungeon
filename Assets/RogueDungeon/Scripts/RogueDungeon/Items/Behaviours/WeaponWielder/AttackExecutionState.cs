using System;
using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Data.Weapons;
using UnityEngine.Assertions;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    internal class AttackExecutionState : BoundToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly IWeaponInput _input;
        private readonly IWeaponControlState _controlState;
        private readonly IAttackExecutionDuration _duration;

        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_LEFT, _duration.Value),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_RIGHT, _duration.Value),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IComboInfo comboInfo, IComboCounter comboCounter, IWeaponInput input, IWeaponControlState controlState, IAttackExecutionDuration duration) : 
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
            if (_controlState.CanAttack() && _input.TryConsume(WeaponInputCommand.Attack))
                stateChanger.To<AttackToAttackTransitionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}