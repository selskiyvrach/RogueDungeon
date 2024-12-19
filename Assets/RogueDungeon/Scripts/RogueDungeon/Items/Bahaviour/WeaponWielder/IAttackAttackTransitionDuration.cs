using System;
using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public interface IAttackAttackTransitionDuration : IParameter
    {
    }

    public class AttackAttackTransitionDuration : Parameter, IAttackAttackTransitionDuration
    {
        public AttackAttackTransitionDuration(Func<float> value) : base(value)
        {
        }
    }
}