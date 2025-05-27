using Common.Animations;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class HoldMapMove : PlayerMove
    {
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).HoldMapAnimationDuration;
        protected override bool IsLooping => true;

        public HoldMapMove(string id, Map map, IAnimation animation) : base(id, animation) => 
            _map = map;
    }
}