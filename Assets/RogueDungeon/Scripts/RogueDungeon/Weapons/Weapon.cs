namespace RogueDungeon.Weapons
{
    public interface IWeapon
    {
    }

    public class Weapon : IWeapon
    {
        private readonly WeaponBehaviour _weaponBehaviour;
        private readonly WeaponAnimator _weaponAnimator;
        private readonly AttackHitHandler _attackHitHandler;

        public Weapon(WeaponBehaviour weaponBehaviour, WeaponAnimator weaponAnimator, AttackHitHandler attackHitHandler)
        {
            _weaponBehaviour = weaponBehaviour;
            _weaponAnimator = weaponAnimator;
            _attackHitHandler = attackHitHandler;
        }
    }
}