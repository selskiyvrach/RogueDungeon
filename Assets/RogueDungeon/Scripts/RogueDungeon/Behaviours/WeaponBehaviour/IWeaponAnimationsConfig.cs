using RogueDungeon.Animations;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IWeaponAnimationsConfig
    {
        AnimationConfig IdleAnimation { get; }
        IAttackAnimationsConfig GetAttackAnimationConfig(int attackIndex);
    }
}