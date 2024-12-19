using System;
using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public interface IAttackIdleTransitionDuration : IParameter
    {
    }

    public class AttackIdleTransitionDuration : Parameter, IAttackIdleTransitionDuration
    {
        public AttackIdleTransitionDuration(Func<float> value) : base(value)
        {
        }
    }
}