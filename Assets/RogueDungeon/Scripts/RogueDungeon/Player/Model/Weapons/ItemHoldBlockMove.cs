using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemHoldBlockMove : PlayerMove
    {
        private readonly Player _player;
        private readonly IItem _item;
        protected override float Duration => _item.Config.HoldBlockAnimationDuration;
        protected override bool IsLooping => true;
        protected ItemHoldBlockMove(IAnimation animation, Player player, IItem item, string id) : base(id, animation)
        {
            _player = player;
            _item = item;
        }

        public override void Enter()
        {
            base.Enter();
            _player.IsBlocking = true;
            _player.BlockingItem = _item;
        }

        public override void Exit()
        {
            base.Exit();
            _player.IsBlocking = false;
            _player.BlockingItem = null;
        }
    }
}