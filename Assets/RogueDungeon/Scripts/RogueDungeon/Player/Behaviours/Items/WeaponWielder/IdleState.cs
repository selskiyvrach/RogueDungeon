using Common.Fsm;
using RogueDungeon.Characters.Commands;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    internal class IdleState : IState, IEnterableState
    {
        private readonly ICharacterCommands _weaponInput;
        private readonly ICanAttackGetter _canAttackGetter;
        private readonly IComboCounter _comboCounter;

        public IdleState(ICharacterCommands weaponInput, ICanAttackGetter canAttackGetter, IComboCounter comboCounter)
        {
            _weaponInput = weaponInput;
            _canAttackGetter = canAttackGetter;
            _comboCounter = comboCounter;
        }

        public void Enter() => 
            _comboCounter.AttackIndex = -1;

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if(_canAttackGetter.CanAttack && _weaponInput.TryConsume<IAttackCommand>())
                stateChanger.To<AttackPrepareState>();
        }
    }
}