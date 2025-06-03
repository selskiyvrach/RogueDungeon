using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class ItemLowerBlockMove : ItemMove
    {
        private readonly IBlockItemWielder _wielder;
        private readonly IPlayerInput _input;
        private readonly IBlockingItem _item;
        private readonly InputUnit _blockKey;

        protected override float Duration => _item.LowerBlockAnimationDuration;

        protected ItemLowerBlockMove(IBlockingItem item, IAnimation animation, string id, IPlayerInput input, IBlockItemWielder wielder) : base(id, animation, wielder, input)
        {
            _item = item;
            _input = input;
            _wielder = wielder;
            _blockKey = _input.GetKey(InputKey.Block);
        }

        public override void Enter()
        {
            if(_wielder.BlockingItem == _item)
                _wielder.BlockingItem = null;
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            if (_wielder.IsDedicatedBlockItem(_item) && _blockKey.IsHeld)
                return false;
            
            if(_item is Shield && _input.GetKey(_wielder.GetInputKeyForItem(_item)).IsHeld)
                return false;
            
            return base.CanTransitionTo();
        }
    }
}