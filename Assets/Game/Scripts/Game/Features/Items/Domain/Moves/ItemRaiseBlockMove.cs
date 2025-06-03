using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class ItemRaiseBlockMove : ItemMove
    {
        private readonly IBlockingItem _item;
        private readonly IBlockItemWielder _wielder;
        private readonly IPlayerInput _input;
        private readonly InputUnit _blockKey;

        protected override float Duration => _item.RaiseBlockAnimationDuration;

        protected ItemRaiseBlockMove(IAnimation animation, IBlockItemWielder wielder,
            IBlockingItem item, string id, IPlayerInput input) : base(id, animation, wielder, input)
        {
            _wielder = wielder;
            _item = item;
            _input = input;
            _blockKey = _input.GetKey(InputKey.Block);
        }

        public override void Enter()
        {
            _blockKey.Reset();
            var itemKey = _input.GetKey(_wielder.GetInputKeyForItem(_item));
            if(_item is Shield && itemKey.IsDown)
                itemKey.Reset();
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            var itemKey = _input.GetKey(_wielder.GetInputKeyForItem(_item));
            var isShieldWithUseItemInput = _item is Shield && (itemKey.IsDown || itemKey.IsHeld);
            var isDedicatedBlockerWithBlockInput = _wielder.IsDedicatedBlockItem(_item) && (_blockKey.IsDown || _blockKey.IsHeld);
            return  base.CanTransitionTo() && (isShieldWithUseItemInput || isDedicatedBlockerWithBlockInput);
        }
    }
}