using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerPrepareAttackMove : PlayerAttackBaseMove
    {
        private readonly Player _player;
        private readonly IWeapon _weapon;

        protected PlayerPrepareAttackMove(PlayerPrepareAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator mediator, Player player, IWeapon weapon) : base(config, animation, playerInput, mediator)
        {
            _player = player;
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();   
            _player.Stamina.AddDelta(-_weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Stamina.Current >= _weapon.AttackStaminaCost;
    }
}