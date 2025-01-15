using System;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Characters.Commands;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Data.Weapons;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    internal class AttackExecutionState : BoundToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        private readonly ICharacterCommands _input;
        private readonly IIsAttackInUncancellableAnimationPhaseSetter _isAttackInUncancellableAnimationPhaseSetter;
        private readonly ICanAttackGetter _canAttackGetter;
        private readonly IParameter<IAttackExecutionDuration> _duration;
        private readonly IAttackHitEventHandler _hitEventHandler;

        protected override AnimationData Animation => _comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] switch
        {
            AttackDirection.BottomLeft => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_LEFT, _duration.Value),
            AttackDirection.BottomRight => new AnimationData(AnimationNames.ATTACK_TO_BOTTOM_RIGHT, _duration.Value),
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackExecutionState(IAnimator animator, IComboInfo comboInfo, IComboCounter comboCounter,
            ICharacterCommands input, IParameter<IAttackExecutionDuration> duration,
            IIsAttackInUncancellableAnimationPhaseSetter isAttackInUncancellableAnimationPhaseSetter,
            ICanAttackGetter canAttackGetter, IAttackHitEventHandler hitEventHandler) : 
            base(animator)
        {
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
            _input = input;
            _duration = duration;
            _isAttackInUncancellableAnimationPhaseSetter = isAttackInUncancellableAnimationPhaseSetter;
            _canAttackGetter = canAttackGetter;
            _hitEventHandler = hitEventHandler;
        }

        public override void Enter()
        {
            base.Enter();
            _isAttackInUncancellableAnimationPhaseSetter.IsAttackInUncancellableAnimationState = true;
        }

        public override void Exit()
        {
            base.Exit();
            _isAttackInUncancellableAnimationPhaseSetter.IsAttackInUncancellableAnimationState = false;
        }

        protected override void OnEvent(string name)
        {
            Assert.AreEqual(name, AnimationNames.HIT_EVENT);
            _hitEventHandler.HandleHit();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;
            if (_canAttackGetter.CanAttack && _input.TryConsume<IAttackCommand>())
                stateChanger.To<AttackToAttackTransitionState>();
            else if (_comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] == AttackDirection.BottomRight)
                stateChanger.To<AttackFinishState<RightDirection>>();
            else if (_comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] == AttackDirection.BottomLeft)
                stateChanger.To<AttackFinishState<LeftDirection>>();
            else
                throw new InvalidOperationException();
        }
    }
}