using Common.Animations;
using Input;
using Moves;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class RaiseMapMove : PlayerInputMove
    {
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).RaiseMapDuration; 
        protected override InputKey RequiredKey => InputKey.UseRightHandItem;
        protected override RequiredState State => RequiredState.Down;
        public RaiseMapMove(string id, IAnimation animation, Map map, IPlayerInput input) : base(id, animation, input) => 
            _map = map;
    }
}