namespace Game.Features.Player.Domain
{
    public class PlayerControlStateMediator
    {
        private readonly PlayerModel _player;
        private bool CanPerformAnyAction => _player.IsAlive;
        public bool IsDodging => _player.DodgeState != PlayerDodgeState.None;
        public bool IsAttackInUncancellableState { private get; set; }
        public bool CanAttack => CanPerformAnyAction && !IsDodging;
        public bool CanDodge => CanPerformAnyAction && !IsAttackInUncancellableState;

        public PlayerControlStateMediator(PlayerModel player) => 
            _player = player;
    }
}