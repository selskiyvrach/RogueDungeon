namespace RogueDungeon.Player
{
    public class PlayerControlStateMediator
    {
        private readonly Player _player;
        private bool CanPerformAnyAction => _player.IsAlive;

        public bool IsDodging { private get; set; }
        public bool IsAttackInUncancellableState { private get; set; }
        public bool CanAttack => CanPerformAnyAction && !IsDodging;
        public bool CanDodge => CanPerformAnyAction && !IsAttackInUncancellableState;

        public PlayerControlStateMediator(Player player) => 
            _player = player;
    }
}