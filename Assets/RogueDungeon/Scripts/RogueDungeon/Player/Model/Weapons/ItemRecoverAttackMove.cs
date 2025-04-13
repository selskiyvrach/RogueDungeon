using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemRecoverAttackMove : PlayerMove
    {
        private readonly IWeapon _weapon;
        private readonly PlayerControlStateMediator _controlStateMediator;
        protected override float Duration => ((WeaponConfig)_weapon.Config).AttackRecoveryDuration;

        protected ItemRecoverAttackMove(IWeapon weapon, PlayerFinishAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput, 
            PlayerControlStateMediator controlStateMediator) : base(config, animation, playerInput)
        {
            _weapon = weapon;
            _controlStateMediator = controlStateMediator;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() || !_controlStateMediator.CanAttack;
    }
}