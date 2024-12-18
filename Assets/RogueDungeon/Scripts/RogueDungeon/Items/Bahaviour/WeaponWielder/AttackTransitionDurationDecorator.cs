using RogueDungeon.Items.Handling.Common;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    public class AttackTransitionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackAttackTransitionDuration>, IAttackAttackTransitionDuration
    {
        public AttackTransitionDurationDecorator(IAttackAttackTransitionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentHandheldItemProvider currentHandheldItem) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentHandheldItem)
        {
        }
    }
}