using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.Common
{
    public abstract class WeaponActionDurationBasedOnWeightModifier<T> : ParameterDecorator<T> where T : IParameter
    {
        private readonly IStrengthAttribute _strengthAttribute;
        private readonly IAgilityAttribute _agilityAttribute;
        private readonly ICurrentItemGetter _currentItemGetter;

        protected WeaponActionDurationBasedOnWeightModifier(T baseParameter,
            IStrengthAttribute strengthAttribute,
            IAgilityAttribute agilityAttribute, ICurrentItemGetter currentItemGetter) : base(baseParameter)
        {
            _strengthAttribute = strengthAttribute;
            _agilityAttribute = agilityAttribute;
            _currentItemGetter = currentItemGetter;
        }

        protected override float GetValue(T decorated) => 
            decorated.Value * CalculateBonus();

        private float CalculateBonus()
        {
            var itemStrToAgRatio = _currentItemGetter.Item.Weight / 10;
            var strEffect = GetAttributeBonus(_strengthAttribute.Value) * itemStrToAgRatio;
            var agEffect = GetAttributeBonus(_agilityAttribute.Value) * (1 - itemStrToAgRatio);
            return 1 + strEffect + agEffect;
        }

        private float GetAttributeBonus(int value) => 
            ((float)value - 5) / 5;
    }
}