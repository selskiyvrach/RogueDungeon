using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemAbsorbBlockImpactMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly Player _player;

        protected override float Duration => _item.Config.BlockImpactAbsorptionDuration;

        protected ItemAbsorbBlockImpactMove(IItem item, PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player) : base(config, animation, playerInput)
        {
            _item = item;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.HasUnabsorbedBlockImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.HasUnabsorbedBlockImpact;
    }
}