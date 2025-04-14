using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemIdleMove : PlayerMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.Config.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public ItemIdleMove(IItem item, IAnimation animation, string id) : base(id, animation) => 
            _item = item;
    }
}