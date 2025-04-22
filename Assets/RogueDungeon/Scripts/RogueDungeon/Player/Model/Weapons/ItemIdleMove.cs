using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemIdleMove : ItemMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.Config.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public ItemIdleMove(IItem item, IAnimation animation, string id, PlayerHandsBehaviour hands, IPlayerInput input) : base(id, animation, hands, input) => 
            _item = item;
    }
}