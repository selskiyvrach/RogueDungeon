using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponConfig : ScriptableObject, IAttackComboConfig, IWeaponAnimationsConfig
    {
        [SerializeField] private AnimationConfig _idleAnimation;
        [SerializeField] private AttackConfig[] _attacks;

        int IAttackComboConfig.Count => _attacks.Length;
        
        AnimationConfig IWeaponAnimationsConfig.IdleAnimation => _idleAnimation;

        IAttackTimingsProvider IAttackComboConfig.GetTimings(int attackIndex) => 
            _attacks[attackIndex];

        IAttackAnimationsProvider IWeaponAnimationsConfig.GetAttackAnimationConfig(int attackIndex) => 
            _attacks[attackIndex];
    }
}