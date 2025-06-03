using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
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