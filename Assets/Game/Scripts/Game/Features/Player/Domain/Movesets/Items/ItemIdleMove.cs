using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class ItemIdleMove : ItemMove
    {
        private readonly IHandheldItem _item;
        protected override float Duration => _item.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public ItemIdleMove(IHandheldItem item, IAnimation animation, string id, IItemTransitionsLockedProvider transitionsLockedProvider, IPlayerInput input) : base(id, animation, transitionsLockedProvider, input) => 
            _item = item;
    }
}