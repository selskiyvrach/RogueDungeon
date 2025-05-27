using Common.Animations;
using Input;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class ItemAbsorbBlockImpactMove : ItemMove
    {
        private readonly IBlockItemWielder _wielder;
        private readonly IBlockingItem _item;
        protected override float Duration => ((BlockingItemConfig)_item.Config).BlockImpactAbsorptionDuration;

        protected ItemAbsorbBlockImpactMove(IBlockingItem item, IAnimation animation, IBlockItemWielder wielder,
            string id, IPlayerInput input) : base(id, animation, wielder, input)
        {
            _item = item;
            _wielder = wielder;
        }

        public override void Enter()
        {
            base.Enter();
            _wielder.HasUnabsorbedBlockImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _wielder.HasUnabsorbedBlockImpact;
    }
}