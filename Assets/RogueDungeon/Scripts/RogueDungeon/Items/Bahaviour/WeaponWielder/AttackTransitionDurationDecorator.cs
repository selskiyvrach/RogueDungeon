using RogueDungeon.Items.Bahaviour.Common;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class AttackTransitionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackAttackTransitionDuration>, IAttackAttackTransitionDuration
    {
        public AttackTransitionDurationDecorator(IAttackAttackTransitionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentItemGetter currentItemGetterAndSetter) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentItemGetterAndSetter)
        {
        }
    }
}