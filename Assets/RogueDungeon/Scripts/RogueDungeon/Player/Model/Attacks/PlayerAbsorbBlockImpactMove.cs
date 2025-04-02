using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerAbsorbBlockImpactMove : PlayerMove
    {
        private readonly Player _player;

        protected PlayerAbsorbBlockImpactMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player) : base(config, animation, playerInput) => 
            _player = player;

        public override void Enter()
        {
            base.Enter();
            _player.HasUnabsorbedBlockImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.HasUnabsorbedBlockImpact;
    }
}