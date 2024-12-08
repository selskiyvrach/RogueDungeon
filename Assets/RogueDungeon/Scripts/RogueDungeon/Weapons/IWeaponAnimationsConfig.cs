using RogueDungeon.Animations;

namespace RogueDungeon.Weapons
{
    public interface IWeaponAnimationsConfig
    {
        AnimationConfig IdleAnimation { get; }
        IAttackAnimationsProvider GetAttackAnimationConfig(int attackIndex);
    }
}