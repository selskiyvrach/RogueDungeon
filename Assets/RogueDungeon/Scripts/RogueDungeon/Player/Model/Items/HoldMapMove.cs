using Common.Animations;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Items
{
    public class HoldMapMove : PlayerMove
    {
        private readonly Player.Model.Player _player;
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).HoldMapAnimationDuration;
        protected override bool IsLooping => true;

        public HoldMapMove(string id, Map map, IAnimation animation, Player.Model.Player player) : base(id, animation)
        {
            _map = map;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.IsHoldingBreath = true;
        }

        public override void Exit()
        {
            base.Exit();
            _player.IsHoldingBreath = false;
        }
    }
}