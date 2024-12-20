using Common.Parameters;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface IWeaponActionDurationPercentBonus : IParameter
    {
        
    }

    public interface IStrengthPercentBonus : IParameter
    {
    }

    public interface IAgilityPercentBonus : IParameter
    {
    }
    

    public class WeaponActionDurationPercentBonus : IWeaponActionDurationPercentBonus
    {
        private readonly IStrengthPercentBonus _strengthPercentBonus;
        private readonly IAgilityPercentBonus _agilityPercentBonus;
        private readonly ICurrentItemGetter _currentItemGetter;

        public float Value => Calculate();

        public WeaponActionDurationPercentBonus(ICurrentItemGetter currentItemGetter, IAgilityPercentBonus agilityPercentBonus, IStrengthPercentBonus strengthPercentBonus)
        {
            _currentItemGetter = currentItemGetter;
            _agilityPercentBonus = agilityPercentBonus;
            _strengthPercentBonus = strengthPercentBonus;
        }

        private float Calculate()
        {
            var itemStrToAgRatio = _currentItemGetter.Item.Weight / 10;
            var strEffect = _strengthPercentBonus.Value * itemStrToAgRatio;
            var agEffect = _agilityPercentBonus.Value * (1 - itemStrToAgRatio);
            return 1 + strEffect + agEffect;
        }
    }
    
    public abstract class WeaponActionDurationBasedOnWeightModifier<T> : ParameterDecorator<T> where T : IParameter
    {
        private readonly IWeaponActionDurationPercentBonus _durationPercentBonus; 

        protected WeaponActionDurationBasedOnWeightModifier(T baseParameter, IWeaponActionDurationPercentBonus durationPercentBonus) : base(baseParameter) =>
            _durationPercentBonus = durationPercentBonus;

        protected override float GetValue(T decorated) => 
            decorated.Value * CalculateBonus();

        private float CalculateBonus() => 
            1 + _durationPercentBonus.Value;
    }
}