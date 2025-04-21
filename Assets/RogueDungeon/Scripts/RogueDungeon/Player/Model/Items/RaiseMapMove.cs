using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Items
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