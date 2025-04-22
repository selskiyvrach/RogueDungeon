using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemTransitionBetweenAttacksMove : ItemMove
    {
        private readonly Player _player;
        private readonly PlayerHandsBehaviour _hands;
        private readonly IWeapon _weapon;
        protected override float Duration => ((WeaponConfig)_weapon.Config).TransitionBetweenAttacksDuration;

        protected override InputKey RequiredKey => _hands.ThisHand(_weapon) == _hands.RightHand 
            ? InputKey.UseRightHandItem 
            : InputKey.UseLeftHandItem;

        protected override RequiredState State => RequiredState.Down;

        public ItemTransitionBetweenAttacksMove(IWeapon weapon, IAnimation animation, IPlayerInput playerInput, string id, PlayerHandsBehaviour hands, Player player) : base(id, animation, hands, playerInput)
        {
            _weapon = weapon;
            _hands = hands;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.Stamina.AddDelta(- _weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Stamina.Current >= _weapon.AttackStaminaCost;
    }
}