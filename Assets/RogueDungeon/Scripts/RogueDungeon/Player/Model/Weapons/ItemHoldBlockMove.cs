using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemHoldBlockMove : PlayerMove
    {
        private readonly IPlayerInput _playerInput;
        private readonly Player _player;
        private readonly IItem _item;
        protected override float Duration => _item.Config.HoldBlockAnimationDuration;
        protected override bool IsLooping => true;
        protected ItemHoldBlockMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player, IItem item) : base(ItemConfig.Names.BLOCK_HOLD, animation)
        {
            _playerInput = playerInput;
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

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerInput.HasInput(InputKey.Block);
    }
}