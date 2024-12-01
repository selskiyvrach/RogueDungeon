using Common.Properties;

namespace RogueDungeon.Player
{
    public class CharacterControlStateResolver
    {
        private readonly IReadOnlyProperty<AttackState> _attackState;
        private readonly IReadOnlyProperty<DodgeState> _dodgeState;

        public CharacterControlStateResolver(IReadOnlyProperty<AttackState> attackState, IReadOnlyProperty<DodgeState> dodgeState)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
        }

        private bool NotDodging => _dodgeState.Value == DodgeState.None;
        private bool NotExecutingAttack => _attackState.Value != AttackState.Executing;
        public bool CanStartHardHandsAnim() => NotDodging;
        public bool CanStartSoftHandsAnim() => NotDodging;
        public bool CanStartHardMovementAnim() => NotExecutingAttack;
    }
}