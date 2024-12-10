using Zenject;

namespace RogueDungeon.Weapons
{
    public interface IWeaponInstaller
    {
        void InstallBindings(WeaponConfig config, DiContainer container);
        IWeapon ResolveWeapon();
    }
}