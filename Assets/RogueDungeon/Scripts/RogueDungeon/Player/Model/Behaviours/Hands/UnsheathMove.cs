using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class UnsheathMove : PlayerMove
    {
        private readonly IHandheldItem _item;
        protected override float Duration => _item.UnsheathDuration;

        public UnsheathMove(string id, IAnimation animation, IHandheldItem item) : base(id, animation) => 
            _item = item;
    }
}