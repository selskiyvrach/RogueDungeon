using Common.Parameters;

namespace RogueDungeon.Items.Handling.Common
{
    public abstract class WeaponActionDurationBasedOnWeightModifier<T> : ParameterDecorator<T> where T : IParameter
    {
        private readonly IStrengthAttribute _strengthAttribute;
        private readonly IAgilityAttribute _agilityAttribute;
        private readonly ICurrentHandheldItemProvider _currentHandheldItem;

        protected WeaponActionDurationBasedOnWeightModifier(T baseParameter,
            IStrengthAttribute strengthAttribute,
            IAgilityAttribute agilityAttribute, ICurrentHandheldItemProvider currentHandheldItem) : base(baseParameter)
        {
            _strengthAttribute = strengthAttribute;
            _agilityAttribute = agilityAttribute;
            _currentHandheldItem = currentHandheldItem;
        }

        protected override float GetValue(T decorated) => 
            decorated.Value * CalculateBonus();

        private float CalculateBonus()
        {
            var itemStrToAgRatio = _currentHandheldItem.ItemInfo.Weight / 10;
            var strEffect = GetAttributeBonus(_strengthAttribute.Value) * itemStrToAgRatio;
            var agEffect = GetAttributeBonus(_agilityAttribute.Value) * (1 - itemStrToAgRatio);
            return 1 + strEffect + agEffect;
        }

        private float GetAttributeBonus(int value) => 
            ((float)value - 5) / 5;
    }
}