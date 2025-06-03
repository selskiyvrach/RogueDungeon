using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class HoldMapMove : PlayerMove
    {
        private readonly Map _map;
        protected override float Duration => _map.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public HoldMapMove(string id, Map map, IAnimation animation) : base(id, animation) => 
            _map = map;
    }
}