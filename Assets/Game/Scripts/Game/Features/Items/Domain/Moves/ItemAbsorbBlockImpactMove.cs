using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class ItemAbsorbBlockImpactMove : ItemMove
    {
        private readonly IBlockItemWielder _wielder;
        private readonly IBlockingItem _item;
        protected override float Duration => _item.BlockImpactAbsorptionAnimationDuration;

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