using System;
using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public interface IAttackExecutionDuration : IParameter
    {
    }

    public class AttackExecutionDuration : Parameter, IAttackExecutionDuration
    {
        public AttackExecutionDuration(Func<float> value) : base(value)
        {
        }
    }
}