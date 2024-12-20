using System;
using Common.Parameters;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
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