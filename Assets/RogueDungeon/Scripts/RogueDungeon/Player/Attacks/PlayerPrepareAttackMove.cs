using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;

namespace RogueDungeon.Weapons
{
    public class PlayerPrepareAttackMove : PlayerAttackBaseMove
    {
        private readonly Player.Player _player;
        
        protected PlayerPrepareAttackMove(PlayerPrepareAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator mediator, Player.Player player) : base(config, animation, playerInput, mediator) =>
            _player = player;

        public override void Enter()
        {
            base.Enter();   
            _player.Stamina.Spend(12);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Stamina.Current > 12;
    }
}