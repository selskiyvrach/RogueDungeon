using Common.Animations;
using RogueDungeon.Combat;
using RogueDungeon.Input;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMove : AttackMove
    {
        private readonly PlayerAttackMoveConfig _config;
        private readonly IPlayerInput _input;
        
        public PlayerAttackMove(IPlayerInput input, PlayerAttackMoveConfig config, IAnimator animator,
            IAttacksMediator mediator) : base(config, animator, mediator)
        {
            _config = config;
            _input = input;
        }

        protected override void HandleAttack(IAttacksMediator attacksMediator) => 
            attacksMediator.MediatePlayerAttack(_config.Damage);

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