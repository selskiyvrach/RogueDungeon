using RogueDungeon.Items.Handling.Common;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    public class IdleTransitionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackIdleTransitionDuration>, IAttackIdleTransitionDuration
    {
        public IdleTransitionDurationDecorator(IAttackIdleTransitionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentHandheldItemProvider currentHandheldItem) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentHandheldItem)
        {
        }
    }
}