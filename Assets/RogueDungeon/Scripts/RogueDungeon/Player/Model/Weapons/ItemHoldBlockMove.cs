using System;
using Common.Animations;
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
            // if the blocking item is the same - probably means we're returningx from absorb block impact move
            if (_player.BlockingItem != null && _player.BlockingItem != _item)
                throw new InvalidOperationException("Blocking item already exists");
            _player.BlockingItem = _item;
        }
    }
}