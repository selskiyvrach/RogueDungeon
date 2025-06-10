using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Items;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class SheathMove : PlayerMove
    {
        private readonly IHandheldItem _item;
        private readonly IItemSwapper _swapper;
        protected override float Duration => _item.UnsheathDuration;
        
        public SheathMove(IItemSwapper swapper, IAnimation animation, string id, IHandheldItem item) : base(id, animation)
        {
            _swapper = swapper;
            _item = item;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if(IsFinished && _swapper.CurrentItem == _item)
                _swapper.CurrentItem = null;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _swapper.CurrentItem != null && _swapper.CurrentItem != _swapper.IntendedItem && _swapper.CanSheathCurrentItem;
    }
}