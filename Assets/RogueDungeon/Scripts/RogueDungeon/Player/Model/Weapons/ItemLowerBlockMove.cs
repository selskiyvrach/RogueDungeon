using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemLowerBlockMove : ItemMove
    {
        private readonly Player _player;
        private readonly IPlayerInput _input;
        private readonly PlayerHandsBehaviour _hands;
        private readonly IItem _item;
        private readonly InputUnit _blockKey;

        protected override float Duration => ((BlockingItemConfig)_item.Config).LowerBlockDuration;

        protected ItemLowerBlockMove(IItem item, IAnimation animation, string id, PlayerHandsBehaviour hands, IPlayerInput input, Player player) : base(id, animation, hands, input)
        {
            _item = item;
            _hands = hands;
            _input = input;
            _player = player;
            _blockKey = _input.GetKey(InputKey.Block);
        }

        public override void Enter()
        {
            if(_player.BlockingItem == _item)
                _player.BlockingItem = null;
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            if (_hands.IsDedicatedToBlock(_item) && _blockKey.IsHeld)
                return false;
            
            if(_item is Shield && _input.GetKey(_hands.UseItemInput(_item)).IsHeld)
                return false;
            
            return base.CanTransitionTo();
        }
    }
}