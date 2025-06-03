using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class UnsheathMove : PlayerMove
    {
        private readonly IHandheldItem _item;
        protected override float Duration => _item.UnsheathDuration;

        public UnsheathMove(string id, IAnimation animation, IHandheldItem item) : base(id, animation) => 
            _item = item;
    }
}