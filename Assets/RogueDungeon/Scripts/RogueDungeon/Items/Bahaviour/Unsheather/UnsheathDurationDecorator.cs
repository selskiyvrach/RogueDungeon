using RogueDungeon.Items.Handling.Common;

namespace RogueDungeon.Items.Handling.Unsheather
{
    public class UnsheathDurationDecorator : WeaponActionDurationBasedOnWeightModifier<IUnsheathDuration>, IUnsheathDuration 
    {
        public UnsheathDurationDecorator(IUnsheathDuration baseParameter, IStrengthAttribute strengthAttribute, IAgilityAttribute agilityAttribute, ICurrentHandheldItemProvider currentHandheldItem) : base(baseParameter, strengthAttribute, agilityAttribute, currentHandheldItem)
        {
        }
    }
}