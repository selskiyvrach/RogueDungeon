using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerAbsorbBlockImpactMove : PlayerMove
    {
        private readonly PlayerBlockerHandler _blocker;

        protected PlayerAbsorbBlockImpactMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerBlockerHandler blocker) : base(config, animation, playerInput) => 
            _blocker = blocker;

        public override void Enter()
        {
            base.Enter();
            _blocker.HasUnabsorbedImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _blocker.HasUnabsorbedImpact;
    }
}