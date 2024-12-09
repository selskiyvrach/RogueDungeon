using UnityEngine;

namespace RogueDungeon.Weapons
{
    public interface IWeaponAnimationsConfig
    {
        AnimationClip IdleAnimation { get; }
        IAttackAnimationsProvider GetAttackAnimationConfig(int attackIndex);
    }
}