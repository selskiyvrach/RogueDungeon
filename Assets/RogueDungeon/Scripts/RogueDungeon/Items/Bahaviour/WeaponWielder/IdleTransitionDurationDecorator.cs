using RogueDungeon.Items.Bahaviour.Common;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class IdleTransitionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackIdleTransitionDuration>, IAttackIdleTransitionDuration
    {
        public IdleTransitionDurationDecorator(IAttackIdleTransitionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentItemGetter currentItemGetterAndSetter) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentItemGetterAndSetter)
        {
        }
    }
}