using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class ItemIdleMove : ItemMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.Config.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public ItemIdleMove(IItem item, IAnimation animation, string id, IItemTransitionsLockedProvider transitionsLockedProvider, IPlayerInput input) : base(id, animation, transitionsLockedProvider, input) => 
            _item = item;
    }
}