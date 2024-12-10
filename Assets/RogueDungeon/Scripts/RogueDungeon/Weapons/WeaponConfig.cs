using RogueDungeon.Collisions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponConfig : ScriptableObject, IAttackComboCountAndTimingsConfig, IWeaponAnimationsConfig, IWeaponParametersConfig
    {
        [field: TitleGroup("Prefab"), HideLabel, SerializeField] public WeaponInstaller Prefab { get; private set; }
        [SerializeField, HideLabel, TitleGroup("Idle animation")] private AnimationClip _idleAnimation;
        [SerializeField, TitleGroup("Attacks")] private AttackConfig[] _attacks;

        int IAttackComboCountAndTimingsConfig.Count => _attacks.Length;
        
        AnimationClip IWeaponAnimationsConfig.IdleAnimation => _idleAnimation;

        IAttackTimingsProvider IAttackComboCountAndTimingsConfig.GetTimings(int attackIndex) => 
            attackIndex == 0 || !_attacks[attackIndex].OverrideTimings ? _attacks[0] : _attacks[attackIndex];

        IAttackAnimationsProvider IWeaponAnimationsConfig.GetAttackAnimationConfig(int attackIndex) => 
            _attacks[attackIndex];

        float IWeaponParametersConfig.GetDamage(int attackIndex) => 
            attackIndex == 0 || !_attacks[attackIndex].OverrideDamage 
                ? _attacks[0].Damage 
                : _attacks[attackIndex].Damage;

        Positions IWeaponParametersConfig.GetPositionsHitMask(int attackIndex) =>
            attackIndex == 0 || !_attacks[attackIndex].OverrideHitMask 
                ? _attacks[0].HitMask 
                : _attacks[attackIndex].HitMask;
    }
}