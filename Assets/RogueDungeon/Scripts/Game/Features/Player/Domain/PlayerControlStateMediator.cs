namespace Game.Features.Player.Domain
{
    public class PlayerControlStateMediator
    {
        private readonly Player _player;
        private bool CanPerformAnyAction => _player.IsAlive;
        public bool IsDodging => _player.DodgeState != DodgeState.None;
        public bool IsAttackInUncancellableState { private get; set; }
        public bool CanAttack => CanPerformAnyAction && !IsDodging;
        public bool CanDodge => CanPerformAnyAction && !IsAttackInUncancellableState;

        public PlayerControlStateMediator(Player player) => 
            _player = player;
    }
}