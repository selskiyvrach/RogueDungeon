using RogueDungeon.Items.Handling.Common;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    public class ExecutionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackExecutionDuration>, IAttackExecutionDuration
    {
        public ExecutionDurationDecorator(IAttackExecutionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentHandheldItemProvider currentHandheldItem) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentHandheldItem)
        {
        }
    }
}