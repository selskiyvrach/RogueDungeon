using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class HandHeldIdle : PlayerMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.Config.IdleAnimationDuration;
        public HandHeldIdle(string id, IAnimation animation, IItem item) : base(id, animation) => 
            _item = item;
    }
}