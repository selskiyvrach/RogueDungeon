using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
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