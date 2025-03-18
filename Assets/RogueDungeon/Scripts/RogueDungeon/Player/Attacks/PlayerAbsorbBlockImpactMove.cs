using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerAbsorbBlockImpactMove : PlayerMove
    {
        private readonly Player.Player _player;

        protected PlayerAbsorbBlockImpactMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player.Player player) : base(config, animation, playerInput) => 
            _player = player;

        public override void Enter()
        {
            base.Enter();
            _player.BlockerHandler.HasUnabsorbedImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.BlockerHandler.HasUnabsorbedImpact;
    }
}