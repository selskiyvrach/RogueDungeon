using System;
using Common.Animations;
using Common.UtilsDotNet;
using Input;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class ItemHoldBlockMove : ItemMove
    {
        private readonly IBlockItemWielder _wielder;
        private readonly IBlockingItem _item;
        protected override float Duration => ((BlockingItemConfig)_item.Config).HoldBlockAnimationDuration;
        protected override bool IsLooping => true;
        protected ItemHoldBlockMove(IAnimation animation, IBlockItemWielder wielder, IBlockingItem item, string id, IPlayerInput input) : base(id, animation, wielder, input)
        {
            _wielder = wielder;
            _item = item.ThrowIfNull();
        }

        public override void Enter()
        {
            base.Enter();
            // if the blocking item is the same - probably means we're returning from absorb block impact move
            if (_wielder.BlockingItem != null && _wielder.BlockingItem != _item)
                throw new InvalidOperationException("Blocking item already exists");
            _wielder.BlockingItem = _item;
        }
    }
}