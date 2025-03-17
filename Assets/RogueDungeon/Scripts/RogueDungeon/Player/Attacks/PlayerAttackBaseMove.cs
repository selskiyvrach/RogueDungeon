using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public abstract class PlayerAttackBaseMove : PlayerMove
    {
        private readonly PlayerAttackBaseMoveConfig _config;
        private readonly PlayerControlStateMediator _playerControlStateMediator;
        
        protected PlayerAttackBaseMove(PlayerAttackBaseMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerControlStateMediator playerControlStateMediator) : base(config, animation, playerInput)
        {
            _config = config;
            _playerControlStateMediator = playerControlStateMediator;
        }

        public override void Enter()
        {
            base.Enter();
            _playerControlStateMediator.IsAttackInUncancellableState = _config.IsUncancellable;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlStateMediator.IsAttackInUncancellableState = false;
        }
    }
}