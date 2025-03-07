using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMove : AttackMove
    {
        private readonly PlayerAttackMoveConfig _config;
        private readonly IPlayerInput _input;
        
        public PlayerAttackMove(IPlayerInput input, PlayerAttackMoveConfig config, IAnimation animation,
            IPlayerAttacksMediator mediator) : base(config, animation, mediator)
        {
            _config = config;
            _input = input;
        }

        protected override void HandleAttack(IPlayerAttacksMediator playerAttacksMediator) => 
            playerAttacksMediator.MediatePlayerAttack(_config.Damage);

        public override void Enter()
        {
            base.Enter();
            if(_config.RequiredInput != InputKey.None)
                _input.ConsumeInput(_config.RequiredInput);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_config.RequiredInput == InputKey.None || _input.HasInput(_config.RequiredInput));
    }
}