using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player;

namespace RogueDungeon.Weapons
{
    public class PlayerPrepareAttackMove : PlayerAttackBaseMove
    {
        private readonly Player.Player _player;
        private readonly IWeapon _weapon;

        protected PlayerPrepareAttackMove(PlayerPrepareAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator mediator, Player.Player player, IWeapon weapon) : base(config, animation, playerInput, mediator)
        {
            _player = player;
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();   
            _player.Stamina.Spend(_weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Stamina.CanAfford(_weapon.AttackStaminaCost);
    }
}