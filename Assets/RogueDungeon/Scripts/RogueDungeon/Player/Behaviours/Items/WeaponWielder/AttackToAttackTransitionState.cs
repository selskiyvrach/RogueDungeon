using System;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IParameter<IAttackAttackTransitionDuration> _duration;
        private readonly ICanAttackGetter _canAttackGetter;
        private readonly IComboCounter _comboCounter;
        private readonly IComboInfo _comboInfo;

        protected override float Duration => _duration.Value;

        public AttackToAttackTransitionState(IParameter<IAttackAttackTransitionDuration> duration, ICanAttackGetter canAttackGetter, IComboCounter comboCounter, IComboInfo comboInfo)
        {
            _duration = duration;
            _canAttackGetter = canAttackGetter;
            _comboCounter = comboCounter;
            _comboInfo = comboInfo;
        }

        public override void Enter()
        {
            _comboCounter.AttackIndex = ++_comboCounter.AttackIndex % _comboInfo.AttackDirectionsInCombo.Length;
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;
            if (_canAttackGetter.CanAttack)
                stateChanger.To<AttackExecutionState>();
            else if (_comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] == AttackDirection.BottomRight)
                stateChanger.To<AttackFinishState<RightDirection>>();
            else if (_comboInfo.AttackDirectionsInCombo[_comboCounter.AttackIndex] == AttackDirection.BottomLeft)
                stateChanger.To<AttackFinishState<LeftDirection>>();
            else
                throw new InvalidOperationException();
        }
    }
}