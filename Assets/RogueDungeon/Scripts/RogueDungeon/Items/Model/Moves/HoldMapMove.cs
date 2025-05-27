using Common.Animations;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class HoldMapMove : PlayerMove
    {
        private readonly IItemWielder _wielder;
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).HoldMapAnimationDuration;
        protected override bool IsLooping => true;

        public HoldMapMove(string id, Map map, IAnimation animation, IItemWielder wielder) : base(id, animation)
        {
            _map = map;
            _wielder = wielder;
        }

        public override void Enter()
        {
            base.Enter();
            _wielder.IsHoldingBreath = true;
        }

        public override void Exit()
        {
            base.Exit();
            _wielder.IsHoldingBreath = false;
        }
    }
}