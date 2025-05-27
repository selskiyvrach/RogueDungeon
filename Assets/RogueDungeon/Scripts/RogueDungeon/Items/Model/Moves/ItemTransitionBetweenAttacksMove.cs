using Common.Animations;
using Input;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class ItemTransitionBetweenAttacksMove : ItemMove
    {
        private readonly IAttackItemWielder _wielder;
        private readonly IWeapon _weapon;
        protected override float Duration => ((WeaponConfig)_weapon.Config).TransitionBetweenAttacksDuration;
        protected override InputKey RequiredKey => _wielder.GetInputKeyForItem(_weapon);
        protected override RequiredState State => RequiredState.Down;

        public ItemTransitionBetweenAttacksMove(IWeapon weapon, IAnimation animation, IPlayerInput playerInput, string id, IAttackItemWielder wielder) : base(id, animation, wielder, playerInput)
        {
            _weapon = weapon;
            _wielder = wielder;
        }

        public override void Enter()
        {
            base.Enter();
            _wielder.Stamina.AddDelta(- _weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _wielder.Stamina.Current >= _weapon.AttackStaminaCost;
    }
}