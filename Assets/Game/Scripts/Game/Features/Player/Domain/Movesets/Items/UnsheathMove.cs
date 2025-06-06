using Game.Libs.Items;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class UnsheathMove : PlayerMove
    {
        private readonly IHandheldItem _item;
        protected override float Duration => _item.UnsheathDuration;

        public UnsheathMove(string id, IAnimation animation, IHandheldItem item) : base(id, animation) => 
            _item = item;
    }
}