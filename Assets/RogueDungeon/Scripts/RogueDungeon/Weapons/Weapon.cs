namespace RogueDungeon.Weapons
{
    public interface IWeapon
    {
    }

    public class Weapon : IWeapon
    {
        private readonly WeaponBehaviour _weaponBehaviour;
        private readonly WeaponWorldSpaceAnimator _weaponWorldSpaceAnimator;
        private readonly AttackHitHandler _attackHitHandler;

        public Weapon(WeaponBehaviour weaponBehaviour, WeaponWorldSpaceAnimator weaponWorldSpaceAnimator, AttackHitHandler attackHitHandler)
        {
            _weaponBehaviour = weaponBehaviour;
            _weaponWorldSpaceAnimator = weaponWorldSpaceAnimator;
            _attackHitHandler = attackHitHandler;
            
            _weaponWorldSpaceAnimator.Initialize();
            _weaponBehaviour.Enable();
        }
    }
}