using Game.Libs.Input;
using Game.Libs.Movesets;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class RaiseMapMove : PlayerInputMove
    {
        private readonly Map _map;
        protected override float Duration => _map.RaiseMapDuration; 
        protected override InputKey RequiredKey => InputKey.UseRightHandItem;
        protected override RequiredState State => RequiredState.Down;
        public RaiseMapMove(string id, IAnimation animation, Map map, IPlayerInput input) : base(id, animation, input) => 
            _map = map;
    }
}