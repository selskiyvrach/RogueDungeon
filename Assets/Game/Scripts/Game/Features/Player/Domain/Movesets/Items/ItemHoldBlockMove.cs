using System;
using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Animations;
using Libs.Utils.DotNet;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class ItemHoldBlockMove : ItemMove
    {
        private readonly IBlockItemWielder _wielder;
        private readonly IBlockingItem _item;
        protected override float Duration => _item.HoldBlockAnimationDuration;
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