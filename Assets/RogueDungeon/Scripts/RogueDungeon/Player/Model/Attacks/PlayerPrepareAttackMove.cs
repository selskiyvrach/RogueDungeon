using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerPrepareAttackMove : PlayerMove
    {
        private readonly PlayerHandsBehaviour _hands;
        private readonly Player _player;
        private readonly IWeapon _weapon;

        protected override InputKey RequiredKey => _hands.ThisHand(_weapon) == _hands.RightHand ? InputKey.UseRightHandItem : InputKey.UseLeftHandItem;
        
        protected PlayerPrepareAttackMove(PlayerPrepareAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player, IWeapon weapon, PlayerHandsBehaviour hands) 
            : base(config, animation, playerInput)
        {
            _player = player;
            _weapon = weapon;
            _hands = hands;
        }

        public override void Enter()
        {
            base.Enter();   
            _player.Stamina.AddDelta(-_weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Stamina.Current >= _weapon.AttackStaminaCost && (_player.Hands.IsDoubleGrip || _player.Hands.OppositeHand(_weapon).IsIdle);
    }
}