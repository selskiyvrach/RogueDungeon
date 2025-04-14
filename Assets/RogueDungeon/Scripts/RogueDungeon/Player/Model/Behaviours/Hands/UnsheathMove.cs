using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class UnsheathMove : PlayerMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.UnsheathDuration;

        public UnsheathMove(string id, IAnimation animation, IItem item) : base(id, animation)
        {
            _item = item;
        }
    }
}