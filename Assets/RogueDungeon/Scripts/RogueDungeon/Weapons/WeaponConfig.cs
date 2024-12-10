using System;
using Common.ScreenSpaceEffects;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public interface IWeaponActionsDurationsProvider
    {
        float AttackPrepareDuration { get; }
        float AttackExecuteDuration { get; }
        float AttackFinishDuration { get; }
        float SheathDuration { get; }
        // block duration
    }

    [Serializable]
    public class BaseWeaponActionsDurations : IWeaponActionsDurationsProvider
    {
        [field: SerializeField] public float AttackPrepareDuration { get; private set;} = .5f;
        [field: SerializeField] public float AttackExecuteDuration { get; private set;} = .5f;
        [field: SerializeField] public float AttackFinishDuration { get; private set;} = .5f;
        [field: SerializeField] public float SheathDuration { get; private set;} = .5f;
    }

    public interface ICharacterAttributesProvider
    {
        int Strength { get; }
        int Agility { get; }
    }

    public interface IWeaponActionsBaseValuesProvider : IWeaponActionsDurationsProvider
    {
                
    }

    public class WeaponActionsDurationsCalculator : IWeaponActionsDurationsProvider
    {
        // player: strength 1-10
        // player: agility 1-10
        // base is 5 each +-1 gives +-20 percent action speed
        
        // weapon: weight 1-10
        // weight itself 5 - default speed, each point of difference costs 10% speed. 1 weight -> 140%, 10 weight -> 50%
        // strength effectiveness 0% at 1, 100% at 10
        // agility effectiveness 0% at 10, 100% at 1
        
        private readonly IWeaponWeightProvider _weightProvider;
        private readonly ICharacterAttributesProvider _attributesProvider;
        private readonly IWeaponActionsBaseValuesProvider _baseDurationValues;

        public float AttackPrepareDuration { get; private set; }
        public float AttackExecuteDuration { get; private set; }
        public float AttackFinishDuration { get; private set; }
        public float SheathDuration { get; private set; }

        public WeaponActionsDurationsCalculator(IWeaponWeightProvider weightProvider, ICharacterAttributesProvider attributesProvider, IWeaponActionsBaseValuesProvider baseDurationValues)
        {
            _weightProvider = weightProvider;
            _attributesProvider = attributesProvider;
            _baseDurationValues = baseDurationValues;
            Refresh();
        }

        public void Refresh()
        {
            var weaponWeightCoeff = (_weightProvider.Weight - 5) / 10;
            var weaponHeavyToLightRatio = _weightProvider.Weight / 10;
            var strCoeff = - ((float)_attributesProvider.Strength - 5) / 5 * weaponHeavyToLightRatio;
            var agCoeff = - ((float)_attributesProvider.Agility - 5) / 5 * (1 - weaponHeavyToLightRatio);
            var totalCoeff = 1 + weaponWeightCoeff + strCoeff + agCoeff;
            
            AttackPrepareDuration = _baseDurationValues.AttackPrepareDuration * totalCoeff;
            AttackExecuteDuration = _baseDurationValues.AttackExecuteDuration * totalCoeff;
            AttackFinishDuration = _baseDurationValues.AttackFinishDuration * totalCoeff;
            SheathDuration = _baseDurationValues.SheathDuration * totalCoeff;
        }
    }

    public interface IWeaponAttackDirectionsProvider
    {
        ScreenSpaceDirection[] ComboAttackDirections { get; }
    }

    public interface IWeaponWeightProvider
    {
        float Weight { get; }
    }

    public class WeaponConfig : ScriptableObject, IWeaponAttackDirectionsProvider, IWeaponWeightProvider
    {
        [field: SerializeField] public WeaponInstaller Prefab { get; private set; }
        [field: SerializeField] public float Weight { get; private set; } = 5;
        [field: SerializeField] public ScreenSpaceDirection[] ComboAttackDirections { get; private set; } = 
        {
            ScreenSpaceDirection.BottomLeft,
            ScreenSpaceDirection.BottomRight
        };
    }
}