using Common.Animations;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemAbsorbBlockImpactMove : PlayerMove
    {
        private readonly IBlockingItem _item;
        private readonly Player _player;
        protected override float Duration => ((BlockingItemConfig)_item.Config).BlockImpactAbsorptionDuration;

        protected ItemAbsorbBlockImpactMove(IBlockingItem item, IAnimation animation, Player player, string id) : base(id, animation)
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