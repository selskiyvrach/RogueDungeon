using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class ItemRecoverAttackMove : ItemMove
    {
        private readonly IWeapon _weapon;
        private readonly IAttackItemWielder _wielder;
        protected override float Duration => _weapon.AttackRecoveryAnimationDuration;

        protected ItemRecoverAttackMove(IWeapon weapon, IAnimation animation, string id, IPlayerInput input, IAttackItemWielder wielder) : base(id, animation, wielder, input)
        {
            _weapon = weapon;
            _wielder = wielder;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() || !_wielder.CanAttack;
    }
}