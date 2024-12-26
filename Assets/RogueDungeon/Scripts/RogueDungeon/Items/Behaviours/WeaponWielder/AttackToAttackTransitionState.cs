using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IParameter<IAttackAttackTransitionDuration> _duration;
        private readonly IWeaponControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IComboInfo _comboInfo;

        protected override float Duration => _duration.Value;

        public AttackToAttackTransitionState(IParameter<IAttackAttackTransitionDuration> duration, IWeaponControlState controlState, IComboCounter comboCounter, IComboInfo comboInfo)
        {
            _duration = duration;
            _controlState = controlState;
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
            if(!IsTimerOff)
                return;
            if (_controlState.CanAttack())
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}