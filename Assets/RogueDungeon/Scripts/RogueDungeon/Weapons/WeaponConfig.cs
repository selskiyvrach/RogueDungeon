using Common.ScreenSpaceEffects;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    // player: base action durations
        // attack prepare
        // attack execute
        // attack finish
        // weapon sheath
        // weapon unsheath
    
    // player: strength 1-10
    // player: agility 1-10
        // base is 5 each +-1 gives +-20 percent action speed
        
    // weapon: weight 1-10
        // weight itself 5 - default speed, each point of difference costs 10% speed. 1 weight -> 140%, 10 weight -> 50%
        // strength effectiveness 0% at 1, 100% at 10
        // agility effectiveness 0% at 10, 100% at 1
        
    // weight 3, agility 7, strength 4. ag +40%*.7 = +28%, str -20%*.3 = -6%. sum +22%
    // weight 3, agility 9, strength 5. ag +80%*.7 = +56%, str 0%. sum +56%
    // weight 3, agility 9, strength 3. ag +80%*.7 = +56%, str -40%*.3 = -12% sum +44%
    // weight 1, agility 10, strength 5. ag +100%* sum +100%

    public interface IWeaponActionsDurationsProvider
    {
        float AttackPrepareDuration { get; }
        float AttackExecuteDuration { get; }
        float AttackFinishDuration { get; }
        float UnsheathDuration { get; }
        float SheathDuration { get; }
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
        private readonly IWeaponWeightProvider _weightProvider;
        private readonly ICharacterAttributesProvider _attributesProvider;
        private readonly IWeaponActionsBaseValuesProvider _baseDurationValues;

        public float AttackPrepareDuration { get; private set; }
        public float AttackExecuteDuration { get; private set; }
        public float AttackFinishDuration { get; private set; }
        public float UnsheathDuration { get; private set; }
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
            UnsheathDuration = _baseDurationValues.UnsheathDuration * totalCoeff;
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
        [field: SerializeField] public float Weight { get; private set; } = 1;
        [field: SerializeField] public ScreenSpaceDirection[] ComboAttackDirections { get; private set; } = 
        {
            ScreenSpaceDirection.BottomLeft,
            ScreenSpaceDirection.BottomRight
        };
    }
}