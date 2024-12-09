using RogueDungeon.Collisions;

namespace RogueDungeon.Weapons
{
    public interface IWeaponParametersConfig
    {
        float GetDamage(int attackIndex);
        Positions GetPositionsHitMask(int attackIndex);
    }
}