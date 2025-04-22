using System;
using Common.Animations;
using Common.UtilsDotNet;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemHoldBlockMove : ItemMove
    {
        private readonly Player _player;
        private readonly IBlockingItem _item;
        protected override float Duration => ((BlockingItemConfig)_item.Config).HoldBlockAnimationDuration;
        protected override bool IsLooping => true;
        protected ItemHoldBlockMove(IAnimation animation, Player player, IBlockingItem item, string id,
            PlayerHandsBehaviour hands, IPlayerInput input) : base(id, animation, hands, input)
        {
            _player = player;
            _item = item.ThrowIfNull();
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