using RogueDungeon.Characters.Commands;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Items.WeaponWielder;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerControlStateMediator : ITickable
    {
        private readonly ICharacterCommands _characterCommands;
        private readonly IDodgeStateGetter _dodgeStateGetter;
        private readonly IIsAttackInUncancellableAnimationPhaseGetter _isAttackInUncancellableAnimationPhaseGetter;
        private readonly ICanDodgeSetter _canDodgeSetter;
        private readonly ICanAttackSetter _canAttackSetter;

        public PlayerControlStateMediator(IDodgeStateGetter dodgeStateGetter, IIsAttackInUncancellableAnimationPhaseGetter isAttackInUncancellableAnimationPhaseGetter, ICanDodgeSetter canDodgeSetter, ICanAttackSetter canAttackSetter, ICharacterCommands characterCommands)
        {
            _dodgeStateGetter = dodgeStateGetter;
            _isAttackInUncancellableAnimationPhaseGetter = isAttackInUncancellableAnimationPhaseGetter;
            _canDodgeSetter = canDodgeSetter;
            _canAttackSetter = canAttackSetter;
            _characterCommands = characterCommands;
        }

        [Inject]
        public void Tick()
        {
            _canAttackSetter.CanAttack = _dodgeStateGetter.DodgeState == DodgeState.None &&
                                         !_characterCommands.IsCurrentCommand<IDodgeLeftCommand>() &&
                                         !_characterCommands.IsCurrentCommand<IDodgeRightCommand>();
            
            _canDodgeSetter.CanDodge = !_isAttackInUncancellableAnimationPhaseGetter.IsAttackInUncancellableAnimationState;
        }
    }
}