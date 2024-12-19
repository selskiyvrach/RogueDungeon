using Common.Fsm;
using RogueDungeon.Items.Weapons;
using RogueDungeon.Player;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IAttackAttackTransitionDuration _duration;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IComboInfo _comboInfo;

        protected override float Duration => _duration.Value;

        public AttackToAttackTransitionState(IAttackAttackTransitionDuration duration, IControlState controlState, IComboCounter comboCounter, IComboInfo comboInfo)
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