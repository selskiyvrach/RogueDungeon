using Game.Features.Items.Domain.Configs;
using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class ItemPrepareAttackMove : ItemMove
    {
        private readonly IAttackItemWielder _wielder;
        private readonly IWeapon _weapon;
        protected override float Duration => ((WeaponConfig)_weapon.Config).PrepareAttackDuration;
        protected override InputKey RequiredKey => _wielder.GetInputKeyForItem(_weapon);
        protected override RequiredState State => RequiredState.Down;

        protected ItemPrepareAttackMove(IAnimation animation, IPlayerInput playerInput, IAttackItemWielder wielder, IWeapon weapon, string id) : base(id, animation, wielder, playerInput)
        {
            _wielder = wielder;
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();
            _wielder.Stamina.AddDelta(-_weapon.AttackStaminaCost);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _wielder.Stamina.Current >= _weapon.AttackStaminaCost;
    }
}