using Common.Animations;
using Input;

namespace RogueDungeon.Items.Model.Moves
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