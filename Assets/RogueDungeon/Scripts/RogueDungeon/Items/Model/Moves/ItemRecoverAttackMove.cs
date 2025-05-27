using Common.Animations;
using Input;
using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model.Moves
{
    public class ItemRecoverAttackMove : ItemMove
    {
        private readonly IWeapon _weapon;
        private readonly IAttackItemWielder _wielder;
        protected override float Duration => ((WeaponConfig)_weapon.Config).AttackRecoveryDuration;

        protected ItemRecoverAttackMove(IWeapon weapon, IAnimation animation, string id, IPlayerInput input, IAttackItemWielder wielder) : base(id, animation, wielder, input)
        {
            _weapon = weapon;
            _wielder = wielder;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() || !_wielder.CanAttack;
    }
}