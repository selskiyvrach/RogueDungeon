using RogueDungeon.Items.Bahaviour.Common;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class ExecutionDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IAttackExecutionDuration>, IAttackExecutionDuration
    {
        public ExecutionDurationDecorator(IAttackExecutionDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentItemGetter currentItemGetterAndSetter) : 
            base(baseParameter, strengthAttribute, agilityAttribute, currentItemGetterAndSetter)
        {
        }
    }
}