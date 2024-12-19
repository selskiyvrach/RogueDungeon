using RogueDungeon.Items.Bahaviour.Common;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class UnsheathDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IUnsheathDuration>, IUnsheathDuration 
    {
        public UnsheathDurationDecorator(IUnsheathDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentItemGetter currentItemGetterAndSetter) : base(baseParameter, strengthAttribute, agilityAttribute, currentItemGetterAndSetter)
        {
        }
    }
}